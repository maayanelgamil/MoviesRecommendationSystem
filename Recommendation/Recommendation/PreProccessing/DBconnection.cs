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
        Dictionary<int, string> movies;// contains all the movies and their details

        public DBconnection()
        {
            readMoviesVectors();
            readMoviesNames();
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
        private void readMoviesNames()
        {
            movies = new Dictionary<int, string>();
            using (FileStream fs = File.OpenRead(DIRECTORY_PATH + "\\movies.csv"))
            using (StreamReader reader = new StreamReader(fs))
            {
                string line = reader.ReadLine(); //reading headrs
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (line.IndexOf('(') != line.LastIndexOf('('))
                    {
                        // removes movies other names
                        line = line.Remove(line.IndexOf('('), line.LastIndexOf('(') - line.IndexOf('(') + 1);
                    }

                    if (line.IndexOf('(') == -1)
                    {
                        // skip movies with no year
                        continue;
                    }
                    string[] values = line.Split(',');
                    int movieID = Int32.Parse(values[0]);

                    if (!moviesVectors.ContainsKey(movieID)) // insert onley the relevant movies
                    {
                        // skip movies with no data
                        continue;
                    }

                    string name = "";

                    if (values.Length == 4)// starts with "the"
                    {
                        name = values[1].Trim('\"');
                        string start = values[2].Substring(0, values[2].IndexOf('('));
                        name = (start + name);
                    }
                    else if (values.Length == 3 && values[1].IndexOf('(')!=-1) // don't start with "the"
                    {
                        name = values[1].Substring(0, values[1].IndexOf('(') - 1);
                    }
                    else // skip movies with bad names
                    {
                        continue;
                    }

                    movies.Add(movieID, name.Trim().ToLower());
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
