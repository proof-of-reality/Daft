using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public class PropertyController : Controller
{
    private readonly IAsyncRepository<Property> _repository;

    public PropertyController(IAsyncRepository<Core.Models.Property> repo)
    {
        _repository = repo;
    }

    public IActionResult Index()
    {
        var props = _repository.ListAsync((1, 10));
        return View(new[] { new Property("oi", "kkk", RentCategory.Share, PropertyType.Appartment, 500) });
    }
}
