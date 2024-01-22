using System;
using System.Linq;

namespace souborna_prace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // SČÍTÁNÍ ZLOMKŮ

            do
            {
                int pocet;
                Console.Write("Zadejte počet zlomků k sečtení (min. 3): ");
                string pocet_zlomku = Console.ReadLine();

                while (!int.TryParse(pocet_zlomku, out pocet) || pocet < 3)
                {
                    Console.WriteLine("Počet zlomků k sečtení musí být celé číslo o minimální hodnotě 3!");
                    Console.Write("Zadejte počet zlomků k sečtení (min. 3): ");
                    pocet_zlomku = Console.ReadLine();
                }

                string[] zlomky = new string[pocet];
                int[] citatele = new int[pocet];
                int[] jmenovatele = new int[pocet];

                for (int i = 0; i < pocet; i++)
                {
                    bool validniZlomek = false;

                    while (!validniZlomek) // opakuje tento cyklus, dokud validniZlomek není true
                    {
                        Console.Write($"Zadejte {i + 1}. zlomek ve tvaru a/b: ");
                        zlomky[i] = Console.ReadLine();

                        string[] zlomekCasti = zlomky[i].Split('/');

                        if (zlomekCasti.Length == 2 && zlomky[i].Contains("/") &&
                            !string.IsNullOrEmpty(zlomekCasti[0]) && !string.IsNullOrEmpty(zlomekCasti[1]) &&
                            int.TryParse(zlomekCasti[0], out citatele[i]) && int.TryParse(zlomekCasti[1], out jmenovatele[i]))
                        {
                            if (jmenovatele[i] != 0)
                                validniZlomek = true;
                            else
                                Console.WriteLine($"Ve {i + 1}. zlomku nemůže být nula ve jmenovateli!");
                        }
                        else
                        {
                            Console.WriteLine($"{i + 1}. zlomek není ve správném tvaru!");
                        }
                    }

                    // Uprava pro záporné zlomky
                    if (zlomky[i].StartsWith("-"))
                    {
                        citatele[i] = -citatele[i];
                        zlomky[i] = $"{citatele[i]}/{Math.Abs(jmenovatele[i])}";
                    }
                }

                // Vypsání jednotlivých zlomků
                Console.WriteLine();
                for (int i = 0; i < pocet; i++)
                {
                    Console.WriteLine($"Zlomek {i + 1}: {zlomky[i]}");
                }

                // Násobení jmenovatelů
                int spolecnyJmenovatel = 1;
                foreach (int jmenovatel in jmenovatele)
                {
                    spolecnyJmenovatel *= jmenovatel;
                }

                // Rozšíření čitatelů
                for (int i = 0; i < pocet; i++)
                {
                    citatele[i] *= (int)(spolecnyJmenovatel / jmenovatele[i]);
                }

                // Sečtení čitatelů
                int soucetCitatelu = citatele.Sum();

                // Zkrácení zlomku
                int gcd = NajdiNejvetsiSpolecnyDelitel(Math.Abs(soucetCitatelu), spolecnyJmenovatel);
                soucetCitatelu /= gcd;
                spolecnyJmenovatel /= gcd;

                // Vypsání výsledku
                if (soucetCitatelu < 0)
                {
                    Console.WriteLine($"Součet zlomků je: -{Math.Abs(soucetCitatelu)}/{spolecnyJmenovatel}");
                }
                else
                {
                    Console.WriteLine($"Součet zlomků je: {soucetCitatelu}/{spolecnyJmenovatel}");
                }

                // Otázka, zda pokračovat nebo ukončit
                Console.WriteLine();
                Console.Write("Pro ukončení programu stiskněte 'k'. Pro pokračování stiskněte jakoukoliv jinou klávesu. ");
                char volba = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.WriteLine();

                if (volba == 'k')
                {
                    break; // Ukončení programu
                }

            } while (true);

            Console.WriteLine("Program ukončen.");
        }

        // Funkce pro nalezení největšího společného dělitele
        static int NajdiNejvetsiSpolecnyDelitel(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
