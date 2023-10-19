import { lazy } from 'react';
import { Routes, Route } from 'react-router-dom';

const Overview = lazy(() => import('../../pages/Overview'));

export default function Navigation() {
    return (
        <Routes>
            <Route path='/' element={<Overview />} />
        </Routes>
    );
}