import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import reportWebVitals from './reportWebVitals';
import 'bootstrap/dist/css/bootstrap.min.css';
import './index.css';
import { HelmetProvider } from 'react-helmet-async';
import SEO from './components/SEO'

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <HelmetProvider>
      <App>
        <SEO
          image= {window.location.href + "logo512.png"}
          title="Amit Philips - Coder, Blogger"
          description="Amit Philips is a programmer specializing in creating cloud based solutions"
          name="amitphilips"
          type="article" />
      </App>
    </HelmetProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
