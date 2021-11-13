import { useCallback, useState } from 'react';
import { getAuthorizationHeader } from '../helpers/AuthHelper';
import useMountedState from './useMountedState';

type RequestMethod = 'GET' | 'POST';

const useHttp = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const { isMounted } = useMountedState();

    const onError = (message: string) => {
        if (!isMounted()) return;
        setError(message);
        setIsLoading(false);
    };

    const onFetched = (data: any, callback: (data: any) => void) => {
        if (!isMounted()) return;
        setIsLoading(false);
        callback(data);
    };

    const sendRequest = useCallback(
        async (
            url: string,
            callback: (res: unknown) => void,
            errorHandler?: (error: string, errorCode?: number) => void,
            method?: RequestMethod,
            headers?: HeadersInit,
            body?: any
        ) => {
            setIsLoading(true);
            setError(null);
            try {
                const authHeader = getAuthorizationHeader();
                const requestHeaders = { ...authHeader, ...(headers ?? {}) };
                const response = await fetch(url, {
                    method: method ?? 'GET',
                    headers: requestHeaders,
                    body: body ? JSON.stringify(body) : null,
                });
                let data = null;
                try {
                    data = await response.json();
                } catch (err: any) {
                    data = null;
                }
                if (!response.ok) {
                    const message = data?.errorMessage || 'Something went wrong!';
                    if (errorHandler) {
                        errorHandler(message, response.status);
                    }
                    onError(message);
                } else {
                    onFetched(data, callback);
                }
            } catch (err: any) {
                if (errorHandler) {
                    errorHandler(err.message);
                }
                onError(err.message);
            }
        },
        []
    );

    return { isLoading, error, sendRequest };
};

export default useHttp;
