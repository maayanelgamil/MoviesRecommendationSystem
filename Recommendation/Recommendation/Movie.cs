using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommendation
{
    class Movie
    {
        int movieID;
        public int MovieID
        {
            get
            {
                return movieID;
            }

            set
            {
                movieID = value;
            }
        }

        string movieName;
        public string MovieName
        {
            get
            {
                return movieName;
            }

            set
            {
                movieName = value;
            }
        }

        List<string> genres;
        public List<string> Genres
        {
            get
            {
                return genres;
            }

            set
            {
                genres = value;
            }
        }

        string year;
        public string Year
        {
            get
            {
                return year;
            }

            set
            {
                year = value;
            }
        }

        public Movie(int movieID,string movieName,string year,List<string> genres)
        {
            this.movieID = movieID;
            this.movieName = movieName;
            this.year = year;
            this.genres = genres;
        }
       
    }
}
