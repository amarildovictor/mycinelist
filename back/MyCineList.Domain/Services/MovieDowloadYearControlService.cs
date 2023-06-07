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
                List<MovieDowloadYearControl>? movieDowloadYearControls = MovieDowloadYearControlRepo.GetNextCall() ?? new List<MovieDowloadYearControl>();

                foreach (MovieDowloadYearControl movieDowloadYearControl in movieDowloadYearControls)
                {
                    await AddRangeWithJSONResponseString($"/titles?titleType=movie&year={movieDowloadYearControl.Year}&info=base_info&limit=50");

                    movieDowloadYearControl.InfoUpdateDate = DateTime.Now;
                    movieDowloadYearControl.ToUpdateNextCall = false;
                    MovieDowloadYearControlRepo.Update(movieDowloadYearControl);
                    await MovieDowloadYearControlRepo.SaveChangesAsync();
                }

                if (movieDowloadYearControls.Count() > 0)
                {
                    await StartUpdateResizingImages();
                }
            }
            catch { throw; }
        }

        public async Task StartUpdateUpcoming(){
            try
            {
                await AddRangeWithJSONResponseString($"/titles/x/upcoming?titleType=movie&info=base_info&limit=50");
                await StartUpdateResizingImages();
            }
            catch { throw; }
        }

        public async Task StartUpdateResizingImages(){
            try
            {
                using (var client = new HttpClient())
                {
                    var request = GetRequestResizingImagesFunctionAPI();
                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();

                        var responseObj = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch { throw; }
        }

        private async Task AddRangeWithJSONResponseString(string? next)
        {
            var client = new HttpClient();
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
                            //next = movieJsonResponse.page < 50 ? movieJsonResponse.next : null;
                            next=null;
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
                RequestUri = new Uri($"{Environment.GetEnvironmentVariable("RAPIDAPI_MOVIESDATABASE_URL")}{next}"),
                Headers =
                {
                    { "X-RapidAPI-Key", Environment.GetEnvironmentVariable("X-RapidAPI-Key") },
                    { "X-RapidAPI-Host", Environment.GetEnvironmentVariable("X-RapidAPI-Host") },
                },
            };
        }

        private HttpRequestMessage GetRequestResizingImagesFunctionAPI()
        {
            return new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Environment.GetEnvironmentVariable("RESIZING_IMAGES_FUNCTION_URL")!)
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