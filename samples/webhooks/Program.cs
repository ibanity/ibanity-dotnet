using Ibanity.Apis.Client;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var ibanityService = new IbanityServiceBuilder().
    SetEndpoint(configuration["Ibanity:Endpoint"]).
    AddClientCertificate(
        configuration["Ibanity:Certificates:Client:Path"],
        configuration["Ibanity:Certificates:Client:Passphrase"]).
    Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(ibanityService);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
