using System;

class Program
{
    // İkili arama (Binary Search) fonksiyonu
    static bool BinarySearch(int[] array, int target)
    {
        // Arama yapılacak aralığın sol ve sağ sınırlarını belirler
        int left = 0;
        int right = array.Length - 1;

        // Arama dizisi bitene kadar devam eder
        while (left <= right)
        {
            // Dizinin orta elemanını bul
            int mid = (left + right) / 2;

            // Eğer hedef sayı ortadaki sayı ise hedef bulundu
            if (array[mid] == target)
            {
                return true;  // Hedef bulundu, true döner
            }
            // Hedef sayı ortadakinden büyükse aramanın sağ tarafına geç
            else if (array[mid] < target)
            {
                left = mid + 1;  // Sol sınırı ortaya kaydırarak aramaya devam et
            }
            // Hedef sayı ortadakinden küçükse aramanın sol tarafına geç
            else
            {
                right = mid - 1;  // Sağ sınırı ortaya kaydırarak aramaya devam et
            }
        }

        // Eğer while döngüsünden çıkılırsa hedef sayı bulunamamıştır
        return false;
    }

    static void Main(string[] args)
    {
        // Kullanıcıdan sayı dizisini al
        Console.WriteLine("Bir dizi tam sayı girin (aralarına boşluk koyarak): ");
        string input = Console.ReadLine();  // Kullanıcının girdiği diziyi string olarak alır

        // String diziyi int dizisine çevir
        int[] numbers = Array.ConvertAll(input.Split(' '), int.Parse);  // Boşluklara göre ayırıp sayılara dönüştürür

        // Sayıları küçükten büyüğe sıralar
        Array.Sort(numbers);  // İkili arama sıralı dizide çalıştığı için diziyi sıralıyoruz

        // Sıralı diziyi ekrana yazdır
        Console.WriteLine("Sıralı dizi: " + string.Join(", ", numbers));

        // Kullanıcıdan aranacak hedef sayıyı al
        Console.WriteLine("Aramak istediğiniz sayıyı girin: ");
        int target = int.Parse(Console.ReadLine());  // Hedef sayıyı al

        // İkili arama ile sayının dizide olup olmadığını kontrol et
        bool found = BinarySearch(numbers, target);  // BinarySearch fonksiyonunu çağır

        // Sonuca göre dizide bulunup bulunmadığını ekrana yazdır
        if (found)
        {
            Console.WriteLine("Sayı dizide bulundu.");  // Eğer sayı bulunmuşsa
        }
        else
        {
            Console.WriteLine("Sayı dizide bulunamadı.");  // Eğer sayı bulunamamışsa
        }

        // Programın kapanmadan önce bir tuşa basılmasını bekle
        Console.WriteLine("Çıkmak için bir tuşa basın...");
        Console.ReadKey();  // Programı sonlandırmadan önce kullanıcıdan giriş bekler
    }
}
