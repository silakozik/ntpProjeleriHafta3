using System;

class Program
{
    static void Main()
    {
        // Enerji maliyeti matrisi örneği (her hücrede harcanacak enerji)
        int[,] grid = {
            { 1, 3, 1, 2 },
            { 2, 8, 4, 1 },
            { 3, 6, 1, 5 },
            { 2, 4, 3, 2 }
        };

        int N = grid.GetLength(0); // NxN matrisin boyutu

        // Enerji maliyeti matrisini ekrana yazdır
        Console.WriteLine("Enerji matrisi (grid):");
        PrintMatrix(grid, N);

        // En az enerji harcanan yolu bulmak için fonksiyon çağrısı
        int minEnergy = FindMinEnergy(grid, N);

        Console.WriteLine($"\nEn az enerji harcanarak (0, 0) noktasından (N-1, N-1) noktasına ulaşmak için gereken enerji: {minEnergy}");

        // Program kapanmadan önce bekleyelim
        Console.WriteLine("Çıkmak için bir tuşa basın...");
        Console.ReadKey();
    }

    // En az enerji harcanarak (0, 0)'dan (N-1, N-1)'e ulaşmak için gereken enerjiyi hesaplayan fonksiyon
    static int FindMinEnergy(int[,] grid, int N)
    {
        // Enerji maliyetlerini tutacak bir DP tablosu oluşturuyoruz
        int[,] dp = new int[N, N];

        // Başlangıç hücresine enerji maliyetini koy
        dp[0, 0] = grid[0, 0];

        // İlk satırın enerji maliyetini hesapla (sadece sağa hareket edilebilir)
        for (int j = 1; j < N; j++)
        {
            dp[0, j] = dp[0, j - 1] + grid[0, j];
        }

        // İlk sütunun enerji maliyetini hesapla (sadece aşağı hareket edilebilir)
        for (int i = 1; i < N; i++)
        {
            dp[i, 0] = dp[i - 1, 0] + grid[i, 0];
        }

        // Geri kalan hücrelerin enerji maliyetini hesapla
        for (int i = 1; i < N; i++)
        {
            for (int j = 1; j < N; j++)
            {
                // Sağa, aşağıya ve sağa çapraz hareketlerin minimumunu alıyoruz
                int right = dp[i, j - 1];
                int down = dp[i - 1, j];
                int diagonal = dp[i - 1, j - 1];

                dp[i, j] = grid[i, j] + Math.Min(right, Math.Min(down, diagonal));
            }
        }

        // DP tablosunu ekrana yazdır
        Console.WriteLine("\nDinamik Programlama Tablosu (dp):");
        PrintMatrix(dp, N);

        // Son hücreye (N-1, N-1) ulaşmak için gereken minimum enerji
        return dp[N - 1, N - 1];
    }

    // Bir matrisi 2D formatta ekrana yazdıran fonksiyon
    static void PrintMatrix(int[,] matrix, int N)
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}
