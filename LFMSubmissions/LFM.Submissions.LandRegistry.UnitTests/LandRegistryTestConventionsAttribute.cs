using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;
using Ploeh.AutoFixture.Xunit;

namespace LFM.Submissions.LandRegistry.UnitTests
{
    public class LandRegistryTestConventionsAttribute : AutoDataAttribute
    {
        public LandRegistryTestConventionsAttribute()
            : base(new Fixture().Customize((new AutoFakeItEasyCustomization())))
        {
        }
    }
}
