using Podcast.BLL.ViewModels.TopicViewModels;
using Podcast.DAL.DataContext.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Podcast.BLL.Services.Contracts;

public interface ITopicService : ICrudService<Topic, TopicViewModel, TopicCreateViewModel, TopicUpdateViewModel>
{
    Task<bool> CreateAsync(TopicCreateViewModel createViewModel, ModelStateDictionary modelState, string folderPath);

     Task<bool?> UpdateAsync(TopicUpdateViewModel vm, ModelStateDictionary modelState, string folderPath);

}