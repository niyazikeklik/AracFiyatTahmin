using AracFiyatTahmin.Data;
using AracFiyatTahmin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AracFiyatTahmin.Controllers
{
    public class HomeController : Controller
    {

        // GET: Islemler
        ApplicationDBContext db;

        string[] beygirgucleri = new string[] { "100 BG ve altı ", "101 - 125 BG ", " 126 - 150 BG ", "151 - 175 BG", "176 - 200 BG ", "201 - 225 BG ", "226 - 250 BG ", "251 - 275 BG ", "276 - 300 BG ", " 301 - 325 BG", "326 - 350 BG ", "376 - 400 BG ", "451 - 475 BG", "50 BG ve altı", "51 - 75 BG", "601 BG ve üzeri", "76 - 100 BG" };
        string[] durumlar = new string[] { "Sıfır kilometre", "İkinci el", "Hasarlı", "Klasik" };
        string[] vitesler = new string[] { "Düz", "Otomatik", "Yarı Otomatik" };
        string[] yakitturleri = new string[] { "Benzin", "Dizel", "Benzin/LPG", "Hibrit" };

        public HomeController(ApplicationDBContext _db) {
            db = _db;
        }

        public ActionResult Table()
        {
            var degerler = db.arac.ToList();
            degerler.Reverse();
            return View(degerler);
        }
        public ActionResult Bizkimiz()
        {

            return View();
        }
        public ActionResult Index()
        {
            var degerler = db.arac.ToList();
            degerler.Reverse();

            ViewBag.oran = yardımcıMethodlar.oranbul(degerler);

            aracTable yeni = new aracTable();
            yeni.islemiyapan = "-";
            yeni.beygirgucu = "-";
            yeni.durum = "-";
            yeni.yakitturu = "-";
            yeni.vites = "-";

            ViewBag.islem = false;
            ViewBag.soneklenen = yeni;
            return View(degerler);

        }
        [HttpPost]
        public ActionResult Index(aracTable yeniislem)
        {
            yeniislem.fiyat = Convert.ToInt32(MakineOgrenmesiApi.getFiyat(yeniislem) * 
                MakineOgrenmesiApi.Oran(yeniislem));

            int indisBeygırGucu = int.Parse(yeniislem.beygirgucu);
            int indisDurum = int.Parse(yeniislem.durum);
            int indisVites = int.Parse(yeniislem.vites);
            int indisYakitturu = int.Parse(yeniislem.yakitturu);

            yeniislem.beygirgucu = beygirgucleri[indisBeygırGucu - 1];
            yeniislem.durum = durumlar[indisDurum];
            yeniislem.vites = vitesler[indisVites];
            yeniislem.yakitturu = yakitturleri[indisYakitturu];
            yeniislem.islemdogruluk = "1";

            db.arac.Add(yeniislem);
            db.SaveChanges();

            var degerler = db.arac.ToList();
            degerler.Reverse();
            ViewBag.oran = yardımcıMethodlar.oranbul(degerler);
            ViewBag.islem = true;
            ViewBag.soneklenen = yeniislem;
            return View(degerler);
        }
        [HttpPost]
        public ActionResult cevap(string veri, int id)
        {
            aracTable kayit = db.arac.FirstOrDefault(x => x.islemno == id);
            kayit.islemdogruluk = veri;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}


