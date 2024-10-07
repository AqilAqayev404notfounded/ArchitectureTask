using Microsoft.AspNetCore.Mvc;
using Podcast.BLL.Services.Contracts;
using Podcast.BLL.ViewModels.TopicViewModels;
using Podcast.DAL.DataContext;
using Podcast.DAL.DataContext.Entities;

namespace Podcast.MVC.Areas.Admin.Controllers
{

    [AutoValidateAntiforgeryToken]
    [Area("Admin")]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        public async Task<IActionResult> Index()
        {
            var topic =await _topicService.GetListAsync();
            return View(topic);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create( TopicCreateViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }


            await _topicService.CreateAsync(model);
            return RedirectToAction("Index");

        }

        public IActionResult UpdateAsync() 
        { 
            return View() ;
        }

        [HttpPost]
        public async Task< IActionResult> UpdateAsync(TopicUpdateViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _topicService.UpdateAsync(model);
            return RedirectToAction("Index");


        }
        public IActionResult Delete() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete()
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

        }




    }
}
