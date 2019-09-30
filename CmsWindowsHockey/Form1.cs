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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            teamsList.AddRange(ReadFromFile("Elitserien", "2019/2020", "..\\..\\hockey.txt"));
            teamsList.AddRange(ReadFromFile("Elitserien", "2018/2019", "..\\..\\hockey2.txt"));


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

    }
}
