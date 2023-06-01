import React from 'react';
import ReactDOM from 'react-dom/client';
import {BrowserRouter as Router, Routes, Route} from 'react-router-dom';
import './index.css';
import App from './App';
import "bootswatch/dist/lumen/bootstrap.min.css";
import Home from './pages/Home';
import Search from './pages/Search';
import Movie from './pages/Movie';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <Router basename={process.env.PUBLIC_URL}>
      <Routes>
        <Route path='/' element={<App><Home></Home></App>} />
        <Route path='/movie/:id' element={<App><Movie></Movie></App>} />
        <Route path='/search/' element={<App><Search></Search></App>} />
        <Route path='/search/timeline/:movieTimelineRelease' element={<App><Search></Search></App>} />
        {/* <Route path='*' element={<PageNotFound />} /> */}
      </Routes>
    </Router>
  </React.StrictMode>
);