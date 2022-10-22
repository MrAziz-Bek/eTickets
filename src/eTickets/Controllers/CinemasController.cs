using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eTickets.Controllers;

public class CinemasController : Controller
{
    private readonly ICinemasService _service;

    public CinemasController(ICinemasService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var allCinemas = await _service.GetAllAsync();
        return View(allCinemas);
    }
}