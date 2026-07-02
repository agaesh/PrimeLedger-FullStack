using Microsoft.Extensions.DependencyInjection;
using Moq;
using PrimeAPI.Application.Interface;
using PrimeAPI.Application.Services;
using PrimeAPI.Domain;
using PrimeAPI.Infrasfructure;
using PrimeLedger.Shared.DTO.TaxRegime;
using PrimeLedger.Shared.Enums;
using PrimeTest.Common;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace PrimeAPI.Tests.Integration
{
    public class TaxCodeHistoryControllerIntegrationTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TaxCodeHistoryControllerIntegrationTests(CustomWebApplicationFactory factory)
        {
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
                Console.WriteLine("[ACTION] Sending GET request to /PrimeApi/tax-regime/");

                var response = await _client.GetAsync("/PrimeApi/tax-regime/");

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
        public async Task Create_Then_GetById_Works()
        {
            var sw = Stopwatch.StartNew();
            var testName = nameof(Create_Then_GetById_Works);

            try
            {
                Console.WriteLine($"\n=== Starting Test: {testName} ===");

                var history = new TaxRegimeCreateDTO
                {
                    CodeType = TaxCodeType.GST,
                    EffectiveFrom = DateTime.UtcNow,
                    EffectiveTo = DateTime.UtcNow.AddMonths(1),
                    IsActive = false
                };

                Console.WriteLine($"[ACTION] Creating tax regime: CodeType={history.CodeType}, IsActive={history.IsActive}");

                // Create
                var createResponse = await _client.PostAsJsonAsync("/PrimeApi/tax-regime", history);
                Console.WriteLine($"[RESULT] Create Response Status: {createResponse.StatusCode}");
                Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

                var created = await createResponse.Content.ReadFromJsonAsync<TaxRegime>();
                Assert.NotNull(created);
                Console.WriteLine($"[RESULT] Tax regime created with Id: {created.Id}, CodeType: {created.CodeType}");
                Assert.Equal("GST", created.CodeType.ToString());

                // Get by Id
                Console.WriteLine($"[ACTION] Fetching tax regime by Id: {created.Id}");
                var getResponse = await _client.GetAsync($"/PrimeApi/tax-regime/{created.Id}");
                Console.WriteLine($"[RESULT] Get Response Status: {getResponse.StatusCode}");
                Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

                var fetched = await getResponse.Content.ReadFromJsonAsync<TaxRegime>();
                Assert.NotNull(fetched);
                Console.WriteLine($"[RESULT] Tax regime fetched successfully - Id: {fetched.Id}");
                Assert.Equal(created.Id, fetched.Id);

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
        public async Task CreateActiveGST_ThenCreateAnotherActiveGST_ShouldThrowInvalidOperation()
        {
            var sw = Stopwatch.StartNew();
            var testName = nameof(CreateActiveGST_ThenCreateAnotherActiveGST_ShouldThrowInvalidOperation);

            try
            {
                Console.WriteLine($"\n=== Starting Test: {testName} ===");

                // Arrange: mocked repository with stateful behavior to simulate persistence
                var mockRepo = new Mock<ITaxRegimeRepository>();
                TaxRegime? activeRegime = null;

                Console.WriteLine("[SETUP] Initializing mock repository");

                // Setup GetActiveRegime to return the currently active regime
                mockRepo.Setup(r => r.GetActiveRegime()).ReturnsAsync(() =>
                {
                    Console.WriteLine($"[MOCK] GetActiveRegime called - Current active regime: {(activeRegime == null ? "NULL" : $"Id={activeRegime.Id}, IsActive={activeRegime.IsActive}, EffectiveFrom={activeRegime.EffectiveFrom}, EffectiveTo={activeRegime.EffectiveTo}")}");
                    return activeRegime;
                });

                // Setup AddAsync to capture the added regime as active if IsActive is true
                mockRepo.Setup(r => r.AddAsync(It.IsAny<TaxRegime>()))
                    .Returns<TaxRegime>(async regime =>
                    {
                        Console.WriteLine($"[MOCK] AddAsync called - regime: CodeType={regime.CodeType}, IsActive={regime.IsActive}, EffectiveFrom={regime.EffectiveFrom}, EffectiveTo={regime.EffectiveTo}");

                        if (regime.IsActive)
                        {
                            regime.Id = 1; // Simulate DB assigning an ID
                            activeRegime = regime;
                            Console.WriteLine($"[MOCK] Regime is active - Setting as active regime in state");
                        }
                        return regime;
                    });

                var service = new TaxRegimeService(mockRepo.Object);
                Console.WriteLine("[SETUP] TaxRegimeService created");

                // First active GST
                var gst1 = new TaxRegimeCreateDTO
                {
                    CodeType = TaxCodeType.GST,
                    EffectiveFrom = DateTime.UtcNow,
                    EffectiveTo = DateTime.UtcNow.AddMonths(1),
                    IsActive = true,
                };

                Console.WriteLine($"[ACTION] Adding first active GST: EffectiveFrom={gst1.EffectiveFrom}, EffectiveTo={gst1.EffectiveTo}, IsActive={gst1.IsActive}");
                await service.AddAsync(gst1);
                Console.WriteLine($"[RESULT] First GST added successfully");

                // Second active GST overlapping
                var gst2 = new TaxRegimeCreateDTO
                {
                    CodeType = TaxCodeType.GST,
                    EffectiveFrom = DateTime.UtcNow.AddDays(5),
                    EffectiveTo = DateTime.UtcNow.AddMonths(2),
                    IsActive = true,
                };

                Console.WriteLine($"[ACTION] Attempting to add second active GST: EffectiveFrom={gst2.EffectiveFrom}, EffectiveTo={gst2.EffectiveTo}, IsActive={gst2.IsActive}");

                // Assert: service throws InvalidOperationException
                var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddAsync(gst2));
                Console.WriteLine($"[RESULT] Expected exception thrown: {exception.Message}");
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
        public async Task CreateInactiveGSTWithClosedRange_ThenCreateActiveGSTOverlapping_ShouldSucceed()
        {
            var sw = Stopwatch.StartNew();
            var testName = nameof(CreateInactiveGSTWithClosedRange_ThenCreateActiveGSTOverlapping_ShouldSucceed);

            try
            {
                Console.WriteLine($"\n=== Starting Test: {testName} ===");

                var gstInactive = new TaxRegimeCreateDTO
                {
                    CodeType = TaxCodeType.GST,
                    EffectiveFrom = DateTime.UtcNow.AddMonths(-2),
                    EffectiveTo = DateTime.UtcNow.AddMonths(-1),
                    IsActive = false,
                };

                Console.WriteLine($"[ACTION] Creating inactive GST: EffectiveFrom={gstInactive.EffectiveFrom}, EffectiveTo={gstInactive.EffectiveTo}, IsActive={gstInactive.IsActive}");
                var createResponse1 = await _client.PostAsJsonAsync("/PrimeApi/tax-regime", gstInactive);
                Console.WriteLine($"[RESULT] Inactive GST creation response: {createResponse1.StatusCode}");
                Assert.Equal(HttpStatusCode.Created, createResponse1.StatusCode);

                var gstActive = new TaxRegimeCreateDTO
                {
                    CodeType = TaxCodeType.GST,
                    EffectiveFrom = DateTime.UtcNow.AddMonths(-2),
                    EffectiveTo = DateTime.UtcNow.AddMonths(1),
                    IsActive = true,
                };

                Console.WriteLine($"[ACTION] Creating active GST (overlapping): EffectiveFrom={gstActive.EffectiveFrom}, EffectiveTo={gstActive.EffectiveTo}, IsActive={gstActive.IsActive}");
                var createResponse2 = await _client.PostAsJsonAsync("/PrimeApi/tax-regime", gstActive);
                Console.WriteLine($"[RESULT] Active GST creation response: {createResponse2.StatusCode} (should succeed)");
                Assert.Equal(HttpStatusCode.Created, createResponse2.StatusCode); // should succeed

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
        public async Task GetById_ReturnsNotFound_WhenInvalidId()
        {
            var sw = Stopwatch.StartNew();
            var testName = nameof(GetById_ReturnsNotFound_WhenInvalidId);

            try
            {
                Console.WriteLine($"\n=== Starting Test: {testName} ===");
                Console.WriteLine("[ACTION] Sending GET request with invalid Id: 9999");

                var response = await _client.GetAsync("/PrimeApi/tax-regime/9999");

                Console.WriteLine($"[RESULT] Response Status Code: {response.StatusCode}");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

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
        public async Task Update_ReturnsNoContent_WhenValid()
        {
            var sw = Stopwatch.StartNew();
            var testName = nameof(Update_ReturnsNoContent_WhenValid);

            try
            {
                Console.WriteLine($"\n=== Starting Test: {testName} ===");

                var history = new TaxRegime
                {
                    CodeType =  TaxCodeType.GST,    
                    EffectiveFrom = DateTime.UtcNow,
                    EffectiveTo = DateTime.UtcNow.AddMonths(1),
                    IsActive = true
                };


                Console.WriteLine($"[ACTION] Updating tax regime Id 1: IsActive=1");

                var updateResponse = await _client.PutAsJsonAsync($"/PrimeApi/tax-regime/{created.Id}", created);
                Console.WriteLine($"[RESULT] Update Response Status: {updateResponse.StatusCode}");
                Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

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
        public async Task Delete_ReturnsNoContent_WhenValid()
        {
            var sw = Stopwatch.StartNew();
            var testName = nameof(Delete_ReturnsNoContent_WhenValid);

            try
            {
                Console.WriteLine($"\n=== Starting Test: {testName} ===");

                var history = new TaxRegime
                {
                    CodeType = TaxCodeType.GST,
                    EffectiveFrom = DateTime.UtcNow,
                    EffectiveTo = DateTime.UtcNow.AddMonths(1),
                    IsActive = true,
                };

                // Create
                Console.WriteLine($"[ACTION] Creating tax regime: CodeType={history.CodeType}, IsActive={history.IsActive}");
                var createResponse = await _client.PostAsJsonAsync("/PrimeApi/tax-regime", history);
                var created = await createResponse.Content.ReadFromJsonAsync<TaxRegime>();
                Console.WriteLine($"[RESULT] Tax regime created with Id: {created.Id}");

                // First delete → should succeed
                Console.WriteLine($"[ACTION] Deleting tax regime with Id: {created.Id}");
                var deleteResponse = await _client.DeleteAsync($"/PrimeApi/tax-regime/{created.Id}");
                Console.WriteLine($"[RESULT] First delete response: {deleteResponse.StatusCode}");
                Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

                // Second delete → should return NotFound
                Console.WriteLine($"[ACTION] Attempting to delete the same regime again (should return NotFound)");
                var deleteAgainResponse = await _client.DeleteAsync($"/PrimeApi/tax-regime/{created.Id}");
                Console.WriteLine($"[RESULT] Second delete response: {deleteAgainResponse.StatusCode}");
                Assert.Equal(HttpStatusCode.NotFound, deleteAgainResponse.StatusCode);

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
