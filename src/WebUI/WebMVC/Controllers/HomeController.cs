using Interface.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebMVC.Controllers.Base;
using WebMVC.Models;


namespace WebMVC.Controllers;

[Authorize]
public class HomeController : AppBaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _iUnitWork;


    public HomeController(ILogger<HomeController> logger, IUnitOfWork iUnitOfWork) : base(iUnitOfWork)
    {
        _logger = logger;
        _iUnitWork = iUnitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    public IActionResult Restaurant()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}