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
    public class ParcelAdvisorTests
    {
        [InlineAutoData(210, 280, 130, 50)]
        [InlineAutoData(280, 390, 180, 75)]
        [InlineAutoData(380, 550, 200, 85)]
        public void Can_advise_tight_fits_regardless_of_order_of_dimensions(int d1, int d2, int d3, int cost, ParcelAdvisor advisor)
        {
            var dimensions = new[] { d1, d2, d3 }.OrderBy(d => Guid.NewGuid()).ToArray();

            var parcel = new Parcel(dimensions[0], dimensions[1], dimensions[2], 5.2M);

            var offer = advisor.Advise(parcel);

            // cost is 10 times of actual for sake of unit testing
            Assert.That(offer.Cost, Is.EqualTo(cost / 10M));
        }
    }
}
