using RestSharp;

namespace Core.REST;

public sealed class RequestBuilder
{
    private string resource = string.Empty;
    private Method method = Method.Get;
    private readonly Dictionary<string, string> Headers = [];
    private object? jsonBody;

    public RequestBuilder WithResource(string resource)
    {
        this.resource = resource;
        return this;
    }

    public RequestBuilder WithMethod(Method method)
    {
        this.method = method;
        return this;
    }

    public RequestBuilder WithHeader(string name, string value)
    {
        Headers.Add(name, value);
        return this;
    }

    public RequestBuilder WithJsonBody(object body)
    {
        jsonBody = body;
        return this;
    }

    public RestRequest Build()
    {
        if (string.IsNullOrEmpty(resource))
        {
            throw new InvalidOperationException("Resource must be set before building the request.");
        }

        var request = new RestRequest(resource, method);
        foreach (var (name, value) in Headers)
        {
            request.AddHeader(name, value);
        }

        if (jsonBody is not null)
        {
            request.AddJsonBody(jsonBody);
        }

        return request;
    }
}