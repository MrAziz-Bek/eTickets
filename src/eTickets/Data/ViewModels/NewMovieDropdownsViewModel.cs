using eTickets.Models;

namespace eTickets.Data.ViewModels;

public class NewMovieDropdownsViewModel
{
    public NewMovieDropdownsViewModel()
    {
        Producers = new();
        Cinemas = new();
        Actors = new();
    }

    public List<Producer> Producers { get; set; }
    public List<Cinema> Cinemas { get; set; }
    public List<Actor> Actors { get; set; }
}