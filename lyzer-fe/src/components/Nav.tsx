import React from "react";
import { Routes, Route } from "react-router-dom";
import Overview from "../pages/Overview";


export default function Nav() {
    return (
        <Routes>
            <Route path="/" element={<Overview />} />
        </Routes>
    );
}