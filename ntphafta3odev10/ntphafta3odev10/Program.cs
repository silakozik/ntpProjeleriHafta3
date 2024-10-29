using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Kullanıcıdan sayı dizisini al
        Console.WriteLine("Lütfen aralarına boşluk koyarak sayıları girin (örn: 3 5 2 8):");
        string input = Console.ReadLine();  // Kullanıcıdan aldığı string girdisini okur
        int[] numbers = Array.ConvertAll(input.Split(' '), int.Parse);  // String girdisini ayırır ve her bir parçayı int dizisine çevirir

        // Kullanılacak matematiksel operatörler dizisi
        char[] operators = { '+', '-', '*', '/' };  // 4 farklı operatör: toplama, çıkarma, çarpma ve bölme

        // Geçerli kombinasyonları depolamak için bir liste oluştur
        List<string> results = new List<string>();  // Tüm geçerli (sonuç sıfırdan büyük olan) ifadeleri saklar
        bool allResultsValid = true;  // Tüm sonuçların sıfırdan büyük olup olmadığını kontrol eder

        // DFS ile ilk sayıyı başlangıç noktası yaparak işlemlere başla
        FindValidExpressions(numbers, operators, 1, numbers[0].ToString(), numbers[0], ref results, ref allResultsValid);

        // Eğer geçerli kombinasyonlar varsa bunları ekrana yazdır
        if (results.Count > 0)
        {
            Console.WriteLine("Geçerli kombinasyonlar:");
            foreach (var result in results)
            {
                Console.WriteLine(result);  // Her bir geçerli sonucu listeye ekle ve ekrana yazdır
            }
        }

        // Eğer tüm sonuçlar sıfırdan büyükse başarılı olduğunu bildiren bir mesaj göster
        if (allResultsValid)
        {
            Console.WriteLine("\nTebrikler, labirenti geçtiniz!");
        }
        else
        {
            Console.WriteLine("\nBazı kombinasyonlar sıfırdan küçük olduğu için labirenti geçemediniz.");
        }

        Console.WriteLine("Çıkmak için bir tuşa basın...");
        Console.ReadKey();  // Programı durdurmadan önce kullanıcının bir tuşa basmasını bekler
    }

    // DFS ile sayılar arasına operatör ekleyerek geçerli kombinasyonları bulma
    // Bu fonksiyon her bir sayıyı sırayla alır ve her operatörle kombinasyonlar dener
    static void FindValidExpressions(int[] numbers, char[] operators, int index, string expression, int currentValue, ref List<string> results, ref bool allResultsValid)
    {
        // Eğer dizinin sonuna ulaştıysak, sonucu kontrol et
        if (index == numbers.Length)
        {
            // Eğer sonuç sıfırdan büyükse, bu geçerli bir kombinasyon olarak kabul edilir
            if (currentValue > 0)
            {
                results.Add(expression + " = " + currentValue);  // Geçerli ifadeyi listeye ekle
            }
            else
            {
                // Sonuç sıfırdan küçükse, bunu geçersiz olarak kabul et ve ekrana yazdır
                Console.WriteLine($"Hatalı kombinasyon: {expression} = {currentValue} (Sonuç sıfırdan küçük)");
                allResultsValid = false;  // Eğer herhangi bir sonuç sıfırdan küçükse bu, tüm sonuçların geçerli olmadığını işaret eder
            }
            return;  // Sonuç değerlendirildikten sonra işlevi sonlandır
        }

        // Her bir operatör için recursive (derinlemesine arama) yap
        foreach (var op in operators)
        {
            int nextNumber = numbers[index];  // Dizideki sıradaki sayıyı al
            string newExpression = expression + " " + op + " " + nextNumber;  // Yeni matematiksel ifadeyi oluştur (operatörü ve sayıyı ekleyerek)

            // Operatöre göre işlemi gerçekleştir ve sonucun güncel değerini hesapla
            switch (op)
            {
                case '+':
                    // Toplama işlemi: mevcut değere sıradaki sayıyı ekle
                    FindValidExpressions(numbers, operators, index + 1, newExpression, currentValue + nextNumber, ref results, ref allResultsValid);
                    break;
                case '-':
                    // Çıkarma işlemi: mevcut değerden sıradaki sayıyı çıkar
                    FindValidExpressions(numbers, operators, index + 1, newExpression, currentValue - nextNumber, ref results, ref allResultsValid);
                    break;
                case '*':
                    // Çarpma işlemi: mevcut değeri sıradaki sayıyla çarp
                    FindValidExpressions(numbers, operators, index + 1, newExpression, currentValue * nextNumber, ref results, ref allResultsValid);
                    break;
                case '/':
                    // Bölme işlemi: sıfıra bölme hatasını engellemek için kontrol
                    if (nextNumber != 0)  // Eğer sıradaki sayı sıfır değilse bölme işlemini gerçekleştir
                    {
                        FindValidExpressions(numbers, operators, index + 1, newExpression, currentValue / nextNumber, ref results, ref allResultsValid);
                    }
                    // Eğer sıfırsa, bu durumda bölme işlemi yapılmaz (sıfıra bölme hatası önlenir)
                    break;
            }
        }
    }
}
