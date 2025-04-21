using FinishedGamesADO.Models;
using FinishedGamesADO.Repository.Abstracitions;
using Microsoft.AspNetCore.Mvc;

namespace FinishedGamesADO.Controllers;

[Route("v2/Games")]
public class GamesController : Controller
{
    private readonly ILogger<GamesController> _logger;

    private readonly IConfiguration _configuration;

    private readonly IGameRepository _gameRepository;

    public GamesController(ILogger<GamesController> logger, IConfiguration configuration, IGameRepository gameRepository)
    {
        _logger = logger;
        _configuration = configuration;
        _gameRepository = gameRepository;
    }

    // GET: GamesController
    public ActionResult GetAllAsync()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var game = await _gameRepository.GetByIdAsync(id);
        if (game == null)
        {
            return NotFound();
        }
        return View(game);
    }

    // POST: GamesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult CreateAsync(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // POST: GamesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult UpdateAsync(Guid id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: GamesController/Delete/5
    public ActionResult DeleteAsync(Guid id)
    {
        return View();
    }
}