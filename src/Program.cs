using System;
using System.Windows.Forms;

namespace viewparquet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormPreview mainForm = args.Length < 2 
                ? new FormPreview()
                : new FormPreview(args[2]);

            Application.Run(mainForm);
        }
    }
}
