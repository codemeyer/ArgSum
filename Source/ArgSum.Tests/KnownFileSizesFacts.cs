using FluentAssertions;
using Xunit;

namespace ArgSum.Tests
{
    public class KnownFileSizesFacts
    {
        [Fact]
        public void KnownFile_ReturnsTrue()
        {
            var status = KnownFiles.IsKnownFile(16924);

            status.IsKnown.Should().Be(KnownState.Exact);
            status.Description.Should().Contain("Phoenix");
        }

        [Fact]
        public void UnknownFile_ReturnsFalse()
        {
            var status = KnownFiles.IsKnownFile(123456);

            status.IsKnown.Should().Be(KnownState.Unknown);
        }

        [Fact]
        public void KnownApproxFile_IsProbablyTrackFile_BottomOfRange()
        {
            var status = KnownFiles.IsKnownFile(12000);

            status.IsKnown.Should().Be(KnownState.Probable);
            status.Description.Should().Be("an F1GP Track File?");
        }

        [Fact]
        public void KnownApproxFile_IsProbablyTrackFile_TopOfRange()
        {
            var status = KnownFiles.IsKnownFile(24000);

            status.IsKnown.Should().Be(KnownState.Probable);
            status.Description.Should().Be("an F1GP Track File?");
        }
    }
}
