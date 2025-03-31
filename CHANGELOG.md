# [0.13.0](https://github.com/carlreid/StreamMaster/compare/v0.12.0...v0.13.0) (2025-03-30)


### Bug Fixes

* Remove debug log message ([4435801](https://github.com/carlreid/StreamMaster/commit/443580116bf6ca5c209d77491a4ce33c1e1597a4))
* Use `Release` version ([2fe37db](https://github.com/carlreid/StreamMaster/commit/2fe37db654f3c724922fef18a16221b9eb658520))


### Features

* Give credit to contributors ([96b90c6](https://github.com/carlreid/StreamMaster/commit/96b90c6897e367a90d379d582f98deec9f160118))
* Video player fallback to hls/mpegts ([ba8c87e](https://github.com/carlreid/StreamMaster/commit/ba8c87e1b8ebc35d3b601b71bede7991e90bc3a7))

# [0.12.0](https://github.com/carlreid/StreamMaster/compare/v0.11.3...v0.12.0) (2025-03-24)


### Bug Fixes

* Ensure that multiselect value is `''` if null ([0307ec2](https://github.com/carlreid/StreamMaster/commit/0307ec29cbbc271fdda72e6305660f4d9ea1d3fb))
* Ensure to release lock in `final` ([6a76d65](https://github.com/carlreid/StreamMaster/commit/6a76d65cc006278d6503d7ced7e34a5897d79044))
* Filter away potential undefined stations ([a58f8d7](https://github.com/carlreid/StreamMaster/commit/a58f8d705c11264fcfdf17eaece99c0bb905780d))
* Make use of `IHttpClientFactory` to get clients ([a7ca54a](https://github.com/carlreid/StreamMaster/commit/a7ca54a0685c374ea96a7f98b6f333e2371e957c))
* Remove from hook dependency ([84d9be3](https://github.com/carlreid/StreamMaster/commit/84d9be34380f7be7b803124c6b9f8b6db35b766f))


### Features

* Set Streammaster specific UA for SD ([49b7448](https://github.com/carlreid/StreamMaster/commit/49b7448f2f931e620042a95089d67b430ef02f36))

## [0.11.3](https://github.com/carlreid/StreamMaster/compare/v0.11.2...v0.11.3) (2025-03-15)


### Bug Fixes

* Fallback to EPG logos if channel misses one ([8c4d748](https://github.com/carlreid/StreamMaster/commit/8c4d7486145bd702de36720bbeaba41932c74ef0))

## [0.11.2](https://github.com/carlreid/StreamMaster/compare/v0.11.1...v0.11.2) (2025-03-13)


### Bug Fixes

* Adjustment to command profiles and groups ([5e8828a](https://github.com/carlreid/StreamMaster/commit/5e8828ac70290b307f75cd009f70de0f5a8d3fd1))
* Adjustment to Enum alignment ([0cfbf1f](https://github.com/carlreid/StreamMaster/commit/0cfbf1fc24a319792835ff9d207aef1b4f56c107))
* Custom playlist scan update channels and logos ([5407116](https://github.com/carlreid/StreamMaster/commit/540711600610029ce7d313f542fb73bca15247c4))
* Don't `break` out of the EPG iteration ([539ba01](https://github.com/carlreid/StreamMaster/commit/539ba0100efbe550bbed0d7921ab7b650bfe0532))

## [0.11.1](https://github.com/carlreid/StreamMaster/compare/v0.11.0...v0.11.1) (2025-03-05)


### Bug Fixes

* Dropdown text alignment ([7486efa](https://github.com/carlreid/StreamMaster/commit/7486efa12af8546564616f3e51f68e194b1a510e))
* Persist rank to CurrentRank ([74818c8](https://github.com/carlreid/StreamMaster/commit/74818c863edd968ed3e2b03dd688c47f0b50e88e))

# [0.11.0](https://github.com/carlreid/StreamMaster/compare/v0.10.2...v0.11.0) (2025-02-26)


### Bug Fixes

* Add critical operation checks and early exits ([e4e6ba5](https://github.com/carlreid/StreamMaster/commit/e4e6ba58f0dbc460aff6f58025d721bc119d375e))
* Add nullglob for safer array handling ([bd218c9](https://github.com/carlreid/StreamMaster/commit/bd218c937deab9cdad76b3a0607f4c203c1a8c7b))
* Add retry policy and fail if unable to switch channel ([8ae5c55](https://github.com/carlreid/StreamMaster/commit/8ae5c554effe8dfde7857f7ed73e3cc625bed415))
* Add trap for temp script cleanup ([6c7327b](https://github.com/carlreid/StreamMaster/commit/6c7327bd4e26e1b87adab8f59a8152878780639d))
* Cache EPG logos ([0b31fc9](https://github.com/carlreid/StreamMaster/commit/0b31fc961c5868f1bc145089b4d198fd4daa322c))
* Eho failures to chmod ([1ca591d](https://github.com/carlreid/StreamMaster/commit/1ca591d5691b51b912c157dbb7d71ac1cc31b380))
* Handle case where PUID/PGID is `0` ([b759b3a](https://github.com/carlreid/StreamMaster/commit/b759b3a692226bcc396c06b5c75ed2ee22a78165))
* Handle failure to creat config.json ([753523f](https://github.com/carlreid/StreamMaster/commit/753523f1756bad062bc11ed01e5964762012a5a7))
* Improve directory rename safety with atomic operation ([8e37e4b](https://github.com/carlreid/StreamMaster/commit/8e37e4b50d0e16d8b47bf53fa63251d7fbaec4ee))
* Improve video playback ([34c96cc](https://github.com/carlreid/StreamMaster/commit/34c96cc4bdb098dbe6cd11c017d4dceb08296826))
* Remove handling of non-existing migration IDs ([4b3398e](https://github.com/carlreid/StreamMaster/commit/4b3398e76e6dd3b342131ee4791b19203e023966))
* Shellcheck related suggestions and fixes ([15a9af0](https://github.com/carlreid/StreamMaster/commit/15a9af0d074470cb4f7126831ec71189349a234d))
* Should attempt redirect first ([ae7925b](https://github.com/carlreid/StreamMaster/commit/ae7925b6f0d6fdb2b805f6bddf1073f2ecfa4cbb))
* Source env things first ([0b46c58](https://github.com/carlreid/StreamMaster/commit/0b46c58ae607227620847ce08f7fa8c8e1207a68))
* Trap postgres in case of failure ([2313119](https://github.com/carlreid/StreamMaster/commit/2313119ec48f4a29481b7f44d903ddd995b741e0))
* Wrap in quotes to avoid bad names ([0365eb1](https://github.com/carlreid/StreamMaster/commit/0365eb134f249323ef041e3ddfc2b1b6685855bb))


### Features

* Add auto set EPG button more front facing ([464aa01](https://github.com/carlreid/StreamMaster/commit/464aa01d5365f01a777b323cb07fa3e038ed0b33))
* Add Play Stream dialog ([9504cdd](https://github.com/carlreid/StreamMaster/commit/9504cdd8c2425ed524dda204799a97d72ffeefa0))
* Add validation of PUID and PGID values ([7ed4915](https://github.com/carlreid/StreamMaster/commit/7ed4915ddbd0c1c0b9068f4fc3de8785ab04169a))
* Handle directory creation with fail output ([341fd2b](https://github.com/carlreid/StreamMaster/commit/341fd2b5e21c562dd86bda1374c8d7e08d5e5eac))
* Handle faulire cases ([8126757](https://github.com/carlreid/StreamMaster/commit/8126757c93da5a23a44c4ab657d992cab6da4830))
* Make PostgreSQL wait parameters configurable ([c945727](https://github.com/carlreid/StreamMaster/commit/c945727dfce1672b06f57508c91cb8790313fc65))
* Output failures when `chown` ([828270c](https://github.com/carlreid/StreamMaster/commit/828270c54f6e3fcc997f148a1dfe720059f8fd7a))

# [0.11.0](https://github.com/carlreid/StreamMaster/compare/v0.10.2...v0.11.0) (2025-02-26)


### Bug Fixes

* Add critical operation checks and early exits ([e4e6ba5](https://github.com/carlreid/StreamMaster/commit/e4e6ba58f0dbc460aff6f58025d721bc119d375e))
* Add nullglob for safer array handling ([bd218c9](https://github.com/carlreid/StreamMaster/commit/bd218c937deab9cdad76b3a0607f4c203c1a8c7b))
* Add retry policy and fail if unable to switch channel ([8ae5c55](https://github.com/carlreid/StreamMaster/commit/8ae5c554effe8dfde7857f7ed73e3cc625bed415))
* Add trap for temp script cleanup ([6c7327b](https://github.com/carlreid/StreamMaster/commit/6c7327bd4e26e1b87adab8f59a8152878780639d))
* Cache EPG logos ([0b31fc9](https://github.com/carlreid/StreamMaster/commit/0b31fc961c5868f1bc145089b4d198fd4daa322c))
* Eho failures to chmod ([1ca591d](https://github.com/carlreid/StreamMaster/commit/1ca591d5691b51b912c157dbb7d71ac1cc31b380))
* Handle case where PUID/PGID is `0` ([b759b3a](https://github.com/carlreid/StreamMaster/commit/b759b3a692226bcc396c06b5c75ed2ee22a78165))
* Handle failure to creat config.json ([753523f](https://github.com/carlreid/StreamMaster/commit/753523f1756bad062bc11ed01e5964762012a5a7))
* Improve directory rename safety with atomic operation ([8e37e4b](https://github.com/carlreid/StreamMaster/commit/8e37e4b50d0e16d8b47bf53fa63251d7fbaec4ee))
* Improve video playback ([34c96cc](https://github.com/carlreid/StreamMaster/commit/34c96cc4bdb098dbe6cd11c017d4dceb08296826))
* Remove handling of non-existing migration IDs ([4b3398e](https://github.com/carlreid/StreamMaster/commit/4b3398e76e6dd3b342131ee4791b19203e023966))
* Shellcheck related suggestions and fixes ([15a9af0](https://github.com/carlreid/StreamMaster/commit/15a9af0d074470cb4f7126831ec71189349a234d))
* Should attempt redirect first ([ae7925b](https://github.com/carlreid/StreamMaster/commit/ae7925b6f0d6fdb2b805f6bddf1073f2ecfa4cbb))
* Source env things first ([0b46c58](https://github.com/carlreid/StreamMaster/commit/0b46c58ae607227620847ce08f7fa8c8e1207a68))
* Trap postgres in case of failure ([2313119](https://github.com/carlreid/StreamMaster/commit/2313119ec48f4a29481b7f44d903ddd995b741e0))
* Wrap in quotes to avoid bad names ([0365eb1](https://github.com/carlreid/StreamMaster/commit/0365eb134f249323ef041e3ddfc2b1b6685855bb))


### Features

* Add auto set EPG button more front facing ([464aa01](https://github.com/carlreid/StreamMaster/commit/464aa01d5365f01a777b323cb07fa3e038ed0b33))
* Add Play Stream dialog ([9504cdd](https://github.com/carlreid/StreamMaster/commit/9504cdd8c2425ed524dda204799a97d72ffeefa0))
* Add validation of PUID and PGID values ([7ed4915](https://github.com/carlreid/StreamMaster/commit/7ed4915ddbd0c1c0b9068f4fc3de8785ab04169a))
* Handle directory creation with fail output ([341fd2b](https://github.com/carlreid/StreamMaster/commit/341fd2b5e21c562dd86bda1374c8d7e08d5e5eac))
* Handle faulire cases ([8126757](https://github.com/carlreid/StreamMaster/commit/8126757c93da5a23a44c4ab657d992cab6da4830))
* Make PostgreSQL wait parameters configurable ([c945727](https://github.com/carlreid/StreamMaster/commit/c945727dfce1672b06f57508c91cb8790313fc65))
* Output failures when `chown` ([828270c](https://github.com/carlreid/StreamMaster/commit/828270c54f6e3fcc997f148a1dfe720059f8fd7a))

# [0.11.1](https://github.com/carlreid/StreamMaster/compare/v0.10.2...v0.11.1) (2025-02-26)


### Bug Fixes

* Add critical operation checks and early exits ([e4e6ba5](https://github.com/carlreid/StreamMaster/commit/e4e6ba58f0dbc460aff6f58025d721bc119d375e))
* Add nullglob for safer array handling ([bd218c9](https://github.com/carlreid/StreamMaster/commit/bd218c937deab9cdad76b3a0607f4c203c1a8c7b))
* Add trap for temp script cleanup ([6c7327b](https://github.com/carlreid/StreamMaster/commit/6c7327bd4e26e1b87adab8f59a8152878780639d))
* Cache EPG logos ([0b31fc9](https://github.com/carlreid/StreamMaster/commit/0b31fc961c5868f1bc145089b4d198fd4daa322c))
* Eho failures to chmod ([1ca591d](https://github.com/carlreid/StreamMaster/commit/1ca591d5691b51b912c157dbb7d71ac1cc31b380))
* Handle case where PUID/PGID is `0` ([b759b3a](https://github.com/carlreid/StreamMaster/commit/b759b3a692226bcc396c06b5c75ed2ee22a78165))
* Handle failure to creat config.json ([753523f](https://github.com/carlreid/StreamMaster/commit/753523f1756bad062bc11ed01e5964762012a5a7))
* Improve directory rename safety with atomic operation ([8e37e4b](https://github.com/carlreid/StreamMaster/commit/8e37e4b50d0e16d8b47bf53fa63251d7fbaec4ee))
* Improve video playback ([34c96cc](https://github.com/carlreid/StreamMaster/commit/34c96cc4bdb098dbe6cd11c017d4dceb08296826))
* Remove handling of non-existing migration IDs ([4b3398e](https://github.com/carlreid/StreamMaster/commit/4b3398e76e6dd3b342131ee4791b19203e023966))
* Shellcheck related suggestions and fixes ([15a9af0](https://github.com/carlreid/StreamMaster/commit/15a9af0d074470cb4f7126831ec71189349a234d))
* Should attempt redirect first ([ae7925b](https://github.com/carlreid/StreamMaster/commit/ae7925b6f0d6fdb2b805f6bddf1073f2ecfa4cbb))
* Source env things first ([0b46c58](https://github.com/carlreid/StreamMaster/commit/0b46c58ae607227620847ce08f7fa8c8e1207a68))
* Trap postgres in case of failure ([2313119](https://github.com/carlreid/StreamMaster/commit/2313119ec48f4a29481b7f44d903ddd995b741e0))
* Wrap in quotes to avoid bad names ([0365eb1](https://github.com/carlreid/StreamMaster/commit/0365eb134f249323ef041e3ddfc2b1b6685855bb))


### Features

* Add auto set EPG button more front facing ([464aa01](https://github.com/carlreid/StreamMaster/commit/464aa01d5365f01a777b323cb07fa3e038ed0b33))
* Add Play Stream dialog ([9504cdd](https://github.com/carlreid/StreamMaster/commit/9504cdd8c2425ed524dda204799a97d72ffeefa0))
* Add validation of PUID and PGID values ([7ed4915](https://github.com/carlreid/StreamMaster/commit/7ed4915ddbd0c1c0b9068f4fc3de8785ab04169a))
* Handle directory creation with fail output ([341fd2b](https://github.com/carlreid/StreamMaster/commit/341fd2b5e21c562dd86bda1374c8d7e08d5e5eac))
* Handle faulire cases ([8126757](https://github.com/carlreid/StreamMaster/commit/8126757c93da5a23a44c4ab657d992cab6da4830))
* Make PostgreSQL wait parameters configurable ([c945727](https://github.com/carlreid/StreamMaster/commit/c945727dfce1672b06f57508c91cb8790313fc65))
* Output failures when `chown` ([828270c](https://github.com/carlreid/StreamMaster/commit/828270c54f6e3fcc997f148a1dfe720059f8fd7a))

# [0.11.0](https://github.com/carlreid/StreamMaster/compare/v0.10.2...v0.11.0) (2025-02-26)


### Bug Fixes

* Add critical operation checks and early exits ([e4e6ba5](https://github.com/carlreid/StreamMaster/commit/e4e6ba58f0dbc460aff6f58025d721bc119d375e))
* Add nullglob for safer array handling ([bd218c9](https://github.com/carlreid/StreamMaster/commit/bd218c937deab9cdad76b3a0607f4c203c1a8c7b))
* Add trap for temp script cleanup ([6c7327b](https://github.com/carlreid/StreamMaster/commit/6c7327bd4e26e1b87adab8f59a8152878780639d))
* Cache EPG logos ([0b31fc9](https://github.com/carlreid/StreamMaster/commit/0b31fc961c5868f1bc145089b4d198fd4daa322c))
* Eho failures to chmod ([1ca591d](https://github.com/carlreid/StreamMaster/commit/1ca591d5691b51b912c157dbb7d71ac1cc31b380))
* Handle case where PUID/PGID is `0` ([b759b3a](https://github.com/carlreid/StreamMaster/commit/b759b3a692226bcc396c06b5c75ed2ee22a78165))
* Handle failure to creat config.json ([753523f](https://github.com/carlreid/StreamMaster/commit/753523f1756bad062bc11ed01e5964762012a5a7))
* Improve directory rename safety with atomic operation ([8e37e4b](https://github.com/carlreid/StreamMaster/commit/8e37e4b50d0e16d8b47bf53fa63251d7fbaec4ee))
* Remove handling of non-existing migration IDs ([4b3398e](https://github.com/carlreid/StreamMaster/commit/4b3398e76e6dd3b342131ee4791b19203e023966))
* Shellcheck related suggestions and fixes ([15a9af0](https://github.com/carlreid/StreamMaster/commit/15a9af0d074470cb4f7126831ec71189349a234d))
* Should attempt redirect first ([ae7925b](https://github.com/carlreid/StreamMaster/commit/ae7925b6f0d6fdb2b805f6bddf1073f2ecfa4cbb))
* Source env things first ([0b46c58](https://github.com/carlreid/StreamMaster/commit/0b46c58ae607227620847ce08f7fa8c8e1207a68))
* Trap postgres in case of failure ([2313119](https://github.com/carlreid/StreamMaster/commit/2313119ec48f4a29481b7f44d903ddd995b741e0))
* Wrap in quotes to avoid bad names ([0365eb1](https://github.com/carlreid/StreamMaster/commit/0365eb134f249323ef041e3ddfc2b1b6685855bb))


### Features

* Add auto set EPG button more front facing ([464aa01](https://github.com/carlreid/StreamMaster/commit/464aa01d5365f01a777b323cb07fa3e038ed0b33))
* Add Play Stream dialog ([9504cdd](https://github.com/carlreid/StreamMaster/commit/9504cdd8c2425ed524dda204799a97d72ffeefa0))
* Add validation of PUID and PGID values ([7ed4915](https://github.com/carlreid/StreamMaster/commit/7ed4915ddbd0c1c0b9068f4fc3de8785ab04169a))
* Handle directory creation with fail output ([341fd2b](https://github.com/carlreid/StreamMaster/commit/341fd2b5e21c562dd86bda1374c8d7e08d5e5eac))
* Handle faulire cases ([8126757](https://github.com/carlreid/StreamMaster/commit/8126757c93da5a23a44c4ab657d992cab6da4830))
* Make PostgreSQL wait parameters configurable ([c945727](https://github.com/carlreid/StreamMaster/commit/c945727dfce1672b06f57508c91cb8790313fc65))
* Output failures when `chown` ([828270c](https://github.com/carlreid/StreamMaster/commit/828270c54f6e3fcc997f148a1dfe720059f8fd7a))

# [0.11.0](https://github.com/carlreid/StreamMaster/compare/v0.10.2...v0.11.0) (2025-02-25)


### Features

* Add Play Stream dialog ([9504cdd](https://github.com/carlreid/StreamMaster/commit/9504cdd8c2425ed524dda204799a97d72ffeefa0))

## [0.10.2](https://github.com/carlreid/StreamMaster/compare/v0.10.1...v0.10.2) (2025-02-24)


### Bug Fixes

* `SMButton` was using the wrong class hook ([bfea17b](https://github.com/carlreid/StreamMaster/commit/bfea17b89114670106751bd539f8e4d8df0b7a1d))
* added clear button back to drop down box, only show if selecting multiple options is available ([033d54b](https://github.com/carlreid/StreamMaster/commit/033d54bbe6973771de989e53bf4d26296242f972))
* added clear button back to drop down box, only show if selecting multiple options is available ([37c648b](https://github.com/carlreid/StreamMaster/commit/37c648bc079372918b578b0a798599f038fe59aa))
* added missing tooltip to dropdown filter button ([91f0ebb](https://github.com/carlreid/StreamMaster/commit/91f0ebbe927f0004401b10530cd91f56cabfc11b))

## [0.10.1](https://github.com/carlreid/StreamMaster/compare/v0.10.0...v0.10.1) (2025-02-24)


### Bug Fixes

* Correctly handle different logo routes ([40a9c06](https://github.com/carlreid/StreamMaster/commit/40a9c066c3391808ac45cd61d5d4a290d835ad20))

# [0.10.0](https://github.com/carlreid/StreamMaster/compare/v0.9.3...v0.10.0) (2025-02-24)


### Features

* Improve program/logo handling in XMLTV ([faa3cb9](https://github.com/carlreid/StreamMaster/commit/faa3cb9b435daac95543e71bad3f7fbc4aff3163))

## [0.9.3](https://github.com/carlreid/StreamMaster/compare/v0.9.2...v0.9.3) (2025-02-22)


### Bug Fixes

* `main` and `latest` as tracking tags ([55597b6](https://github.com/carlreid/StreamMaster/commit/55597b61ca6a3f851863dad7fc73b829b5d034cd))
* Correct link to Docker Hub ([63cf991](https://github.com/carlreid/StreamMaster/commit/63cf991fc6d3fe2507401e48dc6d53730737eda5))
* No release script to single commit and tag ([c037aeb](https://github.com/carlreid/StreamMaster/commit/c037aeb43dca55dc888df7ebdfcfbb42f5144ca9))
* Run after main workflow name ([d3d8d8e](https://github.com/carlreid/StreamMaster/commit/d3d8d8ee2511d4666b1cb9e857249280f656827b))

## [0.9.1](https://github.com/carlreid/StreamMaster/compare/v0.9.0...v0.9.1) (2025-02-22)


### Bug Fixes

* Pull full history ([55650ab](https://github.com/carlreid/StreamMaster/commit/55650ab00ffb05c75757a67ef08a6d08d54eb7c6))

# [0.9.0](https://github.com/carlreid/StreamMaster/compare/v0.8.1...v0.9.0) (2025-02-22)


### Bug Fixes

* Produce git commit and tag on same commit ([e375d2e](https://github.com/carlreid/StreamMaster/commit/e375d2eac4aef67d798cb5f1e698ffae7bacdf50))
* Use `token` to re-fetch same `github.ref` ([28799d6](https://github.com/carlreid/StreamMaster/commit/28799d69514a9a554d47f157a4bb48c76f956429))


### Features

* Mirror images to Docker Hub ([5af71d2](https://github.com/carlreid/StreamMaster/commit/5af71d2a28307e9f8cb472c3947c4f6d5b5ee091))

## [0.16.5](https://github.com/carlreid/StreamMaster/compare/v0.16.4...v0.16.5) (2024-10-31)


### Bug Fixes

* M3U Needs update logging ([580d278](https://github.com/carlreid/StreamMaster/commit/580d278fe142f4b57f87673742e4ce11b5dc127d))

## [0.16.4](https://github.com/carlreid/StreamMaster/compare/v0.16.3...v0.16.4) (2024-10-31)


### Bug Fixes

* Modifying default profiles ([b96e000](https://github.com/carlreid/StreamMaster/commit/b96e000f5ad1368551bb35a4af8ccfaf4da9aa2c))

## [0.16.3](https://github.com/carlreid/StreamMaster/compare/v0.16.2...v0.16.3) (2024-10-31)


### Bug Fixes

* Added loggsettings to backups ([5453521](https://github.com/carlreid/StreamMaster/commit/5453521163fdf4e76b8413dfafbcfc42f8a457e0))

## [0.16.2](https://github.com/carlreid/StreamMaster/compare/v0.16.1...v0.16.2) (2024-10-30)


### Bug Fixes

* Add Response Compression ([698da5d](https://github.com/carlreid/StreamMaster/commit/698da5d5fc696a98a1d3594942eaf9a1d06bbaaf))

## [0.16.1](https://github.com/carlreid/StreamMaster/compare/v0.16.0...v0.16.1) (2024-10-29)


### Bug Fixes

* version bump ([40edd2f](https://github.com/carlreid/StreamMaster/commit/40edd2f2725d9f268e8cabf1e2142f580984c3de))

# [0.16.0](https://github.com/carlreid/StreamMaster/compare/v0.15.0...v0.16.0) (2024-10-29)

# [0.15.0](https://github.com/carlreid/StreamMaster/compare/v0.14.7...v0.15.0) (2024-10-29)

## [0.14.7](https://github.com/carlreid/StreamMaster/compare/v0.14.6...v0.14.7) (2024-10-28)


### Bug Fixes

* version bump ([963b260](https://github.com/carlreid/StreamMaster/commit/963b260c55abfe2223701d994ba8f521b6d722c3))

## [0.14.7](https://github.com/carlreid/StreamMaster/compare/v0.14.6...v0.14.7) (2024-10-28)


### Bug Fixes

* version bump ([963b260](https://github.com/carlreid/StreamMaster/commit/963b260c55abfe2223701d994ba8f521b6d722c3))

## [0.14.6](https://github.com/carlreid/StreamMaster/compare/v0.14.5...v0.14.6) (2024-05-07)


### Bug Fixes

* FFMPEG on ARM64 ([4c8d9f5](https://github.com/carlreid/StreamMaster/commit/4c8d9f58d94582792ae466041e4c852fd58494f2))

## [0.14.4](https://github.com/carlreid/StreamMaster/compare/v0.14.3...v0.14.4) (2024-03-14)


### Bug Fixes

* moved backup dir ([a583171](https://github.com/carlreid/StreamMaster/commit/a583171c2ee954022c81ff69d9d64592d40d6fb5))

## [0.14.3](https://github.com/carlreid/StreamMaster/compare/v0.14.2...v0.14.3) (2024-03-14)


### Bug Fixes

* allow UIDs >= 10 ([40dee89](https://github.com/carlreid/StreamMaster/commit/40dee89c24040844fce42abbb7925fea3c3a1bf7))
* client registration dupe ([9195fcb](https://github.com/carlreid/StreamMaster/commit/9195fcb1be96174ea86d0e92908a428557caa768))

## [0.14.2](https://github.com/carlreid/StreamMaster/compare/v0.14.1...v0.14.2) (2024-03-06)


### Bug Fixes

* startup ([2659421](https://github.com/carlreid/StreamMaster/commit/26594218b4e34a168d8de60176796d7823a4c9b1))

## [0.14.1](https://github.com/carlreid/StreamMaster/compare/v0.14.0...v0.14.1) (2024-02-29)


### Bug Fixes

* Icon selector ([aee88ca](https://github.com/carlreid/StreamMaster/commit/aee88ca71ce7694f7cd58a013ac929a2b7f42906))

# [0.14.0](https://github.com/carlreid/StreamMaster/compare/v0.13.3...v0.14.0) (2024-02-29)

## [0.13.3](https://github.com/carlreid/StreamMaster/compare/v0.13.2...v0.13.3) (2024-02-29)


### Bug Fixes

* Initial ([#212](https://github.com/carlreid/StreamMaster/issues/212)) ([6bcc3a0](https://github.com/carlreid/StreamMaster/commit/6bcc3a07ce6d500e4ac154ecf9bba56fd3ac08f9))

## [0.13.2](https://github.com/carlreid/StreamMaster/compare/v0.13.1...v0.13.2) (2024-02-22)

## [0.13.2-sh.2](https://github.com/carlreid/StreamMaster/compare/v0.13.2-sh.1...v0.13.2-sh.2) (2024-02-22)


### Bug Fixes

* Client writing ([d3f4f54](https://github.com/carlreid/StreamMaster/commit/d3f4f54dcfa39b0d9ccb65e5a974bd72f0848d62))

## [0.13.2-sh.2](https://github.com/carlreid/StreamMaster/compare/v0.13.2-sh.1...v0.13.2-sh.2) (2024-02-22)


### Bug Fixes

* Client writing ([d3f4f54](https://github.com/carlreid/StreamMaster/commit/d3f4f54dcfa39b0d9ccb65e5a974bd72f0848d62))

## [0.13.2-sh.2](https://github.com/carlreid/StreamMaster/compare/v0.13.2-sh.1...v0.13.2-sh.2) (2024-02-22)


### Bug Fixes

* Client writing ([d3f4f54](https://github.com/carlreid/StreamMaster/commit/d3f4f54dcfa39b0d9ccb65e5a974bd72f0848d62))

## [0.13.2-sh.1](https://github.com/carlreid/StreamMaster/compare/v0.13.1...v0.13.2-sh.1) (2024-02-21)


### Bug Fixes

* update videoinfo ([f7231ac](https://github.com/carlreid/StreamMaster/commit/f7231ac323601b3a0387e4d48e7c3c3260eea728))

## [0.13.1](https://github.com/carlreid/StreamMaster/compare/v0.13.0...v0.13.1) (2024-02-11)


### Bug Fixes

* EPG/M3U refresh/proc ([aabc523](https://github.com/carlreid/StreamMaster/commit/aabc523592e677dc00b02c4e5f15f32566148431))

# [0.13.0](https://github.com/carlreid/StreamMaster/compare/v0.12.3...v0.13.0) (2024-02-11)

## [0.12.3](https://github.com/carlreid/StreamMaster/compare/v0.12.2...v0.12.3) (2024-02-11)

## [0.12.2](https://github.com/carlreid/StreamMaster/compare/v0.12.1...v0.12.2) (2024-02-11)

## [0.12.1](https://github.com/carlreid/StreamMaster/compare/v0.12.0...v0.12.1) (2024-02-11)

# [0.12.0](https://github.com/carlreid/StreamMaster/compare/v0.11.2...v0.12.0) (2024-02-11)


### Features

* removed buffer, optimized streaming ([#209](https://github.com/carlreid/StreamMaster/issues/209)) ([4553c81](https://github.com/carlreid/StreamMaster/commit/4553c812926b61415d04798778d09a8fca4f4cdd))

# [0.12.0-debuffer.9](https://github.com/carlreid/StreamMaster/compare/v0.12.0-debuffer.8...v0.12.0-debuffer.9) (2024-02-11)


### Features

* Add logging db ([adefa7e](https://github.com/carlreid/StreamMaster/commit/adefa7e99d2e592a89953fe4f5a40219f66a9aed))

# [0.12.0-debuffer.8](https://github.com/carlreid/StreamMaster/compare/v0.12.0-debuffer.7...v0.12.0-debuffer.8) (2024-02-10)


### Bug Fixes

* Set IsFailed flag and close stream in StreamHandler ([c321a9f](https://github.com/carlreid/StreamMaster/commit/c321a9faf1320cd843bd6c23de3cbeb8b1c11d2e))

# [0.12.0-debuffer.7](https://github.com/carlreid/StreamMaster/compare/v0.12.0-debuffer.6...v0.12.0-debuffer.7) (2024-02-10)


### Bug Fixes

* Handle case when toRead is less than 0 in StreamHandler ([81e637a](https://github.com/carlreid/StreamMaster/commit/81e637ab7160ee355021d301e8985099fc7d4c80))


### Features

* Add logo to input statistics ([29376f8](https://github.com/carlreid/StreamMaster/commit/29376f883b212239a270a0457c5af62d54458799))

# [0.12.0-debuffer.6](https://github.com/carlreid/StreamMaster/compare/v0.12.0-debuffer.5...v0.12.0-debuffer.6) (2024-02-10)


### Bug Fixes

* Removed ringBufferSizeMB ([88c913f](https://github.com/carlreid/StreamMaster/commit/88c913fd2ccecd7897e022dca5031ab6e66cecd8))

# [0.12.0-debuffer.5](https://github.com/carlreid/StreamMaster/compare/v0.12.0-debuffer.4...v0.12.0-debuffer.5) (2024-02-10)


### Bug Fixes

* Commented out log messages to skipInfo execution ([23f9e15](https://github.com/carlreid/StreamMaster/commit/23f9e15f1a10329cfbb464f3f82e14a5eee20378))
* Complete read channel writer in ClientConfiguration ([c995fbe](https://github.com/carlreid/StreamMaster/commit/c995fbe887785d27a0594bd1ebe92ca7184d60f0))
* Fix issue with unregistering client streamers ([fe55a97](https://github.com/carlreid/StreamMaster/commit/fe55a971f5320c9796868574e3cc784b4ef2e621))
* stats ([623fab1](https://github.com/carlreid/StreamMaster/commit/623fab16dc3e758bdf963d9192ddcb9915c42b53))

# [0.12.0-debuffer.4](https://github.com/carlreid/StreamMaster/compare/v0.12.0-debuffer.3...v0.12.0-debuffer.4) (2024-02-10)


### Bug Fixes

* m3u refresh ([d9b509f](https://github.com/carlreid/StreamMaster/commit/d9b509f233f8b7aabf4669dd89fa559aa91909e0))

# [0.12.0-debuffer.3](https://github.com/carlreid/StreamMaster/compare/v0.12.0-debuffer.2...v0.12.0-debuffer.3) (2024-02-10)


### Bug Fixes

* ffmpeg kill ([e91b357](https://github.com/carlreid/StreamMaster/commit/e91b357e57d04684a1f111cf43cb03e13ea97475))

# [0.12.0-debuffer.2](https://github.com/carlreid/StreamMaster/compare/v0.12.0-debuffer.1...v0.12.0-debuffer.2) (2024-02-10)


### Bug Fixes

* CRLF -> LF ([dc2ede3](https://github.com/carlreid/StreamMaster/commit/dc2ede3d9949bec4917fa69562137fe1800a3d3f))

# [0.12.0-debuffer.1](https://github.com/carlreid/StreamMaster/compare/v0.11.2...v0.12.0-debuffer.1) (2024-02-10)


### Features

* removed buffer, optimized streaming ([b9fbf82](https://github.com/carlreid/StreamMaster/commit/b9fbf82b96fa0031e2f0c40d6d7397d17c958dde))

## [0.11.2](https://github.com/carlreid/StreamMaster/compare/v0.11.1...v0.11.2) (2024-02-09)


### Bug Fixes

* M3U Refresh from UI ([7cd0f54](https://github.com/carlreid/StreamMaster/commit/7cd0f54abb62f6539106ce44eca6ae4df606db1b))

## [0.11.1](https://github.com/carlreid/StreamMaster/compare/v0.11.0...v0.11.1) (2024-02-09)


### Bug Fixes

* Change ID to always ([dc56aa9](https://github.com/carlreid/StreamMaster/commit/dc56aa931268001051960dc45f90d6b22a6b4610))

# [0.11.0](https://github.com/carlreid/StreamMaster/compare/v0.10.2...v0.11.0) (2024-02-09)


### Features

* Backups ([3a6174f](https://github.com/carlreid/StreamMaster/commit/3a6174ff9bfbc9131fadc4e71588655104c0496f))

## [0.10.2](https://github.com/carlreid/StreamMaster/compare/v0.10.1...v0.10.2) (2024-02-09)


### Bug Fixes

* version bump ([bcfaa98](https://github.com/carlreid/StreamMaster/commit/bcfaa983975259504938073de465c1121a120342))

## [0.10.1](https://github.com/carlreid/StreamMaster/compare/v0.10.0...v0.10.1) (2024-02-08)


### Bug Fixes

* DTD processing ([643b704](https://github.com/carlreid/StreamMaster/commit/643b704c812d66b780c10c10f94740554bc286c6))
* Fix DtdProcessing in FileUtil.cs ([737d448](https://github.com/carlreid/StreamMaster/commit/737d448a0dda4aef989c878f6948cff6e2cf099f))

# [0.10.0](https://github.com/carlreid/StreamMaster/compare/v0.9.4...v0.10.0) (2024-02-08)


### Bug Fixes

* Fix FileUtil.GetFileDataStream to return a ([7eace41](https://github.com/carlreid/StreamMaster/commit/7eace4175c3d3104ebf6e2819fd5d9c19b81ec78))


### Features

* Add new API method for checking if system is ready ([33093d7](https://github.com/carlreid/StreamMaster/commit/33093d735c16fdcca2b9af515c50483272d99f92))

## [0.9.4](https://github.com/carlreid/StreamMaster/compare/v0.9.3...v0.9.4) (2024-02-08)


### Bug Fixes

* epg ([d303367](https://github.com/carlreid/StreamMaster/commit/d303367cc598575753bafb9cd794eee4149022f7))

## [0.9.3](https://github.com/carlreid/StreamMaster/compare/v0.9.2...v0.9.3) (2024-02-08)


### Bug Fixes

* epg number/color on create ([4e71fd7](https://github.com/carlreid/StreamMaster/commit/4e71fd7a2c3e4144c30471a04b6b64d38fa40277))

## [0.9.2](https://github.com/carlreid/StreamMaster/compare/v0.9.1...v0.9.2) (2024-02-08)


### Bug Fixes

* Update Dockerfile and entrypoint.sh ([80270b6](https://github.com/carlreid/StreamMaster/commit/80270b6a420c3a0142eb6e72874bce3ba787419f))

## [0.9.1](https://github.com/carlreid/StreamMaster/compare/v0.9.0...v0.9.1) (2024-02-08)

# [0.9.0](https://github.com/carlreid/StreamMaster/compare/v0.8.2...v0.9.0) (2024-02-08)


### Features

* pgsql ([315d5ea](https://github.com/carlreid/StreamMaster/commit/315d5ead57e6c0e563cda2d942d16e29197473b8))

## [0.8.2](https://github.com/carlreid/StreamMaster/compare/v0.8.1...v0.8.2) (2024-02-08)


### Bug Fixes

* version bump ([e10bef6](https://github.com/carlreid/StreamMaster/commit/e10bef671770fcfff43e1aa8147bc4dd6c111240))

# [0.9.0-i-am-not-drunk.27](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.26...v0.9.0-i-am-not-drunk.27) (2024-02-08)


### Features

* Update RootSideBar component to display different logo based on system readiness ([71ee097](https://github.com/carlreid/StreamMaster/commit/71ee0976a5218f1bb5003232f2f8bc53c7afbe71))

# [0.9.0-i-am-not-drunk.26](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.25...v0.9.0-i-am-not-drunk.26) (2024-02-08)


### Bug Fixes

* epg gz files ([844203e](https://github.com/carlreid/StreamMaster/commit/844203eab733acb2d953c78ff282ea3d5bd07537))

# [0.9.0-i-am-not-drunk.25](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.24...v0.9.0-i-am-not-drunk.25) (2024-02-08)


### Bug Fixes

* killproc ([a4d711c](https://github.com/carlreid/StreamMaster/commit/a4d711cbf6a335ad81194daa5e9103bb0b2a8fe4))


### Features

* Add ResetButton and SaveButton components to SettingsEditor ([3c3291c](https://github.com/carlreid/StreamMaster/commit/3c3291ca6a55ce4e7f3a8f18901e36c115f94a95))

# [0.9.0-i-am-not-drunk.24](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.23...v0.9.0-i-am-not-drunk.24) (2024-02-08)


### Bug Fixes

* sg stream count ([c4aada3](https://github.com/carlreid/StreamMaster/commit/c4aada3d1857a6e5db2958db86f94c074092b495))

# [0.9.0-i-am-not-drunk.23](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.22...v0.9.0-i-am-not-drunk.23) (2024-02-08)


### Bug Fixes

* Stream Group channel groups ([910d3df](https://github.com/carlreid/StreamMaster/commit/910d3df1fe2ceacbf4c6fc486985b4d512f76ebb))

# [0.9.0-i-am-not-drunk.22](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.21...v0.9.0-i-am-not-drunk.22) (2024-02-08)


### Features

* add Use CUID for channel id ([d1130f3](https://github.com/carlreid/StreamMaster/commit/d1130f3d0fbde82e0f1b4a68d5ff8cd8e1de424f))

# [0.9.0-i-am-not-drunk.21](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.20...v0.9.0-i-am-not-drunk.21) (2024-02-07)

# [0.9.0-i-am-not-drunk.20](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.19...v0.9.0-i-am-not-drunk.20) (2024-02-07)


### Bug Fixes

* Fix permission in entrypoint.sh ([285f80a](https://github.com/carlreid/StreamMaster/commit/285f80a7dadd262ead9d90c0f3585c9e4a8e2172))

# [0.9.0-i-am-not-drunk.19](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.18...v0.9.0-i-am-not-drunk.19) (2024-02-07)

# [0.9.0-i-am-not-drunk.18](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.17...v0.9.0-i-am-not-drunk.18) (2024-02-07)


### Features

* Add repository context initialization before migration ([b0d4b44](https://github.com/carlreid/StreamMaster/commit/b0d4b4477ec784f5b154f674211b7b507ba44a4f))

# [0.9.0-i-am-not-drunk.17](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.16...v0.9.0-i-am-not-drunk.17) (2024-02-07)


### Features

* Add pause and resume functionality for stream readers ([404c4fe](https://github.com/carlreid/StreamMaster/commit/404c4fe793c10565f32a2b38320c45180bdf4bc1))

# [0.9.0-i-am-not-drunk.16](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.15...v0.9.0-i-am-not-drunk.16) (2024-02-07)


### Features

* Add cancellationToken parameter to Task.Delay in ClientReadStream ([b1ca262](https://github.com/carlreid/StreamMaster/commit/b1ca262086de8d7493672092b74679bfdf34a225))

# [0.9.0-i-am-not-drunk.15](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.14...v0.9.0-i-am-not-drunk.15) (2024-02-07)


### Bug Fixes

* Fix timeout issue in ClientReadStream ([b5625e9](https://github.com/carlreid/StreamMaster/commit/b5625e927c7916c927e339a6d6318f2892e31627))


### Performance Improvements

* Improve logging message in StreamHandler.cs ([3cd00f3](https://github.com/carlreid/StreamMaster/commit/3cd00f3d23919a976f302a0286c11f8853036431))

# [0.9.0-i-am-not-drunk.14](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.13...v0.9.0-i-am-not-drunk.14) (2024-02-07)


### Bug Fixes

* CG ID ([bcd8524](https://github.com/carlreid/StreamMaster/commit/bcd8524e90be05a3521bbda569e3d2cf574f5088))
* Fix stream handler restart issue ([98c4f43](https://github.com/carlreid/StreamMaster/commit/98c4f434380ccbec49f30784489e34e977b8000a))

# [0.9.0-i-am-not-drunk.13](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.12...v0.9.0-i-am-not-drunk.13) (2024-02-07)

# [0.9.0-i-am-not-drunk.12](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.11...v0.9.0-i-am-not-drunk.12) (2024-02-07)


### Bug Fixes

* Fixing empty shortids count output in RepositoryContextInitializer ([8076d79](https://github.com/carlreid/StreamMaster/commit/8076d79ddde4e04a28d070999eb73bb9ca31ba05))


### Features

* Add support for M3U settings in the API ([f7fee73](https://github.com/carlreid/StreamMaster/commit/f7fee73dadb06f2c0a8e8d9de48978f1e4f80ab1))

# [0.9.0-i-am-not-drunk.11](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.10...v0.9.0-i-am-not-drunk.11) (2024-02-07)

# [0.9.0-i-am-not-drunk.10](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.9...v0.9.0-i-am-not-drunk.10) (2024-02-07)


### Bug Fixes

* Fixed logging message in StreamHandler ([8d8100e](https://github.com/carlreid/StreamMaster/commit/8d8100e25a841a00a948cefef9fc1dfdf2b199a9))
* Remove preloadPercentage from settings ([099e643](https://github.com/carlreid/StreamMaster/commit/099e643c19b974fe539bba63f187c163999958d6))
* stuff ([e4202dd](https://github.com/carlreid/StreamMaster/commit/e4202dd3f415054e732451e22d608fe519a03885))

# [0.9.0-i-am-not-drunk.9](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.8...v0.9.0-i-am-not-drunk.9) (2024-02-07)


### Bug Fixes

* Fix shortid fix logic in RepositoryContextInitializer ([ed0a47e](https://github.com/carlreid/StreamMaster/commit/ed0a47edcc6a8214935600055e85e232422b8993))

# [0.9.0-i-am-not-drunk.8](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.7...v0.9.0-i-am-not-drunk.8) (2024-02-07)


### Bug Fixes

* fix LastDownloaded time to be in universal time format ([014d4df](https://github.com/carlreid/StreamMaster/commit/014d4df8cd779998c44c0d137f0577b0796aeef8))
* Icon paths ([f1b0fe2](https://github.com/carlreid/StreamMaster/commit/f1b0fe2fa02fce3398c009f88cb3bb7fb8c9d2a4))

# [0.9.0-i-am-not-drunk.7](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.6...v0.9.0-i-am-not-drunk.7) (2024-02-05)


### Bug Fixes

* Add PostgreSQL configuration settings ([39f7c1d](https://github.com/carlreid/StreamMaster/commit/39f7c1d2f28648cdd2a062db143987e6078e0fbc))

# [0.9.0-i-am-not-drunk.6](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.5...v0.9.0-i-am-not-drunk.6) (2024-02-05)


### Bug Fixes

* utc ([d3b0e9d](https://github.com/carlreid/StreamMaster/commit/d3b0e9da95603e62e2e8f8a6a37b24122c2a1ef5))


### Features

* Add Dockerfile changes and expose port 5432 ([78afc55](https://github.com/carlreid/StreamMaster/commit/78afc55b32bd4032ea60e90ce659596a2d1cb586))

# [0.9.0-i-am-not-drunk.6](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.5...v0.9.0-i-am-not-drunk.6) (2024-02-05)


### Bug Fixes

* utc ([d3b0e9d](https://github.com/carlreid/StreamMaster/commit/d3b0e9da95603e62e2e8f8a6a37b24122c2a1ef5))


### Features

* Add Dockerfile changes and expose port 5432 ([78afc55](https://github.com/carlreid/StreamMaster/commit/78afc55b32bd4032ea60e90ce659596a2d1cb586))

# [0.9.0-i-am-not-drunk.6](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.5...v0.9.0-i-am-not-drunk.6) (2024-02-05)


### Bug Fixes

* utc ([d3b0e9d](https://github.com/carlreid/StreamMaster/commit/d3b0e9da95603e62e2e8f8a6a37b24122c2a1ef5))


### Features

* Add Dockerfile changes and expose port 5432 ([78afc55](https://github.com/carlreid/StreamMaster/commit/78afc55b32bd4032ea60e90ce659596a2d1cb586))

# [0.9.0-i-am-not-drunk.6](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.5...v0.9.0-i-am-not-drunk.6) (2024-02-05)


### Bug Fixes

* utc ([d3b0e9d](https://github.com/carlreid/StreamMaster/commit/d3b0e9da95603e62e2e8f8a6a37b24122c2a1ef5))

# [0.9.0-i-am-not-drunk.5](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.4...v0.9.0-i-am-not-drunk.5) (2024-02-03)


### Bug Fixes

* Fix ownership of /config folder ([6cb6f16](https://github.com/carlreid/StreamMaster/commit/6cb6f16fea4e01d52a7867dced8dd66e3385fc21))

# [0.9.0-i-am-not-drunk.4](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.3...v0.9.0-i-am-not-drunk.4) (2024-02-03)


### Bug Fixes

* Optimzied filtering ([078ad2e](https://github.com/carlreid/StreamMaster/commit/078ad2e0b0490464812b8b4ddc422eb3d22a368c))


### Features

* Add support for database settings ([9a2b472](https://github.com/carlreid/StreamMaster/commit/9a2b47223f32b4821616f78170e464c762070b04))

# [0.9.0-i-am-not-drunk.3](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.2...v0.9.0-i-am-not-drunk.3) (2024-02-03)

# [0.9.0-i-am-not-drunk.2](https://github.com/carlreid/StreamMaster/compare/v0.9.0-i-am-not-drunk.1...v0.9.0-i-am-not-drunk.2) (2024-02-03)

# [0.9.0-i-am-not-drunk.1](https://github.com/carlreid/StreamMaster/compare/v0.8.1...v0.9.0-i-am-not-drunk.1) (2024-02-03)


### Features

*  Migrate from SQLite to PostgreSQL ([5e5b91b](https://github.com/carlreid/StreamMaster/commit/5e5b91b9cefd1ff8dd468cae580208c6ffa5df57))
* add in psql support ([13a2bd1](https://github.com/carlreid/StreamMaster/commit/13a2bd15ae8c840c8e437c6c31c2f5f47f32e156))
* pgsql ([281ad7b](https://github.com/carlreid/StreamMaster/commit/281ad7b0c6bb66fd7454d3d4e67d260601d7bce5))

## [0.8.1](https://github.com/carlreid/StreamMaster/compare/v0.8.0...v0.8.1) (2024-02-03)


### Bug Fixes

* Adjusted regex pattern to capture optional build/revision number ([3b6a1f1](https://github.com/carlreid/StreamMaster/commit/3b6a1f13aa4d87660f26baebbf8d5b3b5a8608ca))

## [0.8.1](https://github.com/carlreid/StreamMaster/compare/v0.8.0...v0.8.1) (2024-02-03)


### Bug Fixes

* Adjusted regex pattern to capture optional build/revision number ([3b6a1f1](https://github.com/carlreid/StreamMaster/commit/3b6a1f13aa4d87660f26baebbf8d5b3b5a8608ca))

# [0.8.0](https://github.com/carlreid/StreamMaster/compare/v0.7.1...v0.8.0) (2024-02-03)


### Features

* Add new feature for user ([d9afed5](https://github.com/carlreid/StreamMaster/commit/d9afed5d81a5376cb6bad4958eed791b676f17a8))

# [0.8.0-stream.12](https://github.com/carlreid/StreamMaster/compare/v0.8.0-stream.11...v0.8.0-stream.12) (2024-02-02)


### Bug Fixes

* refactor BuildEpisodeNumbers and original air date ([1d22906](https://github.com/carlreid/StreamMaster/commit/1d2290617ad37a37de5218c653d8c3a7bde8f56b))

# [0.8.0-stream.11](https://github.com/carlreid/StreamMaster/compare/v0.8.0-stream.10...v0.8.0-stream.11) (2024-02-02)


### Bug Fixes

* refactor GetIconUrl to use IIconHelper ([6ef22c0](https://github.com/carlreid/StreamMaster/commit/6ef22c0452cecad50ce06127938f71fadff641a9))

# [0.8.0-stream.10](https://github.com/carlreid/StreamMaster/compare/v0.8.0-stream.9...v0.8.0-stream.10) (2024-02-02)


### Bug Fixes

* issue with retrieving MxfService ([b8f4cd3](https://github.com/carlreid/StreamMaster/commit/b8f4cd3a9ce186a538c1a92f1c1ba8fece26ddab))

# [0.8.0-stream.9](https://github.com/carlreid/StreamMaster/compare/v0.8.0-stream.8...v0.8.0-stream.9) (2024-02-02)


### Features

* Add functionality to handle dummy video streams ([e666c4d](https://github.com/carlreid/StreamMaster/commit/e666c4d3c3c832b9e16f4fb4d8fa5f888e00d1f8))

# [0.8.0-stream.8](https://github.com/carlreid/StreamMaster/compare/v0.8.0-stream.7...v0.8.0-stream.8) (2024-02-02)


### Bug Fixes

* Dummies ([08a0d97](https://github.com/carlreid/StreamMaster/commit/08a0d970acd65f050bdfc929e085c550bfd1b830))
* Dummies ([6b2568f](https://github.com/carlreid/StreamMaster/commit/6b2568f2bffd53e6206bcd2916d3cf91810403dd))

# [0.8.0-stream.7](https://github.com/carlreid/StreamMaster/compare/v0.8.0-stream.6...v0.8.0-stream.7) (2024-02-01)

# [0.8.0-stream.6](https://github.com/carlreid/StreamMaster/compare/v0.8.0-stream.5...v0.8.0-stream.6) (2024-02-01)


### Features

* Add NumericStringComparer class ([6b6002b](https://github.com/carlreid/StreamMaster/commit/6b6002b2ecc3373b613aa786e91228187ad3073c))
* Add support for displaying logos in stream group lineup ([afff393](https://github.com/carlreid/StreamMaster/commit/afff393b3bd7095a542de50eb6900bf89e383465))

# [0.8.0-stream.5](https://github.com/carlreid/StreamMaster/compare/v0.8.0-stream.4...v0.8.0-stream.5) (2024-02-01)


### Features

* Add concurrent processing for XMLTVBuilder ([1a65391](https://github.com/carlreid/StreamMaster/commit/1a6539158715e1bcba238ce4ab59314f51afd88c))

# [0.8.0-stream.4](https://github.com/carlreid/StreamMaster/compare/v0.8.0-stream.3...v0.8.0-stream.4) (2024-02-01)


### Bug Fixes

* Fix video stream service retrieval bug ([5cc615e](https://github.com/carlreid/StreamMaster/commit/5cc615edbba2d8d2973c3cda126f74809726f779))


### Features

* Add new properties to SGup ([982d3d7](https://github.com/carlreid/StreamMaster/commit/982d3d7597a84ba92a2aad441831ad0edbf86599))
* Add option to serialize stream group lineup without indentation ([0dcfd55](https://github.com/carlreid/StreamMaster/commit/0dcfd556b16c7fe065030e08b6dfc1e3e2ad111e))
* service id based of epgnumber ([1371be0](https://github.com/carlreid/StreamMaster/commit/1371be02c88add151d170e6bdaeb89cbda3c0553))

# [0.8.0-stream.3](https://github.com/carlreid/StreamMaster/compare/v0.8.0-stream.2...v0.8.0-stream.3) (2024-02-01)


### Bug Fixes

* changed tvlogos to use dictionary lookups ([fe7910a](https://github.com/carlreid/StreamMaster/commit/fe7910ad786ad6ca144a11018dd1b5d1a401a4bf))
* cleanup ([740584a](https://github.com/carlreid/StreamMaster/commit/740584a02daf1b77aad01a146788ab62e63ac925))
* Update build_docker.ps1 ([b0cbe5d](https://github.com/carlreid/StreamMaster/commit/b0cbe5df3098decc6cd00b21e86db872dde5fb33))
* Update VideoStreamRepository ([dae2a2d](https://github.com/carlreid/StreamMaster/commit/dae2a2daf4813812d5340f3aede99c182d90ed0c))
* Updated regex pattern to capture version, branch, and build/revision number ([809feb6](https://github.com/carlreid/StreamMaster/commit/809feb663a9acd6aec062cd387cb87f38dfcc308))
* version bump ([d6988ed](https://github.com/carlreid/StreamMaster/commit/d6988ed3b2722b439de6607787629e0effa94a06))


### Features

* Add BuildXmltvProgram function ([01abec6](https://github.com/carlreid/StreamMaster/commit/01abec6c3d0828fbc4c0b2b187085fd89dcbde16))
* Add EPGTester project ([0569fa9](https://github.com/carlreid/StreamMaster/commit/0569fa97bd6b0d858140a5d45643300bdf4f2cde))
* Add Get-AssemblyInfo function ([fbc0a14](https://github.com/carlreid/StreamMaster/commit/fbc0a14df7073a3ca9f6e5c24582342793813cd3))
* Add infrastructure services extension ([72f66ae](https://github.com/carlreid/StreamMaster/commit/72f66ae15076e2712a256c0ae628ab6f4dabda12))
* Add method to set SchedulesDirectData ([c20d9a2](https://github.com/carlreid/StreamMaster/commit/c20d9a2ca438fb0727833f5a29bbce69c6a083aa))
* Add program categories to XMLTVBuilderThis commit adds the functionality to build program categories in XMLTVBuilder. This feature will trigger a release bumping a MINOR version. ([f9cecf2](https://github.com/carlreid/StreamMaster/commit/f9cecf2769cab850fecad4a7540cf5b9c2f1c234))
* Add XmltvProgramme property to XmlTv2Mxf class ([94aad5b](https://github.com/carlreid/StreamMaster/commit/94aad5b42c286abb35a099cc4f67893bc6c2f73e))
* Create test streams with updated Tvg_ID and User_Tvg_ID values ([eb6afa5](https://github.com/carlreid/StreamMaster/commit/eb6afa5891cc41a1d0f67962a6d18e62d14982b8))

# [0.8.0-stream.2](https://github.com/carlreid/StreamMaster/compare/v0.8.0-stream.1...v0.8.0-stream.2) (2024-02-01)

### Features

- Normalize version format in updateAssemblyInfo.js ([4a64455](https://github.com/carlreid/StreamMaster/commit/4a644553c1ddc37810ace606e717cc246f4d4e2d))

# [0.8.0-stream.1](https://github.com/carlreid/StreamMaster/compare/v0.7.1...v0.8.0-stream.1) (2024-02-01)

### Bug Fixes

- locking for buffer delegate ([53e2589](https://github.com/carlreid/StreamMaster/commit/53e2589ef14c3d9fc06817489c97743a08a6cd02))

### Features

- Improve performance of ChannelManager ([7f4f616](https://github.com/carlreid/StreamMaster/commit/7f4f616081c4c22912d18b4552f88083a9dda98b))

## [0.7.1](https://github.com/carlreid/StreamMaster/compare/v0.7.0...v0.7.1) (2024-01-30)

### Bug Fixes

- Update cache provider to use sessionStorage ([bf366c0](https://github.com/carlreid/StreamMaster/commit/bf366c066d6b2c29200b6896a7fbc45fdb9c8fde))

# [0.7.0](https://github.com/carlreid/StreamMaster/compare/v0.6.1...v0.7.0) (2024-01-30)

### Features

- Add option to format EPG output ([6d88a38](https://github.com/carlreid/StreamMaster/commit/6d88a38ce7f7e9aedbdd08d887072bed2557c7d0))
- Reset method now uses EPG Number instead of ID ([d2f8996](https://github.com/carlreid/StreamMaster/commit/d2f8996d94ce1759b67c32077753056e61f02b41))

## [0.6.1](https://github.com/carlreid/StreamMaster/compare/v0.6.0...v0.6.1) (2024-01-30)

# [0.6.0](https://github.com/carlreid/StreamMaster/compare/v0.5.0...v0.6.0) (2024-01-30)

### Features

- Updated regex pattern for capturing version, optional branch, ([0e2c256](https://github.com/carlreid/StreamMaster/commit/0e2c256ed7034508f5db4490e269be21dda41bfc))
