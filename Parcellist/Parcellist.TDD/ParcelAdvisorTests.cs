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
        [AutoData]
        public void When_parcel_sizes_match_that_of_a_small_package_Then_a_small_pacakge_is_advised(ParcelAdvisor advisor)
        {
            var parcel = new Parcel(210, 280, 130, 5.2M);

            var offer = advisor.Advise(parcel);

            Assert.That(offer, Is.TypeOf<SmallPackage>());
        }

        [AutoData]
        public void When_parcel_sizes_match_that_of_a_medium_package_Then_a_medium_pacakge_is_advised(ParcelAdvisor advisor)
        {
            var parcel = new Parcel(280, 390, 180, 5.2M);

            var offer = advisor.Advise(parcel);

            Assert.That(offer, Is.TypeOf<MediumPackage>());
        }
    }
}
