using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Data.Entities
{
  public class History
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "The field {0} is mandatory.")]
    [MaxLength(30, ErrorMessage = "The {0} field can not have more than {1} characters.")]
    public string Description { get; set; }

    [Display(Name = "Date")]
    [Required(ErrorMessage = "The field {0} is mandatory.")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }

    public string Remark { get; set; }

    [Display(Name = "Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime DateLocal => Date.ToLocalTime();

    public ServiceType ServiceType { get; set;}

    public Pet Pet { get; set; }
  }
}
