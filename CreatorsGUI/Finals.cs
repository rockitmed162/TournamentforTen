using CreatorsApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static CreatorsGUI.CreatorsGUI;
using ListBox = System.Windows.Forms.ListBox;
using System.Windows.Documents;
namespace CreatorsGUI
{
    public partial class Finals : Form {

        public readonly ListBox listBoxPlayers;
        public readonly CreatorsGUI creatorsGUIForm;
        public readonly TournamentLogic tournamentLogic;
        private Random random = new Random();
        private List<TextBox> playerTextBoxes = new List<TextBox>();
        private List<TextBox> semifinalTextBoxes = new List<TextBox>();


        public Finals(CreatorsGUI creatorsGUI, List<string> players) {
            InitializeComponent();
            creatorsGUIForm = creatorsGUI;
            this.listBoxPlayers = new ListBox();
            tournamentLogic = new TournamentLogic();
            ShufflePlayers(players);
            UpdatePlayersList(players);


            SimulateTournament(players);
            playerTextBoxes.Add(textBox22);
            playerTextBoxes.Add(textBox23);
            playerTextBoxes.Add(textBox24);

            // Lisää semifinaalien tekstiboksit listalle
            semifinalTextBoxes.Add(semifinal1);
            semifinalTextBoxes.Add(semifinal2);



        }
        /*     public void LoadTextFiles() {
                 try {
                     /*  string filePath = "EntryFee.txt"; // File name
                       string tournamentnames = "TournamentInfo.txt"; // File name
                       string rounds = "round.txt"; // File name
                    //   string map = "map.txt"; // File name


                           // Read all lines from the file and populate the ComboBox
                           string[] teams = File.ReadAllLines(filePath);
                           filePath = entryfees.Text;

                           // Read all lines from the file and populate the ComboBox
                           string[] teams2 = File.ReadAllLines(tournamentnames);
                           tournamentnames = Name.Text;


                           // Read all lines from the file and populate the ComboBox
                           string[] teams3 = File.ReadAllLines(rounds);
                           rounds = Rnd.Text;
                       */

