export const getApiUrl = () => {
    return process.env.API_URL ? `${process.env.API_URL}/api` : 'api';
};
