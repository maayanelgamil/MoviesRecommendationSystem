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

        public int UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        public double Rating
        {
            get
            {
                return rating;
            }

            set
            {
                rating = value;
            }
        }

        public UserRank(int userID, double rating)
        {
            this.UserID = userID;
            this.Rating = rating;
        }
    }
}
