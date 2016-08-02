using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcellist
{
    public class ParcelAdvisor
    {
        private IList<Package> packages = new Package[]
        {
            new Package("Small package", 210, 280, 130, 5M),
            new Package("Medium package", 280, 390, 180, 7.5M),
            new Package("Large package", 380, 550, 200, 8.5M)
        };

        public const decimal MaxWeight = 25M;

        public Package Advise(Parcel parcel)
        {
            if (null == parcel)
            {
                throw new ArgumentNullException(nameof(parcel));
            }

            if (parcel.Weight >= MaxWeight)
            {
                return null;
            }

            return this.packages.FirstOrDefault(p => p.Fits(parcel));
        }
    }
}
