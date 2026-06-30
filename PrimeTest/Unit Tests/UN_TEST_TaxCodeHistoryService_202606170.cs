using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Moq;
using PrimeAPI.Application.Interface;
using PrimeAPI.Application.Services;
using PrimeAPI.Domain;
using System.Diagnostics;
using Xunit;
using PrimeTest.Common;
using PrimeLedger.Shared.DTO.TaxRegime;
using PrimeLedger.Shared.Enums;
namespace PrimeTest.Unit_Tests
{
    public class UN_TEST_TaxCodeHistoryService_202606170
    {
        private readonly Mock<ITaxRegimeRepository> _mockRepo;
        private readonly TaxRegimeService _service;
        private const string Version = "1.0.0-20260617";

        public UN_TEST_TaxCodeHistoryService_202606170()
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

                // Arrange: mock repository to accept any TaxCodeHistory entity
                _mockRepo.Setup(x => x.AddAsync(It.IsAny<TaxRegime>()))
          .ReturnsAsync((TaxRegime h) => h);


                // Act
                await _service.AddAsync(dto);

                // Assert: verify repository was called once with a mapped entity
                _mockRepo.Verify(x => x.AddAsync(It.Is<TaxRegime>(h =>
                    h.IsActive == dto.IsActive
                )), Times.Once);

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
                await Assert.ThrowsAsync<ArgumentNullException>(
                    () => _service.AddAsync(null));

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

                // Simulate an existing active regime so service detects conflict
                var existing = new TaxRegime
                {
                    Id = 1,
                    CodeType = TaxCodeType.GST,
                    EffectiveFrom = DateTime.Now.AddDays(-1),
                    EffectiveTo = DateTime.Now.AddMonths(1),
                    IsActive = true
                };
                _mockRepo.Setup(x => x.GetActiveRegime()).ReturnsAsync(existing);

                await Assert.ThrowsAsync<InvalidOperationException>(
                    () => _service.AddAsync(history));

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
    }
}

