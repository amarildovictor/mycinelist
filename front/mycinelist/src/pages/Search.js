import { useState, useEffect } from "react";
import { getAxiosApiServer } from "../api/axiosBase";
import MovieCard from "../components/MovieCard";
import { useParams, useSearchParams } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import Button from "react-bootstrap/Button";

export default function Search(props) {
  const [movieList, setMovieList] = useState([]);
  const [searchParams] = useSearchParams();
  const searchText = searchParams.has("searchText") ? searchParams.get("searchText") : null;
  const { movieTimelineRelease } = useParams();
  const [url, setUrl] = useState(getUrl());
  const [page, setPage] = useState(1);
  const [loadedApiMovie, setLoadedApiMovie] = useState(false);
  const [loadMore, setLoadMore] = useState(false);

  function getUrl(page = 1) {
    let searchUrl = null;
    if (searchText) {
      searchUrl = `Movie/search/?searchText=${searchText}&`;
    } else if (movieTimelineRelease) {
      searchUrl = `Movie/search/timeline/${movieTimelineRelease}?`;
    } else {
      searchUrl = "Movie/search?";
    }

    return searchUrl + `page=${page}`;
  }

  useEffect(() => {
    const getMovies = async () => {
      const response = await getAxiosApiServer().get(url);
      return response.data;
    };

    const getMoviesByPage = async () => {
      const responseMovieList = await getMovies();
      if (responseMovieList) {
        if (responseMovieList.length === 30) {
          setPage(page + 1);
          setLoadMore(true);
        }
        else {
          setLoadMore(false);
        }
        
        setMovieList([...movieList, ...responseMovieList]);
        setLoadedApiMovie(true);
      }
    };
    getMoviesByPage();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [url]);

  return (
    <>
      <div className="d-flex justify-content-center flex-wrap">
        {movieList.length > 0 ? (
          movieList.map((movie) => (
            <MovieCard key={movie.id} movie={movie} logged={props.logged}></MovieCard>
          ))
        ) : loadedApiMovie ? (
          <div className="d-flex justify-content-center my-3">
            <h5>
              <FontAwesomeIcon icon="fa-solid fa-magnifying-glass" className="text-warning" />
              <span className="ms-2">Busca sem resultados!</span>
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
          <Button id="loadMore" variant="primary" onClick={() => setUrl(getUrl(page))}>
            <FontAwesomeIcon icon="fa-solid fa-down-long" />
            <span className="mx-2">Carregar Mais</span>
            <FontAwesomeIcon icon="fa-solid fa-down-long" />
          </Button>
        </div>
        : null
      }
    </>
  );
}
