import { useState, useEffect } from 'react';
import { getAxiosApiServer } from '../api/axiosBase';
import MovieCard from '../components/MovieCard';
import { useParams, useSearchParams } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Button from 'react-bootstrap/Button';
import $ from 'jquery';

export default function Search(props) {
    const [movieList, setMovieList] = useState([]);
    const [searchParams] = useSearchParams();
    const searchText = searchParams.has('searchText') ? searchParams.get('searchText') : null;
    const {movieTimelineRelease} = useParams();
    const [url, setUrl] = useState(urlInicial());
    const [page, setPage] = useState(1);
    
    function urlInicial() {
        if (searchText){
            return 'Movie/search/?searchText=' + searchText;
        } else if (movieTimelineRelease) {
            return `Movie/search/timeline/${movieTimelineRelease}`;
        } else {
            return 'Movie/search/';
        }
    }

    useEffect(() => {
        const getMovies = async () => {
            const response = await getAxiosApiServer().get(url);
            return response.data;
        }

        const getMoviesByPage = async () => {
            const responseMovieList = await getMovies();
            if (responseMovieList) {
                if (responseMovieList.length === 30){
                    setPage(page + 1);
                } else {
                    $('#loadMore').hide();
                }
                setMovieList([...movieList, ...responseMovieList]);
            }
        };
        getMoviesByPage();
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [url]);

    return (
        <>
            <div className='d-flex justify-content-center flex-wrap'>
            {
                movieList.length > 0 ?
                    movieList.map(movie =>(
                        <MovieCard key={movie.id} movie={movie}></MovieCard>
                    ))
                :
                <div className='d-flex justify-content-center my-3'>
                    <h5>
                        <FontAwesomeIcon icon="fa-solid fa-magnifying-glass" className='text-warning' />
                        <span className='ms-2'>Busca sem resultados!</span>
                    </h5>
                </div>
            }
            </div>
            <div className='d-flex justify-content-center'>
                <Button id='loadMore' variant="primary" onClick={() => setUrl(`Movie/search/timeline/COMING_SOON?page=${page}`)}>
                    <FontAwesomeIcon icon="fa-solid fa-down-long" />
                    <span className='mx-2'>Carregar Mais</span>
                    <FontAwesomeIcon icon="fa-solid fa-down-long" />
                </Button>
            </div>
        </>
    )
}