using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommendation
{
    /// <summary>
    /// The class is used to rank the different movies saves for each movie the movies to recommend
    /// </summary>
    public class Ranker
    {
        int numOfMovies;
        public Ranker(int _numOfMovies)
        {
            numOfMovies = _numOfMovies;
        }

        private List<String> getRecommendedMovies(List<UserRank> movieUsersRank)
        {

            return null;
        }
    }
}
