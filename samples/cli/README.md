# CLI client sample

## How to run

This application needs some environment variables to be set.

If you configured a .env file at the repository root, you will be able to load the variables and run the sample like this:

```sh
env $(../../scripts/get-integration-env | xargs) dotnet run
```

This command depends on _Python 3_ and _OpenSSL_ to run.

If you are running Windows, you can use [Cygwin](https://www.cygwin.com) to execute this command.
