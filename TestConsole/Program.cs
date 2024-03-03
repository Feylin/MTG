using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessLogic;
using DataAccess;
using PWDTK_DOTNET451;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repo = new Repository();

            //var cards = repo.Cards();

            //var card1 = cards[0];
            //var card2 = cards[1];

            //BE_Card card1 = new BE_Card("Test ", "Red", 1, 1, 3, "image", "creature", "", "Common", "Legends");
            //BE_Card card2 = new BE_Card("Test", "Red", 1, 1, 3, "image", "creature", "", "Common", "Legends");
            //BE_Card card3 = new BE_Card("Test", "Red", 1, 1, 3, "image", "creature");

            //List<BE_Card> cardList = new List<BE_Card>();
            //cardList.Add(card1);
            //cardList.Add(card2);
            //cardList.Add(card3);

            //List<BE_Card> hashCodeList = cards.ToList();
            //List<BE_Card> hashCodeList = cardList;

            //Stopwatch sw = new Stopwatch();

            //sw.Start();

            //hashCodeList.GroupBy(hash => hash)
            //    .Where(grouping => grouping.Count() > 1)
            //    .ToList()
            //    .ForEach(groupItem => Console.WriteLine(@"{0} duplicated {1} times with these values {2}",
                                                        //groupItem.Key,
                                                        //groupItem.Count(),
                                                        //string.Join(" ", groupItem)));

            //sw.Stop();

            //Console.WriteLine(sw.ElapsedTicks);

            //sw.Restart();

            //var hashset = new HashSet<int>();
            //if (hashCodeList.Where(s => s != null).Any(s => !hashset.Add(s.GetHashCode())))
            //    Console.WriteLine(@"there are duplicates");

            //sw.Stop();

            //Console.WriteLine(sw.ElapsedTicks);
             
            //byte[] saltBytes = PWDTK.GetRandomSalt(); // 64 bytes length is standard
            
            //int iteration = 23600;
            //string password = "PixicoP";
            
            //byte[] hashbBytes = PWDTK.PasswordToHash(saltBytes, password, iteration);

            //repo.AddUser("Feylin", Convert.ToBase64String(saltBytes), Convert.ToBase64String(hashbBytes));

            //byte[] salt = Convert.FromBase64String("liu3CT5QJ/Im5hw7zakY7ANVlUFGvoxMAJxQTmmrUMZ6ZkUKvOv0X+I5oZ8Hra9Ls6EIJktA5HNeSIfK8OHsUA==");
            //byte[] hash = Convert.FromBase64String("kbnGSEm3LFrX5Ev2gdi5DGUdEALLtbGUV6c2RxSmR7/sYtEtlI+1KEKmlmeHNqFL7QBaauRQa/mpaiTVQxN9MA==");

            //bool comparePassword = PWDTK.ComparePasswordToHash(salt, password, hash, iteration);

            //Console.WriteLine(comparePassword);

            //try
            //{
            //    Console.WriteLine(repo.Random(10, 10));
            //}
            //catch (DivideByZeroException ex)
            //{
                
            //    Console.WriteLine(ex);
            //}

            //List<BE_Card> cards = new List<BE_Card>();

            //for (int i = 0; i < 3; i++)
            //{
            //    BE_Card card = repo.GetRandomCard("Uncommon", "Theros");
            //    cards.Add(card);
            //}

            /*
             * Completed sets:
             * Born of the Gods, Theros, Dragon's Maze, Gatecrash, Return to Ravnica, Avacyn Restored, Dark Ascension, Innistrad, Magic 2014
             * Card app size 1076; 621
             */
            List<BE_Card> booster = repo.Boosterpack("Dragon's Maze");

            foreach (var card in booster)
            {
                Console.WriteLine(@"{0,-28} {1,-12} {2}", card.Name, card.Rarity, card.Set);
            }
        }
    }
}
