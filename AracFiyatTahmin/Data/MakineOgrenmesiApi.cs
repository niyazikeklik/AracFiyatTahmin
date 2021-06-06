using System;
using System.Net;
using AracFiyatTahmin.Models;

namespace AracFiyatTahmin
{
    public static class MakineOgrenmesiApi
    {

        public static int getGender(aracTable yeniislem)
        {
            string urlParameters = "http://f5040d08-6255-41bf-9665-f8612b202cda.westeurope.azurecontainer.io/score?data=[[" + yeniislem.modelyili + "," + yeniislem.yakitturu + "," + yeniislem.vites + "," + yeniislem.beygirgucu + "," + yeniislem.durum + "," + yeniislem.km + "]]";
            WebClient wb = new WebClient();
            var response = wb.DownloadString(urlParameters);
            int basla = response.IndexOf("[") + 1;
            int bitis = response.IndexOf("]", basla);
            string ayıklanan = response.Substring(basla, bitis - basla);
            ayıklanan = ayıklanan.Substring(0, ayıklanan.IndexOf('.'));
            int fiyat = int.Parse(ayıklanan);
            Random rastgele = new Random();
            int sayi = rastgele.Next(25000, 1000000);
            sayi = sayi - (sayi % 10000);
            return fiyat;
        }
        static public double Oran(aracTable yeniislem)
        {
            double yedekDurum = yardımcıMethodlar.yerBulucuDurum(int.Parse(yeniislem.durum));
            double yedekVites = yardımcıMethodlar.yerBulucuVites(int.Parse(yeniislem.vites));
            double yedekYakitturu = yardımcıMethodlar.yerBulucuYakit(int.Parse(yeniislem.yakitturu));
            // 0.0 - 6.0
            double dinamic_bg_oran = 0.3;
            dinamic_bg_oran = int.Parse(yeniislem.beygirgucu) > 06 ? 0.4 : dinamic_bg_oran;
            dinamic_bg_oran = int.Parse(yeniislem.beygirgucu) > 12 ? 0.5 : dinamic_bg_oran;
            double oran = (
              ((int.Parse(yeniislem.beygirgucu)) * dinamic_bg_oran) +
                (yedekVites) + (yedekDurum) + (yedekYakitturu) +
                ((yeniislem.modelyili - 1999) * 0.03) +
                (yeniislem.km / 1000 * -0.0003));
            return oran < 0.5 ? 0.5 : oran;
        }
        // 60764

    }
}