import React from 'react';
import './styles/App.scss';
import Nav from './components/Nav';
import { Outlet } from 'react-router';

function App() {
  return (
    <div>
      <Nav />
      <Outlet />
    </div>
  );
}

export default App;
