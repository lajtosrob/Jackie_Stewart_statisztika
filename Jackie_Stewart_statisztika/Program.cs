using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace Jackie_Stewart_statisztika
{
    internal class Program
    {
        static List<Jackie> jackies = new List<Jackie>();
        static void Main(string[] args)
        {


            //.2 Adatbeolvasás

            /* string[] sorok = File.ReadAllLines("DATAS\\Jackie.txt");
            for(int i = 1; i <sorok.Length; i++)
            {
                string[] adatok = sorok[i].Split("\t");

                int year = int.Parse(adatok[0]);
                int races = int.Parse(adatok[1]);
                int wins = int.Parse(adatok[2]);
                int podiums = int.Parse(adatok[3]);
                int poles = int.Parse(adatok[4]);
                int fastest = int.Parse(adatok[5]);

                Jackie jackieDatas = new(year, races, wins, podiums, poles, fastest);

                jackies.Add(jackieDatas);
            } 
            
            Console.WriteLine("A beolvasás készen van!");
             */

            StreamReader sr = new StreamReader("DATAS\\jackie.txt");

            string headerLine = sr.ReadLine();

            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split("\t");

                Jackie jackieDatas = new Jackie(
                  int.Parse(line[0]),
                 int.Parse(line[1]),
                int.Parse(line[2]),
                 int.Parse(line[3]),
                 int.Parse(line[4]),
                 int.Parse(line[5])
                );

                jackies.Add(jackieDatas);

            }

            sr.Close();
            Console.WriteLine("2. feladat: A beolvasás készen van!");

            //3.

            Console.WriteLine("3. feladat: " + jackies.Count());

            //4.

            Console.WriteLine("4. feladat: " + jackies.OrderByDescending(x => x.Races).First().Year);

            //5. 

            var decadeStatistics = jackies.GroupBy(x => x.Year / 10 * 10)
                .Select(g => new
                {
                    Decade = $"{g.Key}-es évek: ",
                    Wins = g.Sum(s => s.Wins)
                })
                .OrderByDescending(y => y.Wins);

            Console.WriteLine("5. feladat: ");

            foreach (var decade in decadeStatistics)
            {
                Console.WriteLine($"\t{decade.Decade}{decade.Wins} megnyert verseny");
            }

            //6.

            Console.WriteLine("6.feladat: jackie.html");

            string name = "Jackie Stewart";
            string htmlFileName = "jackie.html";

            // HTML generálása
            string htmlContent = GenerateHtmlContent(name);

            // HTML mentése a fájlba
            SaveHtmlFile(htmlFileName, htmlContent);

            Console.WriteLine($"{htmlFileName} fájl sikeresen létrehozva.");
        }

        static string GenerateHtmlContent(string name)
        {
            // HTML generálás
            string html = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Jackie Stewart statisztika</title>
                    <style>
                        table {{
                            border-collapse: collapse;
                        }}

                        table, th, td {{
                            border: 1px solid black;
                        }}

                        th, td {{
                            padding: 5px;
                        }}
                    </style>
                </head>
                <body>
                    <h1>{name}</h1>
                    <table>
                        <tr>
                            <th>Versenyzés éve</th>
                            <th>Versenyszám</th>
                            <th>Győzelmek száma</th>
                        </tr>";

                            // Adatok beillesztése a táblázatba
                            foreach (var stat in jackies)
                            {
                                html += $@"
                        <tr>
                            <td>{stat.Year}</td>
                            <td>{stat.Races}</td>
                            <td>{stat.Wins}</td>
                        </tr>";
                            }

                            // HTML zárása
                            html += @"
                    </table>
                </body>
                </html>";

            return html;
        }

        static void SaveHtmlFile(string fileName, string content)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(content);
            }
        }
    }

}



