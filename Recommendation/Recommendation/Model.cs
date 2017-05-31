using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommendation
{
    class Model
    {
        Dictionary<string, List<string> > moviesRecommendations; 

        public Model()
        {
            initRecommendations();
        }

        private void initRecommendations()
        {
            string line;
            string movie;
            string movieToRecommend;
            List<string> moviesToRecommend;
            moviesRecommendations = new Dictionary<string, List<string>>();
            using (FileStream fs = File.OpenRead("recommendation.txt"))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        moviesToRecommend = new List<string>();
                        movie = line.Substring(0, line.IndexOf(','));
                        string[] moviesArray = line.Split(',');
                        for (int i = 1; i < moviesArray.Length-1; i++)
                        {
                            moviesToRecommend.Add(moviesArray[i]);
                        }

                        if (!moviesRecommendations.ContainsKey(movie))
                            moviesRecommendations.Add(movie, moviesToRecommend);
                    }
                }
            }
        }

        public List<string> getRecommendation(string movieName)
        {
            if (moviesRecommendations.ContainsKey(movieName))
                return moviesRecommendations[movieName];
            return null;
        }
    }
}
