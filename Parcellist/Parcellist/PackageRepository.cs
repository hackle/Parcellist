using System;
using System.Collections.Generic;

namespace Parcellist
{
    public class PackageRepository : IPackageRepository
    {
        private static IList<Package> packages = new Package[]
        {
            new Package("Small package", 210, 280, 130, 5M),
            new Package("Medium package", 280, 390, 180, 7.5M),
            new Package("Large package", 380, 550, 200, 8.5M)
        };

        public IEnumerable<Package> Get()
        {
            return packages;
        }
    }
}