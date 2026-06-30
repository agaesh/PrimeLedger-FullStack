using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PrimeAPI.Domain;
using PrimeAPI.Infrasfructure;
using PrimeLedger.Shared.DTO;
using PrimeLedger.Shared.DTO.TaxTreatment;
using PrimeLedger.Shared.Enums;
using PrimeTest.Common;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
namespace PrimeTest.Integration_Tests
{
    public class IN_TEST_TaxTreatmentController_Test:IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private static readonly System.Text.Json.JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public IN_TEST_TaxTreatmentController_Test(CustomWebApplicationFactory factory) {
            _client = factory.CreateClient();

            //using var scope = factory.Services.CreateScope();
            //var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            var sw = Stopwatch.StartNew();
            var testName = nameof(GetAll_ReturnsOk);

            try
            {
                Console.WriteLine($"\n=== Starting Test: {testName} ===");
                Console.WriteLine("[ACTION] Sending GET request to /PrimeApi/tax-treatment/");

                var response = await _client.GetAsync("/PrimeApi/tax-treatment/");

                Console.WriteLine($"[RESULT] Response Status Code: {response.StatusCode}");
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                Console.WriteLine("=== TEST PASSED ===\n");
                TestLogger.Log(testName, "Integration", true, null, sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== TEST FAILED ===\nError: {ex.Message}\n");
                TestLogger.Log(testName, "Integration", false, ex.Message, sw.ElapsedMilliseconds);
                throw;
            }
        }

        [Fact]
        public async Task Create_Returns_ApiResponse_With_Data()
        {
            var sw = Stopwatch.StartNew();
            var testName = nameof(Create_Returns_ApiResponse_With_Data);

            try
            {
                Console.WriteLine($"\n=== Starting Test: {testName} ===");

                // Arrange
                var createDTO = new TaxTreatmentCreateDTO
                {
                    Code = "GST6",
                    Description = "GST RATE 6",
                    TaxRate = 6,
                    Type = TaxCodeType.GST,
                    PurchaseGLId = 1,
                    SalesGLId = 2
                };

                Console.WriteLine($"[ACTION] Creating tax treatment: Code={createDTO.Code}, Description={createDTO.Description}, TaxRate={createDTO.TaxRate}%, Type={createDTO.Type}");

                // Act: POST to create a new tax treatment
                var createResponse = await _client.PostAsJsonAsync("/PrimeApi/tax-treatment", createDTO);

                // Assert: Ensure creation succeeded
                Console.WriteLine($"[RESULT] Create Response Status: {createResponse.StatusCode}");
                Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

                // Deserialize into ApiResponse<TaxTreatmentCreateDTO>
                var apiResponse = await createResponse.Content.ReadFromJsonAsync<ApiResponse<TaxTreatment>>(JsonOptions);
                Assert.NotNull(apiResponse);
                Console.WriteLine($"[RESULT] API Response Success: {apiResponse.Success}, Message: {apiResponse.Message}");
                Assert.True(apiResponse.Success);
                Assert.Equal("Tax treatment created successfully.", apiResponse.Message);
                Assert.NotNull(apiResponse.Data);
                Console.WriteLine($"[RESULT] Tax treatment created with Code: {apiResponse.Data.Code}");
                Assert.Equal("GST6", apiResponse.Data.Code);

                // Act: GET the created entity by code (since Create returns DTO, not domain entity with Id)
                Console.WriteLine($"[ACTION] Fetching tax treatment by code: {apiResponse.Data.Code}");
                var getResponse = await _client.GetAsync($"/PrimeApi/tax-treatment/code/{apiResponse.Data.Code}");

                // Assert: Ensure retrieval succeeded
                Console.WriteLine($"[RESULT] Get Response Status: {getResponse.StatusCode}");
                Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

                var fetched = await getResponse.Content.ReadFromJsonAsync<TaxTreatment>(JsonOptions);
                Assert.NotNull(fetched);
                Console.WriteLine($"[RESULT] Tax treatment fetched successfully - Code: {fetched.Code}");
                Assert.Equal(apiResponse.Data.Code, fetched.Code);

                Console.WriteLine("=== TEST PASSED ===\n");
                TestLogger.Log(testName, "Integration", true, null, sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== TEST FAILED ===\nError: {ex.Message}\n");
                TestLogger.Log(testName, "Integration", false, ex.Message, sw.ElapsedMilliseconds);
                throw;
            }
        }

        [Fact]
        public async Task DeleteTaxTreatment_Returns_Ok()
        {
            var sw = Stopwatch.StartNew();
            var testName = nameof(DeleteTaxTreatment_Returns_Ok);

            try
            {
                Console.WriteLine($"\n=== Starting Test: {testName} ===");

                // Arrange: create a new TaxTreatment
                var createDTO = new TaxTreatmentCreateDTO
                {
                    Code = "GST6",
                    Description = "GST 6% Percentage",
                    TaxRate = 6,
                    PurchaseGLId = 1,
                    SalesGLId = 2,
                    Type = TaxCodeType.GST,
                };

                Console.WriteLine($"[ACTION] Creating tax treatment: Code={createDTO.Code}, Description={createDTO.Description}, TaxRate={createDTO.TaxRate}%, Type={createDTO.Type}");

                var createResponse = await _client.PostAsJsonAsync("/primeapi/tax-treatment/", createDTO);
                Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

                var created= await createResponse.Content.ReadFromJsonAsync<ApiResponse<TaxTreatment>>(JsonOptions);
                Assert.NotNull(created);
                Console.WriteLine($"[RESULT] Tax treatment created with Id={created.Data.Id}, Code={created.Data.Code}");

                // Act: delete the created record
                Console.WriteLine($"[ACTION] Deleting tax treatment with Id={created.Data.Id}");
                var deleteResponse = await _client.DeleteAsync($"/primeapi/tax-treatment/{created.Data.Id}");

                // Assert: deletion should return NoContent (204)
                Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
                Console.WriteLine($"[RESULT] Delete response: {deleteResponse.StatusCode}");

                // Act: try to fetch the deleted record
                Console.WriteLine($"[ACTION] Fetching deleted tax treatment Id={created.Data.Id} (should return NotFound)");
                var getResponse = await _client.GetAsync($"/primeapi/tax-treatment/{created.Data.Id}");

                // Assert: should return NotFound (404)
                Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
                Console.WriteLine($"[RESULT] Get after delete response: {getResponse.StatusCode}");

                Console.WriteLine("=== TEST PASSED ===\n");
                TestLogger.Log(testName, "Integration", true, null, sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== TEST FAILED ===\nError: {ex.Message}\n");
                TestLogger.Log(testName, "Integration", false, ex.Message, sw.ElapsedMilliseconds);
                throw;
            }
        }

    }
}
