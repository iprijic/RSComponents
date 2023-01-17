import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';

import { createRoot } from 'react-dom/client'

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

const root = createRoot(rootElement);

//ReactDOM.render(
//    <BrowserRouter basename={baseUrl}>
//        <App />
//    </BrowserRouter>,
//    rootElement);

root.render(
    <React.StrictMode>
        <BrowserRouter basename={baseUrl}>
            <App />
        </BrowserRouter>
    </React.StrictMode>
);

registerServiceWorker();

