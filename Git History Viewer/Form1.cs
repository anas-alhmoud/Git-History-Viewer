using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LibGit2Sharp;

namespace Git_History_Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Repository repo;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int x = 800;
            int y = 300;
            using (repo = new Repository(@"C:\Users\anas\Desktop\repo"))
            {
                foreach (var branch in repo.Branches)
                {
                    if (branch.IsCurrentRepositoryHead)
                    {
                        var listEnumerator = branch.Commits.GetEnumerator(); // Get enumerator

                        bool condition = true;
                        while (condition)
                        {
                            var g = e.Graphics;
                            condition = listEnumerator.MoveNext();

                            // draw circle
                            g.DrawEllipse(new Pen(Brushes.Black), x, y, 50, 50);
                            // hash
                            g.DrawString(listEnumerator.Current.Sha.Substring(0, 7), Font, Brushes.Black, new PointF(x, y + 60));

                            if (condition)
                            {
                                // draw line
                                x -= 60;
                                g.DrawLine(new Pen(Brushes.Black), x, y + 25, x + 50, y + 25);
                                x -= 60;
                            }

                        }



                    }
                }
            }
        }
    }
}
