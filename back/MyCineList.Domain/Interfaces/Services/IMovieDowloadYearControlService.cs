using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCineList.Domain.Interfaces.Services
{
    public interface IMovieDowloadYearControlService
    {
        /// <summary>
        /// Similar to Movie Service AddRange. 
        /// This service update the database with the movies of a public webservice. It obtains the movies list by year.
        /// The internal process is responsable to Deserialize the JSON string response to C# object.
        /// </summary>
        Task StartUpdateMovieCatalog();
    }
}