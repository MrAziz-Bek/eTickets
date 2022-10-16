using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eTickets.Controllers;

[Route("[controller]")]
public class ActorsController : Controller
{
    private readonly ILogger<ActorsController> _logger;
    private readonly AppDbContext _context;

    public ActorsController(ILogger<ActorsController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var data = _context.Actors.ToList();
        return View();
    }
}