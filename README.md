# Ibanity .NET client library

[![Build status](https://github.com/ibanity/ibanity-dotnet/actions/workflows/ci.yaml/badge.svg)](https://github.com/ibanity/ibanity-dotnet/actions/workflows/ci.yaml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/ibanity/ibanity-dotnet/blob/master/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/Ibanity)](https://www.nuget.org/packages/Ibanity/)

This .NET Client library offers various services you can use in order to submit requests towards the Ibanity Platform.

## Quick start

Configure the library using `IbanityServiceBuilder`.

Minimal configuration values are:

- The Ibanity URL
- Your application private key and public certificate as _.pfx_ file (PKCS#12)
- The passphrase for the private key

```csharp
var ibanityService = new IbanityServiceBuilder().
    SetEndpoint("https://api.ibanity.com").
    AddClientCertificate(
        certificatePath,
        certificatePassword).
    Build();
```

You can then make use of **Ponto Connect** services through your `IIbanityService` instance.

```csharp
var financialInstitutions = await ibanityService.PontoConnect.FinancialInstitutions.List();
```

All services are thread safe and can be configured as singleton if you want to leverage frameworks like _IServiceProvider_ or _Castle Windsor_. To avoid exhausting client ports, you should use a single `IIbanityService` instance across your application.

### Code samples

There are three sample projects available within this repository:

- [Console application sample](https://github.com/ibanity/ibanity-dotnet/tree/master/samples/cli)
- [Web application sample](https://github.com/ibanity/ibanity-dotnet/tree/master/samples/webapp)
- [Webhooks sample](https://github.com/ibanity/ibanity-dotnet/tree/master/samples/webhooks)

See `ClientSample` class for extended examples.

### Use token to authenticate requests

A token is required to access most resource types.

```csharp
var ibanityService = new IbanityServiceBuilder().
    SetEndpoint("https://api.ibanity.com").
    AddClientCertificate(
        certificatePath,
        certificatePassword).
    AddPontoConnectOAuth2Authentication(
        pontoConnectClientId,
        pontoConnectClientSecret).
    Build();

var token = await ibanityService.PontoConnect.TokenService.GetToken(refreshToken);
var accounts = await ibanityService.PontoConnect.Accounts.List(token);

// Once done, you have to save the refresh token somewhere, so you can use it later to get another token.
// Refresh token value contained within the token instance is automatically updated from time to time.
SaveToken(token.RefreshToken);
```

### Perform custom requests to Ibanity

You can perform custom HTTP calls to Ibanity using the `IApiClient`.

It can be accessed by calling:

```csharp
var lowLevelClient = ibanityService.PontoConnect.ApiClient;
```

### Configure proxy

If you are using a Web application firewall or a proxy, you can configure it in the `IbanityServiceBuilder`, using one of these methods:

```csharp
AddProxy(IWebProxy proxy);
AddProxy(Uri endpoint);
AddProxy(string endpoint);
```

```csharp
var ibanityService = new IbanityServiceBuilder().
    SetEndpoint("https://api.ibanity.com").
    AddClientCertificate(
        certificatePath,
        certificatePassword).
    AddProxy("https://internal.proxy.com").
    Build();
```

### Sign custom requests to Ibanity

If you want to sign HTTP requests, you can use the `IHttpSignatureService` from the library.

```csharp
var signatureService = new HttpSignatureServiceBuilder().
    SetEndpoint("https://api.ibanity.com").
    AddCertificate(
        certificateId,
        certificatePath,
        certificatePassword).
    Build();
```

### Configure HTTP timeouts

```csharp
var ibanityService = new IbanityServiceBuilder().
    SetEndpoint("https://api.ibanity.com").
    AddClientCertificate(
        certificatePath,
        certificatePassword).
    SetTimeout(TimeSpan.FromSeconds(30)).
    Build();
```

### Verify and parse webhook events

```csharp
var webhookEvent = await ibanityService.Webhooks.VerifyAndDeserialize(
    payload, // webhook body
    signature); // webhook 'Signature' header

switch (webhookEvent)
{
    case SynchronizationSucceededWithoutChange synchronizationEvent:
        Console.WriteLine(synchronizationEvent.SynchronizationSubtype);
        break;
    ...
}
```

## Certificates

### Creating PFX certificate file

You may need to convert your certificate from _.pem_ files to a _.pfx_ file (also known as PKCS#12 format). You can use an _OpenSSL_ command to do so:

```
openssl pkcs12 -export -in certificate.pem -inkey private_key.pem -out certificate.pfx
```

Where:

- _certificate.pem_ is your existing certificate
- _private_key.pem_ is the private key matching your existing certificate
- _certificate.pfx_ is the resulting PKCS#12 file

You will have to enter the passphrase protecting the _private_key.pem_ file, then (twice) the passphrase you want to use to encrypt the _certificate.pfx_ file.

### Loading certificate

Mutual TLS client certificate (as well as signature certificate) can be loaded in two different ways:

- Using the path to the file holding the certificate, and its passphrase.
- Using a `X509Certificate2` instance, allowing you to load the certificate from a custom location you manage.

#### Azure App Service

When running on _Azure App Service_, even when loading the certificate from a file, Windows must access the certificate store. This may lead to an exception when the library is loading certificates.

You can use Azure CLI to allow this operation:

```bash
az webapp config appsettings set --name <app-name> --resource-group <resource-group-name> --settings WEBSITE_LOAD_USER_PROFILE=1
```

More information is available in [App Service documentation](https://learn.microsoft.com/en-us/azure/app-service/configure-ssl-certificate-in-code?tabs=linux#load-certificate-from-file).

## Requirements

Either:

- .NET Framework 4.6.2 (or above)
- .NET Core 2.0 (or above)
- .NET 5.0 (or above)
