using System;
using System.Collections.Generic;
using AracFiyatTahmin.Models;

namespace AracFiyatTahmin
{
    static public class yardımcıMethodlar
    {
        static public double oranbul(List<aracTable> degerler)
        {
            double countEvet = 0;
            double countHayır = 0;
            foreach (var item in degerler)
            {
                if (item.islemdogruluk == "1") countEvet++;
                else countHayır++;

            }
            double sonuc2 = countEvet / (countEvet + countHayır) * 100;
            sonuc2 = Math.Round(sonuc2, 1);
            return sonuc2;
        }
        static public double yerBulucuVites(int indisVites)
        {
            double yedekVites = 0.005;
            yedekVites = 2 == indisVites ? 0.015 : yedekVites;
            yedekVites = 1 == indisVites ? 0.15 : yedekVites;
            return yedekVites;
        }
        static public double yerBulucuDurum(int indisDurum)
        {
            double yedekDurum = 4;
            yedekDurum = 0 == indisDurum ? 0.25 : yedekDurum;
            yedekDurum = 1 == indisDurum ? 0.015 : yedekDurum;
            yedekDurum = 2 == indisDurum ? 0.005 : yedekDurum;
            return yedekDurum;
        }
        static public double yerBulucuYakit(int indisYakit)
        {
            double yedekVites = 4;
            yedekVites = 0 == indisYakit ? 0.25 : yedekVites;
            yedekVites = 1 == indisYakit ? 0.015 : yedekVites;
            yedekVites = 2 == indisYakit ? 0.005 : yedekVites;
            return yedekVites;
        }

    }

}