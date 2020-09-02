using System;
using System.Windows.Forms;

namespace StringFinder
{
    public partial class FrmProgressBar : Form
    {
        System.Timers.Timer timer = new System.Timers.Timer(50);
        public FrmProgressBar()
        {
            InitializeComponent();
        }

        private void FrmProgressBar_Load(object sender, EventArgs e)
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!Visible) return;
            Invoke(new Action(() =>
            {
                if (MainProBar.Value >= 100)
                {
                    MainProBar.Value = 0;
                }
                else
                {
                    MainProBar.Value++;
                }
            }));
        }
        private void FrmProgressBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
        }
    }
}
