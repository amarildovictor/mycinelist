import Button from 'react-bootstrap/Button';
import Carousel from 'react-bootstrap/Carousel';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import MovieCard from '../components/MovieCard';

export default function Home() {
    return (
        <>
            <div className='d-flex justify-content-center flex-wrap'><MovieCard /><MovieCard /><MovieCard /><MovieCard /><MovieCard /></div>
            <div className='d-flex justify-content-center flex-wrap'>
                <div className='w-25 me-3' style={{minWidth:'355px'}}>
                    <h6 className='d-flex justify-content-center mt-3 mb-3 ms-4 me-4 pb-1 border-bottom rounded-pill'>
                        <FontAwesomeIcon className='me-2 mt-1' icon="fa-solid fa-angles-right" />
                        <span>Em Breve</span>
                        <FontAwesomeIcon className='ms-2 mt-1' icon="fa-solid fa-angles-left" />
                    </h6>
                    <Carousel interval={null}>
                        <Carousel.Item>
                            <img
                            className="d-block w-100"
                            src="https://www.adobe.com/br/express/create/media_127a4cd0c28c2753638768caf8967503d38d01e4c.jpeg?width=400&format=jpeg&optimize=medium"
                            alt="First slide"
                            />
                            <Carousel.Caption>
                            <h3>First slide label</h3>
                            <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                        <Carousel.Item>
                            <img
                            className="d-block w-100"
                            src="https://play-lh.googleusercontent.com/DTzWtkxfnKwFO3ruybY1SKjJQnLYeuK3KmQmwV5OQ3dULr5iXxeEtzBLceultrKTIUTr"
                            alt="Second slide"
                            />

                            <Carousel.Caption>
                            <h3>Second slide label</h3>
                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                        <Carousel.Item>
                            <img
                            className="d-block w-100"
                            src="https://www.tailorbrands.com/wp-content/uploads/2020/07/mcdonalds-logo.jpg"
                            alt="Third slide"
                            />

                            <Carousel.Caption>
                            <h3>Third slide label</h3>
                            <p>
                                Praesent commodo cursus magna, vel scelerisque nisl consectetur.
                            </p>
                            </Carousel.Caption>
                        </Carousel.Item>
                    </Carousel>
                </div>
                <div className='w-25 me-3' style={{minWidth:'355px'}}>
                    <h6 className='d-flex justify-content-center mt-3 mb-3 ms-4 me-4 pb-1 border-bottom rounded-pill'>
                        <FontAwesomeIcon className='me-2 mt-1' icon="fa-solid fa-angles-right" />
                        <span>Estreias</span>
                        <FontAwesomeIcon className='ms-2 mt-1' icon="fa-solid fa-angles-left" />
                    </h6>
                    <Carousel interval={null}>
                        <Carousel.Item>
                            <img
                            className="d-block w-100"
                            src="https://www.adobe.com/br/express/create/media_127a4cd0c28c2753638768caf8967503d38d01e4c.jpeg?width=400&format=jpeg&optimize=medium"
                            alt="First slide"
                            />
                            <Carousel.Caption>
                            <h3>First slide label</h3>
                            <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                        <Carousel.Item>
                            <img
                            className="d-block w-100"
                            src="https://play-lh.googleusercontent.com/DTzWtkxfnKwFO3ruybY1SKjJQnLYeuK3KmQmwV5OQ3dULr5iXxeEtzBLceultrKTIUTr"
                            alt="Second slide"
                            />

                            <Carousel.Caption>
                            <h3>Second slide label</h3>
                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                        <Carousel.Item>
                            <img
                            className="d-block w-100"
                            src="https://www.tailorbrands.com/wp-content/uploads/2020/07/mcdonalds-logo.jpg"
                            alt="Third slide"
                            />

                            <Carousel.Caption>
                            <h3>Third slide label</h3>
                            <p>
                                Praesent commodo cursus magna, vel scelerisque nisl consectetur.
                            </p>
                            </Carousel.Caption>
                        </Carousel.Item>
                    </Carousel>
                </div>
                <div className='w-25' style={{minWidth:'355px'}}>
                    <h6 className='d-flex justify-content-center mt-3 mb-3 ms-4 me-4 pb-1 border-bottom rounded-pill'>
                        <FontAwesomeIcon className='me-2 mt-1' icon="fa-solid fa-angles-right" />
                        <span>Em Cartaz</span>
                        <FontAwesomeIcon className='ms-2 mt-1' icon="fa-solid fa-angles-left" />
                    </h6>
                    <Carousel interval={null}>
                        <Carousel.Item>
                            <img
                            className="d-block w-100"
                            src="https://www.adobe.com/br/express/create/media_127a4cd0c28c2753638768caf8967503d38d01e4c.jpeg?width=400&format=jpeg&optimize=medium"
                            alt="First slide"
                            />
                            <Carousel.Caption>
                            <h3>First slide label</h3>
                            <p>Nulla vitae elit libero, a pharetra augue mollis interdum.</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                        <Carousel.Item>
                            <img
                            className="d-block w-100"
                            src="https://play-lh.googleusercontent.com/DTzWtkxfnKwFO3ruybY1SKjJQnLYeuK3KmQmwV5OQ3dULr5iXxeEtzBLceultrKTIUTr"
                            alt="Second slide"
                            />

                            <Carousel.Caption>
                            <h3>Second slide label</h3>
                            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                            </Carousel.Caption>
                        </Carousel.Item>
                        <Carousel.Item>
                            <img
                            className="d-block w-100"
                            src="https://www.tailorbrands.com/wp-content/uploads/2020/07/mcdonalds-logo.jpg"
                            alt="Third slide"
                            />

                            <Carousel.Caption>
                            <h3>Third slide label</h3>
                            <p>
                                Praesent commodo cursus magna, vel scelerisque nisl consectetur.
                            </p>
                            </Carousel.Caption>
                        </Carousel.Item>
                    </Carousel>
                </div>
            </div>
            <div className='d-flex justify-content-center mt-3'>
                <Button variant="primary">
                    <FontAwesomeIcon className='me-2' icon="fa-solid fa-book-open" />
                    Acesse o catálogo de filmes completo aqui!
                </Button>
            </div>
        </>
    )
}