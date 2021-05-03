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
        public class CommitMetaData
        {
            public CommitMetaData(string sha, int x, int y)
            {
                this.sha = sha;
                this.x = x;
                this.y = y;
            }
            public string sha;
            public int x;
            public int y;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Repository repo;

            int x = 50;
            int y = 50;
            List<CommitMetaData> listCommitMetaData = new List<CommitMetaData>();

            using (repo = new Repository(@"C:\Users\anas\Desktop\repo"))
            {
                var g = e.Graphics;

                foreach (var branch in repo.Branches)
                {
                    var arr = branch.Commits.ToArray<Commit>();

                    bool checkout = false;
                    for (int i = arr.Length - 1; i > -1; i--)
                    {
                        if (listCommitMetaData.Find(c => c.sha == arr[i].Sha) != null)
                        {
                            x += 120;
                            continue;
                        }

                        // draw circle
                        g.DrawEllipse(new Pen(Brushes.Black), x, y, 50, 50);

                        // hash
                        g.DrawString(arr[i].Sha.Substring(0, 7), Font, Brushes.Black, new PointF(x, y + 60));


                        if (!checkout)
                        {
                            var parent = listCommitMetaData.Find(c => c.sha == arr[i].Parents.First<Commit>().Sha);

                            if (parent != null)
                            {
                                g.DrawLine(new Pen(Brushes.Black), parent.x + 25, parent.y + 80, x + 20, y - 10);
                            }

                            checkout = true;
                        }

                        listCommitMetaData.Add(new CommitMetaData(arr[i].Sha, x, y));

                        if (i > 0)
                        {
                            // draw line
                            x += 60;
                            g.DrawLine(new Pen(Brushes.Black), x, y + 25, x + 50, y + 25);
                            x += 60;
                        }


                    }

                    y += 170;
                    x = 50;
                }
            }
        }
    }
}
