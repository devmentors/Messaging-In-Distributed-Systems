namespace SuperStore.Shared.Deduplication;

public class DeduplicationOptions
{
    public bool Enabled { get; set; }
    public string Interval { get; set; }
    public int MessageEvictionWindowInDays { get; set; }
}