
using System.ComponentModel.DataAnnotations;
namespace AracFiyatTahmin.Models
{
    public class aracTable
    {
        [Key]
        public int islemno { get; set; }
        public int modelyili { get; set; }
        public string yakitturu { get; set; }
        public string vites { get; set; }
        public string beygirgucu { get; set; }
        public string durum { get; set; }
        public int km { get; set; }
        public int fiyat { get; set; }
        public string islemiyapan { get; set; }
        public string islemdogruluk { get; set; }
    }
}
