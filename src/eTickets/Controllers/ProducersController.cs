using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eTickets.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class ProducersController : Controller
{
    private readonly IProducersService _service;

    public ProducersController(IProducersService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var allProducers = await _service.GetAllAsync();
        return View(allProducers);
    }

    //GET: producers/details/{id}
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var producerDetails = await _service.GetByIdAsync(id);

        if (producerDetails is null)
            return View("NotFound");

        return View(producerDetails);
    }

    //GET: producers/create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("ProfilePictureURL,Fullname,Bio")] Producer producer)
    {
        if (!ModelState.IsValid)
            return View(producer);

        await _service.AddAsync(producer);
        return RedirectToAction(nameof(Index));
    }

    //GET: producers/edit/{id}
    public async Task<IActionResult> Edit(int id)
    {
        var producerDetails = await _service.GetByIdAsync(id);

        if (producerDetails is null)
            return View("NotFound");

        return View(producerDetails);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,Fullname,Bio")] Producer producer)
    {
        if (!ModelState.IsValid)
            return View(producer);

        if (id == producer.Id)
        {
            await _service.UpdateAsync(id, producer);
            return RedirectToAction(nameof(Index));
        }

        return View(producer);
    }

    //GET: producers/delete/{id}
    public async Task<IActionResult> Delete(int id)
    {
        var producerDetails = await _service.GetByIdAsync(id);

        if (producerDetails is null)
            return View("NotFound");

        return View(producerDetails);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var producerDetails = await _service.GetByIdAsync(id);

        if (producerDetails is null)
            return View("NotFound");

        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}