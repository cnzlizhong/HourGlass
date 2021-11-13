import { TOKEN_NAME } from '../constants/AuthConstants';

export const getAuthorizationHeader = (): HeadersInit => {
    const jwtToken = localStorage.getItem(TOKEN_NAME);
    return jwtToken ? { Authorization: `Bearer ${jwtToken}` } : {};
};
