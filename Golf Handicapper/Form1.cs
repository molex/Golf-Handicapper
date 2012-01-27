using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Golf_Handicapper
{
    public partial class Form1 : Form
    {
        int count = 1;
        ArrayList scoreDiff = new ArrayList();
        double bestScore;
        double handicap;
        string strHandicap; 
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void txtScore_KeyPress(object sender, KeyPressEventArgs e) 
        {
            if (!char.IsControl(e.KeyChar) 
                && !char.IsDigit(e.KeyChar) 
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' 
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
       }

        private void txtRating_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtSlope_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void btnAddScore_Click(object sender, EventArgs e)
        {
            
            double score = Double.Parse(txtScore.Text);
            double slope = Double.Parse(txtSlope.Text);
            double rating = Double.Parse(txtRating.Text);

            double adjustedScore = ((score - rating) * (113 / slope));

            if (count <= 20)
            {
                scoreDiff.Add(adjustedScore);
                count++;
                txtScore.Text = "";
                lblAdded.Text = "Score Added " + scoreDiff.Count.ToString() + " Total Score(s)" ;
            }

            else
            {
                count = count - 20;
                scoreDiff.Add(adjustedScore);
                count++;
                txtScore.Text = "";
                lblAdded.Text = "Score Added " + scoreDiff.Count.ToString() + " Total Score(s)";
                
            }

           

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtScore.Text = "";
            txtSlope.Text = "";
            txtRating.Text = "";
            scoreDiff.Clear();
			lblAdded.Text = "     ";
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            int i = count - 1;
            double[] tempSorted = new Double[i];
            scoreDiff.CopyTo(tempSorted);
            Array.Sort(tempSorted);
                         
            
            if (scoreDiff.Count <= 5 || scoreDiff.Count == 6)
            {

                bestScore = tempSorted[0];
                
            }
            else if (scoreDiff.Count == 7 || scoreDiff.Count == 8)
            {

                bestScore = ((tempSorted[0] + tempSorted[1]) / 2);
                
            }
            else if (scoreDiff.Count == 9 || scoreDiff.Count == 10)
            {

                bestScore = ((tempSorted[0] + tempSorted[1] + tempSorted[2]) / 3);
               
            }
            else if (scoreDiff.Count == 11 || scoreDiff.Count == 12)
            {

                bestScore = ((tempSorted[0] + tempSorted[1] + tempSorted[2] + tempSorted[3]) / 4);
                
            }
            else if (scoreDiff.Count == 13 || scoreDiff.Count == 14)
            {

                bestScore = ((tempSorted[0] + tempSorted[1] + tempSorted[2] + tempSorted[3] + tempSorted[4]) / 5);
                
            }
            else if (scoreDiff.Count == 15 || scoreDiff.Count == 16)
            {

                bestScore = ((tempSorted[0] + tempSorted[1] + tempSorted[2] + tempSorted[3] + tempSorted[4] + tempSorted[5]) / 6);
                
            }
            else if (scoreDiff.Count == 17)
            {

                bestScore = ((tempSorted[0] + tempSorted[1] + tempSorted[2] + tempSorted[3] + tempSorted[4] + tempSorted[5] + tempSorted[6]) / 7);
                
            }
            else if (scoreDiff.Count == 18)
            {

                bestScore = ((tempSorted[0] + tempSorted[1] + tempSorted[2] + tempSorted[3] + tempSorted[4] + tempSorted[5] + tempSorted[6] + tempSorted[7]) / 8);
                
            }
            else if (scoreDiff.Count == 19)
            {

                bestScore = ((tempSorted[0] + tempSorted[1] + tempSorted[2] + tempSorted[3] + tempSorted[4] + tempSorted[5] + tempSorted[6] + tempSorted[7] + tempSorted[8]) / 9);
                
            }
            else if (scoreDiff.Count >= 20)
            {

                bestScore = ((tempSorted[0] + tempSorted[1] + tempSorted[2] + tempSorted[3] + tempSorted[4] + tempSorted[5] + tempSorted[6] + tempSorted[7] + tempSorted[8] + tempSorted[9]) / 10);
                
            }
            else
            {
                MessageBox.Show("This should not happen!!");
            }
            handicap = (bestScore * .96);
            strHandicap = String.Format("{0:0}", handicap);
            
            lblAdded.Text = ("Your  Handicap is: " + strHandicap); 
			
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileSaver.ShowDialog();
            
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileGetter.ShowDialog();
        }

        private void fileSaver_FileOk(object sender, CancelEventArgs e)
        {
            StreamWriter sw = new StreamWriter(fileSaver.FileName);
            foreach (Double item in scoreDiff)
            {
                sw.WriteLine(item);
            }
            sw.Close();
        }

        private void fileGetter_FileOk(object sender, CancelEventArgs e)
        {
            StreamReader sr = new StreamReader(fileGetter.FileName);
            count = 1;
            string input =  null;
            double dblInput = 0.00;
            while ((input = sr.ReadLine()) != null)
            {
                dblInput = double.Parse(input);
                scoreDiff.Add(dblInput);
                count++;
            }
            sr.Close();
        }

        

        private void btnLoad_Click(object sender, EventArgs e)
        {
            fileGetter.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            fileSaver.ShowDialog();
        }
    }
}
