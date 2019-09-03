using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KanaliigaTeamTracker
{
        //Näihin +2 koska date sisältää 2 pilkkua
        //0  Date
        //1  Team name
        //2  Contact email
        //3  Contact Kapteenin discord
        //4  Contact Firman nettisivut,
        //5  Contact Kapteenin sähköposti(Oikeuksia varten),
        //6  Contact Varakapteenin discord,
        //7  Contact Varakapteenin sähköposti(Oikeuksia varten),
        //8  Player 1 name,
        //9  Player 1 email,
        //10 Player 1 Steam ID,
        //11 Player 1 Rank,
        //12 Player 2 name,
        //13 Player 2 email,
        //14 Player 2 Steam ID,
        //15 Player 2 Rank,
        //16 Player 3 name,
        //17 Player 3 email,
        //18 Player 3 Steam ID,
        //19 Player 3 Rank,
        //20 Player 4 name,
        //21 Player 4 email,
        //22 Player 4 email,
        //23 Player 4 Steam ID,
        //24 Player 4 Rank

    public partial class Form1 : Form
    {
        public static string filePath = @"C:\temp\file.csv";
        public static string outputFilePath = @"C:\temp\KanaliigaTeamOutput.csv";
        public static string finalOutput;

        
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = filePath;
            textBox3.Text = outputFilePath;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            //Generate button
            filePath = textBox1.Text;
            finalOutput = textBox3.Text;
            generateFile(filePath);
            textBox2.Text = "File generated to " + finalOutput.ToString();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            //Select output button
            
            openFileDialog1.ShowDialog();
            filePath = openFileDialog1.FileName;
            textBox1.Text = filePath;
        }
        
        private void Button3_Click(object sender, EventArgs e)
        {
            //Output file button
            folderBrowserDialog1.ShowDialog();
            outputFilePath = folderBrowserDialog1.SelectedPath;
            finalOutput = outputFilePath + "\\KanaliigaTeamOutput.csv";
            textBox3.Text = finalOutput;
        }
        public static void generateFile(string file)
        {
            List<Team> teams = new List<Team>();
            List<string> lines = File.ReadAllLines(file).ToList();
            foreach (var line in lines)
            {
                string[] entries = line.Split(',');
                Team newTeam = new Team();
                newTeam.TeamName = entries[3];
                newTeam.Rank1 = entries[13];
                newTeam.Rank2 = entries[17];
                newTeam.Rank3 = entries[21];

                //Tarkastus onko timissä neljännen pelaajan rankkia
                if (entries.Length == 26)
                {
                    newTeam.Rank4 = entries[entries.Length - 1];
                }
                else
                {
                    newTeam.Rank4 = "null";
                }
                newTeam.Player1 = entries[10];
                newTeam.Player2 = entries[14];
                newTeam.Player3 = entries[18];

                //Tarkastus onko timissä neljäs pelaaja
                if (entries.Length == 26)
                {
                    newTeam.Player4 = entries[22];
                }
                else
                {
                    newTeam.Player4 = "null";
                }

                teams.Add(newTeam);
            }

            //Poistetaan ylimmän rivin luoma "tiimi"
            teams.RemoveAt(0);

            List<string> output = new List<string>();

            //Lisätään ylimmäinen rivi tiedostoon
            output.Add("Team name,Player 1 name,Player 1 rank,Player 2 name,Player 3 rank,Player 3 name,Player 3 rank,Player 4 name,Player 4 rank");

            foreach (var team in teams)
            {
                output.Add($"Team: { team.TeamName }, { team.Player1 }, { team.Rank1 }, { team.Player2 }, { team.Rank2 }, { team.Player3 }, { team.Rank3 }, { team.Player4 }, { team.Rank4 } ");
            }

            //tiedoston polku ilman tiedostonimeä
            string outputPath;
            outputPath = Path.GetDirectoryName(finalOutput);

            //Jos kansiota ei löydy, luodaan polku
            System.IO.Directory.CreateDirectory(outputPath);

            //Luodaan tiedosto
            File.WriteAllLines(finalOutput, output);
        }
    }
}

