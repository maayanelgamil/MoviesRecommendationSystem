using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommendation
{
    class UserRank
    {
        private int userID;
        private double rating;

        public UserRank(int userID, double rating)
        {
            this.userID = userID;
            this.rating = rating;
        }
    }
}
