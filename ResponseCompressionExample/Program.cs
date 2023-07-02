using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddResponseCompression(options =>
{
	options.EnableForHttps = true;
	options.Providers.Add<BrotliCompressionProvider>();
	options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
	options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
	options.Level = CompressionLevel.Fastest;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseResponseCompression();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
