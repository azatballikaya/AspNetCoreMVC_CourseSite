using efCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efCoreApp.Controllers{
    public class OgrenciController:Controller{
        private readonly DataContext _context;

        public OgrenciController(DataContext context){
            _context=context;
        }

        [HttpGet]
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci p1){
            _context.Ogrenciler.Add(p1);
           await _context.SaveChangesAsync();
            return RedirectToAction("Index","Ogrenci");
        }
        public async Task<IActionResult> Index(){
            var ogrenciler= await _context.Ogrenciler.ToListAsync();
            return View(ogrenciler);
        }
    }
}