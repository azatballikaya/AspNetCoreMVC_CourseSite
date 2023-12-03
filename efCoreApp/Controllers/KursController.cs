using efCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efCoreApp.Controllers{
    public class KursController:Controller{
        private readonly DataContext _context;
        public KursController(DataContext context){
            _context=context;
        }
        public async Task< IActionResult> Index(){
            var kurslar=await _context.Kurslar.ToListAsync();
            return View(kurslar);
        }
        [HttpGet]
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kurs p1){
            _context.Kurslar.Add(p1);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id){
            if(id==null)
            return NotFound();

            var kurs=await _context.Kurslar.Include(x=>x.KursKayitlari).ThenInclude(x=>x.Ogrenci).FirstOrDefaultAsync(x=>x.KursId==id);
            if(kurs==null)
            return NotFound();
            return View(kurs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Kurs model){
                      _context.Kurslar.Update(model);
                     await _context.SaveChangesAsync();
            return RedirectToAction("Index","Kurs");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id){
            if(id==null)
            return NotFound();
            var kurs= await _context.Kurslar.FindAsync(id);
            if(kurs==null)
            return NotFound();
            return View(kurs);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm]int id){
            var kurs= await _context.Kurslar.FindAsync(id);
             _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Kurs");
        }
    }
}