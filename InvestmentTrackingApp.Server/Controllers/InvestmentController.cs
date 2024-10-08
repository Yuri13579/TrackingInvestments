﻿using InvestmentTracking.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentTrackingApp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestmentController(IInvestmentCalculator investmentCalculator) : ControllerBase
{
    private readonly IInvestmentCalculator _investmentCalculator = investmentCalculator;

    [HttpGet("remaining-shares")]
    public IActionResult GetRemainingShares(int sharesSold)
    {
        try
        {
            var remainingShares = _investmentCalculator.CalculateRemainingShares(sharesSold);
            return Ok(new { RemainingShares = remainingShares });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet("cost-basis-sold")]
    public IActionResult GetCostBasisOfSoldShares(int sharesSold, int accountingStrategyNumber)
    {
        try
        {
            var costBasis = _investmentCalculator.CalculateCostBasisOfSoldShares(accountingStrategyNumber, sharesSold);
            return Ok(new { CostBasisOfSoldShares = costBasis });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet("cost-basis-remaining")]
    public IActionResult GetCostBasisOfRemainingShares(int sharesSold, int accountingStrategyNumber)
    {
        try
        {
            var costBasis =
                _investmentCalculator.CalculateCostBasisOfRemainingShares(accountingStrategyNumber, sharesSold);
            return Ok(new { CostBasisOfRemainingShares = costBasis });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet("profit")]
    public IActionResult GetProfit(int sharesSold, decimal salePrice, int accountingStrategyNumber)
    {
        try
        {
            var profit = _investmentCalculator.CalculateProfit(accountingStrategyNumber, sharesSold, salePrice);
            return Ok(new { Profit = profit });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
}