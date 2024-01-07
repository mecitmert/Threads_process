using System;
using System.Collections;
using System.Threading;

class SonucKutusu
{
    public ArrayList CiftSayilar { get; set; } = new ArrayList();
    public ArrayList TekSayilar { get; set; } = new ArrayList();
    public ArrayList AsalSayilar { get; set; } = new ArrayList();

    public ArrayList SayilariTuruneGoreGetir(SayiTur tur)
    {
        switch (tur)
        {
            case SayiTur.Cift:
                return CiftSayilar;
            case SayiTur.Tek:
                return TekSayilar;
            case SayiTur.Asal:
                return AsalSayilar;
            default:
                throw new ArgumentException("Geçersiz sayı türü.");
        }
    }
}

enum SayiTur
{
    Cift,
    Tek,
    Asal
}

class Program
{
    static ArrayList orijinalListe = new ArrayList();
    static SonucKutusu sonucKutusu = new SonucKutusu();
    static object kilitlemeNesnesi = new object();
    static int simdikiThreadNumarasi = 1;

    static void Main()
    {
        // 1'den 1.000.000'e kadar olan sayıları içeren ArrayList'i oluştur
        for (int i = 1; i <= 1000000; i++)
        {
            orijinalListe.Add(i);
        }

        // 4 eşit parçaya ayırmak için her bir alt liste boyutu
        int altListeBoyutu = 250000;

        while (true)
        {
            // Thread'leri sırayla başlat ve bekle
            Thread t1 = new Thread(() => BelirliAraliktaSayilariBul(1, altListeBoyutu, sonucKutusu));
            t1.Start();
            t1.Join();

            Thread t2 = new Thread(() => BelirliAraliktaSayilariBul(altListeBoyutu + 1, 2 * altListeBoyutu, sonucKutusu));
            t2.Start();
            t2.Join();

            Thread t3 = new Thread(() => BelirliAraliktaSayilariBul(2 * altListeBoyutu + 1, 3 * altListeBoyutu, sonucKutusu));
            t3.Start();
            t3.Join();

            Thread t4 = new Thread(() => BelirliAraliktaSayilariBul(3 * altListeBoyutu + 1, orijinalListe.Count, sonucKutusu));
            t4.Start();
            t4.Join();

            Thread.Sleep(1000);

            SayilariGoster();
        }
    }

    static void SayilariGoster()
    {
        Console.WriteLine("Görmek istediğiniz sayı türünü girin (Çift(0), Tek(1), Asal(2)): ");
        string input = Console.ReadLine();

        if (Enum.TryParse<SayiTur>(input, true, out var sayiTur))
        {
            ArrayList sayilar = sonucKutusu.SayilariTuruneGoreGetir(sayiTur);

            Console.WriteLine($"--- {sayiTur} Sayılar ---");
            foreach (var sayi in sayilar)
            {
                Console.Write(sayi + " ");
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Geçersiz giriş. Lütfen geçerli bir sayı türü girin.");
        }
    }

    static bool CiftMi(int sayi)
    {
        return sayi % 2 == 0;
    }

    static bool AsalMi(int sayi)
    {
        if (sayi < 2)
            return false;

        for (int i = 2; i <= Math.Sqrt(sayi); i++)
        {
            if (sayi % i == 0)
                return false;
        }

        return true;
    }

    static void BelirliAraliktaSayilariBul(int baslangic, int bitis, SonucKutusu kutu)
    {
        lock (kilitlemeNesnesi)
        {
            int threadNo = simdikiThreadNumarasi;
            Console.WriteLine($"Thread {threadNo} çalışmaya başladı. Aralık: {baslangic}-{bitis}");

            for (int i = baslangic; i <= bitis; i++)
            {
                if (CiftMi(i))
                {
                    kutu.CiftSayilar.Add(i);
                }
                else
                {
                    kutu.TekSayilar.Add(i);
                }

                if (AsalMi(i))
                {
                    kutu.AsalSayilar.Add(i);
                }
            }

            Console.WriteLine($"Thread {threadNo} çalışmasını tamamladı.");

            // Bir sonraki thread için numarayı güncelle
            simdikiThreadNumarasi = (simdikiThreadNumarasi % 4) + 1;
        }
    }
}