        // Read all lines from the file and populate the ComboBox
        /*
                        string map = File.Exists("map.txt")? File.ReadAllText("map.txt") : "hehe";
                        string tournamentnames = File.Exists("TournamentInfo.txt")? File.ReadAllText("TournamentInfo.txt") : "hehe2";
                        string rounds = File.Exists("round.txt")? File.ReadAllText("round.txt") : "hehe3";
                        string filePath = File.Exists("filePath.txt")? File.ReadAllText("filePath.txt") : "hehe1";

                    }
                    catch { }
                    }
        */
        private void Name_TextChanged(object sender, EventArgs e) {
            try {
                string map = File.Exists("~/map.txt")? File.ReadAllText("~/map.txt") : "hehe";
                string tournamentnames = File.Exists("~/TournamentInfo.txt")? File.ReadAllText("~/TournamentInfo.txt") : "hehe2";
                string rounds = File.Exists("~/round.txt")? File.ReadAllText("~/round.txt") : "hehe3";
                string filePath = File.Exists("~/filePath.txt")? File.ReadAllText("~/filePath.txt") : "hehe1";

                entryfees.Text = filePath;
                Name.Text = tournamentnames;
                Rnd.Text = rounds;
                Map0.Text = map;

                MessageBox.Show("refreshed tournament configures!");
            }
            catch (Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void ShufflePlayers(List<string> players) {
            Random rng = new Random();
            int n = players.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                string value = players[k];
                players[k] = players[n];
                players[n] = value;
            }
        }


        public Finals(CreatorsGUI creatorsGUIForm) {
            this.creatorsGUIForm = creatorsGUIForm;
        }

        public void UpdatePlayersList(List<string> players) {
            // Päivitä pelaajien lista tarpeidesi mukaan
            listBoxPlayers.DataSource = players;


            // Näytä pelaajat tekstibokseissa (olettaen, että pelaajien määrä ei ylitä 11)
            for (int i = 0; i < players.Count && i < 11; i++) {
                switch (i) {
                    case 0:
                        textBoxPlayer1.Text = players[i];
                        break;
                    case 1:
                        textBoxPlayer2.Text = players[i];
                        break;
                    case 2:
                        textBoxPlayer3.Text = players[i];
                        break;
                    case 3:
                        textBoxPlayer4.Text = players[i];
                        break;
                    case 4:
                        textBoxPlayer5.Text = players[i];
                        break;
                    case 5:
                        textBoxPlayer6.Text = players[i];
                        break;
                    case 6:
                        textBoxPlayer7.Text = players[i];
                        break;
                    case 7:
                        textBoxPlayer8.Text = players[i];
                        break;
                    case 8:
                        textBoxPlayer9.Text = players[i];
                        break;
                    case 9:
                        textBoxPlayer10.Text = players[i];
                        break;


                        // ... (sama kuin edellisessä vastauksessa)
                }
            }
        }
        private void StartTournamentButton_Click_1(object sender, EventArgs e) {
            // Hanki pelaajat tekstikentistä tai muista käyttöliittymän elementeistä           
            // Hae pelaajat CreatorsGUI-formista
            // Päivitä pelaajien lista
            CreatorsGUI creatorsGUIForm = AppData.CreatorsGUIForm;

            if (creatorsGUIForm != null) {
                List<string> players = creatorsGUIForm.Players;
                UpdatePlayersList(players);
                List<string> winners = tournamentLogic.SimulateTournament(players);
                Console.WriteLine("test");




                /*
                    {
              textBoxPlayer1.Text,
              textBoxPlayer2.Text,
              textBoxPlayer3.Text,
              textBoxPlayer4.Text,
              textBoxPlayer5.Text,
              textBoxPlayer6.Text,
              textBoxPlayer7.Text,
              textBoxPlayer8.Text,
              textBoxPlayer9.Text,
              textBoxPlayer10.Text,

                  };*/

                // Päivitä käyttöliittymäelementtejä, kuten voittajien näyttö

                //  voittaja 
                try {
                    Console.WriteLine("Turnauksen voittaja: " + winners[0]);
                    MessageBox.Show("Turnauksen voittaja: " + winners[0]);
                }
                catch {
                    Console.WriteLine("failed");
                    MessageBox.Show("Ei pelaajia!!!");
                }
            }
        }
        public List<string> SimulateTournament(List<string> players) {

            while (players.Count > 1) {
                List<string> roundWinners = new List<string>();

                for (int i = 0; i < players.Count - 1; i += 2) {
                    string player1 = players[i];
                    string player2 = players[i + 1];

                    Console.WriteLine($"Ottelu: {player1} vs {player2}");
                    Console.WriteLine($"i: {i}");


                    // Tässä voisi olla logiikkaa ottelun simulointiin ja voittajan valitsemiseen.
                    // Esimerkiksi:
                    // string winner = SimulateMatch(player1, player2);
                    string winner = SimulateMatch(player1, player2);

                    Console.WriteLine($"Voittaja: {winner}\n");

                    roundWinners.Add(winner);
                }

                players = roundWinners;
            }
            return players;
        }

        public string SimulateMatch(string player1, string player2) {
            // Tässä voisi olla monimutkaisempaa logiikkaa ottelun simulointiin.
            // Esimerkiksi arpomista tai pelaajien taitotason vertailua.

            // Simuloidaan yksinkertainen ottelu arpomalla voittaja.
            return random.Next(2) == 0 ? player1 : player2;
        }
  
            private void Form_Load(object sender, EventArgs e) {
                string tnam = File.Exists("TournamentInfo.txt") ? File.ReadAllText("TournamentInfo.Txt") : "o";
                Name.Text = tnam;

                string map = File.Exists("map.txt") ? File.ReadAllText("map.Txt") : "o";
                Map0.Text = map;

                string rnd = File.Exists("round.txt") ? File.ReadAllText("round.Txt") : "o";
                Rnd.Text = rnd;

                string bet = File.Exists("EntryFee.txt") ? File.ReadAllText("EntryFee.Txt") : "o";
                entryfees.Text = bet;
            }
        
        private void StartTournamentButton_Click_2(object sender, EventArgs e)
        {

        }

        private void E1_Click(object sender, EventArgs e)
        {
            Q1.Text = textBoxPlayer1.Text;
    
           
        }

        private void E2_Click(object sender, EventArgs e)
        {
            Q1.Text = textBoxPlayer2.Text;
        }

        private void E3_Click(object sender, EventArgs e)
        {
            Q2.Text = textBoxPlayer3.Text;
        }

        private void E4_Click(object sender, EventArgs e)
        {
            Q2.Text = textBoxPlayer4.Text;
        }

        private void E5_Click(object sender, EventArgs e)
        {
            Q3.Text = textBoxPlayer5.Text;
        }

        private void E6_Click(object sender, EventArgs e)
        {
            Q3.Text = textBoxPlayer6.Text;
        }

        private void E7_Click(object sender, EventArgs e)
        {
            Q4.Text = textBoxPlayer7.Text;
        }

        private void E8_Click(object sender, EventArgs e)
        {
            Q4.Text = textBoxPlayer8.Text;
        }

        private void E9_Click(object sender, EventArgs e)
        {
            Q5.Text = textBoxPlayer9.Text;
        }

        private void E10_Click(object sender, EventArgs e)
        {
            Q5.Text = textBoxPlayer10.Text;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            textBox23.Text = Q4.Text;  
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            textBox24.Text = Q3.Text;
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            textBox22.Text = Q2.Text;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            textBox23.Text = Q5.Text;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            textBox22.Text = Q1.Text;
        }
        private void Button10_Click(object sender, EventArgs e)
        {
            // Kutsu metodia pelaajien sekoittamiseksi ja asettamiseksi semifinaaleihin
            ShufflePlayersAndSetSemifinals();
        }

        private void ShuffleList<T>(List<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        private void ShufflePlayersAndSetSemifinals()
        {
            // Kopioi pelaajat tekstibokseista
            List<string> shuffledPlayers = playerTextBoxes.Select(tb => tb.Text).ToList();

            // Sekoita pelaajat
            ShuffleList(shuffledPlayers);

            // Aseta kaksi ensimmäistä pelaajaa semifinaalien tekstibokseihin
            for (int i = 0; i < Math.Min(shuffledPlayers.Count, semifinalTextBoxes.Count); i++)
            {
                semifinalTextBoxes[i].Text = shuffledPlayers[i];
            }
        }

        private void Tnames_TextChanged(object sender, EventArgs e) {
        }

        private void ToolStripContainer1_ContentPanel_Load(object sender, EventArgs e) {

        }

        private void Name_TextChanged_1(object sender, EventArgs e) {
            string tnam = File.Exists("TournamentInfo.txt") ? File.ReadAllText("TournamentInfo.Txt") : "o";
            Name.Text = tnam;
        }

        private void Map0_TextChanged(object sender, EventArgs e) {
            string map = File.Exists("map.txt") ? File.ReadAllText("map.Txt") : "o";
            Map0.Text = map;
        }

        private void Rnd_TextChanged(object sender, EventArgs e) {
            string rnd = File.Exists("round.txt") ? File.ReadAllText("round.Txt") : "o";
            Rnd.Text = rnd;
        }

        private void Entryfees_TextChanged(object sender, EventArgs e) {
           
            string bet = File.Exists("EntryFee.txt") ? File.ReadAllText("EntryFee.Txt") : "o";
            entryfees.Text = bet;
           
        }
    }
}   