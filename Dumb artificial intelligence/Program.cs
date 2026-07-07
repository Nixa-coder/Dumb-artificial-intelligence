using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Dumb_artificial_intelligence
{
    internal class Program
    {
        static Dictionary<string, string> dic = new Dictionary<string, string>();
        static string fajl = "znanje.txt";

        static void Main(string[] args)
        {
            Console.Title = "Dumb Artificial Intelligence";
            UcitajZnanje();

            Console.WriteLine("Hello, how may I assist you?");

            while (true)
            {
                Console.Write("\n> ");

                string pitanje = Console.ReadLine().ToLower();

                if (pitanje == "exit")
                {
                    break;
                }
                if(pitanje.StartsWith("delete"))
                {
                    Delete(pitanje);
                    continue;
                }
                if (pitanje.StartsWith("rewrite"))
                {
                    Rewrite(pitanje);
                    continue;
                }
                {
                    
                }

                ProveraDaLiPostojiRec(pitanje);
            }

            
        }

        static void Rewrite(string command)
        {
            string QuestionForRewrite = command.Substring(8).Trim();
            if (dic.ContainsKey(QuestionForRewrite))
            {
                Console.WriteLine($"Current answer: { dic[QuestionForRewrite]}");
                Console.WriteLine("Please enter the new answer:");
                string newAnswer = Console.ReadLine();
                dic[QuestionForRewrite] = newAnswer;
                SacuvajZnanje();
                Console.WriteLine("Answer updated successfully.");
            }
            else
            {
                Console.WriteLine("That question doesnt exist in memory");
            }
        }
        static void Delete(string command)
        {
            string QuestionForErasure = command.Substring(7).Trim();

            if (dic.ContainsKey(QuestionForErasure))
            {
                dic.Remove(QuestionForErasure);
                SacuvajZnanje();
                Console.WriteLine("The question and answer had been exterminated from the face of this ai.");
            }
            else
            {
                Console.WriteLine("That question doesn't exist in the memory.");
            }
        }

        static void ProveraDaLiPostojiRec(string pitanje)
        {
            if (dic.ContainsKey(pitanje))
            {
                Console.WriteLine(dic[pitanje]);
            }
            else
            {
                Console.WriteLine("I don't know that one. Teach me the answer:");
                string odgovor = Console.ReadLine();
                dic.Add(pitanje, odgovor);
                SacuvajZnanje();
                Console.WriteLine("Got it, thanks!");
            }
        }

        static void SacuvajZnanje()
        {
            using (StreamWriter sw = new StreamWriter(fajl))
            {
                foreach (var par in dic)
                    sw.WriteLine(par.Key + "|" + par.Value);
            }
        }

        static void UcitajZnanje()
        {
            if (!File.Exists(fajl)) return;

            foreach (var linija in File.ReadAllLines(fajl))
            {
                var delovi = linija.Split('|');
                if (delovi.Length == 2)
                    dic[delovi[0]] = delovi[1];
            }
        }
    }
}