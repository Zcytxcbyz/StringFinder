using System;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace StringFinder
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainForm());
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            DialogResult result = MessageBox.Show(string.Format("Unhandled exception caught：{0}\r\nMessage：{1}\r\nStackTrace：{2}", ex.GetType(), ex.Message, ex.StackTrace), "Unhandled exception", MessageBoxButtons.AbortRetryIgnore);
            if (result == DialogResult.Abort) Environment.Exit(0);
            else if (result == DialogResult.Retry)
            {
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                Environment.Exit(0);
            }        
        }
    }
}
