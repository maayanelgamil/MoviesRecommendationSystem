using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommendation
{
    class DBconnection
    {
        const string DIRECTORY_PATH = "";

        /// <summary>
        /// sets the movie vector to the given list
        /// </summary>
        /// <param name="movieID">movie id</param>
        /// <param name="movieVector">the given list to change</param>
        public void getMovieVector(int movieID, ref List<UserRank> movieVector);
    }
}
