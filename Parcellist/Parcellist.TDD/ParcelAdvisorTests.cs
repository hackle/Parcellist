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
            new Package("Medium package", 280, 390, 180, 7.5M),
            new Package("Small package", 210, 280, 130, 5M),
            new Package("Large package", 380, 550, 200, 8.5M)
        };

        [Test]
        public void Must_be_constructed_with_a_valid_parcel_repo()
        {
            Assert.Throws<ArgumentNullException>(() => new ParcelAdvisor(null));
        }

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
            decimal maxWeight,
            [Frozen] IPackageRepository packageRepo,
            ParcelAdvisor advisor)
        {
            Mock.Get(packageRepo).Setup(p => p.GetPackages()).Returns(packages.Shuffle());
            Mock.Get(packageRepo).Setup(p => p.GetMaxWeight()).Returns(maxWeight);

            // shuffle
            var allowedWeight = maxWeight - 1;
            var dimensions = new[] { d1, d2, d3 }.Shuffle().ToArray();

            var parcel = new Parcel(dimensions[0], dimensions[1], dimensions[2], allowedWeight);

            var package = advisor.Advise(parcel);

            // cost is 10 times of actual for sake of unit testing
            Assert.That(package.Cost, Is.EqualTo(cost / 10M));
        }

        [AutoMoqData]
        public void If_packages_are_not_available_Then_gets_error(
            Parcel parcel, 
            [Frozen] IPackageRepository packageRepo, 
            ParcelAdvisor advisor)
        {
            Mock.Get(packageRepo).Setup(p => p.GetPackages()).Returns(() => null);
            
            Assert.Throws<InvalidOperationException>(() => advisor.Advise(parcel), "no package");
        }

        [AutoMoqData]
        public void If_parcel_is_too_large_Then_gets_error(
            decimal maxWeight,
            [Frozen] IPackageRepository packageRepo, 
            ParcelAdvisor advisor)
        {
            Mock.Get(packageRepo).Setup(p => p.GetPackages()).Returns(packages);
            Mock.Get(packageRepo).Setup(p => p.GetMaxWeight()).Returns(maxWeight);

            // just slightly too large!
            Assert.Throws<InvalidOperationException>(() => advisor.Advise(new Parcel(381, 550, 200, maxWeight - 1)), "no package");
        }

        [AutoMoqData]
        public void If_parcel_is_too_heavy_Then_even_fits_will_get_error(
            decimal maxWeight, 
            [Frozen] IPackageRepository packageRepo, 
            ParcelAdvisor advisor)
        {
            Mock.Get(packageRepo).Setup(r => r.GetMaxWeight()).Returns(maxWeight);
            // just slightly too heavy!
            Assert.Throws<InvalidOperationException>(() => advisor.Advise(new Parcel(1, 1, 1, maxWeight + 1)), "too heavy");
        }
    }

    public static class EnumerableExtension
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> @this)
        {
            return @this.OrderBy((t) => Guid.NewGuid());
        }
    }
}
