using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Kullanıcı "exit" yazana kadar devam eden döngü
        while (true)
        {
            // Kullanıcıdan iki polinom al
            Console.WriteLine("İki polinom girin (örneğin: 2x^2 + 3x - 5). Çıkmak için 'exit' yazın.");

            // Birinci polinomu al
            Console.Write("Birinci polinom: ");
            string polinom1 = Console.ReadLine(); // Kullanıcının girdiği birinci polinom
            if (polinom1.ToLower() == "exit") break; // Eğer kullanıcı "exit" yazarsa programı sonlandır

            // İkinci polinomu al
            Console.Write("İkinci polinom: ");
            string polinom2 = Console.ReadLine(); // Kullanıcının girdiği ikinci polinom
            if (polinom2.ToLower() == "exit") break; // Eğer kullanıcı "exit" yazarsa programı sonlandır

            // Polinomları çözümleyerek terimlerine ayırıyoruz
            Dictionary<int, int> poly1 = ParsePolynom(polinom1); // Birinci polinomu çözümle
            Dictionary<int, int> poly2 = ParsePolynom(polinom2); // İkinci polinomu çözümle

            // Polinomların toplamını hesapla
            Dictionary<int, int> sum = AddPolynoms(poly1, poly2); // İki polinomu topla
            // Polinomların farkını hesapla
            Dictionary<int, int> diff = SubtractPolynoms(poly1, poly2); // İki polinomu çıkar

            // Sonuçları yazdır
            Console.WriteLine("Polinomların toplamı: " + FormatPolynom(sum)); // Toplam sonucu ekrana yazdır
            Console.WriteLine("Polinomların farkı: " + FormatPolynom(diff)); // Çıkarma sonucu ekrana yazdır
        }
    }

    // Polinomları çözümleyerek terimlerine ayıran fonksiyon
    static Dictionary<int, int> ParsePolynom(string polynom)
    {
        var terms = new Dictionary<int, int>(); // Kuvvet ve katsayı çiftlerini tutmak için sözlük

        // Polinomu boşluklara göre parçalara ayır (eksi işareti dahil terimleri ayırmak için "-" işaretini "+-" ile değiştiriyoruz)
        string[] parts = polynom.Replace("-", "+-").Split('+');
        foreach (string part in parts)
        {
            if (string.IsNullOrEmpty(part)) continue; // Boş stringleri atla

            // Terimleri işliyoruz
            string[] subParts = part.Split('x'); // x'e göre ayırıyoruz
            int coeff = 0; // Katsayı
            int power = 0; // Kuvvet

            if (subParts.Length == 1) // Eğer sabit terimse (x içermez)
            {
                coeff = int.Parse(subParts[0]); // Sabit terimin katsayısı
                power = 0; // Sabit terimler için kuvvet 0'dır
            }
            else // x içeriyorsa
            {
                coeff = string.IsNullOrEmpty(subParts[0]) ? 1 : int.Parse(subParts[0]); // Eğer x'in katsayısı belirtilmemişse 1 al
                power = subParts.Length > 1 && subParts[1].Contains("^") ? int.Parse(subParts[1].Replace("^", "")) : 1; // Eğer x'in üstü belirtilmişse onu al, yoksa kuvvet 1'dir
            }

            // Aynı kuvvetten daha önce eklenmişse, katsayıları topluyoruz
            if (terms.ContainsKey(power))
            {
                terms[power] += coeff; // Mevcut kuvvetin katsayısına ekle
            }
            else
            {
                terms.Add(power, coeff); // Yeni kuvvet-katsayı çifti ekle
            }
        }

        return terms; // Polinom terimlerini döndür
    }

    // Polinomları toplama fonksiyonu
    static Dictionary<int, int> AddPolynoms(Dictionary<int, int> poly1, Dictionary<int, int> poly2)
    {
        var result = new Dictionary<int, int>(poly1); // İlk polinomu başlangıç olarak kopyala

        // İkinci polinomun terimlerini ekle
        foreach (var kvp in poly2)
        {
            if (result.ContainsKey(kvp.Key)) // Eğer aynı kuvvetten terim varsa, katsayıları topluyoruz
            {
                result[kvp.Key] += kvp.Value;
            }
            else // Eğer aynı kuvvetten terim yoksa, yeni terim ekliyoruz
            {
                result.Add(kvp.Key, kvp.Value);
            }
        }

        return result; // Toplam sonucu döndür
    }

    // Polinomları çıkarma fonksiyonu
    static Dictionary<int, int> SubtractPolynoms(Dictionary<int, int> poly1, Dictionary<int, int> poly2)
    {
        var result = new Dictionary<int, int>(poly1); // İlk polinomu başlangıç olarak kopyala

        // İkinci polinomun terimlerini çıkar
        foreach (var kvp in poly2)
        {
            if (result.ContainsKey(kvp.Key)) // Eğer aynı kuvvetten terim varsa, katsayıları çıkarıyoruz
            {
                result[kvp.Key] -= kvp.Value;
            }
            else // Eğer aynı kuvvetten terim yoksa, terimi eksi değeriyle ekliyoruz
            {
                result.Add(kvp.Key, -kvp.Value);
            }
        }

        return result; // Çıkarma sonucu döndür
    }

    // Polinomları okunabilir formata dönüştürme fonksiyonu
    static string FormatPolynom(Dictionary<int, int> poly)
    {
        var result = new List<string>(); // Sonuçları tutacak liste

        // Polinomdaki her bir terimi sırayla işleyelim (büyük kuvvetten küçüğe doğru sırala)
        foreach (var kvp in poly.OrderByDescending(x => x.Key))
        {
            string term = "";
            if (kvp.Value == 0) continue; // Eğer katsayı 0 ise terimi atla

            // Katsayı ve terimleri biçimlendir
            if (kvp.Key == 0) // Sabit terim (x yok)
            {
                term = $"{kvp.Value}";
            }
            else if (kvp.Key == 1) // x'in kuvveti 1 ise
            {
                term = kvp.Value == 1 ? "x" : $"{kvp.Value}x";
            }
            else // Diğer kuvvetler için (x^n)
            {
                term = kvp.Value == 1 ? $"x^{kvp.Key}" : $"{kvp.Value}x^{kvp.Key}";
            }

            result.Add(term); // Terimi sonuca ekle
        }

        // Polinomu string olarak formatla
        return string.Join(" + ", result).Replace("+-", "- "); // Pozitif ve negatif terimleri birleştir ve yazdır
    }
}
