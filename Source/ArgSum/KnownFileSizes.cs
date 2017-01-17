using System.Collections.Generic;

namespace ArgSum
{
    public class KnownFileSizeRange
    {
        public KnownFileSizeRange(ValueRange range, string description)
        {
            Range = range;
            Description = description;
        }

        public ValueRange Range { get; private set; }
        public string Description { get; private set; }

        public bool IsInRange(long size)
        {
            return Range.From <= size && size <= Range.To;
        }
    }

    public struct ValueRange
    {
        public ValueRange(int from, int to)
        {
            From = from;
            To = to;
        }

        public long From { get; private set; }
        public long To { get; private set; }
    }

    public static class KnownFiles
    {
        private static readonly Dictionary<long, string> Files = new Dictionary<long, string>
        {
            { 14, "F1GP Setup File" },
            { 326, "F1GP Multi-Setup File" },
            { 1166, "F1GP Preferences (F1PREFS.DAT)" },
            { 1484, "F1GP Name File" },
            { 3578, "F1GP Saved Game (Between races)"  },
            { 16924, "F1GP Track Phoenix (F1CT01.DAT)" },
            { 15453, "F1GP Track Interlagos (F1CT02.DAT)" },
            { 15905, "F1GP Track Imola (F1CT01.DAT)" },
            { 20497, "F1GP Track Monte Carlo (F1CT04.DAT)" },
            { 15081, "F1GP Track Montreal (F1CT05.DAT)" },
            { 13541, "F1GP Track Mexico City (F1CT06.DAT)" },
            { 15640, "F1GP Track Magny-Cours (F1CT07.DAT)" },
            { 14561, "F1GP Track Silverstone (F1CT08.DAT)" },
            { 14834, "F1GP Track Hockenheim (F1CT09.DAT)" },
            { 14550, "F1GP Track Hungaroring (F1CT10.DAT)" },
            { 18500, "F1GP Track Spa-Francorchamps (F1CT11.DAT)" },
            { 15020, "F1GP Track Monza (F1CT12.DAT)" },
            { 13368, "F1GP Track Estoril (F1CT13.DAT)" },
            { 14329, "F1GP Track Barcelona (F1CT14.DAT)" },
            { 15801, "F1GP Track Suzuka (F1CT15.DAT) or Adelaide (F1CT16.DAT)" },
            { 26736, "F1GP Saved Game (In-race)" }
        };

        private static readonly IEnumerable<KnownFileSizeRange> ApproxFiles = new List<KnownFileSizeRange>
        {
            new KnownFileSizeRange(new ValueRange(12000, 24000), "an F1GP Track File?")
        };

        public static KnownFileStatus IsKnownFile(long size)
        {
            var isKnown = Files.ContainsKey(size);

            if (isKnown)
            {
                return new KnownFileStatus
                {
                    IsKnown = KnownState.Exact,
                    Description = GetFileDescription(size)
                };
            }

            foreach (var approx in ApproxFiles)
            {
                if (approx.IsInRange(size))
                {
                    return new KnownFileStatus
                    {
                        IsKnown = KnownState.Probable,
                        Description = approx.Description
                    };
                }
            }

            return new KnownFileStatus
            {
                IsKnown = KnownState.Unknown,
                Description = GetFileDescription(size)
            };
        }

        private static string GetFileDescription(long size)
        {
            if (Files.ContainsKey(size))
            {
                return Files[size];
            }

            return "Unrecognized file.";
        }
    }

    public class KnownFileStatus
    {
        public KnownState IsKnown { get; set; }
        public string Description { get; set; }
    }

    public enum KnownState
    {
        Unknown,
        Exact,
        Probable
    }
}
