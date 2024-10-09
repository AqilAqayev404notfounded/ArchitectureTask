using Microsoft.AspNetCore.Mvc;
using Podcast.BLL.Services.Contracts;
using Podcast.BLL.ViewModels.TopicViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;



namespace Podcast.MVC.Areas.Admin.Controllers
{

    [AutoValidateAntiforgeryToken]
    [Area("Admin")]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;
        private readonly string FOLDER_PATH;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public TopicController(ITopicService topicService, IWebHostEnvironment webHostEnvironment)
        {

            _topicService = topicService;
            _webHostEnvironment = webHostEnvironment;
            FOLDER_PATH = FOLDER_PATH = Path.Combine(_webHostEnvironment.WebRootPath, "images", "topics");

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

           
            var succeeded = await _topicService.CreateAsync(model, ModelState, FOLDER_PATH);


            if (succeeded) return RedirectToAction("Index");
            return View(model);

        }


        public IActionResult Update() 
        { 

            return View();

        }


        [HttpPost]
        public async Task< IActionResult> Update(int? id,TopicUpdateViewModel model)
        {
            var result = await _topicService.UpdateAsync(model, ModelState, FOLDER_PATH);
            if (result == false) return View(model);
            if (result == null) return BadRequest();
            return RedirectToAction("Index");

        }

        public IActionResult Delete() 
        {

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            if (id == null) return NotFound();


            var result = await _topicService.RemoveAsync(id);


            return RedirectToAction("Index");

        }

    }
}
