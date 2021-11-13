import { Route } from 'react-router';
import Home from './components/Home/Home';
import Login from './components/Login';

const App = () => {
    return (
        <div id="app-container">
            <Route exact path="/" component={Home} />

            <Route path="/login" component={Login} />
        </div>
    );
};

export default App;
