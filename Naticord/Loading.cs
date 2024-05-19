using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Naticord
{
    public partial class Loading : Form
    {
        //private Naticord parentForm;
        private Random random = new Random();
        private List<string> quotes = new List<string>
        {
            "Loading...",
            "AAAAAAAAAAAAAAAAAAAAA",
            "why the fuck does this function NOT WORK - pat",
            "chicken cheese nugget",
            "how was your day",
            "cool beans",
            "fkjsdfgkjsdfjklsdjgklwdf ui9egrhui",
            "i have severe brain damage afte reading this code - pat"
        };

        public Loading(/*Naticord parentForm*/)
        {
            InitializeComponent();
            //this.parentForm = parentForm;
        }

        public void UpdateProgress(string message, int progress)
        {
            loadingQuotes.Text = message;
            loadingProgress.Value = progress;
        }

        private void Loading_Shown(object sender, EventArgs e)
        {
            // Start loading process when the form is shown
            //parentForm.StartLoading();
        }

        private string GetRandomQuote()
        {
            return quotes[random.Next(quotes.Count)];
        }
    }
}
