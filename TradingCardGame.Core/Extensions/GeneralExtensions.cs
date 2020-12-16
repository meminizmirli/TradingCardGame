using System;
using System.IO;
using System.Text.RegularExpressions;
using TradingCardGame.Interface.Models.SocialModels;

namespace TradingCardGame.Core.Extensions
{
    public static class GeneralExtensions
    {
        public static string ToApplicationPath(this string fileName)
        {
            var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath ?? string.Empty).Value;
            return Path.Combine(appRoot, fileName);
        }

        /// <summary>
        /// Path e göre klasör kontrolü ve yok ise oluşturması
        /// </summary>
        /// <param name="filePath"></param>
        public static void CreateDirectoryIfNotExists(this string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);

            if (Directory.Exists(directory)) return;

            Directory.CreateDirectory(directory ?? string.Empty);
        }

        /// <summary>
        /// Path e göre dosya kontrolü ve yok ise oluşturulması
        /// </summary>
        /// <param name="filePath"></param>
        public static void CreateFileIfNotExists(this string filePath)
        {
            if (File.Exists(filePath)) return;

            using (var stream = File.Create(filePath)) { }
        }

        /// <summary>
        /// Console ekranında satır silme işlemi
        /// </summary>
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine("");
        }

        /// <summary>
        /// Kullanıcıya göre kazanma oranı
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static double WinRate(this Account account)
        {
            return Math.Round(account.LoseScore == 0 ? 1 : (double)account.WinScore / account.LoseScore, 2);
        }
    }
}
