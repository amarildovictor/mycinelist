import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getAxiosApiServer } from './../api/axiosBase';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { customParseFloat, getImageByMovie } from '../api/utils';
import Stars from '../components/Stars';

export default function Movie(props) {
    const { id } = useParams();
    const [movie, setMovie] = useState();
    const [isSavingFavorite, setIsSavingFavorite] = useState(false);
    const [isFavorite, setIsFavorite] = useState(false);

    useEffect(() => {
        const getMovies = async () => {
            getAxiosApiServer().get(`Movie/${id}`)
                .then(function (response) {
                    setMovie(response.data);
                    setIsFavorite(response.data.userFavorite);
                })
                .catch(function (error) {
                    console.log(error);
                })
        };
        getMovies();
    }, [id]);

    const onclick_FavoriteMovie = async () => {
        var userMovieList = { movie: { id: movie.id } };

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
                }).catch(function (error) {
                    console.log(error);
                });
        }

        setIsSavingFavorite(false);
    };

    return (
        <>
            {movie ?
                <div className='my-3'>
                    <div className='d-flex justify-content-center align-items-center flex-row-reverse'>
                        {props.logged ?
                            <div id="favoriteMovieHeartDiv" title={`${isFavorite ? 'Remover da' : 'Adicionar na'} minha lista`} className='ms-2 mb-1'>
                                <div className="border rounded m-1 p-1 bg-light" style={{minWidth:'100px', cursor: 'pointer'}} onClick={onclick_FavoriteMovie}>
                                    {isSavingFavorite ?
                                        <div className="spinner-border text-danger spinner-border-sm" role="status">
                                            <span className="visually-hidden">Loading...</span>
                                        </div>
                                        :
                                        <FontAwesomeIcon icon={`fa-${isFavorite ? 'solid' : 'regular'} fa-heart`} className="text-danger"/>
                                    }
                                    <span className='fw-normal ms-2'>{isFavorite ? 'Remover' : 'Adicionar'}</span>
                                </div>
                            </div> : null
                        }
                        <h5 className='fw-bold'>
                            <FontAwesomeIcon icon="fa-solid fa-video" className='text-primary me-2' />
                            <span>{movie.imdbTitleText}</span>
                        </h5>
                    </div>
                    <div className='row mt-2 justify-content-center'>
                        <div className='col-4 d-flex justify-content-center align-items-center overflow-hidden mb-2' style={{ minWidth: '355px' }}>
                            <img className="rounded mw-100" src={getImageByMovie(movie, 'Medium').primaryImageUrl}
                                alt={movie.imdbTitleText} title={movie.imdbTitleText} />
                        </div>
                        <div className='col'>
                            <div className='border border-success rounded shadow bg-light p-2'>
                                <h6 className='fw-bold'>
                                    <FontAwesomeIcon icon="fa-solid fa-clapperboard" className='text-success me-2' />
                                    Sinopse
                                </h6>
                                {movie.plot ? movie.plot.imdbPlainText : '-'}
                            </div>
                            <div className='border border-danger rounded shadow bg-light p-2 mt-2'>
                                <h6 className='fw-bold'>
                                    <FontAwesomeIcon icon="fa-solid fa-ticket" className='text-danger me-2' />
                                    Categorias
                                </h6>
                                <div className='d-flex flex-wrap'>
                                    {movie.genresMovie ?
                                        movie.genresMovie.map(genre => (
                                            <div key={genre.genresIMDBGenreID} className='p-1 me-2 border border-warning rounded bg-white font-monospace'>
                                                .{genre.genresIMDBGenreID}
                                            </div>
                                        ))
                                        : <span className='p-1'>Sem categorias</span>
                                    }
                                </div>
                            </div>
                            <div className='border border-primary rounded shadow bg-light p-2 mt-2'>
                                <h6 className='fw-bold'>
                                    <FontAwesomeIcon icon="fa-solid fa-rocket" className='text-primary me-2' />
                                    Data do lançamento
                                </h6>
                                {movie.releaseDate ?
                                    new Date(movie.releaseDate.formatedDate).toLocaleDateString()
                                    : <span>Não há registro até o momento</span>
                                }
                            </div>
                            <div className='border border-success rounded shadow bg-light p-2 mt-2'>
                                <h6 className='fw-bold'>
                                    <FontAwesomeIcon icon="fa-solid fa-gavel" className='text-success me-2' />
                                    Dê sua nota a este filme!
                                </h6>
                                <Stars logged={props.logged} movie={movie}/>
                            </div>
                            <div className='d-flex'>
                                <div className='border border-danger rounded shadow bg-light p-2 mt-2'>
                                    <h6 className='fw-bold'>
                                        <FontAwesomeIcon icon="fa-solid fa-hashtag" className='text-danger me-2' />
                                        Notas
                                    </h6>
                                    <div className='d-flex flex-row m-auto mt-2 text-center text-white fw-bold'>
                                        <div className='d-flex flex-column border border-primary bg-primary bg-gradient rounded justify-content-center pt-2 px-3'>
                                            <span>IMDB</span>
                                            <h2 className='fw-bolder'>{customParseFloat(movie.imdbAggregateRatting, 1)}</h2>
                                        </div>
                                        <div className='d-flex flex-column border border-success bg-success bg-gradient  rounded ms-2 justify-content-center pt-2 px-3'>
                                            <span>MyCineList</span>
                                            <h2 className='fw-bolder'>{customParseFloat(movie.myCineListRating, 1)}</h2>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                :
                <div className='d-flex justify-content-center my-3'>
                    <h5>
                        <FontAwesomeIcon icon="fa-solid fa-magnifying-glass" className='text-warning' />
                        <span className='ms-2'>Filme não encontrado!</span>
                    </h5>
                </div>
            }
        </>
    )
}