using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_li_quan_cafe.DataAccess;
using Quan_li_quan_cafe.DataAccess.Repositories;
using Quan_li_quan_cafe.Views;

namespace Quan_li_quan_cafe
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Chạy form đăng nhập
            Application.Run(new LoginForm());
        }
    }
}
