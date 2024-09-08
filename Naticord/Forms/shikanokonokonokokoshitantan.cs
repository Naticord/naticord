using System;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;

namespace Naticord.Forms
{
    public partial class shikanokonokonokokoshitantan : Form
    {
        private WaveOutEvent waveOut;
        private Mp3FileReader mp3Reader;

        public shikanokonokonokokoshitantan()
        {
            InitializeComponent();

            this.Load += new EventHandler(Form_Load);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var mp3Stream = new MemoryStream(Properties.Resources.youdeer);

                mp3Reader = new Mp3FileReader(mp3Stream);
                waveOut = new WaveOutEvent();
                waveOut.Init(mp3Reader);
                waveOut.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing audio: " + ex.Message);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
            }
            if (mp3Reader != null)
            {
                mp3Reader.Dispose();
            }

            base.OnFormClosing(e);
        }
    }
}
