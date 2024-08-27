import React, { useState } from 'react';
import { getRemainingShares, getCostBasisOfSoldShares, getCostBasisOfRemainingShares, getProfit } from '../services/InvestmentService';
import './InvestmentCalculator.css';

const InvestmentCalculator: React.FC = () => {
    const [sharesSold, setSharesSold] = useState<number>(0);
    const [salePrice, setSalePrice] = useState<number>(0);
    const accountingStrategyNumber = 1; // Fifo
    const [remainingShares, setRemainingShares] = useState<number | null>(null);
    const [costBasisSold, setCostBasisSold] = useState<number | null>(null);
    const [costBasisRemaining, setCostBasisRemaining] = useState<number | null>(null);
    const [profit, setProfit] = useState<number | null>(null);
    const [error, setError] = useState<string | null>(null);

    const handleCalculate = async () => {
        setError(null);
        try {
            const remainingSharesData = await getRemainingShares(sharesSold, accountingStrategyNumber);
            const costBasisSoldData = await getCostBasisOfSoldShares(sharesSold, accountingStrategyNumber);
            const costBasisRemainingData = await getCostBasisOfRemainingShares(sharesSold, accountingStrategyNumber);
            const profitData = await getProfit(sharesSold, salePrice, accountingStrategyNumber);

            setRemainingShares(remainingSharesData.remainingShares);
            setCostBasisSold(costBasisSoldData.costBasisOfSoldShares);
            setCostBasisRemaining(costBasisRemainingData.costBasisOfRemainingShares);
            setProfit(profitData.profit);
        } catch (error) {
            setError("An error occurred while calculating the results. Please try again.");
            console.error('Error calculating results:', error);
        }
    };

    return (
        <div className="investment-calculator">
            <h1>Investment Calculator</h1>
            <div>
                <label htmlFor="sharesSold">Shares Sold:</label>
                <input
                    id="sharesSold"
                    type="number"
                    value={sharesSold}
                    onChange={(e) => setSharesSold(Number(e.target.value))}
                />
            </div>
            <div>
                <label htmlFor="salePrice">Sale Price per Share:</label>
                <input
                    id="salePrice"
                    type="number"
                    value={salePrice}
                    onChange={(e) => setSalePrice(Number(e.target.value))}
                />
            </div>
            <div>
                <label htmlFor="accountingStrategy">Accounting Strategy:</label>
                <select
                    id="accountingStrategy"
                    value={accountingStrategyNumber}
                    disabled
                >
                    <option value="1">Fifo</option>
                </select>
            </div>
            <button onClick={handleCalculate}>Calculate</button>

            {error && <div className="error-message">{error}</div>}

            {remainingShares !== null && (
                <div className="results">
                    <h2>Results:</h2>
                    <p>Remaining Shares: {remainingShares}</p>
                    <p>Cost Basis of Sold Shares: ${costBasisSold?.toFixed(2)}</p>
                    <p>Cost Basis of Remaining Shares: ${costBasisRemaining?.toFixed(2)}</p>
                    <p>Total Profit: ${profit?.toFixed(2)}</p>
                </div>
            )}
        </div>
    );
};

export default InvestmentCalculator;
