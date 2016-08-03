using Moq;
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
        private IList<Package> packages = new Package[]
        {
            new Package("Small package", 210, 280, 130, 5M),
            new Package("Medium package", 280, 390, 180, 7.5M),
            new Package("Large package", 380, 550, 200, 8.5M)
        };

        [AutoMoqData]
        public void Must_be_given_a_valid_parcel(ParcelAdvisor advisor)
        {
            Assert.Throws<ArgumentNullException>(() => advisor.Advise(null) );
        }

        [InlineAutoMoqData(1, 1, 1, 50)]
        [InlineAutoMoqData(210, 280, 130, 50)]
        [InlineAutoMoqData(245, 300, 145, 75)]
        [InlineAutoMoqData(280, 390, 180, 75)]
        [InlineAutoMoqData(300, 400, 190, 85)]
        [InlineAutoMoqData(380, 550, 200, 85)]
        public void Can_advise_tight_fits_regardless_of_order_of_dimensions(
            int d1, 
            int d2, 
            int d3, 
            int cost, 
            [Frozen] IPackageRepository packageRepo,
            ParcelAdvisor advisor)
        {
            Mock.Get(packageRepo).Setup(p => p.Get()).Returns(packages);

            // shuffle
            var dimensions = new[] { d1, d2, d3 }.OrderBy(d => Guid.NewGuid()).ToArray();

            var parcel = new Parcel(dimensions[0], dimensions[1], dimensions[2], 5.2M);

            var package = advisor.Advise(parcel);

            // cost is 10 times of actual for sake of unit testing
            Assert.That(package.Cost, Is.EqualTo(cost / 10M));
        }

        [AutoMoqData]
        public void If_parcel_is_too_large_Then_gets_error([Frozen] IPackageRepository packageRepo, ParcelAdvisor advisor)
        {
            Mock.Get(packageRepo).Setup(p => p.Get()).Returns(packages);

            // just slightly too large!
            Assert.Throws<InvalidOperationException>(() => advisor.Advise(new Parcel(381, 550, 200, 5.2M)), "too large");
        }

        [AutoMoqData]
        public void If_parcel_is_too_heavy_Then_even_fits_will_gets_error(ParcelAdvisor advisor)
        {
            // just slightly too heavy!
            Assert.Throws<InvalidOperationException>(() => advisor.Advise(new Parcel(1, 1, 1, ParcelAdvisor.MaxWeight + 1)), "too heavy");
        }
    }
}
