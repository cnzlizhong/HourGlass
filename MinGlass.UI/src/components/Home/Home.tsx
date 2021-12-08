import { useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { Button } from 'antd';
import { useAuthContext } from '../../store/AuthContext';
import styles from './Home.module.less';
import useGetCurrentUser from '../../hooks/useGetCurrentUser';

const Home = () => {
    const history = useHistory();
    const { user, logout } = useAuthContext();
    const { getCurrentUser, isGettingUser } = useGetCurrentUser();
    useEffect(() => {
        getCurrentUser(() => {
            history.push('/login');
        });
    }, []);

    const onLogout = () => {
        logout();
        history.push('/login');
    };

    const homeContent = isGettingUser ? (
        <div className="text-2xl">Loading...</div>
    ) : (
        <>
            <div className="absolute top-0 right-0 m-4">
                <Button onClick={onLogout}>Logout</Button>
            </div>
            <div className={styles.greeting}>Hello, {user?.firstName}</div>
            <div className={styles.title}>Welcome to MinGlass app!</div>
        </>
    );

    return <div className={styles.home}>{homeContent}</div>;
};

export default Home;
