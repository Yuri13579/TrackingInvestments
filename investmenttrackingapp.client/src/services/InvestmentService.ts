import axios from 'axios';

const API_URL = 'https://localhost:7243/api/Investment'; // Adjust this URL to your API's base URL

export const getRemainingShares = async (sharesSold: number) => {
    const response = await axios.get(`${API_URL}/remaining-shares`, {
        params: { sharesSold }
    });
    return response.data;
};

export const getCostBasisOfSoldShares = async (sharesSold: number) => {
    const response = await axios.get(`${API_URL}/cost-basis-sold`, {
        params: { sharesSold }
    });
    return response.data;
};

export const getCostBasisOfRemainingShares = async (sharesSold: number) => {
    const response = await axios.get(`${API_URL}/cost-basis-remaining`, {
        params: { sharesSold }
    });
    return response.data;
};

export const getProfit = async (sharesSold: number, salePrice: number) => {
    const response = await axios.get(`${API_URL}/profit`, {
        params: { sharesSold, salePrice }
    });
    return response.data;
};
