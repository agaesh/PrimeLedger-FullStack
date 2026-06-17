using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;
using PrimeLedger.Shared.DTO.GLAccounts;
using PrimeLedger.Shared.Enums;

public class GLAccountControllerIntegrationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public GLAccountControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Create_Then_GetById_Works()
    {
        // Arrange: create DTO
        var dto = new GlAccountCreateDTO
        {
            AccountCode = "1000",
            AccountName = "Cash",
            AccountType = AccountType.ASSET
        };

        // Act: POST new account
        var postResponse = await _client.PostAsJsonAsync("/PrimeApi/gl-accounts", dto);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        // Location header contains the created resource id (CreatedAtAction)
        var location = postResponse.Headers.Location?.ToString();
        Assert.False(string.IsNullOrEmpty(location));

        // Act: GET by returned id
        var getResponse = await _client.GetAsync(location);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        options.Converters.Add(new JsonStringEnumConverter());
        var account = await getResponse.Content.ReadFromJsonAsync<GlAccountDTO>(options: options);
        Assert.NotNull(account);
        Assert.Equal("Cash", account.AccountName);
    }

    [Fact]
    public async Task GetAll_ReturnsAccounts()
    {
        // Seed one account
        var dto = new GlAccountCreateDTO
        {
            AccountCode = "2000",
            AccountName = "Revenue",
            AccountType = AccountType.REVENUE
        };
        await _client.PostAsJsonAsync("/PrimeApi/gl-accounts", dto);

        // Act
        var response = await _client.GetAsync("/PrimeApi/gl-accounts?pageNumber=1&pageSize=10");
        response.EnsureSuccessStatusCode();

        var options2 = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        options2.Converters.Add(new JsonStringEnumConverter());
        var accounts = await response.Content.ReadFromJsonAsync<List<GlAccountDTO>>(options: options2);
        Assert.NotEmpty(accounts);
    }

    [Fact]
    public async Task Delete_RemovesAccount()
    {
        var dto = new GlAccountCreateDTO
        {
            AccountCode = "3000",
            AccountName = "Expense",
            AccountType = AccountType.EXPENSE
        };
        var postResponse = await _client.PostAsJsonAsync("/PrimeApi/gl-accounts", dto);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
        var location = postResponse.Headers.Location?.ToString();
        Assert.False(string.IsNullOrEmpty(location));

        // Act: delete by returned id
        var deleteResponse = await _client.DeleteAsync(location);
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Verify not found
        var getResponse = await _client.GetAsync(location);
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}
