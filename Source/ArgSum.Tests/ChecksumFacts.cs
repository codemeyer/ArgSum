using FluentAssertions;
using Xunit;

namespace ArgSum.Tests
{
    public class ChecksumFacts
    {
        [Fact]
        public void CalculateChecksum_DoesWhatItShould()
        {
            var bytes = new byte[] {1, 2, 3, 4, 5, 6, 7, 8};
            var checksumCalculator = new ChecksumCalculator();

            var checksum = checksumCalculator.Calculate(bytes);

            checksum.Checksum1.Should().Be(36);
            checksum.Checksum2.Should().Be(52201);
        }
    }
}
