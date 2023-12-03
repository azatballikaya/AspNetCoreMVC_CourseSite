using efCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efCoreApp.Controllers{
    public class KursKayitController:Controller{
        private readonly DataContext _context;
        public KursKayitController(DataContext context){
            _context=context;
        }
        public async Task<IActionResult> Index(){
            var kursKayitlari=await _context.KursKayitlari.Include(x=>x.Ogrenci).Include(y=>y.Kurs).ToListAsync();
            return View(kursKayitlari);
        }
        [HttpGet]
        public async Task<IActionResult> Create(){
            ViewBag.Ogrenciler=new SelectList(await _context.Ogrenciler.ToListAsync(),"OgrenciId","AdSoyad") ;
            ViewBag.Kurslar=new SelectList(await _context.Kurslar.ToListAsync(),"KursId","Baslik");
            return View();
        }
         [HttpPost]
         [ValidateAntiForgeryToken]
          public async Task<IActionResult> Create(KursKayit p1){
            p1.KayitTarihi=DateTime.Now;
            _context.KursKayitlari.Add(p1);
            await _context.SaveChangesAsync();

             return RedirectToAction("Index");
          }
    }
}