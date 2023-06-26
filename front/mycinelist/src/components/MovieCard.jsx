import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useNavigate } from 'react-router-dom';
import { customParseFloat, getImageByMovie } from '../api/utils';
import { getAxiosApiServer } from './../api/axiosBase';
import { useState } from 'react';
import $ from 'jquery';
import Stars from './Stars';

export default function MovieCard(props) {
    const navigate = useNavigate();
    const [isFavorite, setIsFavorite] = useState(props.movie.userFavorite);
    const [isSavingFavorite, setIsSavingFavorite] = useState(false);
    const reductedMovieInfo = getImageByMovie(props.movie, 'Small');

    props.movie.imdbAggregateRatting = customParseFloat(props.movie.imdbAggregateRatting, 1);
    props.movie.myCineListRating = customParseFloat(props.movie.myCineListRating, 1);

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
                    <Stars movie = {props.movie}/>
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