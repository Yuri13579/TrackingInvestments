using InvestmentTracking.Business.Services;
using InvestmentTracking.Business.Services.Interfaces;
using InvestmentTracking.Data.Model;
using InvestmentTracking.Data.Repositories.Interfaces;
using Moq;
using FluentAssertions;
using InvestmentTracking.BusinessData.Strategies;

namespace InvestmentTracking.Tests
{
    public class InvestmentCalculatorTests
    {
        private readonly Mock<ICachingPurchaseLotRepository> _mockRepository;
        private readonly IInvestmentCalculator _investmentCalculator;

        public InvestmentCalculatorTests()
        {
            _mockRepository = new Mock<ICachingPurchaseLotRepository>();
            _investmentCalculator = new InvestmentCalculator(_mockRepository.Object);
        }

        [Fact]
        public void CalculateRemainingShares_ShouldReturnCorrectValue()
        {
            // Arrange
            var purchaseLots = new List<PurchaseLot>
            {
                new PurchaseLot(100, 20m, new DateTime(2024, 1, 1)),
                new PurchaseLot(150, 30m, new DateTime(2024, 2, 1)),
                new PurchaseLot(120, 10m, new DateTime(2024, 3, 1))
            };
            _mockRepository.Setup(repo => repo.GetPurchaseLots()).Returns(purchaseLots);

            // Act
            var result = _investmentCalculator.CalculateRemainingShares(100);

            // Assert
            result.Should().Be(270);
        }

        [Fact]
        public void CalculateCostBasisOfSoldShares_ShouldReturnCorrectValue()
        {
            // Arrange
            var purchaseLots = new List<PurchaseLot>
            {
                new PurchaseLot(100, 20m, new DateTime(2024, 1, 1)),
                new PurchaseLot(150, 30m, new DateTime(2024, 2, 1)),
                new PurchaseLot(120, 10m, new DateTime(2024, 3, 1))
            };
            _mockRepository.Setup(repo => repo.GetPurchaseLots()).Returns(purchaseLots);

            // Mocking FifoStrategy (if it is also a class with logic)
            var mockFifoStrategy = new Mock<FifoStrategy>();
            mockFifoStrategy.Setup(s => s.CalculateCostBasisOfSoldShares(purchaseLots, 100)).Returns(3500);

            // Act
            var result = _investmentCalculator.CalculateCostBasisOfSoldShares(150);

            // Assert
            result.Should().Be(3500);
        }

        [Fact]
        public void CalculateCostBasisOfRemainingShares_ShouldReturnCorrectValue()
        {
            // Arrange
            var purchaseLots = new List<PurchaseLot>
            {
                new PurchaseLot(100, 20m, new DateTime(2024, 1, 1)),
                new PurchaseLot(150, 30m, new DateTime(2024, 2, 1)),
                new PurchaseLot(120, 10m, new DateTime(2024, 3, 1))
            };
            _mockRepository.Setup(repo => repo.GetPurchaseLots()).Returns(purchaseLots);

            // Mocking FifoStrategy (if it is also a class with logic)
            var mockFifoStrategy = new Mock<FifoStrategy>();
            mockFifoStrategy.Setup(s => s.CalculateCostBasisOfRemainingShares(purchaseLots, 150)).Returns(2100);

            // Act
            var result = _investmentCalculator.CalculateCostBasisOfRemainingShares(150);

            // Assert
            result.Should().Be(2100);
        }

        [Fact]
        public void CalculateProfit_ShouldReturnCorrectValue()
        {
            // Arrange
            var purchaseLots = new List<PurchaseLot>
            {
                new PurchaseLot(100, 20m, new DateTime(2024, 1, 1)),
                new PurchaseLot(150, 30m, new DateTime(2024, 2, 1)),
                new PurchaseLot(120, 10m, new DateTime(2024, 3, 1))
            };
            _mockRepository.Setup(repo => repo.GetPurchaseLots()).Returns(purchaseLots);

            // Mocking FifoStrategy (if it is also a class with logic)
            var mockFifoStrategy = new Mock<FifoStrategy>();
            mockFifoStrategy.Setup(s => s.CalculateCostBasisOfSoldShares(purchaseLots, 150)).Returns(23.33m);

            // Act
            var result = _investmentCalculator.CalculateProfit(150, 40m);

            // Assert
            result.Should().Be(2500);
        }
    }
}
