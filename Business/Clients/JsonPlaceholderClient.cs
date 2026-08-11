using Business.Models;
using Core;
using Core.REST;
using RestSharp;

namespace Business.Clients;

public interface IJsonPlaceholderClient
{
    RestResponse<List<User>> GetUsers();
    RestResponse<User> CreateUser(string name, string username);
    RestResponse GetInvalidEndpoint();
}

public sealed class JsonPlaceholderClient
    : ApiClientBase, IJsonPlaceholderClient
{
    private const string UsersEndpoint = "/users";
    private const string InvalidEndpoint = "/invalidendpoint";

    public JsonPlaceholderClient(ApiSettings settings)
        : base(settings) { }

    public RestResponse<List<User>> GetUsers()
    {
        var request = new RequestBuilder()
            .WithResource(UsersEndpoint)
            .WithMethod(Method.Get)
            .WithHeader("Accept", "application/json")
            .Build();
        return Execute<List<User>>(request);
    }

    public RestResponse<User> CreateUser(string name, string username)
    {
        var request = new RequestBuilder()
            .WithResource(UsersEndpoint)
            .WithMethod(Method.Post)
            .WithJsonBody(new { name, username })
            .Build();
        return Execute<User>(request);
    }

    public RestResponse GetInvalidEndpoint()
    {
        var request = new RequestBuilder()
            .WithResource(InvalidEndpoint)
            .WithMethod(Method.Get)
            .Build();
        return Execute(request);
    }
}