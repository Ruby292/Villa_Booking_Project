using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amanoi.Domain.Entities;

public class Villa
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên Villa không được để trống.")]
    [MaxLength(50, ErrorMessage = "Tên Villa không được vượt quá 50 ký tự.")]
    public required string Name { get; set; }
    public string? Description { get; set; }

    [Range(1000000, 1000000000, ErrorMessage = "Giá phải từ 1,000,000 đến 1,000,000,000 VND.")]
    public double Price { get; set; }
    public int Sqft { get; set; }

    [Range(1, 10)]
    public int Occupancy { get; set; }

    [Display(Name = "Image Url")]
    public string? ImageUrl { get; set; }

    //ngày được tạo
    public DateTime? Created_Data { get; set; }

    //ngày villa được updated 
    public DateTime? Updated_Date { get; set; }

    // Add this navigation property for the one-to-many relationship
    public ICollection<VillaNumber>? VillaNumbers { get; set; }
}