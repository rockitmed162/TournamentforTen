using CreatorsApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
    public partial class CreatorsGUI : Form {
        WebClient webClient;
        string updateURL = "http://localhost/updates/update"; // Replace with your update file URL (without file extension) & if its problematic and you need multiple files updates(one way is to add .zip or multiply the updateurl lines for eachfile.
        string updateFilePath = @""; // Destination to save updated file

        private List<string> players = new List<string>();

        public CreatorsGUI() {
            InitializeComponent();

            this.Login.Click += new System.EventHandler(this.Login_Click);
            Start.Enabled = false;

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
        private void button6_Click_1(object sender, EventArgs e) {
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
        private void Form_Load(object sender, EventArgs e) {
            AppData.CreatorsGUIForm = this;
        }

        private void Start_Click(object sender, EventArgs e) {
            try {
                string gamePath = @"GunZ.exe";

                // Check if the file exists at the specified path
                if (!System.IO.File.Exists(gamePath)) {
                    MessageBox.Show("Game executable not found at specified path!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Start the game executable
                Process.Start(gamePath);
            }
            catch (Exception ex) {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckUpdates_Click(object sender, EventArgs e) {
            try {
                using (WebClient client = new WebClient()) {
                    Uri updateUri = new Uri(updateURL);
                    client.OpenReadAsync(updateUri); // Aloita pyyntö asynkronisesti

                    client.OpenReadCompleted += (s, args) =>
                    {
                        if (args.Error != null) {
                            MessageBox.Show("Error checking updates: " + args.Error.Message, "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        long fileSize = 0;
                        if (args.Result != null) {
                            fileSize = args.Result.Length;
                            MessageBox.Show($"Update file size: {fileSize} bytes", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDownload.Enabled = true;
                        }
                    };
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error checking updates: " + ex.Message, "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDownload_Click(object sender, EventArgs e) {
            try {
                webClient = new WebClient();
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted);

                // Get the file name from the URL
                string fileName = Path.GetFileName(updateURL);

                // Start downloading the file asynchronously
                webClient.DownloadFileAsync(new Uri(updateURL), updateFilePath + fileName);
            }
            catch (Exception ex) {
                MessageBox.Show("Error downloading updates: " + ex.Message, "Update Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            progressBar.Value = e.ProgressPercentage;
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            if (e.Cancelled) {
                MessageBox.Show("Download cancelled", "Update Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.Error != null) {
                MessageBox.Show("Error downloading updates: " + e.Error.Message, "Update Download", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                MessageBox.Show("Download completed", "Update Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void User_TextChanged(object sender, EventArgs e) {

        }

        private void Pass_TextChanged(object sender, EventArgs e) {

        }


        private void Login_Click(object sender, EventArgs e) {

            //Update SQL connection strings for Username/Password/DB/Server to match yours.
            string connectionString = "Data Source=jop\\mssqlserver02;Initial Catalog=GunzDB;Persist Security Info=True;User ID=sa;Password=Asdasd12!;Encrypt=False";

            using (SqlConnection con = new SqlConnection(connectionString)) {
                con.Open();
                try {
                    using (SqlCommand loginCmd = new SqlCommand("SELECT * FROM Login WHERE userid=@UserID AND password=@Password", con)) {
                        loginCmd.Parameters.AddWithValue("@UserID", User.Text);
                        loginCmd.Parameters.AddWithValue("@Password", Pass.Text);

                        using (SqlDataReader loginReader = loginCmd.ExecuteReader()) {
                            if (loginReader.Read()) // Check if the login query returns any rows
                            {
                                int uGradeID = Convert.ToInt32(loginReader["UGradeID"]);

                                if (uGradeID == 0) //0 = Normal User
                                {
                                    // Member login logic
                                    // Handle session or user details as required
                                    Start.Enabled = true;
                                    MessageBox.Show("Login successful!");
                                 
                                    return;
                                }
                                else if (uGradeID == 255) {
                                    // Admin login logic
                                    // Handle session or user details as required
                                    MessageBox.Show("Logged in as an admin.");
                                    return;
                                }
                                else {
                                    MessageBox.Show("Invalid credentials or account type.");
                                    // Handle invalid credentials or account type
                                    return;
                                }
                            }
                            else {
                                MessageBox.Show("Invalid credentials.");
                                // Handle invalid credentials
                                return;

                            }
                        }
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}