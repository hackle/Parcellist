using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcellist
{
    public class Parcel
    {
        private int v1;
        private int v2;
        private int v3;
        private decimal v4;

        public Parcel(int v1, int v2, int v3, decimal v4)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
        }
    }
}
