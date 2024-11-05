using Microsoft.AspNetCore.Mvc.Rendering;
using Amanoi.Domain.Entities;

namespace Amanoi.Web.Models.ViewModels;

public class VillaNumberVM
{
    public VillaNumber? VillaNumber { get; set; }
    public IEnumerable<SelectListItem>? VillaList { get; set; }
}
