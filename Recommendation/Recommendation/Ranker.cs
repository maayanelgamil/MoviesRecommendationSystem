//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Recommendation
//{
//    /// <summary>
//    /// The class is used to rank the different movies saves for each movie the movies to recommend
//    /// </summary>
//    public class Ranker
//    {
//        DBconnection DB;
//        MovieParams[] moviesComputedParams;
//        Dictionary<int, List<int>> movieRecommendations;
//        int numOfRecommended;
//        List<int> moviesID;

//        /// <summary>
//        /// C'tor for the ranker
//        /// </summary>
//        /// <param name="_numOfMovies">The number of movies in the database</param>
//        /// <param name="_numOfRecommended">The number of movies to recommend for each movie</param>
//        public Ranker( int _numOfRecommended)
//        {
//            DB = new DBconnection();
//            numOfRecommended = _numOfRecommended;
//            moviesComputedParams = new MovieParams[moviesID.Count()];
//            movieRecommendations = new int[numOfMovies][];
//        }

//        /// <summary>
//        /// Computes the parameters needed for pearson correlation for all the movies in the database
//        /// </summary>
//        private void computeAllParams()
//        {
//            List<UserRank> movieUsersRank;
//            foreach (int id in moviesID)
//            {
//                movieUsersRank = new List<UserRank>();
//                DB.getMovieVector(id, ref movieUsersRank);
//                moviesComputedParams[id] = computeMovieParams(ref movieUsersRank);
//            }
//        }

//        /// <summary>
//        /// Computes the movie parameters for a single movie
//        /// </summary>
//        /// <param name="movieUsersRank">The list of users ranking to update</param>
//        /// <returns></returns>
//        private MovieParams computeMovieParams(ref List<UserRank> movieUsersRank)
//        {
//            MovieParams mp = new MovieParams();
//            //compute here

//            return mp;
//        }

//        /// <summary>
//        /// Compute recommmendation for all the movies in the database
//        /// </summary>
//        private void computeRecommendations()
//        {
//            for (int i = 0; i < moviesComputedParams.Length; i++)
//            {
//                movieRecommendations[i] = computeMovieRecommendation(i);
//            }     
//        }

//        /// <summary>
//        /// Compute the recommended movies for the movie to recommend
//        /// </summary>
//        /// <param name="movieToRecommend">The id of the movie to recommend</param>
//        /// <returns>Array of recommended movies (ids)</returns>
//        private int[] computeMovieRecommendation(int movieToRecommend)
//        {
//            int[] recommendedMovies = new int[numOfRecommended];
//            SortedList<double, int> similarMovies = new SortedList<double, int>();
//            double rank = 0;
//            double max=0;
//            numOfMovies = similarMovies.Count;

//            //compute similarity with all the other movies
//            for (int i = 1; i <= numOfMovies; i++)
//            {
//                rank = computeSimilarity
//                    (moviesComputedParams[movieToRecommend], moviesComputedParams[i],movieToRecommend,i);
//                similarMovies.Add(rank, i);
//            }

//            //recommend get the top movies and save them
//            for (int i = 0; i < numOfRecommended && i < numOfMovies; i++)
//            {
//                max = similarMovies.Last().Key;
//                recommendedMovies[i] = similarMovies[max];
//                similarMovies.Remove(max);
//            }

//            return recommendedMovies;
//        }

//        /// <summary>
//        /// Computation of the similarity between the vectors using pearson correlation
//        /// </summary>
//        /// <param name="movieParams1">The parameters of the first movie</param>
//        /// <param name="movieParams2">The parameters of the second movie</param>
//        /// <returns></returns>
//        private double computeSimilarity(MovieParams movieParams1, MovieParams movieParams2,int id1, int id2)
//        {
//            //computation of the formula here
//            return 0;
//        }

//        //private List<String> getRecommendedMovies(String movieID)
//        //{
//        //    return null;
//        //}
//    }
//}
