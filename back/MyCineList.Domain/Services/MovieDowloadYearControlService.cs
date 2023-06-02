using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCineList.Domain.Entities;
using MyCineList.Domain.Interfaces.Repositories;
using MyCineList.Domain.Interfaces.Services;
using Newtonsoft.Json;

namespace MyCineList.Domain.Services
{
    public class MovieDowloadYearControlService : IMovieDowloadYearControlService
    {
        public IMovieDowloadYearControlRepo MovieDowloadYearControlRepo { get; }

        public IMovieRepo MovieRepo { get; }

        public IMovieService MovieService { get; }
        
        public MovieDowloadYearControlService(IMovieDowloadYearControlRepo movieDowloadYearControlRepo, IMovieRepo movieRepo, IMovieService movieService)
        {
            this.MovieDowloadYearControlRepo = movieDowloadYearControlRepo;
            this.MovieService = movieService;
            this.MovieRepo = movieRepo;
        }

        public async Task StartUpdateMovieCatalog(){
            try
            {
                List<MovieDowloadYearControl>? movieDowloadYearControls = MovieDowloadYearControlRepo.GetNextCall();

                foreach (MovieDowloadYearControl movieDowloadYearControl in movieDowloadYearControls ?? new List<MovieDowloadYearControl>())
                {
                    await AddRangeWithJSONResponseString(movieDowloadYearControl.Year);

                    movieDowloadYearControl.InfoUpdateDate = DateTime.Now;
                    movieDowloadYearControl.ToUpdateNextCall = false;
                    MovieDowloadYearControlRepo.Update(movieDowloadYearControl);
                    await MovieDowloadYearControlRepo.SaveChangesAsync();
                }

            }
            catch { throw; }
        }

        private async Task AddRangeWithJSONResponseString(int year)
        {
            var client = new HttpClient();
            string? next = $"/titles?titleType=movie&year={year}&info=base_info&limit=50";
            string? lastNext = string.Empty;

            try
            {
                while (next != null)
                {
                    var request = GetRequestMovieDataBaseAPI(next);
                    
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();
                        var moviesJSONResponseString = await response.Content.ReadAsStringAsync();

                        DefaultJSONResponse<Movie>? movieJsonResponse = JsonConvert.DeserializeObject<DefaultJSONResponse<Movie>>(moviesJSONResponseString);
                        
                        if (movieJsonResponse != null && movieJsonResponse.results != null && movieJsonResponse.results.Count > 0)
                        {
                            await NewMoviesAction(movieJsonResponse.results);

                            lastNext = next;
                            next = movieJsonResponse.page < 50 ? movieJsonResponse.next : null;

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"O Erro ocorreu no seguinte link: {next}. Último next executado: {lastNext}.", ex);
            }
        }

        private async Task NewMoviesAction(List<Movie>? movieJsonResponseResults)
        {
            List<Movie>? newMoviesToAdd = movieJsonResponseResults;
            List<Movie>? newMoviesToUpdate = await FilterNewMoviesByList(newMoviesToAdd);

            if (newMoviesToAdd != null && newMoviesToAdd.Count > 0)
            {
                await MovieService.AddRange(newMoviesToAdd);
            }

            if (newMoviesToUpdate != null && newMoviesToUpdate.Count > 0)
            {
                await MovieService.UpdateRange(newMoviesToUpdate);
            }
        }

        private HttpRequestMessage GetRequestMovieDataBaseAPI(string next)
        {
            return new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://moviesdatabase.p.rapidapi.com{next}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "b643484d4emsh1f7f6f3e0fa55cep15bad6jsn5e933dc5607a" },
                    { "X-RapidAPI-Host", "moviesdatabase.p.rapidapi.com" },
                },
            };
        }

        private async Task<List<Movie>?> FilterNewMoviesByList(List<Movie>? movies)
        {
            List<Movie>? moviesList = await MovieRepo.FilterNewMoviesByList(movies);

            moviesList?.ForEach(x => movies?.RemoveAll(y => y.IMDBID == x.IMDBID));

            return moviesList;
        }
    }
}