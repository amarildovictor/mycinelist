import no_image from '../images/No_Image.jpg';

export const getImageByMovie = (movie, kindOfImage, showDetails = false, description = null) => {
    if (movie.imageMovie){
        if (kindOfImage === "Small" && movie.imageMovie.smallImageUrl){
            return { 
                key: movie.id,
                primaryImageUrl: movie.imageMovie ? movie.imageMovie.smallImageUrl : null,
                titleText: movie.imdbTitleText,
                showDetails: false,
                description: null
            };
        } else if (kindOfImage === "Medium" && movie.imageMovie.mediumImageUrl){
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