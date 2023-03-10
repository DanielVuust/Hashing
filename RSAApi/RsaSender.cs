using BlazorGuiServer.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace RSAApi
{
    public static class RsaSender
    {
        public static void MapRsaSenderEndpoints(this WebApplication app)
        {
            app.MapPost("/rsaSender/EncryptTextUsingPublicRsaKey", (HttpContext httpContext, RsaSenderService service,
                [FromBody] RsaEncryptionDTO dto) =>
            {
                var validationResult = dto.Validate();
                if (validationResult.IsFailed)
                {
                    //TODO Change to ValidationProblem.
                    return Results.Problem(validationResult.Errors.ToString());
                    //return Results.ValidationProblem();
                }

                var encryptedTextResult = service.EncryptUsingRsaPublicKey(dto.RsaXmlString, dto.TextToEncrypt);

                if (encryptedTextResult.IsFailed)
                {
                    return Results.Problem(encryptedTextResult.Errors.ToString());
                }

                return Results.Ok(Convert.ToBase64String(encryptedTextResult.Value));
            });
        }
    }
}
