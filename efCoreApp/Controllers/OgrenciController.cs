using efCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efCoreApp.Controllers{
    public class OgrenciController:Controller{
        private readonly DataContext _context;

        public OgrenciController(DataContext context){
            _context=context;
        }
          public async Task<IActionResult> Index(){
            var ogrenciler= await _context.Ogrenciler.ToListAsync();
            return View(ogrenciler);
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
        
       
        [HttpGet]
        public async Task<IActionResult> Edit(int? id){
            if(id==null)
            return NotFound();
            
            var ogrenci=await _context.Ogrenciler.FindAsync(id);
            if(ogrenci==null)
            return NotFound();


            return View(ogrenci);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Ogrenci model){
            if(id!=model.OgrenciId)
            return NotFound();
            if(ModelState.IsValid){
                try{
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index","Ogrenci");
                }
                catch(DbUpdateConcurrencyException){
                    if(!_context.Ogrenciler.Any(o=>o.OgrenciId==model.OgrenciId))
                    return NotFound();
                    throw;
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id){
            
            if(id==null)
            return NotFound();
            
            var ogrenci=await _context.Ogrenciler.FindAsync(id);
           
            if(ogrenci==null)
            return NotFound();

            return View(ogrenci);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id){
            var ogrenci=await _context.Ogrenciler.FindAsync(id);
            if(ogrenci==null)
            return NotFound();
            _context.Ogrenciler.Remove(ogrenci);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Ogrenci");

        }
        

      
    }
}