using System;
using System.Collections;
using System.Text.Json;
using RestSharp;
using Solicitudes.Domain.Dto;

namespace Solicitudes.Infrastructure.Services.NotificacionesService;

public class EMailService : IEmailService
{
    private readonly string apiUrl;
    private readonly string? resourceRoute;

    public EMailService(IConfiguration configuration)
    {
        apiUrl = configuration["Email:ApiUrl"];
        resourceRoute = configuration["Email:ResourceRoute"];

    }

    public async Task<SendEMailResponse> SendEMail(SendEMailRequest sendEmailRequest)
    {
        var client = new RestClient(apiUrl);

        var request = new RestRequest(resourceRoute, Method.Post);
        request.AddJsonBody(sendEmailRequest);

        var response = client.Execute(request);

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

    public string GetEMailBody(string templateName, Hashtable templateData)
    {
        string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}Templates/{templateName}";
        var parser = new Parser(filePath, templateData);
        return parser.Parse();
    }

    public async Task<SendEMailResponse> SendNotification(string bodyContent, List<string> recipients)
    {
        var sendEmailRequest = new SendEMailRequest
        (
            fromAddress: "pmartinez@nexura.com",///TEST EMAIL DONT CHANGE
            recipients: recipients,
            fromName: "SCEMail Integration Test",
            subject: "InTegration Test",
            bodyContent: bodyContent,
            type: NotificationType.EMAIL,
            certification: false

        ); ;
        return await SendEMail(sendEmailRequest);
    }
}


