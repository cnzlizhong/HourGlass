import { useEffect, useCallback, useRef } from 'react';

const useMountedState = () => {
    const mountedRef = useRef(false);
    const isMounted = useCallback(() => {
        return mountedRef.current;
    }, []);

    useEffect(() => {
        mountedRef.current = true;
        return () => {
            mountedRef.current = false;
        };
    }, []);

    return { isMounted };
};

export default useMountedState;
