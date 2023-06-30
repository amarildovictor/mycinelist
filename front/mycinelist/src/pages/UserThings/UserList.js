import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import MovieCard from "../../components/MovieCard";
import Button from "react-bootstrap/esm/Button";
import { getAxiosApiServer } from "../../api/axiosBase";
import { useEffect, useState } from "react";

export default function UserList(props) {
    const [page, setPage] = useState(1);
    const [userMovieList, setUserMovieList] = useState([]);
    const [loadedApiMovie, setLoadedApiMovie] = useState(false);
    const [loadMore, setLoadMore] = useState(false);

    useEffect(() => {
        if (props.logged) {
            const getMovieList = async () => {
                const response = await getAxiosApiServer().get("/UserApi/movieList");
                return response.data;
            };

            const getMoviesByPage = async () => {
                const responseUserMovieList = await getMovieList();
                if (responseUserMovieList) {
                    if (responseUserMovieList.length === 30) {
                        setPage(page + 1);
                        setLoadMore(true);
                    }
                    else {
                        setLoadMore(false);
                    }

                    setUserMovieList([...userMovieList, ...responseUserMovieList]);
                    setLoadedApiMovie(true);
                }
            };

            getMoviesByPage();
        }
        else {
            window.location.href = "/";
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [props.logged]);

    return (
        <>
            <div className="d-flex justify-content-center flex-wrap">
                {userMovieList.length > 0 ? (
                    userMovieList.map((userMovie) => (
                        <MovieCard key={userMovie.movie.id} movie={userMovie.movie} logged={props.logged}></MovieCard>
                    ))
                ) : loadedApiMovie ? (
                    <div className="d-flex justify-content-center my-3">
                        <h5>
                            <FontAwesomeIcon icon="fa-solid fa-face-sad-tear" className="text-warning" />
                            <span className="ms-2">Não há nenhum filme adicionado em sua lista!</span>
                        </h5>
                    </div>
                ) : (
                    <div className="d-flex justify-content-center align-items-center my-3">
                        <div className="spinner-border text-primary" role="status"></div>
                        <span className="ms-2 text-primary">Aguarde. Carregando a lista...</span>
                    </div>
                )}
            </div>
            {(loadMore && loadedApiMovie) ?
                <div className="d-flex justify-content-center">
                    <Button id="loadMore" variant="primary" >
                        <FontAwesomeIcon icon="fa-solid fa-down-long" />
                        <span className="mx-2">Carregar Mais</span>
                        <FontAwesomeIcon icon="fa-solid fa-down-long" />
                    </Button>
                </div>
                : null
            }
        </>
    )
}