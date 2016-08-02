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
            if (width < 0)
            {
                throw new ArgumentException(nameof(width));
            }

            if (breadth < 0)
            {
                throw new ArgumentException(nameof(breadth));
            }

            if (height < 0)
            {
                throw new ArgumentException(nameof(height));
            }

            if (weight < 0)
            {
                throw new ArgumentException(nameof(weight));
            }

            this.Width = width;
            this.Breadth = breadth;
            this.Height = height;
            this.Weight = weight;
        }
    }
}
