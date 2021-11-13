import { useCallback, useState } from 'react';
import { getAuthorizationHeader } from '../helpers/AuthHelper';

type RequestMethod = 'GET' | 'POST';

const useHttp = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

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

                const data = await response.json();
                if (!response.ok) {
                    const message = data?.errorMessage || 'Something went wrong!';
                    if (errorHandler) {
                        errorHandler(message, response.status);
                    }
                    setError(message);
                    setIsLoading(false);
                } else {
                    setIsLoading(false);
                    callback(data);
                }
            } catch (err: any) {
                if (errorHandler) {
                    errorHandler(err.message);
                }
                setError(err.message);
                setIsLoading(false);
            }
        },
        []
    );

    return { isLoading, error, sendRequest };
};

export default useHttp;
