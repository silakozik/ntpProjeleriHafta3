using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Kullanıcıdan labirent boyutlarını al
        Console.WriteLine("Labirent boyutlarını girin (M ve N): ");
        int M = int.Parse(Console.ReadLine());
        int N = int.Parse(Console.ReadLine());

        // Labirentin geçilebilir olup olmadığını kontrol eden grid
        bool[,] labirent = new bool[M, N];

        // Labirentte her hücrenin geçilebilir olup olmadığını hesapla
        for (int i = 0; i < M; i++)
        {
            for (int j = 0; j < N; j++)
            {
                labirent[i, j] = IsCellOpen(i, j);  // Her hücre için kurallara göre kontrol
            }
        }

        // Başlangıç noktasından (0, 0) sağ alt köşeye (M-1, N-1) yol olup olmadığını kontrol et
        if (FindPath(labirent, M, N))
        {
            Console.WriteLine("Şehre ulaştınız!");
        }
        else
        {
            Console.WriteLine("Şehir kayboldu!");
        }
    }

    // Bir hücre geçilebilir mi kontrol eden fonksiyon
    static bool IsCellOpen(int x, int y)
    {
        // Hem x hem de y'nin basamaklarının asal olup olmadığını kontrol et
        if (!HasPrimeDigits(x) || !HasPrimeDigits(y))
            return false;

        // x ve y'nin toplamı, çarpımına bölünebiliyorsa kapı açılabilir
        int sum = x + y;
        int product = x * y;

        // Eğer çarpım sıfır ise, sadece (0,0) hücresinde olabiliriz, bu durumda geçiş mümkün
        if (product == 0)
            return true;

        return sum % product == 0;
    }

    // Bir sayının basamaklarının asal olup olmadığını kontrol eden fonksiyon
    static bool HasPrimeDigits(int num)
    {
        // Basamakları kontrol etmek için sayıyı string'e çeviriyoruz
        string numStr = num.ToString();
        foreach (char digit in numStr)
        {
            if (!IsPrimeDigit(digit - '0'))  // Her basamağı kontrol et
                return false;
        }
        return true;
    }

    // Basamak asal mı kontrol eden fonksiyon
    static bool IsPrimeDigit(int digit)
    {
        // Asal rakamlar: 2, 3, 5, 7
        return digit == 2 || digit == 3 || digit == 5 || digit == 7;
    }

    // Labirentteki başlangıçtan hedefe bir yol bulmaya çalışan fonksiyon (DFS kullanıyoruz)
    static bool FindPath(bool[,] labirent, int M, int N)
    {
        // Ziyaret edilen hücreleri tutmak için bir grid
        bool[,] visited = new bool[M, N];

        // Yol bulma algoritması (DFS)
        return DFS(labirent, visited, 0, 0, M, N);
    }

    // Derinlemesine arama (DFS) ile labirentte yol bulma
    static bool DFS(bool[,] labirent, bool[,] visited, int x, int y, int M, int N)
    {
        // Eğer hedefe ulaşıldıysa, doğru yol bulunmuştur
        if (x == M - 1 && y == N - 1)
            return true;

        // Eğer hücre labirentte değilse ya da ziyaret edildiyse ya da kapı kapalıysa geri dön
        if (x < 0 || y < 0 || x >= M || y >= N || visited[x, y] || !labirent[x, y])
            return false;

        // Hücreyi ziyaret edilmiş olarak işaretle
        visited[x, y] = true;

        // 4 yönlü hareket (sağ, aşağı, sol, yukarı)
        // Sağa git
        if (DFS(labirent, visited, x + 1, y, M, N))
            return true;

        // Aşağı git
        if (DFS(labirent, visited, x, y + 1, M, N))
            return true;

        // Sola git
        if (DFS(labirent, visited, x - 1, y, M, N))
            return true;

        // Yukarı git
        if (DFS(labirent, visited, x, y - 1, M, N))
            return true;

        // Eğer hiçbir yöne gidilemiyorsa, geri dön
        return false;
    }
}
