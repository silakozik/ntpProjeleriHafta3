using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Mevcut tarihi sabit bir tarihe ayarlamak
        // DateTime.Now yerine istediğiniz bir tarihi ayarlayabilirsiniz
        DateTime currentDate = new DateTime(2023, 10, 28); // Burada yılı, ayı ve günü belirleyebilirsiniz

        // Alternatif olarak DateTime.Now kullanabilirsiniz
        // DateTime currentDate = DateTime.Now;

        // Zaman yolcusunun gitmek istediği yıl aralığı
        int startYear = 2000;
        int endYear = 3000;

        // Geçerli tarihleri saklamak için bir liste
        List<string> validDates = new List<string>();

        // Yıl döngüsü: 2000 ile 3000 arasında
        for (int year = startYear; year <= endYear; year++)
        {
            // Gelecek tarihlere odaklanıyoruz, bu yüzden şu anki yıldan küçük olanları atla
            if (year < currentDate.Year) continue;

            // Ay döngüsü: 1 ile 12 aylar arasında (Ocak'tan Aralık'a)
            for (int month = 1; month <= 12; month++)
            {
                // Ayın rakamlarının toplamı çift olmalı (şart 2)
                if (!IsMonthValid(month)) continue; // Eğer ay geçerli değilse bir sonraki aya geç

                // Gün döngüsü: 1 ile o ayın son gününe kadar (Ocak için 31, Şubat için 28 veya 29)
                int daysInMonth = DateTime.DaysInMonth(year, month); // O ay kaç gün sürüyor (yıl ve ay kombinasyonuna bağlı)
                for (int day = 1; day <= daysInMonth; day++)
                {
                    // Gün asal sayı olmalı (şart 1)
                    if (!IsPrime(day)) continue; // Eğer gün asal değilse bir sonraki güne geç

                    // Yılın rakamları toplamı, yılın dörtte birinden küçük olmalı (şart 3)
                    if (!IsYearValid(year)) continue; // Eğer yıl geçerli değilse bir sonraki yıla geç

                    // Geçerli tarihlerden biri bulundu, bu tarihi DateTime nesnesi olarak oluştur
                    DateTime validDate = new DateTime(year, month, day); // Yıl, ay, gün ile geçerli tarih oluştur
                    if (validDate >= currentDate) // Tarih bugünden sonraki bir tarih olmalı
                    {
                        // Geçerli tarihi listeye ekle
                        validDates.Add(validDate.ToString("dd/MM/yyyy")); // Tarihi belirtilen formatta (gün/ay/yıl) listeye ekle
                    }
                }
            }
        }

        // Geçerli tarihleri ekrana yazdır
        Console.WriteLine("Cihazın kabul ettiği tarihler:");
        foreach (var date in validDates)
        {
            // Her bir geçerli tarihi ekrana yazdır
            Console.WriteLine(date);
        }

        // Programın kapanmasını engellemek için kullanıcıdan bir tuşa basmasını bekle
        Console.WriteLine("Geleceğe gidebileceğiniz tarihler bunlar. İyi yolculuklar :)) ");
        Console.WriteLine("Çıkmak için bir tuşa basın...");
        Console.ReadKey(); // Kullanıcıdan bir tuş girişini bekleyerek programı durdur
    }

    // Ayın rakamlarının toplamı çift olmalı
    static bool IsMonthValid(int month)
    {
        // Ayı basamaklarına bölerek rakamlarının toplamını hesapla (örneğin: 12 -> 1 + 2 = 3)
        int sumOfDigits = 0;
        while (month > 0)
        {
            sumOfDigits += month % 10; // Son basamağı topla
            month /= 10; // Son basamağı at (bir sonraki basamağa geç)
        }
        return sumOfDigits % 2 == 0; // Rakamlar toplamı çift ise true döndür
    }

    // Gün asal sayı olmalı
    static bool IsPrime(int number)
    {
        // Asal sayılar 1'den büyük olmalı
        if (number <= 1) return false; // 1 ve daha küçük sayılar asal değildir
        // Sayının asal olup olmadığını kontrol et (2'den √number'a kadar bölünen olup olmadığını kontrol et)
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0) return false; // Eğer tam bölünüyorsa asal değildir
        }
        return true; // Eğer hiçbir sayıya bölünmüyorsa asaldır
    }

    // Yılın rakamları toplamı, yılın dörtte birinden küçük olmalı
    static bool IsYearValid(int year)
    {
        // Yılın rakamlarının toplamını hesapla (örneğin: 2024 -> 2 + 0 + 2 + 4 = 8)
        int sumOfDigits = 0;
        int tempYear = year; // Yıl değerini geçici değişkende tutuyoruz
        while (tempYear > 0)
        {
            sumOfDigits += tempYear % 10; // Son basamağı topla
            tempYear /= 10; // Son basamağı at (bir sonraki basamağa geç)
        }
        // Yılın rakamları toplamı, yılın dörtte birinden küçük olmalı (örneğin: 2024 -> 2024 / 4 = 506, ve 8 < 506)
        return sumOfDigits < (year / 4); // Şart sağlanıyorsa true döndür
    }
}
