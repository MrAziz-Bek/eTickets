using System.ComponentModel.DataAnnotations;
using eTickets.Data.Base;

namespace eTickets.Models;

public class Producer : IEntityBase
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Profile Picture")]
    public string ProfilePictureURL { get; set; }

    [Display(Name = "Full Name")]
    public string Fullname { get; set; }

    [Display(Name = "Biography")]
    public string Bio { get; set; }

    //Relationships
    public List<Movie> Movies { get; set; }
}