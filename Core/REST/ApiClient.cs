using System.Text.Json;
using System.Text.Json.Serialization;
using log4net;
using RestSharp;
using RestSharp.Serializers.Json;

namespace Core.REST;

public sealed class ApiClient
{
    private readonly RestClient Client;

    public ApiClient(ApiSettings settings)
    {
        var options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        Client = new RestClient(settings.BaseUrl, configureSerialization: s => s.UseSystemTextJson(options));
    }

    private static ILog Log => LogManager.GetLogger(typeof(ApiClient));

    public RestResponse<T> Execute<T>(RestRequest request)
        where T : notnull
    {
        Log.InfoFormat("Sending {0} request to {1}", request.Method, request.Resource);
        var response = Client.Execute<T>(request);
        Log.InfoFormat("Received {0} {1}", (int)response.StatusCode, response.StatusCode);
        return response;
    }
}