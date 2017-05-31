using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            movieRecommendations = new Dictionary<int, List<int>>();
            computeRecommendations();
            writeRecommendations();
        }


        /// <summary>
        /// Compute recommmendation for all the movies in the database
        /// </summary>
        private void computeRecommendations()
        {
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
           int max;
            

            //compute similarity with all the other movies
            foreach (int movieID in moviesID)
            {
                if (movieToRecommend != movieID)
                {
                    Stopwatch sw = new Stopwatch();
                    rank = computeSimilarity
           ( movieToRecommend, movieID);
                    if(rank!=-2)
                        similarMovies.Add(movieID, rank);
                }
            }

            int numOfRankedMovies = similarMovies.Count();

            //recommend get the top movies and save them
            for (int i = 0; i < numOfRecommended && i < numOfRankedMovies; i++)
            {
                max = getMaxEntry(ref similarMovies);
                if (max != -1)
                recommendedMovies.Add(max);
                similarMovies.Remove(max);
            }

            return recommendedMovies;
        }

        private int getMaxEntry(ref Dictionary<int, double> similarMovies)
        {
            KeyValuePair<int, double> maxEntry=new KeyValuePair<int, double>(-5,-5);

            if (similarMovies.Count != 0)
            {
                maxEntry = similarMovies.First();
            }
            else
                return -1;


            foreach (KeyValuePair<int,double> entry in similarMovies)
            {
                if (entry.Value > maxEntry.Value)
                    maxEntry = entry;
            }
            return maxEntry.Key;
        }

        /// <summary>
        /// Computation of the similarity between the vectors using pearson correlation
        /// </summary>
        /// <param name="movieParams1">The parameters of the first movie</param>
        /// <param name="movieParams2">The parameters of the second movie</param>
        /// <returns></returns>
        private double computeSimilarity(int id1, int id2)
        {
            //computation of the formula here
            double sqr_xi=0, sqr_yi=0;
            double var_xi = 0, var_yi = 0;
            double avg_xi = 0, avg_yi = 0;
            double denominator = 0;
            double numerator = 0;
            int i=0, j=0;
            int counter = 0;
            double userRankMovie1 = 0;
            double userRankMovie2 = 0;
            double numOfEquals = 0;

            List<UserRank> movie1Rank=new List<UserRank>(), movie2Rank = new List<UserRank>();

            DB.getMovieVector(id1, ref movie1Rank);
            DB.getMovieVector(id2, ref movie2Rank);

            while(i < movie1Rank.Count && j < movie2Rank.Count())
            {
                if (movie1Rank[i].UserID == movie2Rank[j].UserID) //case its the same user
                {
                    userRankMovie1 = movie1Rank[i].Rating;
                    userRankMovie2 = movie2Rank[j].Rating;

                    if (userRankMovie1 != 0 && userRankMovie2 != 0)
                    {
                        numerator += movie1Rank[i].Rating * movie2Rank[j].Rating;
                        sqr_xi += userRankMovie1 * userRankMovie1;
                        sqr_yi += userRankMovie2 * userRankMovie2;
                        if (userRankMovie1 == userRankMovie2)
                        {
                            numOfEquals++;
                        }
                        avg_xi += userRankMovie1;
                        avg_yi += userRankMovie2;
                        counter++;
                    }
                    i++;
                    j++;

                }
                else if (movie1Rank[i].UserID > movie2Rank[j].UserID)
                    j++;
                else
                    i++;
            }

            if (avg_xi == 0)
            {
                return -2;
            }

            avg_xi = avg_xi / counter;
            avg_yi = avg_yi / counter;

            numerator = numerator - counter * avg_xi * avg_yi;
            var_xi = Math.Sqrt(sqr_xi - counter * avg_xi * avg_xi);
            var_yi = Math.Sqrt(sqr_yi - counter * avg_yi * avg_yi);
            denominator = var_xi * var_yi;
            double rank = numerator / denominator;
            if (counter < 20)
                rank -= 1;
            if (counter < 250)
                rank -= 0.5;
            return rank;
        }

        public List<string> getRecommendedMovies(int movieID)
        {
            List<string> moviesToReturn = new List<string>();
            foreach(int _movieID in movieRecommendations[movieID])
            {
                moviesToReturn.Add(DB.movies[_movieID]);
            }
            return moviesToReturn;
        }

        public void writeRecommendations()
        {
            List<String> moviesToWrite;
            using (FileStream fs = File.OpenWrite(@"C:\Users\Kulik\Desktop\movieDB\recommendation2.txt"))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    foreach (KeyValuePair<int, List<int>> movieRecommendation in movieRecommendations)
                    {
                        writer.Write(DB.movies[movieRecommendation.Key]+",");
                        moviesToWrite = getRecommendedMovies(movieRecommendation.Key);
                        foreach (string recommendedMovie in moviesToWrite)
                        {
                            writer.Write(recommendedMovie + ",");
                        }
                        writer.WriteLine();
                    }
                }
            }
        }
    }
}
