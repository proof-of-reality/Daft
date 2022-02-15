using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public class PropertyController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
