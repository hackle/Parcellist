using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcellist
{
    public class Parcel
    { 
        public int Breadth { get; internal set; }
        public int Height { get; internal set; }
        public int Width { get; internal set; }
        public decimal Weight;

        public Parcel(int width, int breadth, int height, decimal weight)
        {
            this.Width = width;
            this.Breadth = breadth;
            this.Height = height;
            this.Weight = weight;
        }
    }
}
