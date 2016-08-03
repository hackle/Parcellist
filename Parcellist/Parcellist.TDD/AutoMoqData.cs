using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.NUnit3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcellist.TDD
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }

    public class InlineAutoMoqData : InlineAutoDataAttribute
    {
        public InlineAutoMoqData(params object[] arguments) : base(new Fixture().Customize(new AutoMoqCustomization()), arguments)
        {
        }
    }
}
