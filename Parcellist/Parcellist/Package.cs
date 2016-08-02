using System;
using System.Linq;

namespace Parcellist
{ 
    public class Package
    {
        public Package(string name, int width, int breadth, int height, decimal cost)
        {
            if (width < 1)
            {
                throw new ArgumentException(nameof(width));
            }

            if (breadth < 1)
            {
                throw new ArgumentException(nameof(breadth));
            }

            if (height < 1)
            {
                throw new ArgumentException(nameof(height));
            }

            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Breadth = breadth;
            this.Cost = cost;
        }

        public readonly int Breadth;

        public readonly decimal Cost;

        public readonly int Height;

        public readonly string Name;

        public readonly int Width;

        public bool Fits(Parcel parcel)
        {
            var parckageDimensions = new[] { this.Width, this.Height, this.Breadth }.OrderByDescending(a => a);
            var parcelDimensions = new[] { parcel.Width, parcel.Height, parcel.Breadth }.OrderByDescending(a => a);

            return Enumerable.Range(0, parckageDimensions.Count())
                .All(i => parckageDimensions.ElementAt(i) >= parcelDimensions.ElementAt(i));
        }
    }
}