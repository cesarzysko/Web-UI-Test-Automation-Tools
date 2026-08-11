using System.Net;
using Business.Models;

namespace Tests;

[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[Parallelizable(ParallelScope.All)]
[Category("API")]
public sealed class ApiTests
    : ApiTestBase
{
    [Test]
    public void GetUsers_ValidRequest_ReturnsUsersWithRequiredFields()
    {
        Log.Info("Sending request to retrieve list of users");
        var response = Client.GetUsers();

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
            }
        });
    }

    [Test]
    public void GetUsers_ValidRequest_HeaderIsJsonUtf8()
    {
        Log.Info("Sending request to retrieve list of users");
        var response = Client.GetUsers();

        Log.Info("Validating response status code");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.ErrorMessage, Is.Null.Or.Empty);

        Log.Info("Validating content-type header");
        var contentTypeHeader = response.ContentHeaders?
            .FirstOrDefault(h => h.Name.Equals("Content-Type", StringComparison.OrdinalIgnoreCase));
        Assert.Multiple(() =>
        {
            Assert.That(contentTypeHeader, Is.Not.Null, "Content-Type header should be present");
            Assert.That(contentTypeHeader?.Value, Is.EqualTo("application/json; charset=utf-8"));
        });
    }

    [Test]
    public void GetUsers_ValidRequest_ReturnsUsersWithValidFields()
    {
        Log.Info("Sending request to retrieve list of users");
        var response = Client.GetUsers();

        Log.Info("Validating response status code");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.ErrorMessage, Is.Null.Or.Empty);

        Log.Info("Validating user list contains 10 unique, valid users");
        var users = response.Data;
        Assert.That(users, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(users, Has.Count.EqualTo(10),
                "Response should contain exactly 10 users");
            Assert.That(users.Select(u => u.Id), Is.Unique,
                "Each user should have a different Id");
            Assert.That(users, Has.All.Matches<User>(u => !string.IsNullOrEmpty(u.Name)),
                "Each user should have a non-empty Name");
            Assert.That(users, Has.All.Matches<User>(u => !string.IsNullOrEmpty(u.Username)),
                "Each user should have a non-empty Username");
            Assert.That(users, Has.All.Matches<User>(u => !string.IsNullOrEmpty(u.Company.Name)),
                "Each user should have a non-empty Company Name");
        });
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