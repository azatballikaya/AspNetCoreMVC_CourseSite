using System.ComponentModel.DataAnnotations;

namespace efCoreApp.Data{
    public class Ogrenci{
        [Key]
        
        public int OgrenciId { get; set; }
        [Display(Name ="Ögrenci Adı")]
        public string? OgrenciAd { get; set; }
        [Display(Name ="Ögrenci Soyadı")]
        public string? OgrenciSoyad { get; set; }
        public string? Eposta { get; set; }
        public string? Telefon { get; set; }

    }
}