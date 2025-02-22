/**
 * @type {import('semantic-release').GlobalConfig}
 */

module.exports = {
  branches: [
    {
      channel: "main",
      name: "main",
      prerelease: false
    },
    {
      name: "!main",
      prerelease: true
    }
  ],
  ci: false,
  debug: false,
  dryRun: false,
  plugins: [
    [
      "@semantic-release/commit-analyzer",
      {
        preset: "angular",
        releaseRules: [
          { type: "docs", scope: "README", release: "minor" },
          { type: "refactor", release: "minor" },
          { type: "style", release: "patch" },
          { scope: "no-release", release: false },
          { type: "update", release: "patch" }
        ],
        parserOpts: {
          noteKeywords: ["BREAKING CHANGE", "BREAKING CHANGES"]
        }
      }
    ],
    "@semantic-release/release-notes-generator",
    [
      "@semantic-release/changelog",
      {
        changelogFile: "CHANGELOG.md"
      }
    ],
    [
      "@semantic-release/exec",
      {
        verifyConditionsCmd: ":",
        prepareCmd:
          "node src/updateAssemblyInfo.js ${nextRelease.version} ${nextRelease.gitHead} ${nextRelease.channel}"
      }
    ],
    [
      "@semantic-release/git",
      {
        assets: ["CHANGELOG.md", "src/StreamMaster.API/AssemblyInfo.cs"],
        message: "chore(release): ${nextRelease.version} [skip ci]\n\n${nextRelease.notes}"
      }
    ],
    [
      "@semantic-release/github",
      {
        assets: ["CHANGELOG.md"]
      }
    ]
  ]
};
