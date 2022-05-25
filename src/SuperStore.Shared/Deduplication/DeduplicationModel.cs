using System.ComponentModel.DataAnnotations;

namespace SuperStore.Shared.Deduplication;

public class DeduplicationModel
{
    [Key]
    public string MessageId { get; set; }
    public DateTime ProcessedAt { get; set; }
}