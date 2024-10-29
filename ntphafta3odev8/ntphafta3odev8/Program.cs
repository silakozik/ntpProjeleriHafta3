using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Şifrelenmiş mesajı girin (Örnek bir şifreli mesaj)
        string encryptedMessage = "şifrelenmiş mesaj burada"; // Şifrelenmiş mesaj

        // Mesajın şifresini çöz
        string decryptedMessage = DecryptMessage(encryptedMessage);

        // Çözülmüş mesajı ekrana yazdır
        Console.WriteLine("Çözülmüş Mesaj: " + decryptedMessage);

        // Çıkışı görmek için bir tuşa basılmasını bekle
        Console.WriteLine("Çıkmak için bir tuşa basın...");
        Console.ReadKey();
    }

    // Şifrelenmiş mesajı çözme fonksiyonu
    static string DecryptMessage(string encryptedMessage)
    {
        // Fibonacci dizisini hesapla (mesajın uzunluğu kadar)
        List<int> fibonacciNumbers = GenerateFibonacci(encryptedMessage.Length);

        char[] decryptedMessage = new char[encryptedMessage.Length];

        // Her bir karakterin modunu ve Fibonacci dönüşümünü tersine uygula
        for (int i = 0; i < encryptedMessage.Length; i++)
        {
            int encryptedAscii = (int)encryptedMessage[i]; // Şifreli karakterin ASCII değeri
            int fibonacciNumber = fibonacciNumbers[i]; // Fibonacci sayısı

            // Pozisyonun asal olup olmadığını kontrol et
            if (IsPrime(i + 1))
            {
                // Pozisyon asal ise mod 100 işlemi uygulanmış
                encryptedAscii = ReverseMod(encryptedAscii, 100);
            }
            else
            {
                // Pozisyon asal değilse mod 256 işlemi uygulanmış
                encryptedAscii = ReverseMod(encryptedAscii, 256);
            }

            // Ters Fibonacci dönüşümünü uygula (şifre çözme)
            int originalAscii = encryptedAscii / fibonacciNumber;
            decryptedMessage[i] = (char)originalAscii; // Orijinal karakteri ASCII'den dönüştür
        }

        return new string(decryptedMessage);
    }

    // Fibonacci serisi oluşturma fonksiyonu
    static List<int> GenerateFibonacci(int length)
    {
        List<int> fibonacciNumbers = new List<int> { 1, 1 };
        for (int i = 2; i < length; i++)
        {
            fibonacciNumbers.Add(fibonacciNumbers[i - 1] + fibonacciNumbers[i - 2]);
        }
        return fibonacciNumbers;
    }

    // Sayının asal olup olmadığını kontrol eden fonksiyon
    static bool IsPrime(int number)
    {
        if (number <= 1)
            return false;
        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0)
                return false;
        }
        return true;
    }

    // Mod işlemini tersine çevirme fonksiyonu (kalanı eski haline getirmek için)
    static int ReverseMod(int encryptedAscii, int modValue)
    {
        // ASCII değerini modValue modundan geri çevirmeye çalışıyoruz
        for (int originalAscii = 0; originalAscii < modValue; originalAscii++)
        {
            if (originalAscii % modValue == encryptedAscii)
                return originalAscii;
        }
        return encryptedAscii; // Eğer çözüm bulunamazsa aynı değeri döndür
    }
}
