using Podcast.BLL.ViewModels.EpisodeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcast.BLL.ViewModels.TopicViewModels
{
    public class TopicViewModel : IViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CoverUrl { get; set; }
        public List<EpisodeViewModel>? Episodes {  get; set; }
    }
}
