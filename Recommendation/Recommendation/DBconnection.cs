using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommendation
{
    class DBconnection
    {
        const string DIRECTORY_PATH = @"C:\Users\ben\Desktop\לימודים\סמסטר ו\הכנה לפרוייקט\ml-20m";

        Dictionary<int, List<UserRank>> moviesVectors; // contains all the movies vectors by movie ID
        Dictionary<int, Movie> movies;// contains all the movies and their details

        public DBconnection()
        {
            readMoviesVectors();
            readMoviesDetails();
        }

        /// <summary>
        /// creating the movies vector dictionary
        /// </summary>
        private void readMoviesVectors()
        {
            moviesVectors = new Dictionary<int, List<UserRank>>();
            using (FileStream fs = File.OpenRead(DIRECTORY_PATH+ "\\ratings.csv"))
            using (StreamReader reader = new StreamReader(fs))
            {
                string line = reader.ReadLine(); //reading headrs
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    string[] values = line.Split(',');
                    int userID = Int32.Parse(values[0]);
                    int movieID = Int32.Parse(values[1]);
                    double rating = Double.Parse(values[2]);
                    if (!moviesVectors.ContainsKey(movieID))
                    {
                        moviesVectors.Add(movieID, new List<UserRank>());
                    }
                    moviesVectors[movieID].Add(new UserRank(userID, rating));
                }
            }
        }

        /// <summary>
        /// creating the movies corpus
        /// </summary>
        private void readMoviesDetails()
        {
            movies = new Dictionary<int, Movie>();
            using (FileStream fs = File.OpenRead(DIRECTORY_PATH + "\\movies.csv"))
            using (StreamReader reader = new StreamReader(fs))
            {
                string line = reader.ReadLine(); //reading headrs
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    string[] values = line.Split(',');
                    int movieID = Int32.Parse(values[0]);

                    if (!moviesVectors.ContainsKey(movieID)) // insert onley the relevant movies
                    {
                        continue;
                    }
                    string name = values[1];
                    List<string> genres = new List<string>(values[2].Split('|'));
                    movies.Add(movieID, new Movie(movieID, name, genres));
                }
            }
        }

        /// <summary>
        /// get the list of movies id
        /// </summary>
        /// <returns>the list of movies id</returns>
        public List<int> getMoviesIDs()
        {
            return new List<int>(movies.Keys);
        }

        /// <summary>
        /// sets the movie vector to the given list
        /// </summary>
        /// <param name="movieID">movie id</param>
        /// <param name="movieVector">the given list to change</param>
        public void getMovieVector(int movieID, ref List<UserRank> movieVector)
        {
            if (movies.ContainsKey(movieID))
            {
                movieVector = moviesVectors[movieID];
            }
        }
    }
}
