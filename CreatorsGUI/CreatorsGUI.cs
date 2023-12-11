using CreatorsApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static CreatorsApplication.TournamentLogic;

namespace CreatorsGUI {
  public static class AppData {
        public static CreatorsGUI CreatorsGUIForm { get; set; }
   
   }
    public partial class CreatorsGUI : Form
    {

      
        private List<string> players = new List<string>();
       
        public CreatorsGUI()
        {
            InitializeComponent();




        }   
        // Metodi pelaajien hakemiseksi
        public List<string> Players
        {
            get
            {
                return players;
            }

        }  
        // Tässä voit tallentaa pelaajia, esimerkiksi nappia klikattaessa
        private void button6_Click_1(object sender, EventArgs e)
        {
            List<string> players = new List<string>();
           
            List<TextBox> playerTextBoxes = new List<TextBox>();
         List<TextBox> semifinalTextBoxes = new List<TextBox>();
        // Tallenna pelaajat johonkin tietorakenteeseen, esim. List<string>
        players.Add(p1.Text);
            players.Add(p2.Text);
            players.Add(p3.Text);
            players.Add(p4.Text);
            players.Add(p5.Text);
            players.Add(p6.Text);
            players.Add(p7.Text);
            players.Add(p8.Text);
            players.Add(p9.Text);
            players.Add(p10.Text);

            // Add other checkboxes in a similar way

            // Save the selected value of ComboBox1
           
                try {
                    string tournamentName = TournamentNameBox.Text.Trim();
                    if (string.IsNullOrEmpty(tournamentName)) {
                        MessageBox.Show("Please enter a tournament name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string EntryFee = Entryfee.Text.Trim();
                    string round = roundbox.SelectedItem?.ToString() ?? "Not selected";
                    string map = mapbox.SelectedItem?.ToString() ?? "Not selected";
                   




                    // Generate tournament information

                    string tournamentInfo = $"";

                    tournamentInfo += $"{tournamentName}";


             /*       round += $"\nRounds: {round}\n";


                    map += $"\nMap: {map}\n";


                    EntryFee += $"\nEntry Fee:{EntryFee}\n\n";*/

                 
                    // Save tournament information to a file
                File.WriteAllText("TournamentInfo.txt", tournamentInfo);
                File.WriteAllText("round.txt", round);
                File.WriteAllText("map.txt", map);
                File.WriteAllText("EntryFee.txt", EntryFee);
                AppData.CreatorsGUIForm = this;

            MessageBox.Show("thank you, players recorded into tournament!");

            // Päivitä myös Finals-formissa
            Finals finalsForm = new Finals(this, players);
            
            finalsForm.Show();
        
            }
            catch (Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        private void ShufflePlayers(List<string> players)
        {
            Random rng = new Random();
            int n = players.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = players[k];
                players[k] = players[n];
                players[n] = value;
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            AppData.CreatorsGUIForm = this;
        }
  
    }

}