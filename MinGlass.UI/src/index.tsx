import { BrowserRouter } from 'react-router-dom';
import ReactDOM from 'react-dom';
import App from './App';
import './assets/styles/global-styles.less';
import { AuthContextProvider } from './store/AuthContext';

const rootElement = document.getElementById('root');

ReactDOM.render(
    <BrowserRouter>
        <AuthContextProvider>
            <App />
        </AuthContextProvider>
    </BrowserRouter>,
    rootElement
);
