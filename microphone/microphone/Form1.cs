using System.Diagnostics;
using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace microphone
{
    public partial class Form1 : Form
    {
        private const string voicePath = "e:\\voice\\mic.wav";
        SpeechRec s = new SpeechRec();

        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void button1_Click(System.Object sender, System.EventArgs e)
        {
            int WaveBitsPerSample = 16;
            int WaveChannels = 1;
            int sampleRate = 16000;
            var WaveAlignment = WaveBitsPerSample * WaveChannels / 8;
            var WaveBytesPerSec = WaveBitsPerSample * WaveChannels * sampleRate / 8;

            string command = "set recsound time format ms";
            command += " bitspersample " + WaveBitsPerSample;
            command += " channels " + WaveChannels;
            command += " samplespersec " + sampleRate;
            command += " bytespersec " + WaveBytesPerSec;
            command += " alignment " + WaveAlignment;;

            record("open new Type waveaudio Alias recsound", "", 0, 0);
            record(command, "", 0, 0);
            record("record recsound", "", 0, 0);
        }

        public void button2_Click(System.Object sender, System.EventArgs e)
        {
            record("stop recsound ", "", 0, 0);
            record("save recsound " + voicePath, "", 0, 0);
            record("close recsound", "", 0, 0);
            string result = s.speech();
            label1.Text = result;

        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            (new Microsoft.VisualBasic.Devices.Audio()).Play(voicePath);
        }

    }
}
