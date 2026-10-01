using ST_FE.Forms;
using System.Globalization;

namespace ST_FE
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Định dạng số/ngày theo kiểu Việt Nam (1.000.000 đ, dd/MM/yyyy)
            var vi = new CultureInfo("vi-VN");
            CultureInfo.DefaultThreadCurrentCulture = vi;
            CultureInfo.DefaultThreadCurrentUICulture = vi;
            Thread.CurrentThread.CurrentCulture = vi;
            Thread.CurrentThread.CurrentUICulture = vi;

            ApplicationConfiguration.Initialize();

            // Vòng lặp: Đăng nhập -> Màn hình chính -> (Đăng xuất / hết phiên) -> Đăng nhập lại
            while (true)
            {
                using var login = new FormLogin();
                if (login.ShowDialog() != DialogResult.OK) break;

                using var main = new FormMain();
                Application.Run(main);
                if (!main.ReturnToLogin) break;
            }
        }
    }
}
