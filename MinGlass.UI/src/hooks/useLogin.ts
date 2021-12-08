import { useCallback } from 'react';
import { useAuthContext } from '../store/AuthContext';
import { TOKEN_NAME } from '../constants/AuthConstants';
import useHttp from './useHttp';
import { getApiUrl } from '../helpers/UrlHelper';

const apiUrl = getApiUrl();
const loginUrl = `${apiUrl}/Auth/Login`;
const loginMethod = 'POST';
const headers = { 'Content-Type': 'application/json' };

const useLogin = () => {
    const { sendRequest, isLoading, error } = useHttp();
    const { setUser } = useAuthContext();
    const login = useCallback(
        async (email: string, password: string, onLoggedIn: () => void): Promise<void> => {
            const callback = (res: any) => {
                console.log(res);
                localStorage.setItem(TOKEN_NAME, res.jwtToken);
                onLoggedIn();
            };
            const onError = () => {
                setUser(undefined);
            };
            await sendRequest(loginUrl, callback, onError, loginMethod, headers, {
                email,
                password,
            });
        },
        []
    );

    return {
        login,
        isLoggingIn: isLoading,
        logInError: error,
    };
};

export default useLogin;
