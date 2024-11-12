using System.Collections;
using System.Text.Json;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Domain.Dto;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Domain.Enums;
using RestSharp;
using Serilog;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Services.EMail
{
    public class EmailService : IEmailService
    {
        private readonly string? _apiUrl;
        private readonly string? _resourceRoute;

        public EmailService(IConfiguration configuration)
        {
            var configuration1 = configuration;
            _apiUrl = configuration1["Email:ApiUrl"];
            _resourceRoute = configuration1["Email:ResourceRoute"];
        }

        public async Task SendBatch(List<Recipient> recipients)
        {
            foreach (Recipient recipient in recipients)
            {
                var sendEmailRequest = new SendEMailRequest
                (
                    //TODO:Mv to appsettings fromAddress
                    fromAddress: "pmartinez@nexura.com",
                    recipients: [recipient.EMail],
                    fromName: "SCEMail Integration Test",
                    subject: recipient.Subject,
                    bodyContent: GetEMailBody(recipient.Template, recipient.TemplateVars),
                    type: NotificationType.EMAIL,
                    certification: false
                );


                var client = new RestClient(_apiUrl!);
                var request = new RestRequest(_resourceRoute, Method.Post);
                request.AddJsonBody(sendEmailRequest);

                try
                {
                    var response = await client.PostAsync(request);

                    if (response.IsSuccessful)
                    {
                        if (response.Content != null)
                        {
                            var sendEmailResponse = JsonSerializer.Deserialize<SendEMailResponse>(response.Content);
                            Console.WriteLine("Request was successful!");
                            Console.WriteLine($"ResponseText: {sendEmailResponse!.ResponseText}");
                            Console.WriteLine($"StatusCode: {sendEmailResponse.StatusCode}");
                            foreach (var recipientEmail in sendEmailResponse.Recipients)
                            {
                                Console.WriteLine(recipientEmail);
                            }
                        }

                        Console.WriteLine($"Mails Sent {recipient.EMail} - Request was successful!");
                        Console.WriteLine("Response content: " + response.Content);
                    }
                    else
                    {
                        Console.WriteLine($"Request failed to {recipient.EMail} with status code: {response.StatusCode}");
                        Console.WriteLine("Error message: " + response.ErrorMessage);
                    }
                }
                catch (Exception ex)
                {
                    Log.Warning($"Mail Sent {recipient.EMail} ... {ex.Message}");
                }
            }
        }

        private static string GetEMailBody(string filePath, Hashtable templateData)
        {
            var parser = new Parser(filePath, templateData);
            return parser.Parse();
        }
    }
}
