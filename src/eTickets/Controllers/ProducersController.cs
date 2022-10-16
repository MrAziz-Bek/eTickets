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
public class ProducersController : Controller
{
    private readonly ILogger<ProducersController> _logger;    
    private readonly AppDbContext _context;

    public ProducersController(ILogger<ProducersController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
}