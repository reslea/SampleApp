import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
// import AppFormik from './AppFormik/AppFormik';
// import AppForm from './AppForm/AppForm';
import App from './App';
// import AppCheckout from './AppCheckout/AppCheckout';
import 'bootstrap/dist/css/bootstrap.min.css';

export const TokenContext = React.createContext();

ReactDOM.render(
  <React.StrictMode>
    <App />
    {/* <AppForm /> */}
    {/* <AppFormik /> */}
    {/* <AppCheckout /> */}
  </React.StrictMode>,
  document.getElementById('root')
);