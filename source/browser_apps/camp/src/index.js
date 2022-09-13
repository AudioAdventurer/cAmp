import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import { BrowserRouter as Router } from "react-router-dom";
import 'react-bootstrap-range-slider/dist/react-bootstrap-range-slider.css';

ReactDOM.render(
    <Router>
      <App />
    </Router>,
  document.getElementById('root')
);
