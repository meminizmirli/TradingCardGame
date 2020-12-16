using System;
using TradingCardGame.Core.Extensions;
using TradingCardGame.Interface.Models.SocialModels;
using TradingCardGame.Interface.ServiceInterfaces;

namespace TradingCardGame.Pages
{
    public class AccountPage
    {
        private readonly IAccountService _accountService;
        public AccountPage(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Kulanıcı girişini sağlar
        /// </summary>
        /// <returns></returns>
        public Account LoginOrRegister()
        {
            Console.Write("Kullanıcı Adınızı Giriniz : ");
            var name = Console.ReadLine();
            var loginUser = _accountService.LoginOrRegister(name);
            Console.Clear();
            Console.WriteLine($"Hoş Geldin {loginUser.Name} - Kazanma Oranın: {loginUser.WinRate()}");
            Console.WriteLine("");
            return loginUser;
        }
    }
}
