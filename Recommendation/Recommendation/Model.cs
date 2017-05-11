using System;
using System.Collections.Generic;
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
            moviesRecommendations = new Dictionary<string, List<string>>();
        }

        public List<string> getRecommendation(string movieName)
        {
            if (moviesRecommendations.ContainsKey(movieName))
                return moviesRecommendations[movieName];
            return null;
        }
    }
}
