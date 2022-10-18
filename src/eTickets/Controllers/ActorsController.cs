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
        if (!ModelState.IsValid)
        {
            return View(actor);
        }
        _service.Add(actor);
        return RedirectToAction(nameof(Index));
    }
}