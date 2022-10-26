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

    //GET: Movies/Edit/{id}
    public async Task<IActionResult> Edit(int id)
    {
        var movieDetails = await _service.GetMovieByIdAsync(id);

        if (movieDetails is null)
            return View("NotFound");

        var response = new NewMovieViewModel()
        {
            Id = movieDetails.Id,
            Name = movieDetails.Name,
            Description = movieDetails.Description,
            Price = movieDetails.Price,
            StartDate = movieDetails.StartDate,
            EndDate = movieDetails.EndDate,
            ImageURL = movieDetails.ImageURL,
            MovieCategory = movieDetails.MovieCategory,
            CinemaId = movieDetails.CinemaId,
            ProducerId = movieDetails.ProducerId,
            ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
        };

        var movieDropDownsData = await _service.GetNewMovieDropdownsValues();
        ViewBag.Cinemas = new SelectList(movieDropDownsData.Cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(movieDropDownsData.Producers, "Id", "Fullname");
        ViewBag.Actors = new SelectList(movieDropDownsData.Actors, "Id", "Fullname");

        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, NewMovieViewModel movie)
    {
        if (id != movie.Id)
            return View("NotFound");

        if (!ModelState.IsValid)
        {
            var movieDropDownsData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropDownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownsData.Producers, "Id", "Fullname");
            ViewBag.Actors = new SelectList(movieDropDownsData.Actors, "Id", "Fullname");

            return View(movie);
        }

        await _service.UpdateMovieAsync(movie);
        return RedirectToAction(nameof(Index));
    }
}