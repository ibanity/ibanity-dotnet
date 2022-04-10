# CLI client sample

## How to run

This application needs some environment variables to be set.

If you configured a .env file at the repository root, you will be able to load the variables and run the sample like this:

```sh
env $(../../scripts/get-integration-env | xargs) dotnet run
```
