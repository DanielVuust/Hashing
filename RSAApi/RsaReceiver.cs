using System.Security.Cryptography;
using System.Text;
using BlazorGuiServer.Data.Services;
using FluentResults;

namespace RSAApi
{
    public static class RsaReceiver
    {
        private const string KEYCONTAINERNAME = "ContainerName";

        public static void MapRsaReceiverEndpoints(this WebApplication app)
        {
            app.MapGet("/rsaReceiver/CreateRsaKey", (HttpContext httpContext, RsaReceiverService service) =>
            {
                var result = service.GenerateRasKeyAndSaveInCsp(KEYCONTAINERNAME);

                if (result.IsFailed)
                {
                    //TODO Add logging.
                    return Results.Problem(result.Errors.ToString());
                }

                RSA? rsaKey = null;
                try
                {
                    rsaKey = result.Value;

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
                var decryptedTextResult = service.DecryptEncryptedString(Convert.FromBase64String(encryptedText), KEYCONTAINERNAME);

                if (decryptedTextResult.IsFailed)
                {
                    return Results.Problem(decryptedTextResult.Errors.ToString());
                }

                return Results.Ok(Encoding.ASCII.GetString(decryptedTextResult.Value));
            });
            app.MapGet("/rsaReceiver/ClearWindowsCsp", (HttpContext httpContext, RsaReceiverService service) =>
            {
                var decryptedTextResult = service.ClearKeyFromCsp(KEYCONTAINERNAME);

                if (decryptedTextResult.IsFailed)
                {
                    return Results.Problem(decryptedTextResult.Errors.ToString());
                }
                return Results.Ok(decryptedTextResult);
            });
        }
    }
}
