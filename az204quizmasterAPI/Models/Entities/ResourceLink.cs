using System.Diagnostics.CodeAnalysis;

namespace az204quizmasterAPI.Models.Entities
{
    public class ResourceLink
    {
        public int Id { get; set; }
        public required string Url { get; set; }

        public int QAId { get; set; }
        public QA QA { get; set; } = null!;

        [SetsRequiredMembers]
        public ResourceLink(string url) 
        {
            Url = url;
        }
    }
}
