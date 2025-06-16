import React, { useEffect } from 'react';
import {
  Routes,
  Route,
  useLocation
} from 'react-router-dom';

import './css/style.css';

// Import pages
import Orders from './pages/ecommerce/Orders';
import Invoices from './pages/ecommerce/Invoices';
import { setToken, setUrl } from './utils/Auth';

function App() {
  const location = useLocation();
  setUrl();
  setToken();
  
  useEffect(() => {
    document.querySelector('html').style.scrollBehavior = 'auto'
    window.scroll({ top: 0 })
    document.querySelector('html').style.scrollBehavior = ''
  }, [location.pathname]); // triggered on route change

  return (
    <>
      <Routes>
        <Route path="/" element={<Invoices />} />
        <Route path="/ecommerce/orders" element={<Orders />} />
        <Route path="/ecommerce/invoices" element={<Invoices />} />
      </Routes>
    </>
  );
}

export default App;
