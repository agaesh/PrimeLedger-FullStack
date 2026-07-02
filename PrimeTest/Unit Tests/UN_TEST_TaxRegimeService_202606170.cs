using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Moq;
using PrimeAPI.Application.Interface;
using PrimeAPI.Application.Services;
using PrimeAPI.Domain;
using PrimeLedger.Shared.DTO.TaxRegime;
using PrimeLedger.Shared.Enums;
using PrimeTest.Common;
using System.Diagnostics;
using Xunit;
namespace PrimeTest.Unit_Tests
{
    public class UN_TEST_TaxRegimeService_202606170
    {
        private readonly Mock<ITaxRegimeRepository> _mockRepo;
        private readonly TaxRegimeService _service;
        private const string Version = "1.0.0-20260617";

        public UN_TEST_TaxRegimeService_202606170()
        {
            _mockRepo = new Mock<ITaxRegimeRepository>();
            _service = new TaxRegimeService(_mockRepo.Object);

            Console.WriteLine($"Running UN_TEST_TaxCodeHistoryService version {Version}");
        }


        [Fact]
        public async Task AddAsync_ShouldSucceed_WhenValidDataProvided()
        {
            var sw = Stopwatch.StartNew();

            try
            {
                var dto = new TaxRegimeCreateDTO
                {               
                    EffectiveFrom = DateTime.Now,
                    EffectiveTo = DateTime.Now.AddMonths(1),
                    IsActive = true
                };

                Console.WriteLine($"[ACTION] This Unit Test when valid data is provided. )");

                // Arrange: mock repository to accept any TaxCodeHistory entity
                _mockRepo.Setup(x => x.AddAsync(It.IsAny<TaxRegime>()))
          .ReturnsAsync((TaxRegime h) => h);

                Console.WriteLine("[ACTION] Act: Call AddAsync with the DTO.");

                // Act
                await _service.AddAsync(dto);

                Console.WriteLine("[ACTION] Assert: Verify repository was called once with a mapped entity.");
                // Assert: verify repository was called once with a mapped entity
                _mockRepo.Verify(x => x.AddAsync(It.Is<TaxRegime>(h =>
                    h.IsActive == dto.IsActive
                )), Times.Once);

                Console.WriteLine("[RESULT] Test passed.");
                TestLogger.Log(nameof(AddAsync_ShouldSucceed_WhenValidDataProvided),
                    "Unit", true, null, sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                TestLogger.Log(nameof(AddAsync_ShouldSucceed_WhenValidDataProvided),
                    "Unit", false, ex.Message, sw.ElapsedMilliseconds);
                throw;
            }
        }


        [Fact]
        public async Task AddAsync_ShouldThrow_WhenInputIsNull()
        {
            var sw = Stopwatch.StartNew();

            try
            {
                Console.WriteLine("[ACTION] This Unit Test when input is null. Expecting ArgumentNullException.");
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => _service.AddAsync(null));

                Console.WriteLine("[RESULT] Test passed.");
                TestLogger.Log(nameof(AddAsync_ShouldThrow_WhenInputIsNull),
                    "Unit", true, null, sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                TestLogger.Log(nameof(AddAsync_ShouldThrow_WhenInputIsNull),
                    "Unit", false, ex.Message, sw.ElapsedMilliseconds);
                throw;
            }
        }
        [Fact]
        public async Task AddAsync_ShouldThrow_WhenActiveRecordAlreadyExists()
        {
            var sw = Stopwatch.StartNew();

            try
            {
                var history = new TaxRegimeCreateDTO
                {

                    CodeType = TaxCodeType.GST,
                    EffectiveFrom = DateTime.Now,
                    EffectiveTo = DateTime.Now.AddMonths(1),
                    IsActive = true
                };

                Console.WriteLine("[ACTION] Simulate an existing active regime so service detects conflict.");

                // Simulate an existing active regime so service detects conflict
                var existing = new TaxRegime
                {
                    Id = 1,
                    CodeType = TaxCodeType.GST,
                    EffectiveFrom = DateTime.Now.AddDays(-1),
                    EffectiveTo = DateTime.Now.AddMonths(1),
                    IsActive = true
                };
                Console.WriteLine("[ACTION] Mock repository to return existing active regime.");
                _mockRepo.Setup(x => x.GetActiveRegime()).ReturnsAsync(existing);

                Console.WriteLine("[ACTION] Act: Call AddAsync with the DTO and expect InvalidOperationException.");
                await Assert.ThrowsAsync<InvalidOperationException>(
                    () => _service.AddAsync(history));

                Console.WriteLine("[RESULT] Test passed.");
                TestLogger.Log(nameof(AddAsync_ShouldThrow_WhenActiveRecordAlreadyExists),
                    "Unit", true, null, sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                TestLogger.Log(nameof(AddAsync_ShouldThrow_WhenActiveRecordAlreadyExists),
                    "Unit", false, ex.Message, sw.ElapsedMilliseconds);
                throw;
            }
        }
    

        [Fact]
        public async Task AddAsync_ShouldSucceed_WhenNoActiveRecordExists()
        {
            var sw = Stopwatch.StartNew();
            try
            {
                // Arrange: no active regime in the repository
                _mockRepo.Setup(x => x.GetActiveRegime())
                         .ReturnsAsync((TaxRegime)null);

                var history = new TaxRegimeCreateDTO
                {
                    CodeType = TaxCodeType.GST,
                    EffectiveFrom = DateTime.Now,
                    EffectiveTo = DateTime.Now.AddMonths(1),
                    IsActive = true
                };

                // Mock AddAsync to return the entity that was passed in
                _mockRepo.Setup(x => x.AddAsync(It.IsAny<TaxRegime>()))
                         .ReturnsAsync((TaxRegime r) => r);

                // Act
                var result = await _service.AddAsync(history);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(TaxCodeType.GST, result.CodeType);

                // Verify AddAsync was called once
                _mockRepo.Verify(x => x.AddAsync(It.IsAny<TaxRegime>()), Times.Once);
            }
            catch (Exception ex)
            {
                
                TestLogger.Log(nameof(AddAsync_ShouldSucceed_WhenNoActiveRecordExists),
                    "Unit", false, ex.Message, sw.ElapsedMilliseconds);
                throw;
            }
         
        }

    }
}

