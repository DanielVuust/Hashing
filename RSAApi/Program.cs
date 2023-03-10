using BlazorGuiServer.Data.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace RSAApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<RsaReceiverService>();
            builder.Services.AddSingleton<RsaSenderService>();
            builder.Services.AddSingleton<VigenereCipherService>();
            builder.Services.AddSession();
            builder.Services.AddDistributedMemoryCache();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSession();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            var summaries = new[]
            {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast");

            


            app.MapRsaSenderEndpoints();
            app.MapRsaReceiverEndpoints();
            app.MapVigenereCipherEndpoints();

            app.Run();
        }
        
    }

    public class RsaEncryptionDTO : IDto
    {
        public Result<bool> Validate()
        {
            if (string.IsNullOrEmpty(TextToEncrypt))
            {
                return Result.Fail("TextToEncrypt not valid");
            }
            if (string.IsNullOrEmpty(RsaXmlString))
            {
                return Result.Fail("RsaXmlString not valid");
            }

            return Result.Ok();
        }
        public string TextToEncrypt { get; set; }
        public string RsaXmlString { get; set; }

    }
}