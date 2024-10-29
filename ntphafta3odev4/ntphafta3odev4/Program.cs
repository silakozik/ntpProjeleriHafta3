using System;
using System.Data;

class Program
{
    static void Main()
    {
        // Kullanıcıdan matematiksel ifadeyi al
        Console.WriteLine("Bir matematiksel ifade girin (örnek: 3 + 4 * 2 / (1 - 5) ^ 2 ^ 3):");
        string ifade = Console.ReadLine();  // Kullanıcının girdiği ifade

        // İfade önceliklerine göre hesaplanırken süreci açıklayacak bir fonksiyon yazalım
        Console.WriteLine("Çözüm süreci:");

        // İşlemleri adım adım açıklamayı basit tutmak adına,
        // DataTable sınıfını kullanarak işlemi yapacağız. DataTable.Compute işlemi
        // string içindeki matematiksel ifadeyi çözümler.

        try
        {
            // Step 1: Parantez içi işlemleri yap
            // Parantezler en önce hesaplanmalı. Bu yüzden en içteki parantezi bulup çözümlemeye başlıyoruz.
            Console.WriteLine("Adım 1: Parantez içindeki işlemleri çözme...");
            string parantezli = ifade;  // Başlangıçta ifade olduğu gibi
            while (parantezli.Contains("("))  // Parantez varsa işleme devam et
            {
                int openIndex = parantezli.LastIndexOf("(");  // En son açılan parantezin indeksini bul
                int closeIndex = parantezli.IndexOf(")", openIndex);  // Bu parantezi kapatan kapanış parantezini bul
                string parantezIci = parantezli.Substring(openIndex + 1, closeIndex - openIndex - 1);  // Parantez içindeki ifadeyi al
                string sonuc = new DataTable().Compute(parantezIci, null).ToString();  // Bu ifadeyi hesapla
                Console.WriteLine($"Parantez içi ({parantezIci}) = {sonuc}");  // Parantez içinin sonucunu ekrana yaz
                parantezli = parantezli.Substring(0, openIndex) + sonuc + parantezli.Substring(closeIndex + 1);  // Hesaplanan sonucu parantez yerine koy
            }

            // Step 2: Üslü işlemler
            // Parantezler çözüldükten sonra, üslü işlemler önceliklidir.
            Console.WriteLine("Adım 2: Üs işlemlerini çözme...");
            string usluIslemler = parantezli;  // Parantezler çözüldükten sonra yeni ifadeyi al
            while (usluIslemler.Contains("^"))  // Üs işlemi varsa işleme devam et
            {
                string[] parcalar = usluIslemler.Split('^');  // İfadeyi üslü işlemlere göre ayır
                double sol = Convert.ToDouble(new DataTable().Compute(parcalar[0], null));  // Sol taraftaki değeri hesapla
                double sag = Convert.ToDouble(new DataTable().Compute(parcalar[1], null));  // Sağ taraftaki üs değerini hesapla
                double sonuc = Math.Pow(sol, sag);  // Üs işlemini gerçekleştir
                Console.WriteLine($"{parcalar[0]} ^ {parcalar[1]} = {sonuc}");  // Üs işleminin sonucunu ekrana yaz
                usluIslemler = sonuc.ToString();  // Üs işlemi çözülmüş halini ifadeye geri koy
            }

            // Son adım: Çarpma, bölme, toplama ve çıkarma işlemlerini çözme
            // Artık kalan işlemler toplama, çıkarma, çarpma ve bölme olacak.
            Console.WriteLine("Adım 3: Çarpma, bölme, toplama ve çıkarma işlemlerini çözme...");
            var finalSonuc = new DataTable().Compute(parantezli, null);  // Kalan işlemleri hesapla
            Console.WriteLine($"Sonuç: {finalSonuc}");  // Nihai sonucu ekrana yaz
        }
        catch (Exception ex)
        {
            // Herhangi bir hata durumunda, hata mesajını ekrana yazdır
            Console.WriteLine($"Hata: {ex.Message}");
        }

        // Programın sonlandığını belirtmek için bir tuşa basılmasını bekler
        Console.WriteLine("Çıkmak için bir tuşa basın...");
        Console.ReadKey();  // Program sonlanmadan önce kullanıcıdan bir tuş girmesini bekle
    }
}
