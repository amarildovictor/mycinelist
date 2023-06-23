import axios from 'axios';

export const getAxiosApiServer = () => {
    const token = localStorage.getItem('authToken');
    if (token) {
        axios.defaults.headers.Authorization = `Bearer ${token}`;
    }

    var axiosAppServer = axios.create(
        { baseURL: process.env.REACT_APP_API_PRODUCTION_URL }
    );

    if (!process.env.NODE_ENV || process.env.NODE_ENV === 'development')
        axiosAppServer = axios.create({ baseURL: process.env.REACT_APP_API_DEVELOPMENT_URL });

    return axiosAppServer;
};