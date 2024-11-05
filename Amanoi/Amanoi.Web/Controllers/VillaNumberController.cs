using Microsoft.AspNetCore.Mvc;
using Amanoi.Domain.Entities;
using Amanoi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Amanoi.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace Amanoi.Web.Controllers
{
    // GET: VillaController
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var villaNumbers = _db.VillaNumbers.Include(u => u.Villa).ToList();
            //ToList(), Add() là phương thức LINQ được dùng để truy vấn data từ DB
            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            // Fetch the list of Villas from the database and convert them to SelectListItem
            var villaList = _db.Villas
                .Select(u => new SelectListItem
                {
                    Text = u.Name, // The name of the villa to be displayed in the dropdown
                    Value = u.Id.ToString() // The ID of the villa as the value
                }).ToList();

            // Create an instance of VillaNumberVM and assign VillaList
            var viewModel = new VillaNumberVM
            {
                VillaNumber = new VillaNumber(), // Initialize a new VillaNumber object
                VillaList = villaList // Assign the list of villas
            };

            // Pass the ViewModel to the view
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Create(VillaNumber obj)
        {
            // Kiểm tra xem Villa_Number có trùng lặp trong cùng một Villa hay không
            bool roomNumberExists = _db.VillaNumbers.Any(u => u.VillaId == obj.VillaId && u.Villa_Number == obj.Villa_Number);
            if (roomNumberExists)
            {
                ModelState.AddModelError("Villa_Number", "The villa number already exists for the selected villa.");
            }

            // Kiểm tra xem VillaId có hợp lệ hay không
            bool villaIdExists = _db.Villas.Any(v => v.Id == obj.VillaId);
            if (!villaIdExists)
            {
                ModelState.AddModelError("VillaId", "The selected villa does not exist.");
            }

            // Kiểm tra xem ModelState có hợp lệ không
            if (!ModelState.IsValid)
            {
                // Tải lại danh sách VillaList cho dropdown
                var villaList = _db.Villas
                    .Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }).ToList();

                // Tạo lại ViewModel với dữ liệu hiện tại
                var viewModel = new VillaNumberVM
                {
                    VillaNumber = obj,
                    VillaList = villaList
                };

                return View(viewModel); // Trả lại view với thông báo lỗi
            }

            // Thêm đối tượng VillaNumber vào cơ sở dữ liệu và lưu thay đổi
            _db.VillaNumbers.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "The villa number has been created successfully.";
            return RedirectToAction("Index");
        }

    }





    // // GET: Villa/Update/5
    // public IActionResult Update(int VillaNumberId)
    // {
    //     VillaNumbers? obj = _db.VillaNumbers.FirstOrDefault(u => u.VillaId == VillaNumberId);
    //     //retrieve only one record with FirstOrDefault với ý nghĩa: trong csdl, tìm bảng Villas, tìm record có id
    //     //ngoài ra để retrieve một tập hợp các record còn có Where/Find 
    //     if (obj == null)
    //     {
    //         return RedirectToAction("Error", "Home");
    //     }
    //     return View("Update", obj); // Truyền đối tượng Villa vào View để cập nhật
    // }

    // // POST: Villa/Update
    // [HttpPost]
    // public IActionResult Update(Villa obj)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         _db.Villas.Update(obj);
    //         _db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
    //         TempData["success"] = "The villa has been updated successfully.";
    //         return RedirectToAction("Index");
    //     }
    //     return View(); // Trả lại View nếu có lỗi xác thực
    // }

    // // GET 
    // public IActionResult Delete(int VillaId)
    // {
    //     Villa? obj = _db.Villas.FirstOrDefault(u => u.Id == VillaId);
    //     //retrieve only one record with FirstOrDefault với ý nghĩa: trong csdl, tìm bảng Villas, tìm record có id
    //     //ngoài ra để retrieve một tập hợp các record còn có Where/Find 
    //     // if (obj == null)
    //     // {
    //     //     return RedirectToAction("Error", "Home");
    //     // }
    //     return View("Delete", obj); // Truyền đối tượng Villa vào View để cập nhật
    // }

    // // POST: Villa/Update
    // [HttpPost]
    // public IActionResult DeleteConfirmed(Villa obj)
    // {
    //     if (obj != null)
    //     {
    //         _db.Villas.Remove(obj);
    //         _db.SaveChanges();
    //         TempData["success"] = "The villa has been deleted successfully.";
    //     }
    //     else
    //     {
    //         TempData["error"] = "Failed to delete the villa.";
    //     }
    //     return RedirectToAction("Index");
    // }

}




