using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcellist.TDD
{
    [TestFixture]
    public class PackageTests
    {
        [InlineAutoData(0, 1, 1)]
        public void If_dimensions_are_nonsense_Then_throws(int d1, int d2, int d3, decimal cost, string name)
        {
            Assert.Throws<ArgumentException>(() => new Package(name, d1, d2, d3, cost));
        }
    }
}
