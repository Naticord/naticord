using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Windows.Forms;

namespace Naticord
{
    public partial class Settings : Form
    {
        private readonly Version currentVersion = new Version("0.1.1");

        public Settings()
        {
            InitializeComponent();
        }

        private void checkUpdates_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonData = GetLatestVersionFromGitHub();
                JObject releaseData = JObject.Parse(jsonData);

                string tagName = releaseData["tag_name"].ToString();
                Version latestVersion;
                if (Version.TryParse(tagName, out latestVersion))
                {
                    int comparisonResult = latestVersion.CompareTo(currentVersion);

                    if (comparisonResult > 0)
                    {
                        MessageBox.Show($"A newer version of Naticord is available! (v{latestVersion})",
                                        "Update Available",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else if (comparisonResult < 0)
                    {
                        MessageBox.Show($"You are using a newer version of Naticord than the latest available (v{latestVersion}).",
                                        "No Update Available",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("You are using the latest version of Naticord.",
                                        "No Update Available",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }

                    Console.WriteLine("Raw GitHub Data:");
                    Console.WriteLine(jsonData);
                }
                else
                {
                    MessageBox.Show("The version retrieved from GitHub is not in a valid format. This will be fixed soon.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    Console.WriteLine("Raw GitHub Data:");
                    Console.WriteLine(jsonData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving data from GitHub: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private string GetLatestVersionFromGitHub()
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("User-Agent", "NaticordUpdateChecker");
                string url = "https://api.github.com/repos/n1d3v/naticord/releases/latest";
                return client.DownloadString(url);
            }
        }
    }
}
