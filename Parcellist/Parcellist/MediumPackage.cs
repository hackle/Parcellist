using System;

namespace Parcellist
{
    public class MediumPackage : IPackage
    {
        public int Breadth
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public decimal Cost
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Height
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Width
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Fits(Parcel parcel)
        {
            throw new NotImplementedException();
        }
    }
}