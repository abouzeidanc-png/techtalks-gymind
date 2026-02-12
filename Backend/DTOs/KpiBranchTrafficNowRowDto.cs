namespace GYMIND.API.DTOs
{
    public class KpiBranchTrafficNowRowDto
    {
        public DateTime? TrafficTimestamp { get; set; }
        public int? EntryCount { get; set; }
        public decimal? CapacityPercentage { get; set; }
    }
}
