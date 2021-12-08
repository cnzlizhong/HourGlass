import { useCallback } from 'react';
import { useAuthContext } from '../store/AuthContext';
import useHttp from './useHttp';
import { getApiUrl } from '../helpers/UrlHelper';

const apiUrl = getApiUrl();
const getUserUrl = `${apiUrl}/Auth/GetUser`;

const useGetCurrentUser = () => {
    const { sendRequest, isLoading, error } = useHttp();
    const { setUser } = useAuthContext();

    const getCurrentUser = useCallback(async (onGetUserFailed: (err: string) => void) => {
        const callback = (res: any) => {
            setUser(res);
        };
        const onError = (err: string) => {
            setUser(undefined);
            onGetUserFailed(err);
        };
        await sendRequest(getUserUrl, callback, onError);
    }, []);

    return {
        getCurrentUser,
        isGettingUser: isLoading,
        getUserError: error,
    };
};

export default useGetCurrentUser;
