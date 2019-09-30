using CmsWindowsHockey.Hockey;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CmsWindowsHockey
{
    public partial class Form1 : Form
    {
        List<HockeyTeam> teamsList = new List<HockeyTeam>();
        string[] bilar = { "Volvo", "BMW", "Audi", "Skoda",
"Toyota", "Ford", "Mercedes","Seat", "Honda",
"Volkswagen","Opel", "Mazda","Suzuki" };
        public Form1()
        {
            InitializeComponent();
        }

        class HockeyTeamView
        {
            public string Name { get; set; }
            public int GP { get; set; }
            public int WonFullTime { get; set; }
            public int WonOvertime { get; set; }
            public int LostOvertime { get; set; }
            public int LostFulltime { get; set; }
            public int Points { get; set; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            teamsList.AddRange(ReadFromFile("Elitserien", "2019/2020", "..\\..\\hockey.txt"));
            teamsList.AddRange(ReadFromFile("Elitserien", "2018/2019", "..\\..\\hockey2.txt"));


            List<HockeyTeam> list = teamsList.Where(r => r.Goals > 10).ToList();
            int antal = teamsList.Count(r=>r.Goals > 10);

            var uniqueList = teamsList.Select(t => t.Season).Distinct();
            foreach (var season in uniqueList)
                comboBox1.Items.Add(season);
        }

        private static IEnumerable<HockeyTeam> ReadFromFile(string serie, string season, string file)
        {
            foreach (var line in System.IO.File.ReadAllLines(file))
            {
                var t = new HockeyTeam();
                var parts = line.Split('\t');
                t.Serie = serie;
                t.Season = season;
                t.Name = parts[1];
                t.GamesPlayed = Convert.ToInt32(parts[2]);
                t.ThreeP = Convert.ToInt32(parts[3]);
                t.TwoP = Convert.ToInt32(parts[4]);
                t.OneP = Convert.ToInt32(parts[5]);
                t.ZeroP = Convert.ToInt32(parts[6]);
                t.Goals = Convert.ToInt32(parts[7]);
                t.GoalsInslappta = Convert.ToInt32(parts[8]);
                yield return t;
            }

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string selectedSeason = comboBox1.SelectedItem.ToString();
            string nameToSearchFor = namnFilter.Text;



            dataGridView1.DataSource = teamsList
                                            .Where(team => team.Season == selectedSeason && 
                                                           team.Name.Contains(nameToSearchFor)     )
                                            .OrderByDescending(team => team.Points)
                                            .Select(team=>new HockeyTeamView
                                            {
                                                    Name = team.Name,
                                                    GP = team.GamesPlayed,
                                                    LostFulltime = team.ZeroP,
                                                    LostOvertime = team.OneP,
                                                    WonFullTime = team.ThreeP,
                                                    WonOvertime = team.TwoP,
                                                    Points = team.Points
                                            })
                                            .ToList();

        }
        class View
        {
            public View(string n)
            {
                name = n;
            }
            public string name { get; set; }
        }

        private void Lab1_Click(object sender, EventArgs e)
        {
            labGridView.DataSource = bilar.Where(bil => bil.StartsWith("V"))
                .Select(r=>new View(r)).ToList();
        }
    }
}
