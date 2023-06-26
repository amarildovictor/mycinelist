import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useNavigate } from 'react-router-dom';
import { getImageByMovie } from '../api/utils';
import { getAxiosApiServer } from './../api/axiosBase';
import { useState } from 'react';
import $ from 'jquery';

const initialStars = (rating) => {
    let array = [];

    for (let i = 0; i < 5; i++) {
        array.push(i < rating);
    }

    return array;
}

export default function MovieCard(props) {
    const navigate = useNavigate();
    const [isFavorite, setIsFavorite] = useState(props.movie.userFavorite);
    const [userRating, setUserRating] = useState(props.movie.userRating);
    const [isSavingFavorite, setIsSavingFavorite] = useState(false);
    const [stars, setStars] = useState(initialStars(props.movie.userRating));
    const reductedMovieInfo = getImageByMovie(props.movie, 'Small');

    const imdbAggregateRatting = parseFloat(props.movie.imdbAggregateRatting);
    if (imdbAggregateRatting) {
        props.movie.imdbAggregateRatting = imdbAggregateRatting.toFixed(1);
    }
    else {
        props.movie.imdbAggregateRatting = '-.-';
    }

    const myCineListRating = parseFloat(props.movie.myCineListRating);
    if (myCineListRating) {
        props.movie.myCineListRating = myCineListRating.toFixed(1);
    }
    else {
        props.movie.myCineListRating = '-.-';
    }

    const onclick_FavoriteMovie = async ($this) => {
        var userMovieList = { movie: { id: props.movie.id } };
        var card = $($this.currentTarget).closest('.movieCard');

        setIsSavingFavorite(true);

        if (!isFavorite) {
            await getAxiosApiServer().post('/UserApi/movieList', userMovieList)
                .then(function () {
                    setIsFavorite(true);
                }).catch(function (error) {
                    console.log(error);
                });
        }
        else {
            await getAxiosApiServer().delete(`/UserApi/movieList/${userMovieList.movie.id}`)
                .then(function () {
                    setIsFavorite(false);
                    card.remove();
                }).catch(function (error) {
                    console.log(error);
                });
        }

        setIsSavingFavorite(false);
    };

    const onMouseEnter_Stars = ($this, n) => {
        const star = $($this.currentTarget);
        const closestDiv = star.closest('div');

        setStars(stars.map((star, i) => n >= i));

        closestDiv.removeClass('text-warning');
        closestDiv.addClass('text-info');
    }

    const onMouseLeave_Stars = ($this, count) => {
        const closestDiv = $($this.currentTarget).closest('div');

        closestDiv.addClass('text-warning');
        closestDiv.removeClass('text-info');

        treatStars(null);
    }

    const onClick_Stars = async (rating) => {
        if (rating !== userRating) {
            await getAxiosApiServer().post('/UserApi/movieList/updateRating', { movie: { id: props.movie.id }, rating: rating })
                .then(function () {
                    setUserRating(rating);
                    treatStars(rating);
                }).catch(function (error) {
                    console.log(error);
                });
        }
    }

    const treatStars = (rating) => {
        const x = rating ? rating : userRating;

        setStars(stars.map((star, i) => x - 1 >= i));
    }

    return (
        <div className='movieCard ms-1 me-1 mt-2 mb-2' title={props.movie.imdbTitleText}>
            <div className='position-relative'>
                {props.logged ?
                    <div id="favoriteMovieHeartDiv" title='Adicionar na minha lista'>
                        <h5 className="position-absolute top-0 end-0 border rounded m-1 p-1 bg-light">
                            {isSavingFavorite ?
                                <div className="spinner-border text-danger spinner-border-sm" role="status">
                                    <span className="visually-hidden">Loading...</span>
                                </div>
                                :
                                <FontAwesomeIcon icon={`fa-${isFavorite ? 'solid' : 'regular'} fa-heart`} className="text-danger"
                                    onClick={onclick_FavoriteMovie} style={{ cursor: 'pointer' }} />
                            }
                        </h5>
                    </div> : null
                }
                <div className="img d-flex justify-content-center align-items-center overflow-hidden rounded bg-black"
                    onClick={() => navigate(`/movie/${props.movie.id}`)}>
                    <img alt="Filme" className='h-100 w-100' src={reductedMovieInfo.primaryImageUrl}></img>
                </div>
            </div>
            <div className="rounded mt-1 shadow border border-white bg-black">
                <h6 className="pt-2 px-2 w-100 text-truncate text-white" onClick={() => navigate(`/movie/${props.movie.id}`)}
                    style={{ cursor: 'pointer' }}>
                    <FontAwesomeIcon icon="fa-solid fa-caret-right" className='me-1' />
                    {props.movie.imdbTitleText}
                </h6>
                <div className="d-flex align-items-center position-relative" style={{ height: '40px' }}>
                    <div className='ps-2 text-warning'>
                        <FontAwesomeIcon icon={`${stars[0] ? 'fa-solid' : 'fa-regular'} fa-star`} title='Avaliar com Nota 1,0'
                            onMouseEnter={(e) => onMouseEnter_Stars(e, 0)} onMouseLeave={onMouseLeave_Stars}
                            onClick={() => onClick_Stars(1)} />
                        <FontAwesomeIcon icon={`${stars[1] ? 'fa-solid' : 'fa-regular'} fa-star`} title='Avaliar com Nota 2,0'
                            onMouseEnter={(e) => onMouseEnter_Stars(e, 1)} onMouseLeave={onMouseLeave_Stars}
                            onClick={() => onClick_Stars(2)} />
                        <FontAwesomeIcon icon={`${stars[2] ? 'fa-solid' : 'fa-regular'} fa-star`} title='Avaliar com Nota 3,0'
                            onMouseEnter={(e) => onMouseEnter_Stars(e, 2)} onMouseLeave={onMouseLeave_Stars}
                            onClick={() => onClick_Stars(3)} />
                        <FontAwesomeIcon icon={`${stars[3] ? 'fa-solid' : 'fa-regular'} fa-star`} title='Avaliar com Nota 4,0'
                            onMouseEnter={(e) => onMouseEnter_Stars(e, 3)} onMouseLeave={onMouseLeave_Stars}
                            onClick={() => onClick_Stars(4)} />
                        <FontAwesomeIcon icon={`${stars[4] ? 'fa-solid' : 'fa-regular'} fa-star`} title='Avaliar com Nota 5,0'
                            onMouseEnter={(e) => onMouseEnter_Stars(e, 4)} onMouseLeave={onMouseLeave_Stars}
                            onClick={() => onClick_Stars(5)} />
                    </div>
                    <div className='d-flex position-absolute bottom-0 end-0 border-top border-start border-white fs-4 fw-bold justify-content-center'
                        style={{ width: '110px' }}>
                        <span className='text-primary' title='IMDB'>{props.movie.imdbAggregateRatting}</span>
                        <span className='text-white mx-2'>|</span>
                        <span className='text-success' title='MyCineList'>{props.movie.myCineListRating}</span>
                    </div>
                </div>
            </div>
        </div>
    )
}