using Business.Models;
using Core;
using Core.REST;
using RestSharp;

namespace Business.Clients;

public sealed class JsonPlaceholderClient
    : ApiClientBase
{
    public JsonPlaceholderClient(ApiSettings settings) : base(settings) { }

    public RestResponse<List<User>> GetUsers()
    {
        var request = new RestRequest("/users", Method.Get)
            .AddHeader("Accept", "application/json");
        return Execute<List<User>>(request);
    }

    public RestResponse<User> CreateUser(string name, string username)
    {
        var request = new RestRequest("/users", Method.Post)
            .AddJsonBody(new { name, username });
        return Execute<User>(request);
    }

    public RestResponse GetInvalidEndpoint()
    {
        var request = new RestRequest("/invalidendpoint", Method.Get);
        return Execute(request);
    }
}