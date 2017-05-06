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
        DBconnection DB;
        int numOfMovies;
        Dictionary<String, MovieParams> moviesComputeParams;
        public Ranker(int _numOfMovies)
        {
            numOfMovies = _numOfMovies;
            DB = new DBconnection();
            moviesComputeParams = new Dictionary<string, MovieParams>();
        }

        private void rankMovies() { }

        private List<String> getRecommendedMovies(String movieID)
        {
            return null;
        }
    }
}
