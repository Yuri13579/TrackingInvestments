import React, { useState } from 'react';
import { getRemainingShares, getCostBasisOfSoldShares, getCostBasisOfRemainingShares, getProfit } from '../services/investmentService';
import './InvestmentCalculator.css';

const InvestmentCalculator: React.FC = () => {
    const [sharesSold, setSharesSold] = useState<number>(0);
    const [salePrice, setSalePrice] = useState<number>(0);
    const [remainingShares, setRemainingShares] = useState<number | null>(null);
    const [costBasisSold, setCostBasisSold] = useState<number | null>(null);
    const [costBasisRemaining, setCostBasisRemaining] = useState<number | null>(null);
    const [profit, setProfit] = useState<number | null>(null);

    const handleCalculate = async () => {
        try {
            const remainingSharesData = await getRemainingShares(sharesSold);
            const costBasisSoldData = await getCostBasisOfSoldShares(sharesSold);
            const costBasisRemainingData = await getCostBasisOfRemainingShares(sharesSold);
            const profitData = await getProfit(sharesSold, salePrice);

            setRemainingShares(remainingSharesData.remainingShares);
            setCostBasisSold(costBasisSoldData.costBasisOfSoldShares);
            setCostBasisRemaining(costBasisRemainingData.costBasisOfRemainingShares);
            setProfit(profitData.profit);
        } catch (error) {
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
            <button onClick={handleCalculate}>Calculate</button>

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
