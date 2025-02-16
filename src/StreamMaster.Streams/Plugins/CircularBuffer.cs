using System.Buffers;

namespace StreamMaster.Streams.Plugins;

/// <summary>
/// A thread-safe circular buffer for storing and managing byte data.
/// Implements IDisposable for proper resource cleanup.
/// </summary>
public class CircularBuffer : IDisposable
{
    private readonly byte[] _buffer;
    private int _start;
    private int _end;
    private readonly object _lock = new();
    private bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="CircularBuffer"/> class with the specified size.
    /// </summary>
    /// <param name="size">The capacity of the circular buffer.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the size is less than or equal to zero.</exception>
    public CircularBuffer(int size)
    {
        if (size <= 0)
            throw new ArgumentOutOfRangeException(nameof(size), "Size must be greater than zero.");

        _buffer = new byte[size];
    }

    /// <summary>
    /// Writes data into the circular buffer.
    /// If the input data is larger than the buffer capacity, only the most recent data that fits will be written.
    /// Overwrites the oldest data if the buffer is full.
    /// </summary>
    /// <param name="data">The data to write.</param>
    /// <exception cref="ObjectDisposedException">Thrown if the buffer has been disposed.</exception>
    public void Write(ReadOnlySpan<byte> data)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        lock (_lock)
        {
            if (data.Length > _buffer.Length)
            {
                data = data[^_buffer.Length..];
            }

            int dataLength = data.Length;

            if (_end + dataLength <= _buffer.Length)
            {
                data.CopyTo(new Span<byte>(_buffer, _end, dataLength));
            }
            else
            {
                int firstPart = _buffer.Length - _end;
                data[..firstPart].CopyTo(new Span<byte>(_buffer, _end, firstPart));
                data[firstPart..].CopyTo(new Span<byte>(_buffer, 0, dataLength - firstPart));
            }

            _end = (_end + dataLength) % _buffer.Length;
            Size = Math.Min(Size + dataLength, _buffer.Length); // Simplified
            _start = Size == _buffer.Length ? _end : _start; // Only update start if full
        }
    }

    /// <summary>
    /// Reads all available data from the circular buffer without removing it.
    /// </summary>
    /// <returns>A <see cref="ReadOnlyMemory{Byte}"/> containing the data.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the buffer has been disposed.</exception>
    public ReadOnlyMemory<byte> ReadAll()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        lock (_lock)
        {
            if (Size == 0)
                return ReadOnlyMemory<byte>.Empty;

            byte[] result = ArrayPool<byte>.Shared.Rent(Size);
            try
            {
                if (_end > _start)
                {
                    new ReadOnlySpan<byte>(_buffer, _start, Size).CopyTo(new Span<byte>(result));
                }
                else
                {
                    int firstPart = _buffer.Length - _start;
                    new ReadOnlySpan<byte>(_buffer, _start, firstPart).CopyTo(new Span<byte>(result));
                    new ReadOnlySpan<byte>(_buffer).Slice(0, _end).CopyTo(new Span<byte>(result, firstPart, _end));
                }
                return new ReadOnlyMemory<byte>(result, 0, Size);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(result);
            }
        }
    }

    /// <summary>
    /// Marks the specified number of bytes as read, making space for new writes.
    /// </summary>
    /// <param name="count">The number of bytes to mark as read.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if the count is less than or equal to zero, or greater than the current size.
    /// </exception>
    /// <exception cref="ObjectDisposedException">Thrown if the buffer has been disposed.</exception>
    public void MarkRead(int count)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        lock (_lock)
        {
            if (count <= 0 || count > Size)
                throw new ArgumentOutOfRangeException(nameof(count), "Invalid count for marking data as read.");

            _start = (_start + count) % _buffer.Length;
            Size -= count;

            //Ensure that we don't set the `_start` beyond the `_buffer.Length`
            _start %= _buffer.Length;
        }
    }

    public bool TryMarkRead(int count)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        lock (_lock)
        {
            if (count <= 0 || count > Size)
                return false; // Indicate failure

            _start = (_start + count) % _buffer.Length;
            Size -= count;
            return true; // Indicate success
        }
    }

    /// <summary>
    /// Clears all data from the buffer, resetting it to an empty state.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown if the buffer has been disposed.</exception>
    public void Clear()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        lock (_lock)
        {
            _start = 0;
            _end = 0;
            Size = 0;
        }
    }

    /// <summary>
    /// Gets the current size of the circular buffer (number of bytes stored).
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Gets the total capacity of the circular buffer.
    /// </summary>
    public int Capacity => _buffer.Length;

    /// <summary>
    /// Gets a value indicating whether the buffer is full.
    /// </summary>
    public bool IsFull => Size == Capacity;

    /// <summary>
    /// Disposes the circular buffer, clearing its contents and preventing further use.
    /// </summary>
    public void Dispose()
    {
        if (!_isDisposed)
        {
            _isDisposed = true;
            // Avoid clearing the buffer to prevent potential race conditions and data leaks
            // Array.Clear(_buffer, 0, _buffer.Length);
        }
    }
}