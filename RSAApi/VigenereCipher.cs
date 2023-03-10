using BlazorGuiServer.Data.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace RSAApi
{
    public static class VigenereCipher
    {
        public static void MapVigenereCipherEndpoints(this WebApplication app)
        {
            app.MapPost("/vigenereCipher/encryptMessage", (HttpContext httpContext, VigenereCipherService service,
                [FromBody] VigenereCipherMessageDto dto) =>
            {
                var dtoResult = dto.Validate();
                if (dtoResult.IsFailed)
                {
                    return Results.Problem();
                }


                return Results.Ok(service.Encrypt(dto.SecretCode, dto.Message).Value);
            });
            app.MapPost("/vigenereCipher/decryptMessage", (HttpContext httpContext, VigenereCipherService service,
                [FromBody] VigenereCipherMessageDto dto) =>
            {
                var dtoResult = dto.Validate();
                if (dtoResult.IsFailed)
                {
                    return Results.Problem();
                }


                return Results.Ok(service.Decrypt(dto.SecretCode, dto.Message).Value);
            });
        }
    }

    public class VigenereCipherMessageDto : IDto
    {
        public string Message { get; set; }
        public string SecretCode { get; set; }
        public Result<bool> Validate()
        {
            return Result.Ok();
        }
    }
}
