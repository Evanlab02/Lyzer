import { Routes, Route } from 'react-router-dom';
import Overview from '../../pages/Overview';

export default function Navigation() {
    return (
        <Routes>
            <Route path='/' element={<Overview />} />
        </Routes>
    );
}