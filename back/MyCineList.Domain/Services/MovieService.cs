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
    public class MovieService : IMovieService
    {
        public IMovieRepo MovieRepo { get; }
        
        public MovieService(IMovieRepo movieRepo)
        {
            this.MovieRepo = movieRepo;
        }

        public async Task Add(Movie movie)
        {
            try
            {
                MovieRepo.Add(movie);

                await MovieRepo.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddRange(List<Movie>? movies)
        {
            try
            {
                if (movies !=null && movies.Count > 0)
                {
                    MovieRepo.AddRange(movies);

                    await MovieRepo.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddRangeWithJSONResponseString(int year)
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
                            List<Movie>? newMoviesToAdd = await FilterNewMoviesByList(movieJsonResponse.results);

                            lastNext = next;
                            next = movieJsonResponse.page < 50 ? movieJsonResponse.next : null;

                            if (newMoviesToAdd != null && newMoviesToAdd.Count > 0)
                            {
                                await AddRange(newMoviesToAdd);
                            }
                        }
                    }
                }

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception($"O Erro ocorreu no seguinte link: {next}. Último next executado: {lastNext} Mais detalhes: {ex.Message}");
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

        private async Task<List<Movie>?> FilterNewMoviesByList(List<Movie> movies)
        {
            List<Movie>? listWithNewMovies = await MovieRepo.FilterNewMoviesByList(movies);

            return listWithNewMovies;
        }
    }
}