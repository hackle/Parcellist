using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcellist
{
    public class ParcelAdvisor
    {
        public const decimal MaxWeight = 25M;
        private readonly IPackageRepository packageRepository;

        public ParcelAdvisor(IPackageRepository packageRepository)
        {
            if (null == packageRepository)
            {
                throw new ArgumentNullException(nameof(packageRepository));
            }

            this.packageRepository = packageRepository;
        }

        public Package Advise(Parcel parcel)
        {
            if (null == parcel)
            {
                throw new ArgumentNullException(nameof(parcel));
            }

            if (parcel.Weight >= MaxWeight)
            {
                throw new InvalidOperationException("This parcel is too heavy");
            }

            var package = this.packageRepository.Get()?.FirstOrDefault(p => p.Fits(parcel));

            if (null == package)
            {
                throw new InvalidOperationException("This parcel is too large");
            }

            return package;
        }
    }
}
