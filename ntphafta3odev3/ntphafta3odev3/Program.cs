using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ntphafta3odev3
{
    class Program
    {
        static void Main()
        {
            // Kullanıcıdan sayı almak için bir liste oluşturuyoruz
            List<int> numbers = new List<int>();
            int input;

            // Kullanıcıya sayı girmesini istiyoruz (0 girildiğinde duracak)
            Console.WriteLine("Sayıları girin (durdurmak için 0 girin):");
            while (true)
            {
                input = int.Parse(Console.ReadLine());  // Kullanıcının girdiği sayıyı al
                if (input == 0) break;  // 0 girildiğinde döngüden çık
                numbers.Add(input);  // Sayıyı listeye ekle
            }

            // Sayıları küçükten büyüğe sıralıyoruz
            numbers.Sort();

            // Arka arkaya gelen grupları bul ve yazdır
            FindConsecutiveGroups(numbers);

            // Programın kapanmadan önce kullanıcıdan bir tuşa basılmasını bekle
            Console.WriteLine("Çıkmak için bir tuşa basın...");
            Console.ReadLine();
        }

        // Arka arkaya gelen sayıları bulan fonksiyon
        static void FindConsecutiveGroups(List<int> numbers)
        {
            // Eğer liste boşsa uyarı ver ve işlemi sonlandır
            if (numbers.Count == 0)
            {
                Console.WriteLine("Hiç sayı girilmedi.");
                return;
            }

            // İlk elemanı başlangıç olarak belirle
            int start = numbers[0];
            int end = numbers[0];
            bool foundGroup = false;  // Bir grup bulunup bulunmadığını izlemek için

            // Listedeki her eleman üzerinde döngü kur
            for (int i = 1; i < numbers.Count; i++)
            {
                // Eğer mevcut sayı bir öncekinin ardışığı ise
                if (numbers[i] == end + 1)
                {
                    end = numbers[i];  // Grubun sonunu güncelle
                    foundGroup = true;  // Grup bulundu olarak işaretle
                }
                else
                {
                    // Eğer bir grup bulunduysa, grubu yazdır
                    if (foundGroup)
                    {
                        PrintGroup(start, end);  // Grubu yazdır
                    }

                    // Ardışık değilse yeni grup başlat
                    start = numbers[i];
                    end = numbers[i];
                    foundGroup = false;  // Yeni bir gruba başlıyoruz, o yüzden false
                }
            }

            // Döngü sona erdiğinde son bulunan grubu kontrol et ve yazdır
            if (foundGroup)
            {
                PrintGroup(start, end);  // Eğer son grup varsa yazdır
            }
        }

        // Bulunan grubu ekrana yazdıran fonksiyon
        static void PrintGroup(int start, int end)
        {
            // Başlangıç ve bitiş aynı değilse (yani grup varsa) grubu yazdır
            if (start != end)
            {
                Console.WriteLine(start + "-" + end);  // Grup aralığını yazdır
            }
        }
    }
}
