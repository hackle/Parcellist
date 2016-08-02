using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using System;

namespace Parcellist
{
    [TestFixture]
    public class ParcelTests
    {
        [InlineAutoData(-1, 0, 0, 0)]
        [InlineAutoData(0, -1, 0, 0)]
        [InlineAutoData(0, 0, -1, 0)]
        [InlineAutoData(0, 0, 0, -1)]
        public void If_dimensions_or_weight_is_nonsense_Then_throws(int d1, int d2, int d3, int weight)
        {
            Assert.Throws<ArgumentException>(() => new Parcel(d1, d2, d3, weight));
        }

        // I am going to allow sending of air
        [InlineAutoData(1, 1, 1, 1)]
        [InlineAutoData(0, 0, 0, 0)]
        public void If_dimensions_and_weigth_are_good_Then_doe_not_throws(int d1, int d2, int d3, int weight)
        {
            Assert.DoesNotThrow(() => new Parcel(d1, d2, d3, weight));
        }
    }
}
