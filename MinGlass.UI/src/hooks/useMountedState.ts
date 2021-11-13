import { useEffect, useMemo, useRef } from 'react';

const useMountedState = () => {
    const mountedRef = useRef(false);
    const isMounted = useMemo(() => mountedRef.current, [mountedRef.current]);

    useEffect(() => {
        mountedRef.current = true;
        return () => {
            mountedRef.current = false;
        };
    }, []);

    return { isMounted };
};

export default useMountedState;
