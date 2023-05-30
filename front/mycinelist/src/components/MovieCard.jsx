import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import no_image from '../images/No_Image.jpg';

export default function MovieCard(props) {
    if (props.movie.imageMovie == null) {
        props.movie.imageMovie = { imdbPrimaryImageUrl: no_image };
    }

    if (props.movie.imdbAggregateRatting == null){
        props.movie.imdbAggregateRatting = '-.-';
    }

    return (
        <div className='ms-1 me-1 mt-2 mb-2' style={{width:'220px', height:'402px'}} title={props.movie.imdbTitleText}>
            <div>
                <div className="d-flex justify-content-center align-items-center overflow-hidden rounded bg-black" style={{height:'322px'}}>
                    <img alt="Filme" className="mw-100" style={{objectFit:'cover'}} src={props.movie.imageMovie?.imdbPrimaryImageUrl}></img>
                </div>
            </div>
            <div className="position-relative rounded p-2 mt-1 shadow border border-white bg-black">
                <h6 className="w-100 d-inline-block text-truncate text-white">
                    <FontAwesomeIcon icon="fa-solid fa-caret-right" className='me-1' />
                    {props.movie.imdbTitleText}
                </h6>
                <div className="d-flex">
                    <div className='text-warning'>
                        <FontAwesomeIcon icon="fa-solid fa-star" />
                        <FontAwesomeIcon icon="fa-solid fa-star" />
                        <FontAwesomeIcon icon="fa-solid fa-star" />
                        <FontAwesomeIcon icon="fa-regular fa-star" />
                        <FontAwesomeIcon icon="fa-regular fa-star" />
                    </div>
                    <div className='d-flex position-absolute bottom-0 end-0 border-top border-start border-white fs-4 fw-bold justify-content-center'
                    style={{width:'110px',height:'40px'}}>
                        <span className='text-primary' title='IMDB'>{props.movie.imdbAggregateRatting}</span>
                        <span className='text-white mx-2'>|</span>
                        <span className='text-success' title='MyCineList'>7.3</span>
                    </div>
                </div>
            </div>
        </div>
    )
}