import axios from 'axios';
import no_image from '../images/No_Image.jpg';

export const getImageByMovie = (movie, kindOfImage, showDetails = false, description = null) => {
    if (movie.imageMovie) {
        if (kindOfImage === "Small" && movie.imageMovie.smallImageUrl) {
            return {
                key: movie.id,
                primaryImageUrl: movie.imageMovie ? movie.imageMovie.smallImageUrl : null,
                titleText: movie.imdbTitleText,
                showDetails: false,
                description: null
            };
        } else if (kindOfImage === "Medium" && movie.imageMovie.mediumImageUrl) {
            return {
                key: movie.id,
                primaryImageUrl: movie.imageMovie ? movie.imageMovie.mediumImageUrl : null,
                titleText: movie.imdbTitleText,
                showDetails: false,
                description: null
            };
        }
    }

    return {
        key: movie.id,
        primaryImageUrl: movie.imageMovie ? movie.imageMovie.imdbPrimaryImageUrl : no_image,
        titleText: movie.imdbTitleText,
        showDetails: showDetails,
        description: description
    };
}

export const validateEmail = (email) => {
    return email.match(
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
};

export const authSessions = (user) => {
    localStorage.setItem('userEmail', user.email);
    localStorage.setItem('authToken', user.token);
}

export const getUserSession = () => {
    return {
        userEmail: localStorage.getItem('userEmail'),
        authToken: localStorage.getItem('authToken')
    };
}

export const logout = () => {
    localStorage.removeItem('authToken');
    localStorage.removeItem('userEmail');
    delete axios.defaults.headers.Authorization;
}

export const customParseFloat = (floatValue, digits) => {
    const imdbAggregateRatting = parseFloat(floatValue);
    if (imdbAggregateRatting) {
        return imdbAggregateRatting.toFixed(digits);
    }
    else {
        return '-.-';
    }
}