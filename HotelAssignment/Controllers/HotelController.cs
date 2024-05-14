using HotelAssignment.Data;
using HotelAssignment.Models;
using HotelAssignment.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAssignment.Controllers
{
    public class HotelController : Controller
    {
        private readonly HotelDbContext _db;
        private readonly IWebHostEnvironment environment;
        public HotelController(HotelDbContext _db, IWebHostEnvironment environment)
        {
            this._db = _db;
            this.environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var hotel = await _db.HotelData.ToListAsync();
            return View(hotel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(HotelModel model)
        {
            string uniqueFileName = UploadImage(model);
            var hotel = new HotelData()
            {
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,
                Path = uniqueFileName,
                // Image = model.Image,

            };
            await _db.HotelData.AddAsync(hotel);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private string UploadImage(HotelModel model)
        {
            string uniqueFileName = "";
            if (model.ImagePath!=null)
            {
                string uploadFolder = Path.Combine(environment.WebRootPath, "image");
                 uniqueFileName=Guid.NewGuid().ToString() + "_"+ model.ImagePath.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImagePath.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var data = await _db.HotelData.FirstOrDefaultAsync(x => x.Id == Id);
            if (data != null)
            {
                var model = new HotelModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Address = data.Address,
                    Email = data.Email,
                    Path = data.Path,

                };
                return await Task.Run(() => View("Update", model));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(HotelModel model)
        
        
            {
            var result = await _db.HotelData.FindAsync(model.Id);
            string uniqueFileName = string.Empty;
            if (model.ImagePath != null)
            {
                if (result.Path != null)
                {
                    string filepath = Path.Combine(environment.WebRootPath, "image", result.Path);
                    if (System.IO.File.Exists(filepath))
                    {
                        System.IO.File.Delete(filepath);
                    }
                }
                uniqueFileName = UploadImage(model);
            }
            if(model.ImagePath!=null)
            {
  
            }
                if (result != null)
                {
                    result.Id = model.Id;
                    result.Name = model.Name;
                    result.Address = model.Address;
                    result.Email = model.Email;
                    result.Path = uniqueFileName;
                await _db.SaveChangesAsync();


                }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ProfileDetail(int Id)
        {
            var data = await _db.HotelData.FirstOrDefaultAsync(x => x.Id == Id);
            if (data != null)
            {
                var model = new HotelModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Address = data.Address,
                    Email = data.Email,
                    Path = data.Path,

                };
                return await Task.Run(() => View("ProfileDetail", model));
            }
            return RedirectToAction("Index");

        }

        [HttpPost]

        public async Task<IActionResult> Delete(int Id)
        {
            var res = await _db.HotelData.Where(x => x.Id == Id).SingleOrDefaultAsync(); 
            if (res != null)
            {
                string deleteFromFolder = Path.Combine(environment.WebRootPath, "image");
                string currentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFromFolder, res.Path);
                if (currentImage !=null)
                {
                    if (System.IO.File.Exists(currentImage))
                    {
                        System.IO.File.Delete(currentImage);    
                    }
                }
                _db.HotelData.Remove(res);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


    }
}

