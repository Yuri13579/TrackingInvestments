using InvestmentTracking.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentTrackingApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestmentController(IInvestmentCalculator calculator) : ControllerBase
{
    [HttpGet("remaining-shares")]
    public IActionResult GetRemainingShares(int sharesSold)
    {
        var remainingShares = calculator.CalculateRemainingShares(sharesSold);
        return Ok(remainingShares);
    }

    [HttpGet("cost-basis-sold")]
    public IActionResult GetCostBasisOfSoldShares(int sharesSold)
    {
        var costBasis = calculator.CalculateCostBasisOfSoldShares(sharesSold);
        return Ok(costBasis);
    }

    [HttpGet("cost-basis-remaining")]
    public IActionResult GetCostBasisOfRemainingShares(int sharesSold)
    {
        var costBasis = calculator.CalculateCostBasisOfRemainingShares(sharesSold);
        return Ok(costBasis);
    }

    [HttpGet("profit")]
    public IActionResult GetProfit(int sharesSold, decimal salePrice)
    {
        var profit = calculator.CalculateProfit(sharesSold, salePrice);
        return Ok(profit);
    }
}