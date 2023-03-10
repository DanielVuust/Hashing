using BlazorGuiServer.Data.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Cryptography;
using System.Text;
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

            app.MapGet("/rsaReceiver/CreateRsaKey", (HttpContext httpContext, RsaReceiverService service) =>
            {
                RSA rsaKey = null;
                try
                {
                    var result = service.GenerateRasKey2();
                    if (result.IsFailed)
                    {
                        throw new Exception(result.Errors.ToString());
                    }

                    rsaKey = result.Value;
                    httpContext.Session.SetString("rsaXmlString", rsaKey.ToXmlString(true));

                    //Only show user public parameters.
                    return Results.Ok(rsaKey.ToXmlString(false));

                }
                catch (Exception ex)
                {
                    //TODO Add logging.
                    return Results.Problem(ex.ToString());
                }
                finally
                {
                    rsaKey?.Dispose();
                }
            });
            app.MapGet("/rsaReceiver/DecryptRsaEncryptedText", (HttpContext httpContext, RsaReceiverService service, 
                string encryptedText) =>
            {
                var rsaXmlString = httpContext.Session.GetString("rsaXmlString");
                if (rsaXmlString == null)
                {
                    return Results.BadRequest("rsaXmlString not set");
                }
                var xmlKey = service.DecryptEncryptedString(Convert.FromBase64String(encryptedText), rsaXmlString);


                return Results.Ok(Encoding.ASCII.GetString(xmlKey.Value));
            });
            app.MapPost("/rsaSender/EncryptTextUsingPublicRsaKey", (HttpContext httpContext, RsaSenderService service,
                [FromBody] RsaEncryptionDTO dto) =>
            {
                var xmlKey = service.EncryptUsingRsaPublicKey(dto.RsaXmlString, dto.TextToEncrypt);


                return Results.Ok(Convert.ToBase64String(xmlKey.Value));
            });

            app.Run();
        }
        
    }

    public class RsaEncryptionDTO
    {
        public string TextToEncrypt { get; set; }
        public string RsaXmlString { get; set; }

    }
}