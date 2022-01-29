using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualLogo
{
    public partial class MainForm : Form
    {
        private Label[] nGrid = new Label[30];
        private Label[] tWords = new Label[10];

        private string[] magicWords = new string[4];
        private Label selected = null;

        public MainForm()
        {
            InitializeComponent();

            magicWords[0] = "up";
            magicWords[1] = "down";
            magicWords[2] = "left";
            magicWords[3] = "right";

            for (int i = 0; i < nGrid.Length; i++)
            {
                nGrid[i] = pnlGrid.Controls["n" + i] as Label;
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

            l.ForeColor = Color.Yellow;
            l.BackColor = Color.Red;
            selected = l;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            ResetHighlights();

            ResetFocus();

            if (int.TryParse(txtCommand.Text, out int n))
            {
                Highlight(pnlGrid.Controls["n" + (n+1)] as Label);

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
