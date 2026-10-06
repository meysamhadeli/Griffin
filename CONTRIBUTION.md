## Contribution

This is great that you'd like to contribute to this project. All change requests should go through the steps described below.

## Pull Requests

**Please, make sure you open an issue before starting with a Pull Request, unless it's a typo or a really obvious error.** Pull requests are the best way to propose changes.

## Conventional commits

Our repository follows [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/#summary). Release Drafter maintains a draft release; when a maintainer publishes it, the NuGet workflow publishes every Griffin library with the release's shared version. Branch labels select minor or patch increments; patch is the default, and maintainers can apply the `major` label for a breaking release.

Pull requests should have a title that follows the specification, otherwise, merging is blocked. If you are not familiar with the specification simply ask maintainers to modify. You can also use this cheatsheet if you want:

- Use `fix/` branches for patch-level changes and `feat/` branches for minor-level changes; the Release Drafter autolabeler applies the corresponding version labels.
- Use the `major` PR label for breaking changes.
- Publishing a GitHub release triggers the NuGet package workflow. A manual workflow dispatch is also available when a version and source ref are specified.

## Resources

- [How to Contribute to Open Source](https://opensource.guide/how-to-contribute/)
- [Using Pull Requests](https://help.github.com/articles/about-pull-requests/)
- [GitHub Help](https://help.github.com)
