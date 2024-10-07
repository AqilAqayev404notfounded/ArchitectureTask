using Microsoft.AspNetCore.Http;

namespace Podcast.BLL.ViewModels.TopicViewModels
{
    public class TopicUpdateViewModel : IViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public IFormFile? CoverFile { get; set; }
        public string? CoverUrl { get; set; }
    }
}
