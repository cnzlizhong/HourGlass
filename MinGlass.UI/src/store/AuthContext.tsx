import { createContext, ReactNode, useMemo, useContext, useState, useCallback } from 'react';
import { TOKEN_NAME } from '../constants/AuthConstants';
import User from '../models/User';

type AuthContextType = {
    isLoggedIn: boolean;
    user: User | undefined;
    setUser: (user: User | undefined) => void;
    logout: () => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthContextProvider = ({ children }: { children: ReactNode }) => {
    const [user, setUser] = useState<User>();
    const isLoggedIn = useMemo(() => !!user, [user]);
    const logout = useCallback(() => {
        setUser(undefined);
        localStorage.removeItem(TOKEN_NAME);
    }, []);
    return (
        <AuthContext.Provider value={{ isLoggedIn, user, setUser, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuthContext = () => {
    const authContext = useContext(AuthContext);
    if (!authContext) {
        throw new Error('AuthContext must be used within AuthContextProvider.');
    }
    return authContext;
};
