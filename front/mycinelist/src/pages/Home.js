import Button from 'react-bootstrap/Button';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { useNavigate } from 'react-router-dom';
import { getAxiosApiServer } from '../api/axiosBase';
import ReactCarousel from '../components/ReactCarousel';
import { useEffect, useState } from 'react';
import { getImageByMovie } from '../api/utils';

export default function Home() {
    const navigate = useNavigate();
    
    const [comingSoonList, setComingSoonList] = useState([]); 
    const [premieiresList, setPremieiresList] = useState([]);
    const [inFarewellList, setInFarewellList] = useState([]);

    useEffect(() => {
        let movieList = [];
        
        const getMovies = async (url) => {
            await getAxiosApiServer().get(url)
                                    .then(function (response) {
                                        movieList = response.data;
                                    })
                                    .catch(function (error) {
                                        console.log(error);
                                    })
        };

        const newItemCarousel = async (url, callback) => {
            let list = [];
            
            await getMovies(url);
    
            movieList.map(movie => (
                list.push(getImageByMovie(movie, 'Medium'))
            ))
    
            callback(list);
        };

        newItemCarousel('Movie/search/timeline/COMING_SOON?pageNumberMovies=3&ignoreNoImageMovie=true', setComingSoonList);
        newItemCarousel('Movie/search/timeline/PREMIERES?pageNumberMovies=3&ignoreNoImageMovie=true', setPremieiresList);
        newItemCarousel('Movie/search/timeline/IN_FAREWELL?pageNumberMovies=3&ignoreNoImageMovie=true', setInFarewellList);
    }, []);

    return (
        <>
            <div className='d-flex justify-content-center flex-wrap'>
                <div className='w-25 me-3' style={{minWidth:'355px'}}>
                    <h6 className='d-flex justify-content-center mt-3 mb-3 ms-4 me-4 pb-1 border-bottom rounded-pill'>
                        <FontAwesomeIcon className='me-2 mt-1' icon="fa-solid fa-angles-right" />
                        <span>Em Breve</span>
                        <FontAwesomeIcon className='ms-2 mt-1' icon="fa-solid fa-angles-left" />
                    </h6>
                    <ReactCarousel list={comingSoonList}/>
                </div>
                <div className='w-25 me-3' style={{minWidth:'355px'}}>
                    <h6 className='d-flex justify-content-center mt-3 mb-3 ms-4 me-4 pb-1 border-bottom rounded-pill'>
                        <FontAwesomeIcon className='me-2 mt-1' icon="fa-solid fa-angles-right" />
                        <span>Estreias</span>
                        <FontAwesomeIcon className='ms-2 mt-1' icon="fa-solid fa-angles-left" />
                    </h6>
                    <ReactCarousel list={premieiresList}/>
                </div>
                <div className='w-25 me-3' style={{minWidth:'355px'}}>
                    <h6 className='d-flex justify-content-center mt-3 mb-3 ms-4 me-4 pb-1 border-bottom rounded-pill'>
                        <FontAwesomeIcon className='me-2 mt-1' icon="fa-solid fa-angles-right" />
                        <span>Em Despedida</span>
                        <FontAwesomeIcon className='ms-2 mt-1' icon="fa-solid fa-angles-left" />
                    </h6>
                    <ReactCarousel list={inFarewellList}/>
                </div>
            </div>
            <div className='d-flex justify-content-center mt-3'>
                <Button variant="primary" onClick={() => navigate('/search')}>
                    <FontAwesomeIcon className='me-2' icon="fa-solid fa-book-open" />
                    Acesse o catálogo de filmes completo aqui!
                </Button>
            </div>
        </>
    )
}