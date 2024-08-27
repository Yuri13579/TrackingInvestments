import axios from 'axios';

const API_URL = 'https://localhost:7243/api/Investment'; // Adjust this URL to your API's base URL

export const getRemainingShares = async (sharesSold: number, accountingStrategyNumber: number) => {
    const response = await axios.get(`${API_URL}/remaining-shares`, {
        params: { sharesSold, accountingStrategyNumber }
    });
    return response.data;
};

export const getCostBasisOfSoldShares = async (sharesSold: number, accountingStrategyNumber: number) => {
    const response = await axios.get(`${API_URL}/cost-basis-sold`, {
        params: { sharesSold, accountingStrategyNumber }
    });
    return response.data;
};

export const getCostBasisOfRemainingShares = async (sharesSold: number, accountingStrategyNumber: number) => {
    const response = await axios.get(`${API_URL}/cost-basis-remaining`, {
        params: { sharesSold, accountingStrategyNumber }
    });
    return response.data;
};

export const getProfit = async (sharesSold: number, salePrice: number, accountingStrategyNumber: number) => {
    const response = await axios.get(`${API_URL}/profit`, {
        params: { sharesSold, salePrice, accountingStrategyNumber }
    });
    return response.data;
};