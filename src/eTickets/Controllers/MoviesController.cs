using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eTickets.Controllers;

public class MoviesController : Controller
{
    private readonly IMoviesService _service;

    public MoviesController(IMoviesService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var allMovies = await _service.GetAllAsync(m => m.Cinema);
        return View(allMovies);
    }

    //GET: Movies/Details/{id}
    public async Task<IActionResult> Details(int id)
    {
        var movieDetail = await _service.GetMovieByIdAsync(id);
        return View(movieDetail);
    }

    //GET: Movies/Create
    public async Task<IActionResult> Create()
    {
        var movieDropDownsData = await _service.GetNewMovieDropdownsValues();

        ViewBag.Cinemas = new SelectList(movieDropDownsData.Cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(movieDropDownsData.Producers, "Id", "Fullname");
        ViewBag.Actors = new SelectList(movieDropDownsData.Actors, "Id", "Fullname");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(NewMovieViewModel movie)
    {
        if (!ModelState.IsValid)
        {
            var movieDropDownsData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropDownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownsData.Producers, "Id", "Fullname");
            ViewBag.Actors = new SelectList(movieDropDownsData.Actors, "Id", "Fullname");

            return View(movie);
        }

        await _service.AddNewMovieAsync(movie);
        return RedirectToAction(nameof(Index));
    }
}