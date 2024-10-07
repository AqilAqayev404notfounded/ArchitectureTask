using Microsoft.AspNetCore.Http;

namespace Podcast.BLL.ViewModels.TopicViewModels
{
    public class TopicCreateViewModel : IViewModel
    {
        public required string Name { get; set; }
        public required IFormFile CoverFile {  get; set; }
        public string? CoverUrl { get; set; }
    }
}
