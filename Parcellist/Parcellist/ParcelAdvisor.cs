using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcellist
{
    public class ParcelAdvisor
    {
        private IList<IPackage> packages = new IPackage[]
        {
            new SmallPackage(),
            new MediumPackage()
        };

        public IPackage Advise(Parcel parcel)
        {
            return this.packages.FirstOrDefault(p => p.Fits(parcel));
        }
    }
}
