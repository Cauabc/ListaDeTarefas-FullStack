import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import App from '../App';
import Login from '../components/Login/Login'
import Register from '../components/Register/Register';
import { useState } from 'react';


const AppRoutes = () => {

    const [IsAutenticated, setIsAutenticated] = useState(false)


    return (
        <Router>
            <Routes>
                <Route path='/' exact element={<App isAutenticated={IsAutenticated}/>}></Route>
                <Route path='/login' element={<Login setIsAutenticated={setIsAutenticated}/>}></Route>
                <Route path='/register' element={<Register/>}></Route>
            </Routes>
        </Router>
    )
}

export default AppRoutes;