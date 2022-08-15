#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChefsnDishes.Models;

public class Chef {

    [Key]
    public int ChefId { get; set; }

    [Required(ErrorMessage ="is required.")]
    [MinLength(2, ErrorMessage ="must be at least 2 characters.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }


    [Required(ErrorMessage ="is required.")]
    [MinLength(2, ErrorMessage ="must be at least 2 characters.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required(ErrorMessage ="is required.")]
    [Display(Name ="Date of Birth")]
    public DateTime DOB { get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Dish> CreatedDishes { get; set; } = new List<Dish>();


    public string FullName() {
        return FirstName + " " + LastName;
    }

public int GetAge()
{
    DateTime n = DateTime.Now; // To avoid a race condition around midnight
    int age = n.Year - DOB.Year;

    if (n.Month < DOB.Month || (n.Month == DOB.Month && n.Day < DOB.Day))
        age--;

    return age;
}
}