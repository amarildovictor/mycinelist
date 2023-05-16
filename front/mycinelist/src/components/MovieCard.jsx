import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

export default function MovieCard() {
    return (
        <div className='ms-1 me-1 mt-2 mb-2' style={{maxWidth:'220px', height:'380px'}} title="Mandalorian Mandalorian Mandalorian Mandalorian">
            <div>
                <div className="d-flex justify-content-center align-items-start overflow-hidden rounded" style={{height:'300px'}}>
                    <img alt="Filme" className="mw-100" style={{objectFit:'cover'}} src="https://cdn.europosters.eu/image/750webp/103406.webp"></img>
                </div>
            </div>
            <div className="position-relative rounded p-2 mt-1 shadow border border-white bg-black">
                <h6 className="w-100 d-inline-block text-truncate text-white">
                    <FontAwesomeIcon icon="fa-solid fa-caret-right" className='me-1' />
                    Mandalorian Mandalorian Mandalorian Mandalorian
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
                        <span className='text-primary' title='IMDB'>8.2</span>
                        <span className='text-white ms-1 me-1'>|</span>
                        <span className='text-success' title='MyCineList'>7.3</span>
                    </div>
                </div>
            </div>
        </div>
    )
}