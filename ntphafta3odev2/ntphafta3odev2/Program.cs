using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Kullanıcıdan pozitif tam sayıları almak için bir liste oluşturuyoruz
        List<int> numbers = new List<int>();  // Girdi sayıları tutmak için bir liste

        int number;  // Kullanıcının girdiği sayıyı tutacak değişken
        Console.WriteLine("Pozitif tam sayılar girin (0 ile durdurabilirsiniz): ");

        // Kullanıcı 0 girene kadar sayıları almaya devam et
        do
        {
            // Girdiyi güvenli bir şekilde kontrol et
            string input = Console.ReadLine();  // Kullanıcının girdiği string
            bool isValidNumber = int.TryParse(input, out number);  // String'i tam sayıya çevir ve başarı durumunu kontrol et

            // Eğer geçerli bir sayı değilse uyarı ver
            if (!isValidNumber)
            {
                Console.WriteLine("Lütfen geçerli bir tam sayı girin.");
                continue;  // Geçersiz giriş olduğunda döngünün başına dön
            }

            // Pozitif sayı ise listeye ekle
            if (number > 0)
            {
                numbers.Add(number);  // Sadece pozitif sayılar listeye eklenir
            }

        } while (number != 0);  // 0 girildiğinde döngü sonlanır

        // Eğer sayı girilmediyse işlem yapma
        if (numbers.Count == 0)
        {
            Console.WriteLine("Hiçbir sayı girilmedi.");
            return;  // Liste boş ise programı sonlandır
        }

        // Ortalamayı hesapla
        double average = numbers.Average();  // Sayıların ortalamasını hesaplar

        // Medyanı hesapla
        numbers.Sort();  // Sayıları küçükten büyüğe sıralıyoruz
        double median;
        int count = numbers.Count;  // Listenin eleman sayısını al
        if (count % 2 == 0)
        {
            // Çift sayıda eleman varsa ortadaki iki sayının ortalamasını al
            median = (numbers[count / 2 - 1] + numbers[count / 2]) / 2.0;
        }
        else
        {
            // Tek sayıda eleman varsa ortadaki sayı medyandır
            median = numbers[count / 2];
        }

        // Sonuçları ekrana yazdır
        Console.WriteLine("Ortalama: " + average);  // Ortalama değeri yazdır
        Console.WriteLine("Medyan: " + median);  // Medyan değeri yazdır

        // Programı sonlandırmadan önce kullanıcıdan bir tuşa basmasını bekle
        Console.WriteLine("Çıkmak için bir tuşa basın...");
        Console.ReadKey();  // Kullanıcının girişini bekle
    }
}
