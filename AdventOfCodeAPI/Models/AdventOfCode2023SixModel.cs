namespace AdventOfCodeAPI.Models
{
    public class AdventOfCode2023SixModel
    {
        public AdventOfCode2023SixModel() 
        {
            SixASampleData = @"Time:      7  15   30
Distance:  9  40  200";
            SixAData = @"Time:        48     87     69     81
Distance:   255   1288   1117   1623";
            SixBSampleData = @"Time:      7  15   30
Distance:  9  40  200";
            SixBData = @"Time:        48     87     69     81
Distance:   255   1288   1117   1623";
        }
        public string SixASampleData { get; set; }
        public string SixAData { get; set; }
        public string SixBSampleData { get; set; }
        public string SixBData { get; set; }
    }
}
