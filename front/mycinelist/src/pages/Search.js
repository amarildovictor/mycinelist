import { useState, useEffect } from 'react';
import { getAxiosApiServer } from '../api/axiosBase';
import MovieCard from '../components/MovieCard';
import { useParams, useSearchParams } from 'react-router-dom';

export default function Search(props) {
    const [movieList, setMovieList] = useState([]);
    const [searchParams] = useSearchParams();
    const searchText = searchParams.has('searchText') ? searchParams.get('searchText') : null;
    const {movieTimelineRelease} = useParams();
    let url = 'Movie/search/';
    
    if (searchText){
        url += '?searchText=' + searchText;
    } else if (movieTimelineRelease) {
        url += `timeline/${movieTimelineRelease}`;
    }

    useEffect(() => {
        const getMovies = async () => {
        getAxiosApiServer().get(url)
                                .then(function (response) {
                                    setMovieList(response.data);
                                })
                                .catch(function (error) {
                                    console.log(error);
                                })
        };
        getMovies();
    }, [url]);

    return (
        <div className='d-flex justify-content-center flex-wrap'>
        {
            movieList.length > 0 ?
                movieList.map(movie =>(
                    <MovieCard key={movie.id} movie={movie}></MovieCard>
                ))
            :
            <i className="fa-solid fa-rectangle-xmark"></i>
        }
        </div>
    )
}