import { lazy } from 'react';
import { Routes, Route } from 'react-router-dom';
import Landing from '../../pages/Landing';

const Overview = lazy(() => import('../../pages/Overview'));

export default function Navigation() {
    return (
        <Routes>
            <Route path='/' element={<Landing />} />
            <Route path="/overview/" element={<Overview />} />
        </Routes>
    );
}