using FileUploadServiceApi.Application.Commands;
using FileUploadServiceApi.Application.Interfaces;
using FileUploadServiceApi.Infrastructure.Data;
using FileUploadServiceApi.Infrastructure.Repositories;
using FileUploadServiceApi.Infrastructure.ServiceBus;
using FileUploadServiceApi.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<FileDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WriteConnection")));
builder.Services.AddDbContext<ReadFileDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ReadConnection")));

builder.Services.AddScoped<IFileRepository, FileRepository>();
var useAzureBlob = builder.Configuration.GetValue<bool>("UseAzureBlobStorage");
if (useAzureBlob)
{
    builder.Services.AddSingleton<IFileStorageService, AzureBlobStorageService>();
}
else
{
    builder.Services.AddSingleton<IFileStorageService, LocalFileStorageService>();
}
builder.Services.AddSingleton<ServiceBusConfiguration>();

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UploadFileCommandHandler).Assembly));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
