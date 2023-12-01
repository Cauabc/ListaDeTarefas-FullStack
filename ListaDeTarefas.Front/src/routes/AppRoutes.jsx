import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import App from '../App';
import Login from '../components/Login/Login'
import Register from '../components/Register/Register';


const AppRoutes = () => {
    return (
        <Router>
            <Routes>
                <Route path='/' exact element={<App/>}></Route>
                <Route path='/login' element={<Login/>}></Route>
                <Route path='/register' element={<Register/>}></Route>
            </Routes>
        </Router>
    )
}

export default AppRoutes;