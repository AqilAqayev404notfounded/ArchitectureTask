using AutoMapper;
using Podcast.BLL.Extension;
using Podcast.BLL.Services.Contracts;
using Podcast.BLL.ViewModels.TopicViewModels;
using Podcast.DAL.DataContext.Entities;
using Podcast.DAL.Repositories;
using Podcast.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;



namespace Podcast.BLL.Services;

public class TopicManager : CrudManager<Topic, TopicViewModel, TopicCreateViewModel, TopicUpdateViewModel>, ITopicService
{
    private readonly ITopicRepository _topicRepository;
    private readonly IMapper _mapper;


    public TopicManager(IRepositoryAsync<Topic> repository, IMapper mapper, ITopicRepository topicRepository) : base(repository, mapper)
    {
        _topicRepository = topicRepository;
        _mapper = mapper;


    }

    public async Task<bool> CreateAsync(TopicCreateViewModel createViewModel, ModelStateDictionary modelState, string folderPath)
    {

        if (!modelState.IsValid) return false;

        if (!createViewModel.CoverFile.CheckType())
        {
            modelState.AddModelError("Image", "Please enter valid input");
            return false;
        }

        if (!createViewModel.CoverFile.CheckSize(2))
        {
            modelState.AddModelError("Image", "Please enter valid input");
            return false;
        }
        var topicList = await _topicRepository.GetListAsync();
        foreach (var item in topicList)
        {
            if (item.Name.Equals(createViewModel.Name))
            {
                modelState.AddModelError("Name", "There are already topic with this name");
                return false;
            }
        }
        string fileName = await createViewModel.CoverFile.CreateFileAsync(folderPath);
        createViewModel.CoverUrl = fileName;
        var topic = _mapper.Map<Topic>(createViewModel);
        await _topicRepository.CreateAsync(topic);
        return true;
    }

    public async Task<TopicUpdateViewModel?> GetUTopicForUpdateAsync(int id)
    {
        var topic = await _topicRepository.GetAsync(id);
        if (topic == null) return null;
        var vm = _mapper.Map<TopicUpdateViewModel>(topic);
        return vm;
    }

    public async Task<bool> RemoveTopicAsync(int id)
    {
        var topic = await _topicRepository.GetAsync(id);

        if (topic == null) return false;

        await _topicRepository.RemoveAsync(topic);

        return true;
    }

    public async Task<bool?> UpdateAsync(TopicUpdateViewModel vm, ModelStateDictionary modelState, string folderPath)
    {
        if (!modelState.IsValid) return false;
        var existingTopic = await _topicRepository.GetAsync(vm.Id);
        if (existingTopic == null)
        {
            return null;
        }
        if (vm.CoverFile != null)
        {
            if (!vm.CoverFile.CheckType())
            {
                modelState.AddModelError("Image", "Please enter valid input");
                return false;
            }

            if (!vm.CoverFile.CheckSize(2))
            {
                modelState.AddModelError("Image", "Please enter valid input");
                return false;
            }

            string fileName = await vm.CoverFile.CreateFileAsync(folderPath);
            vm.CoverUrl = fileName;
            var oldPath = Path.Combine(folderPath, existingTopic.CoverUrl);
            oldPath.DeleteFile();

        }
        else
        {
            vm.CoverUrl = existingTopic.CoverUrl;
        }
        var topicList = await _topicRepository.GetListAsync();
        foreach (var item in topicList)
        {
            if (item.Name == vm.Name)
            {
                modelState.AddModelError("Name", "There are already topic with this name");
                return false;
            }
        }
        _mapper.Map(vm, existingTopic);
        await _topicRepository.UpdateAsync(existingTopic);
        return true;

    }
}