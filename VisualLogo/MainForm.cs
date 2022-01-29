using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VisualLogo
{
    public partial class MainForm : Form
    {
        private System.Speech.Synthesis.SpeechSynthesizer speechSynthesizer = new System.Speech.Synthesis.SpeechSynthesizer();

        private Label[] nGrid = new Label[30];
        private Label[] tWords = new Label[10];

        private string[] magicWords = new string[4];
        private Label selected = null;
        private Label targeted = null;

        public MainForm()
        {
            InitializeComponent();

            speechSynthesizer.SelectVoice("Microsoft Zira Desktop");

            magicWords[0] = "up";
            magicWords[1] = "down";
            magicWords[2] = "left";
            magicWords[3] = "right";

            for (int i = 0; i < nGrid.Length; i++)
            {
                nGrid[i] = pnlGrid.Controls["n" + (i+1)] as Label;

                nGrid[i].Click += new EventHandler(this.lblClick);
            }

            tWords[0] = tAnna;
            tWords[1] = tMama;
            tWords[2] = tDada;
            tWords[3] = tDog;
            tWords[4] = tCat;
            tWords[5] = tCar;
            tWords[6] = tHome;
            tWords[7] = tFood;
            tWords[8] = tWater;
            tWords[9] = tFamily;

            foreach (Label l in tWords)
            {
                l.Click += new System.EventHandler(this.lblClick);
            }
        }

        private void lblClick(object sender, EventArgs e)
        {
            if (sender is Label)
            {
                ResetHighlights();

                Target(sender as Label);
            }
        }

        private void Say(string prefix, string word)
        {
            speechSynthesizer.SpeakAsync(prefix + word);
        }

        private void ResetHighlights()
        {
            for (int i = 0; i < nGrid.Length; i++)
            {
                Normalize(pnlGrid.Controls["n" + (i+1)] as Label);
            }

            for (int i = 0; i < tWords.Length; i++)
            {
                Normalize(tWords[i]);
            }
        }

        private void ResetFocus()
        {
            txtCommand.Focus();
            txtCommand.SelectAll();
        }


        private void Normalize(Label l)
        {
            if (null == l)
            {
                return;
            }

            l.ForeColor = SystemColors.ControlText;
            l.BackColor = SystemColors.Control;
        }

        private void Highlight(Label l)
        {
            if (null == l)
            {
                selected = null;
                return;
            }

            l.ForeColor = Color.Black;
            l.BackColor = Color.Green;
            selected = l;

            Say("Anna, you found ", l.Text);
        }

        private void Target(Label l)
        {
            if (null == l)
            {
                targeted = null;
                return;
            }

            l.ForeColor = Color.White;
            l.BackColor = Color.Blue;
            targeted = l;

            Say("Anna, let's write ", l.Text);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            ResetHighlights();

            ResetFocus();

            HandleClick();

            if (null == selected)
            {
                Say("Anna, I didn't find " + txtCommand.Text + ".", "");
            }
        }

        private void HandleClick()
        {
            if (int.TryParse(txtCommand.Text, out int n))
            {
                Highlight(pnlGrid.Controls["n" + (n + 1)] as Label);

                return;
            }

            string word = txtCommand.Text.Trim();

            if (magicWords.Contains(word))
            {
                if (null == selected)
                {
                    return;
                }

                if (selected.Name.StartsWith("n"))
                {
                    int index = int.Parse(selected.Name.Substring(1));

                    if ("up" == word)
                    {
                        index = index - 5;
                    }

                    if ("down" == word)
                    {
                        index = index + 5;
                    }

                    if ("left" == word)
                    {
                        index = index - 1;
                    }

                    if ("right" == word)
                    {
                        index = index + 1;
                    }

                    Highlight(pnlGrid.Controls["n" + index] as Label);

                    return;
                }
            }

            Highlight(pnlGrid.Controls["t" + word] as Label);
        }

    }
}
