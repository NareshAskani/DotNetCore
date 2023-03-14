using DadJokesApp.Models;
using DadJokesApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DadJokesApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDadJokesService _dadJokesService;
        private readonly ILogger<IndexModel> _logger;
        private static int _jokesCount = 0;


        public  int JokesCount { get { return _jokesCount; }  set { value = _jokesCount; } }
        public List<DadJokesModel> Jokes { get; set; }
        public string ErrorMessage { get; private set; }

        [BindProperty(Name = "EnteredJokesCount")]
        public int EnteredJokesCount { get; set; } = 0;

        public IndexModel(IDadJokesService dadJokesService, ILogger<IndexModel> logger)
        {
            _dadJokesService = dadJokesService;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            if (_jokesCount == 0)
            {
                LoadJokesCount();
            }
            if (EnteredJokesCount==0)
            {
                LoadJokes(1);

            }
            Task.WaitAll();
        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (EnteredJokesCount>_jokesCount)
                {
                    ModelState.AddModelError(string.Empty, "More than toal jokes count not is not possible.");
                }else
                await LoadJokes(EnteredJokesCount);
            }
        }

        private async Task LoadJokes(int _count)
        {
            try
            {
                Jokes = await _dadJokesService.GetJokesAsync(_count);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error getting jokes from API");
                ModelState.AddModelError(string.Empty, "An error occurred. Please try again later.");
            }
        }
        private async Task LoadJokesCount()
        {
            try
            {
                _jokesCount = await _dadJokesService.GetJokesCountAsync();

            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error getting jokes count from API");
                ModelState.AddModelError(string.Empty, "An error occurred. Please try again later.");
            }
        }
    }

}