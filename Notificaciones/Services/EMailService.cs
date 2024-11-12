using MVCT.Transversales.Services.Notificaciones.Domain.Dtos;
using RestSharp;
using System.Text.Json;

namespace MVCT.Transversales.Services.Notificaciones.Services;

public class EMailService : IEMailService
{
    private readonly string apiUrl;
    private readonly string? resourceRoute;
    private readonly string tokenValue;

    public EMailService(IConfiguration configuration)
    {
        apiUrl = configuration["Email:ApiUrl"]!;
        resourceRoute = configuration["Email:ResourceRoute"]; ;
        tokenValue = configuration["Email:TokenValue"];
    }

    public async Task<SendEMailResponse> SendEMail(SendEMailRequest sendEmailRequest)
    {
        var client = new RestClient(apiUrl);

        var request = new RestRequest(resourceRoute, Method.Post);
        request.AddHeader("X-SCKEY-TOKEN", tokenValue);
        request.AddJsonBody(sendEmailRequest);

        var response = await client.PostAsync(request);

        if (response.IsSuccessful)
        {
            var sendEmailResponse = JsonSerializer.Deserialize<SendEMailResponse>(response.Content);
            Console.WriteLine("Request was successful!");
            Console.WriteLine($"ResponseText: {sendEmailResponse.ResponseText}");
            Console.WriteLine($"StatusCode: {sendEmailResponse.StatusCode}");
            foreach (var recipient in sendEmailResponse.Recipients)
            {
                Console.WriteLine(recipient);
            }
            Console.WriteLine("Request was successful!");
            Console.WriteLine("Response content: " + response.Content);

            return sendEmailResponse;
        }
        else
        {
            Console.WriteLine("Request failed with status code: " + response.StatusCode);
            Console.WriteLine("Error message: " + response.ErrorMessage);
            return null;
        }
    }
}

