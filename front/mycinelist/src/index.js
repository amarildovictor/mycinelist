import React from 'react';
import ReactDOM from 'react-dom/client';
import {BrowserRouter as Router, Routes, Route} from 'react-router-dom';
import './index.css';
import App from './App';
import "bootswatch/dist/lumen/bootstrap.min.css";
import "./css/common.css"
import Home from './pages/Home';
import Search from './pages/Search';
import Movie from './pages/Movie';
import PageNotFound from './pages/PageNotFound';
import Register from './pages/Auth/Register';
import Login from './pages/Auth/Login';
import UserList from './pages/UserThings/UserList';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <Router basename={process.env.PUBLIC_URL}>
      <Routes>
        <Route path='/' element={<App><Home></Home></App>} />
        <Route exact path='/auth/login' element={<App><Login></Login></App>} />
        <Route exact path='/auth/register' element={<App><Register></Register></App>} />
        <Route exact path='/userThings/userList' element={<App><UserList></UserList></App>} />
        <Route exact path='/movie/:id' element={<App><Movie></Movie></App>} />
        <Route exact path='/search/' element={<App><Search></Search></App>} />
        <Route exact path='/search/timeline/:movieTimelineRelease' element={<App><Search></Search></App>} />
        <Route path='*' element={<App><PageNotFound /></App>} />
      </Routes>
    </Router>
  </React.StrictMode>
);