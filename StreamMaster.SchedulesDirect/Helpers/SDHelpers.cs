using SixLabors.ImageSharp;

namespace StreamMaster.SchedulesDirect.Helpers;

public static partial class SDHelpers
{
    // Priority categories in descending order
    private static readonly List<string> categories =
        [
         "box art",
         "key art",
            "vod art",
            "poster art",
            "banner",
            "banner-l1",
            "banner-l2",
            "banner-lo",
            "logo",
            "banner-l3",
            "iconic",
            "staple"
        ];

    private static readonly Dictionary<string, int> categoryPriority = categories
        .Select((category, index) => new { category, index })
        .ToDictionary(item => item.category, item => item.index, StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Filters and selects tiered images from a list of program artwork based on specified criteria.
    /// </summary>
    /// <param name="sdImages">The list of program artwork to filter.</param>
    /// <param name="artWorkSize">The desired size of the artwork. Options include Sm, Md, Lg.</param>
    /// <param name="tiers">The preferred tiers of artwork.</param>
    /// <param name="aspect">The desired aspect ratio.</param>
    /// <returns>A filtered and prioritized list of program artwork.</returns>
    public static List<ProgramArtwork> GetTieredImages(List<ProgramArtwork> sdImages, string artWorkSize, List<string>? tiers = null, string? aspect = null)
    {
        if (sdImages == null)
        {
            throw new ArgumentNullException(nameof(sdImages), "Input list cannot be null.");
        }

        // Order of priority for artwork sizes
        List<string> sizePriority = ["Lg", "Md", "Sm"];
        int requestedSizeIndex = sizePriority.IndexOf(artWorkSize);
        if (requestedSizeIndex == -1)
        {
            throw new ArgumentException("Invalid artWorkSize. Expected one of: Sm, Md, Lg.", nameof(artWorkSize));
        }

        // Adjust size priority based on requested artwork size
        List<string> applicableSizes = [.. sizePriority.Skip(requestedSizeIndex)];

        // Filter images based on provided criteria
        List<ProgramArtwork> filteredImages = [.. sdImages
            .Where(image =>
                !string.IsNullOrEmpty(image.Category) &&
                !string.IsNullOrEmpty(image.Aspect) &&
                !string.IsNullOrEmpty(image.Uri) &&
                (string.IsNullOrEmpty(image.Tier) || tiers?.Contains(image.Tier.ToLower()) != false) &&
                (string.IsNullOrEmpty(aspect) || image.Aspect.Equals(aspect, StringComparison.OrdinalIgnoreCase)))];

        // Process each artwork size in priority order to gather images
        List<ProgramArtwork> prioritizedImages = [];
        foreach (string size in applicableSizes)
        {
            List<ProgramArtwork> imagesOfSize = [.. filteredImages.Where(image => string.Equals(image.Size, size, StringComparison.OrdinalIgnoreCase))];

            if (imagesOfSize.Count != 0)
            {
                // Group images by aspect ratio
                Dictionary<string, List<ProgramArtwork>> aspects = imagesOfSize
                    .GroupBy(image => image.Aspect)
                    .ToDictionary(group => group.Key, group => group.ToList());

                // Process each aspect group to select the highest priority image based on category
                foreach (KeyValuePair<string, List<ProgramArtwork>> aspectGroup in aspects)
                {
                    List<ProgramArtwork> aspectImages = aspectGroup.Value;

                    IEnumerable<ProgramArtwork> prioritizedCategoryImages = aspectImages
                        .OrderBy(image => categoryPriority.TryGetValue(image.Category, out int value) ? value : int.MaxValue)
                        .Take(3);

                    prioritizedImages.AddRange(prioritizedCategoryImages);
                }
            }

            // If we have found some images, break the loop since we have a satisfactory size
            if (prioritizedImages.Count != 0)
            {
                break;
            }
        }

        return prioritizedImages;
    }

    public static bool TableContains(string[] table, string text, bool exactMatch = false)
    {
        if (table == null)
        {
            return false;
        }

        foreach (string str in table)
        {
            if (string.IsNullOrEmpty(str))
            {
                continue;
            }

            if (!exactMatch && str.ContainsIgnoreCase(text))
            {
                return true;
            }

            if (str.EqualsIgnoreCase(text))
            {
                return true;
            }
        }

        return false;
    }
}