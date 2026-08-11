using System.Net;
using Business.Models;
using log4net;
using RestSharp;

namespace Tests;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.All)]
[Category("API")]
public sealed class ApiTests
    : ApiTestBase
{
    private static ILog Log => LogManager.GetLogger(typeof(ApiTests));

    [Test]
    public void GetUsers_ValidRequest_ReturnsUsersWithRequiredFields()
    {
        Log.Info("Building GET request for /users");
        var request = new RestRequest("/users", Method.Get)
            .AddHeader("Accept", "application/json");

        Log.Info("Sending request to retrieve list of users");
        var response = Client.Execute<List<User>>(request);

        Log.Info("Validating response status code");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.ErrorMessage, Is.Null.Or.Empty);

        Log.Info("Validating each user contains required fields");
        var users = response.Data;
        Assert.That(users, Is.Not.Null.And.Not.Empty);

        Assert.Multiple(() =>
        {
            foreach (var user in users)
            {
                Assert.That(user.Id, Is.GreaterThan(0), "Id should be present");
                Assert.That(user.Name, Is.Not.Null.And.Not.Empty, "Name should be present");
                Assert.That(user.Username, Is.Not.Null.And.Not.Empty, "Username should be present");
                Assert.That(user.Email, Is.Not.Null.And.Not.Empty, "Email should be present");
                Assert.That(user.Address, Is.Not.Null, "Address should be present");
                Assert.That(user.Phone, Is.Not.Null.And.Not.Empty, "Phone should be present");
                Assert.That(user.Website, Is.Not.Null.And.Not.Empty, "Website should be present");
                Assert.That(user.Company, Is.Not.Null, "Company should be present");
                Assert.That(user.Company.Name, Is.Not.Null.And.Not.Empty, "Company name should be present");
            }
        });
    }

    [Test]
    public void GetUsers_ValidRequest_HeaderIsJsonUtf8()
    {
        Assert.Fail();
    }

    [Test]
    public void GetUsers_ValidRequest_ReturnsUsersWithValidFields()
    {
        Assert.Fail();
    }

    [Test]
    public void PostUser_ValidRequestWithNameAndUsername_ReturnsNotEmptyWithId()
    {
        Assert.Fail();
    }

    [Test]
    public void GetInvalidEndpoint_InvalidRequest_ReturnsNotFoundCode()
    {
        Assert.Fail();
    }
}