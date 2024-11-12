using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Domain.Dto;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Helpers;
using MVCT.Sisfv.Tansversales.Notificaciones.Worker.Infrastructure.Configurations;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System.Net;

namespace MVCT.Sisfv.Tansversales.Notificaciones.Worker.Services.Donacion
{
    internal class DonacionApiClient
    {
        private string _baseUrl { get; set; }
        private Template _template { get; set; }

        public DonacionApiClient(string baseUrl, Template template)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
            (sender, certificate, chain, sslPolicyErrors) => true;
            _baseUrl = baseUrl;
            _template = template;
        }

        public async Task<List<Recipient>> ObtenerSolicitudesDonacion()
        {

            var recipients = new List<Recipient>();

            try
            {

                RestClientOptions options = new()
                {
                    BaseUrl = new Uri(_baseUrl),
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                };

                RestClient client = new(options);
                var request = new RestRequest();

                var response = await client.GetAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseBody = JsonConvert.DeserializeObject<Response<List<RecipientRequest>>>(response.Content!);
                    Console.WriteLine(responseBody);

                    foreach (var r in responseBody.Data)
                    {
                        recipients.Add(new Recipient
                        {
                            EMail = r.EMail,
                            Template = GetTemplatesPath(_template),
                            TemplateVars = r.TemplateVars,
                            Subject = _template.Subject
                        });
                    }
                }
                else
                {
                    Log.Warning("ObtenerSolicitudesDonacion Non-success status code received: {StatusCode}",
                        response.StatusCode);
                    throw new Exception(
                        $"Failed to request: {_baseUrl} Non-success status code received: {{ response.StatusCode}}");
                }
            }
            catch (HttpRequestException ex)
            {
                Log.Error(ex, $"Failed to request: {_baseUrl}");
                throw new Exception($"Failed to request: {_baseUrl} - {ex.Message}");
            }
            catch (JsonException ex)
            {
                Log.Error(ex, "Failed to deserialize response");
                throw new Exception($"ObtenerSolicitudesDonacion to deserialize response - {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred");
                throw new Exception($"ObtenerSolicitudesDonacion An error occurred - {ex.Message}");
            }

            return recipients;
            ;
        }
        private string GetTemplatesPath(Template template)
        {
            return $"{Helpers.OperatingSystem.GetTemplatesPath()}{template.TemplateHtml}";
        }
    }

}
