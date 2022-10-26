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

    public async Task AddNewMovieAsync(NewMovieViewModel data)
    {
        var newMovie = new Movie()
        {
            Name = data.Name,
            Description = data.Description,
            Price = data.Price,
            ImageURL = data.ImageURL,
            CinemaId = data.CinemaId,
            StartDate = data.StartDate,
            EndDate = data.EndDate,
            MovieCategory = data.MovieCategory,
            ProducerId = data.ProducerId
        };

        await _context.Movies.AddAsync(newMovie);
        await _context.SaveChangesAsync();

        //Add Movie Actors
        foreach (var actorId in data.ActorIds)
        {
            var newActorMovie = new Actor_Movie()
            {
                MovieId = newMovie.Id,
                ActorId = actorId
            };
            await _context.Actors_Movies.AddAsync(newActorMovie);
        }
        await _context.SaveChangesAsync();
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

    public async Task UpdateMovieAsync(NewMovieViewModel data)
    {
        var dbMovie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == data.Id);

        if (dbMovie is not null)
        {
            dbMovie.Name = data.Name;
            dbMovie.Description = data.Description;
            dbMovie.Price = data.Price;
            dbMovie.ImageURL = data.ImageURL;
            dbMovie.CinemaId = data.CinemaId;
            dbMovie.StartDate = data.StartDate;
            dbMovie.EndDate = data.EndDate;
            dbMovie.MovieCategory = data.MovieCategory;
            dbMovie.ProducerId = data.ProducerId;

            _context.Movies.Update(dbMovie);
            await _context.SaveChangesAsync();
        }

        //Remove existing actors
        var existingActorsDb = _context.Actors_Movies.Where(am => am.MovieId == data.Id).ToList();

        _context.Actors_Movies.RemoveRange(existingActorsDb);
        await _context.SaveChangesAsync();

        //Add Movie Actors
        foreach (var actorId in data.ActorIds)
        {
            var newActorMovie = new Actor_Movie()
            {
                MovieId = data.Id,
                ActorId = actorId
            };
            await _context.Actors_Movies.AddAsync(newActorMovie);
        }
        await _context.SaveChangesAsync();
    }
}