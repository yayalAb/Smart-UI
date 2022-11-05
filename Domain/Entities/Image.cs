using System.Reflection.Metadata;

namespace Domain.Entities;

public class Image
{
    public int Id { get; set; }
    public Blob image { get; set; }
}