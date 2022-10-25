using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services;

public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
{
    private readonly AppDbContext _context;
    public MoviesService(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Movie> GetMovieByIdAsync(int id)
    {
        var movieDetails = await _context.Movies
                        .Include(m => m.Cinema)
                        .Include(m => m.Producer)
                        .Include(m => m.Actors_Movies).ThenInclude(am => am.Actor)
                        .FirstOrDefaultAsync(m => m.Id == id);

        return movieDetails;
    }

    public async Task<NewMovieDropdownsViewModel> GetNewMovieDropdownsValues()
    {
        var response = new NewMovieDropdownsViewModel()
        {
            Actors = await _context.Actors.OrderBy(a => a.Fullname).ToListAsync(),
            Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync(),
            Producers = await _context.Producers.OrderBy(p => p.Fullname).ToListAsync()
        };

        return response;
    }
}