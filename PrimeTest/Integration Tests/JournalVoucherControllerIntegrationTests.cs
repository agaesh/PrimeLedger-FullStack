using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using PrimeAPI.Domain;

public class JournalVoucherControllerIntegrationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public JournalVoucherControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Create_ThenGetById_ReturnsVoucherWithLines()
    {
        var entry = CreateBalancedEntry("JV-CREATE");

        var createResponse = await _client.PostAsJsonAsync("/PrimeApi/journal-vouchers", entry);

        Assert.True(
            createResponse.StatusCode == HttpStatusCode.Created,
            await createResponse.Content.ReadAsStringAsync());
        Assert.NotNull(createResponse.Headers.Location);

        var getResponse = await _client.GetAsync(createResponse.Headers.Location);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var createdEntry = await getResponse.Content.ReadFromJsonAsync<JournalEntry>(_jsonOptions);
        Assert.NotNull(createdEntry);
        Assert.Equal("JV-CREATE", createdEntry.EntryNumber);
        Assert.Equal(2, createdEntry.Lines.Count);
        Assert.Equal(125m, createdEntry.Lines.Sum(line => line.Debit));
        Assert.Equal(125m, createdEntry.Lines.Sum(line => line.Credit));
    }

    [Fact]
    public async Task Update_ReplacesExistingLinesAndUpdatesHeader()
    {
        var createResponse = await _client.PostAsJsonAsync(
            "/PrimeApi/journal-vouchers",
            CreateBalancedEntry("JV-UPDATE"));
        Assert.True(
            createResponse.StatusCode == HttpStatusCode.Created,
            await createResponse.Content.ReadAsStringAsync());

        var createdEntry = await createResponse.Content.ReadFromJsonAsync<JournalEntry>(_jsonOptions);
        Assert.NotNull(createdEntry);

        var updatedEntry = new JournalEntry
        {
            EntryNumber = "JV-UPDATED",
            Date = new DateTime(2026, 11, 25),
            Description = "Updated voucher",
            Lines =
            [
                new JournalEntryLine { AccountId = 1, Debit = 200m },
                new JournalEntryLine { AccountId = 2, Credit = 125m },
                new JournalEntryLine { AccountId = 2, Credit = 75m }
            ]
        };

        var updateResponse = await _client.PutAsJsonAsync(
            $"/PrimeApi/journal-vouchers/{createdEntry.Id}",
            updatedEntry);

        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var getResponse = await _client.GetAsync($"/PrimeApi/journal-vouchers/{createdEntry.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var result = await getResponse.Content.ReadFromJsonAsync<JournalEntry>(_jsonOptions);
        Assert.NotNull(result);
        Assert.Equal("JV-UPDATED", result.EntryNumber);
        Assert.Equal("Updated voucher", result.Description);
        Assert.Equal(3, result.Lines.Count);
        Assert.Equal(200m, result.Lines.Sum(line => line.Debit));
        Assert.Equal(200m, result.Lines.Sum(line => line.Credit));
    }

    [Fact]
    public async Task Create_WithUnbalancedLines_ReturnsBadRequest()
    {
        var entry = CreateBalancedEntry("JV-UNBALANCED");
        entry.Lines.Last().Credit = 100m;

        var response = await _client.PostAsJsonAsync("/PrimeApi/journal-vouchers", entry);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Delete_ThenGetById_ReturnsNotFound()
    {
        var createResponse = await _client.PostAsJsonAsync(
            "/PrimeApi/journal-vouchers",
            CreateBalancedEntry("JV-DELETE"));
        Assert.True(
            createResponse.StatusCode == HttpStatusCode.Created,
            await createResponse.Content.ReadAsStringAsync());

        var createdEntry = await createResponse.Content.ReadFromJsonAsync<JournalEntry>(_jsonOptions);
        Assert.NotNull(createdEntry);

        var deleteResponse = await _client.DeleteAsync(
            $"/PrimeApi/journal-vouchers/{createdEntry.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var getResponse = await _client.GetAsync(
            $"/PrimeApi/journal-vouchers/{createdEntry.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    private static JournalEntry CreateBalancedEntry(string entryNumber)
    {
        return new JournalEntry
        {
            EntryNumber = entryNumber,
            Date = new DateTime(2026, 11, 25),
            Description = "Integration test voucher",
            Lines =
            [
                new JournalEntryLine { AccountId = 1, Debit = 125m },
                new JournalEntryLine { AccountId = 2, Credit = 125m }
            ]
        };
    }
}
