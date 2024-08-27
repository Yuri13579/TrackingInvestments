using InvestmentTracking.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentTrackingApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestmentController : ControllerBase
{
    private readonly IInvestmentCalculator _investmentCalculator;

    public InvestmentController(IInvestmentCalculator investmentCalculator)
    {
        _investmentCalculator = investmentCalculator;
    }

    [HttpGet("remaining-shares")]
    public IActionResult GetRemainingShares(int sharesSold)
    {
        try
        {
            var remainingShares = _investmentCalculator.CalculateRemainingShares(sharesSold);
            return Ok(new { RemainingShares = remainingShares });
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("cost-basis-sold")]
    public IActionResult GetCostBasisOfSoldShares(int sharesSold)
    {
        try
        {
            var costBasis = _investmentCalculator.CalculateCostBasisOfSoldShares(sharesSold);
            return Ok(new { CostBasisOfSoldShares = costBasis });
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("cost-basis-remaining")]
    public IActionResult GetCostBasisOfRemainingShares(int sharesSold)
    {
        try
        {
            var costBasis = _investmentCalculator.CalculateCostBasisOfRemainingShares(sharesSold);
            return Ok(new { CostBasisOfRemainingShares = costBasis });
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("profit")]
    public IActionResult GetProfit(int sharesSold, decimal salePrice)
    {
        try
        {
            var profit = _investmentCalculator.CalculateProfit(sharesSold, salePrice);
            return Ok(new { Profit = profit });
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}