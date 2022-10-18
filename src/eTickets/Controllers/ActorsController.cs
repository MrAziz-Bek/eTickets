using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eTickets.Controllers;

public class ActorsController : Controller
{
    private readonly IActorsService _service;

    public ActorsController(IActorsService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAll();
        return View(data);
    }

    // // Get: Actors/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create([Bind("Fullname,ProfilePictureURL,Bio")] Actor actor)
    {
        var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .Select(x => new { x.Key, x.Value.Errors })
                        .ToArray();

        foreach (var error in errors)
        {
            System.Console.WriteLine(error.Key + ": " + error.Errors);
        }

        if (!ModelState.IsValid)
        {
            return View(actor);
        }
        _service.Add(actor);
        return RedirectToAction(nameof(Index));
    }
}