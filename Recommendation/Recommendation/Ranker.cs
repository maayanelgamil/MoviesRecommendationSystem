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
        Dictionary<int,MovieParams> moviesComputedParams;
        Dictionary<int, List<int>> movieRecommendations;
        int numOfRecommended;
        List<int> moviesID;

        /// <summary>
        /// C'tor for the ranker
        /// </summary>
        /// <param name="_numOfMovies">The number of movies in the database</param>
        /// <param name="_numOfRecommended">The number of movies to recommend for each movie</param>
        public Ranker( int _numOfRecommended)
        {
            DB = new DBconnection();
            numOfRecommended = _numOfRecommended;
            moviesID = DB.getMoviesIDs();
            moviesComputedParams = new Dictionary<int, MovieParams>();
            movieRecommendations = new Dictionary<int, List<int>>();
            computeRecommendations();
        }


        /// <summary>
        /// Computes the parameters needed for pearson correlation for all the movies in the database
        /// </summary>
        private void computeAllParams()
        {
            List<UserRank> movieUsersRank;
            foreach (int id in moviesID)
            {
                movieUsersRank = new List<UserRank>();
                DB.getMovieVector(id, ref movieUsersRank);
                moviesComputedParams[id] = computeMovieParams(ref movieUsersRank);
            }
        }

        /// <summary>
        /// Computes the movie parameters for a single movie
        /// </summary>
        /// <param name="movieUsersRank">The list of users ranking to update</param>
        /// <returns></returns>
        private MovieParams computeMovieParams(ref List<UserRank> movieUsersRank)
        {
            MovieParams mp = new MovieParams();
            double sqr_xi = 0;
            double sum_xi = 0;

            foreach (UserRank rank in movieUsersRank)
            {
                sum_xi += rank.Rating;
                sqr_xi += rank.Rating*rank.Rating;
            }
            mp.Avg_xi = sum_xi / moviesID.Count();
            mp.Var_xi = moviesID.Count() * sqr_xi - sum_xi * sum_xi;

            return mp;
        }

        /// <summary>
        /// Compute recommmendation for all the movies in the database
        /// </summary>
        private void computeRecommendations()
        {
            computeAllParams();
            foreach (int movieID in moviesID)
            {  
                movieRecommendations.Add(movieID, computeMovieRecommendation(movieID));
            }                
        }

        /// <summary>
        /// Compute the recommended movies for the movie to recommend
        /// </summary>
        /// <param name="movieToRecommend">The id of the movie to recommend</param>
        /// <returns>Array of recommended movies (ids)</returns>
        private List<int> computeMovieRecommendation(int movieToRecommend)
        {
            List<int> recommendedMovies = new List<int>();
            Dictionary<int, double> similarMovies = new Dictionary<int, double>();
            double rank = 0;
            double max=0;
            

            //compute similarity with all the other movies
            foreach (int movieID in moviesID)
            {
                if (movieToRecommend != movieID)
                {
                    rank = computeSimilarity
           (moviesComputedParams[movieToRecommend], moviesComputedParams[movieID], movieToRecommend, movieID);
                }
                similarMovies.Add(movieID, rank);
            }

            int numOfRankedMovies = similarMovies.Count();

            //recommend get the top movies and save them
            for (int i = 0; i < numOfRecommended && i < numOfRankedMovies; i++)
            {
                recommendedMovies[i] = similarMovies.Max().Key;
                similarMovies.Remove(recommendedMovies[i]);
            }

            return recommendedMovies;
        }

        /// <summary>
        /// Computation of the similarity between the vectors using pearson correlation
        /// </summary>
        /// <param name="movieParams1">The parameters of the first movie</param>
        /// <param name="movieParams2">The parameters of the second movie</param>
        /// <returns></returns>
        private double computeSimilarity(MovieParams movieParams1, MovieParams movieParams2, int id1, int id2)
        {
            //computation of the formula here
            double denominator = movieParams1.Var_xi*movieParams2.Var_xi;
            double numerator = 0;
            int i=0, j=0;
            List<UserRank> movie1Rank=new List<UserRank>(), movie2Rank = new List<UserRank>();

            DB.getMovieVector(id1, ref movie1Rank);
            DB.getMovieVector(id1, ref movie2Rank);

            while(i < movie1Rank.Count && j < movie2Rank.Count())
            {
                if (movie1Rank[i].UserID == movie2Rank[j].UserID)
                {
                    numerator += movie1Rank[i].Rating * movie2Rank[j].Rating;
                    i++;
                    j++;
                }
                else if (movie1Rank[i].UserID > movie2Rank[j].UserID)
                    j++;
                else
                    i++;
            }
            numerator = numerator - moviesID.Count * movieParams1.Avg_xi * movieParams2.Avg_xi;
            return numerator / denominator;
        }

        public List<int> getRecommendedMovies(int movieID)
        {
            return movieRecommendations[movieID];
        }
    }
}
