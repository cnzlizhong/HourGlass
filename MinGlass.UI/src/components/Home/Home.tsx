import { useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { useAuthContext } from '../../store/AuthContext';
import styles from './Home.module.less';
import useGetCurrentUser from '../../hooks/useGetCurrentUser';

const Home = () => {
    const history = useHistory();
    const { user } = useAuthContext();
    const { getCurrentUser, isGettingUser } = useGetCurrentUser();
    useEffect(() => {
        getCurrentUser(() => {
            history.push('/login');
        });
    }, []);

    const homeContent = isGettingUser ? (
        <div className="text-2xl">Loading...</div>
    ) : (
        <>
            <div>Hello, {user?.firstName}</div>
            <div className={styles.title}>Welcome to MinGlass app!</div>
            <div>API URL: {process.env.API_URL}</div>
        </>
    );

    return <div className={styles.home}>{homeContent}</div>;
};

export default Home;
