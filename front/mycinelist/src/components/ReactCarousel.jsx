import Carousel from 'react-bootstrap/Carousel';
import { useNavigate } from 'react-router-dom';

export default function ReactCarousel(props) {
    const navigate = useNavigate();

    return (
        <Carousel interval={null}>
        {
            props.list.map(item => (
                <Carousel.Item key={item.key}>
                    <div className='d-flex justify-content-center align-items-center overflow-hidden bg-black' style={{height:'520px'}}
                        onClick={() => navigate(`/movie/${item.key}`)}>
                        <img
                        className="mw-100"
                        src={item.primaryImageUrl}
                        alt={item.titleText}
                        title={item.titleText}
                        />
                    </div>
                    {
                        item.showDetails ?
                            <Carousel.Caption>
                            <h3>{item.titleText}</h3>
                            <p>{item.description}</p>
                            </Carousel.Caption>
                        : null
                    }
                </Carousel.Item>
            ))
        }
        </Carousel>
    )
}