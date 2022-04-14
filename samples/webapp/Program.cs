using Ibanity.Apis.Client;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var ibanityService = new IbanityServiceBuilder().
    SetEndpoint(configuration["Ibanity:Endpoint"]).
    AddClientCertificate(
        configuration["Ibanity:Certificates:Client:Path"],
        configuration["Ibanity:Certificates:Client:Passphrase"]).
    AddSignatureCertificate(
        configuration["Ibanity:Certificates:Signature:Id"],
        configuration["Ibanity:Certificates:Signature:Path"],
        configuration["Ibanity:Certificates:Signature:Passphrase"]).
    AddPontoConnectOAuth2Authentication(
        configuration["Ibanity:PontoConnect:ClientId"],
        configuration["Ibanity:PontoConnect:ClientSecret"]).
    EnableRetries().
    Build();

// Add services to the container.
builder.Services.
    AddSingleton<IIbanityService>(ibanityService).
    AddSession().
    AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
