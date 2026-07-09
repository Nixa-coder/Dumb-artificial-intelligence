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

                if(pitanje == "help")
                {
                    Help();
                    continue;
                }
                if (pitanje == "exit")
                {
                    Console.WriteLine("You are exiting the program. Goodbye!");
                    Thread.Sleep(2000);
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
                if(pitanje.StartsWith("all questions"))
                {
                    ShowQuestions();
                    continue;
                }
                if(pitanje.StartsWith("search"))
                {
                    Search(pitanje);
                    continue;
                }

                ProveraDaLiPostojiRec(pitanje);
            }

            
        }

        private static void Search(string command)
        {
            string searchTerm = command.Substring(7).Trim();
            if (dic.ContainsKey(searchTerm))
            {
                foreach(var questions in dic.Keys)
                {
                    if(questions.Contains(searchTerm))
                    {
                        Console.WriteLine($"Question: {questions}");
                    }
                }
            }
        }

        public static void Help() 
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("1. all questions - Displays all the questions in memory.");
            Console.WriteLine("2. rewrite <question> - Allows you to rewrite the answer for a specific question.");
            Console.WriteLine("3. delete <question> - Deletes a specific question and its answer from memory.");
            Console.WriteLine("4. search <term> - Searches for questions containing the specified term.");
            Console.WriteLine("5. exit - Exits the program.");
        }

        public static void ShowQuestions()
        {
            if (dic.Count == 0)
            {
                Console.WriteLine("No questions in memory.");
                return;
            }
            else
            {
                foreach (var question in dic.Keys)
                {
                    Console.WriteLine(question);
                }
            }
        }
        static string UcitajViseLinija()
        {
            Console.WriteLine("(Ukucaj 'kraj' u novom redu kada završiš)");
            string sveZajedno = "";
            string linija;

            while ((linija = Console.ReadLine()) != "kraj")
            {
                sveZajedno += linija + "\n";
            }

            return sveZajedno;
        }
        static void Rewrite(string command)
        {
            string QuestionForRewrite = command.Substring(8).Trim();
            if (dic.ContainsKey(QuestionForRewrite))
            {
                Console.WriteLine($"Current answer: { dic[QuestionForRewrite]}");
                Console.WriteLine("Please enter the new answer:");
                string newAnswer = UcitajViseLinija();
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
                string odgovor = UcitajViseLinija();
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
                {
                    string sigurnaVrednost = par.Value.Replace("\n", "\\n");
                    sw.WriteLine(par.Key + "|" + sigurnaVrednost);
                }
            }
        }

        static void UcitajZnanje()
        {
            if (!File.Exists(fajl)) return;

            foreach (var linija in File.ReadAllLines(fajl))
            {
                var delovi = linija.Split('|');
                if (delovi.Length == 2)
                {
                    string vrednost = delovi[1].Replace("\\n", "\n");
                    dic[delovi[0]] = vrednost;
                }
            }
        }
    }
}