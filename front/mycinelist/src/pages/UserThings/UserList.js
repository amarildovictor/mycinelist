import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import MovieCard from "../../components/MovieCard";
import Button from "react-bootstrap/esm/Button";
import { getAxiosApiServer } from "../../api/axiosBase";
import $ from 'jquery';
import { useEffect, useState } from "react";

export default function UserList(props) {
    const [page, setPage] = useState(1);
    const [userMovieList, setUserMovieList] = useState([]);

    useEffect(() => {
        const getMovieList = async () => {
            const response = await getAxiosApiServer().get("/UserApi/movieList");
            return response.data;
        };

        const getMoviesByPage = async () => {
            const responseUserMovieList = await getMovieList();
            if (responseUserMovieList) {
                if (responseUserMovieList.length === 30) {
                    setPage(page + 1);
                } else {
                    $("#loadMore").hide();
                }
                setUserMovieList([...userMovieList, ...responseUserMovieList]);
            }
        };

        getMoviesByPage();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <>
            <div className="d-flex justify-content-center flex-wrap">
                {userMovieList.length > 0 ? (
                    userMovieList.map((userMovie) => (
                        <MovieCard key={userMovie.movie.id} movie={userMovie.movie} logged={props.logged}></MovieCard>
                    ))
                ) : (
                    <div className="d-flex justify-content-center my-3">
                        <h5>
                            <FontAwesomeIcon icon="fa-solid fa-face-sad-tear" className="text-warning" />
                            <span className="ms-2">Não há nenhum filme adicionado em sua lista!</span>
                        </h5>
                    </div>
                )}
            </div>
            <div className="d-flex justify-content-center">
                <Button id="loadMore" variant="primary" >
                    <FontAwesomeIcon icon="fa-solid fa-down-long" />
                    <span className="mx-2">Carregar Mais</span>
                    <FontAwesomeIcon icon="fa-solid fa-down-long" />
                </Button>
            </div>
        </>
    )
}