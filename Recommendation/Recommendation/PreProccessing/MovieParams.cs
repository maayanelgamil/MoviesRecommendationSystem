using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommendation
{

    public class MovieParams
    {
        double var_xi;
        double avg_xi;

        public double Var_xi
        {
            get
            {
                return var_xi;
            }

            set
            {
                var_xi = value;
            }
        }

        public double Avg_xi
        {
            get
            {
                return avg_xi;
            }

            set
            {
                avg_xi = value;
            }
        }
    }
}
