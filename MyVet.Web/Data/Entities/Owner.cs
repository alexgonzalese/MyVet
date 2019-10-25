using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Data.Entities
{
  public class Owner
  {
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Document { get; set; }

    [Required]
    [MaxLength(50)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
    [Required]
    [MaxLength(20)]
    [Display(Name = "Fixed Phone")]
    public string FixedPhone { get; set; }
    [Required]
    [MaxLength(20)]
    [Display(Name = "Cell Phone")]
    public string CellPhone { get; set; }

    [Required]
    [MaxLength(100)]
    public string Address { get; set; }

    [Display(Name = "Owner")]
    public string FullName => $"{FirstName} {LastName}";

    [Display(Name = "Owner")]
    public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

  }
}
