import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useState } from "react";
import { getAxiosApiServer } from "../api/axiosBase";
import $ from 'jquery';

const initialStars = (rating) => {
    let array = [];

    for (let i = 0; i < 5; i++) {
        array.push(i < rating);
    }

    return array;
}

export default function Stars(props) {
    const [stars, setStars] = useState(initialStars(props.movie.userRating));
    const [userRating, setUserRating] = useState(props.movie.userRating);

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
    )
}