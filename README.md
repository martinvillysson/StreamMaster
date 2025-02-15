<p align="center" width="100%">
    <img src="https://raw.githubusercontent.com/carlreid/StreamMaster/refs/heads/main/src/StreamMaster.WebUI/public/images/streammaster_logo.png" alt="StreamMaster Logo"/>
    <H1 align="center" width="100%">StreamMaster</H1>
</p>

> A powerful M3U proxy and stream management platform for Plex DVR, Emby, and Jellyfin Live TV

## Quick Start 🚀

Docker compose example:
```yaml
services:
  streammaster:
    image: ghcr.io/carlreid/streammaster:latest
    container_name: streammaster
    ports:
      - 7095:7095
    restart: unless-stopped
    volumes:
      - ~/streammaster:/config
      - ~/streammaster/tv-logos:/config/tv-logos
```
View available releases at our [container registry](https://github.com/carlreid/StreamMaster/pkgs/container/streammaster).

## Key Features ⭐

- **M3U and EPG Management**: Import multiple files via URL or file with automatic refresh
- **Logo Customization**: Cached logos with local directory support for TV logos
- **Performance Analytics**: Track streaming performance with comprehensive statistics
- **Virtual HDHomeRun**: Create multiple virtual devices with custom configurations
- **Platform Integration**: Seamless compatibility with Plex, Emby, and Jellyfin
- **Modern Stack**: Built with React and .NET for optimal performance
- **Advanced Proxying**: RAM-based operations with failover stream support
- **Efficient Streaming**: Single backend stream serves multiple device in your home

## Documentation 📚

[Find our documentation here](https://carlreid.github.io/StreamMaster/)

## Contributing 🤝

- **Issues**: Bug reports and feature requests ([create an issue](https://github.com/carlreid/StreamMaster/issues))
- **Discussions**: Community feedback and ideas ([open a discussion](https://github.com/carlreid/StreamMaster/discussions))
- **Development**: Pull requests welcome


---

*This repository is a fork of the original SenexCrenshaw/StreamMaster project, which was discontinued(?) in early 2025.*

*For historical reference, see the original [README](README_old.md).*