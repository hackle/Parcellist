using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcellist
{
    public interface IPackageRepository
    {
        IEnumerable<Package> Get();
    }
}
