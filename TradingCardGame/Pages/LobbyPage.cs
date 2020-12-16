using System;
using System.Collections.Generic;
using System.Linq;
using TradingCardGame.Core.Extensions;
using TradingCardGame.Interface.Models.SocialModels;
using TradingCardGame.Interface.ServiceInterfaces;

namespace TradingCardGame.Pages
{
    public class LobbyPage
    {
        private List<Account> _otherAccounts;

        private readonly IAccountService _accountService;
        public LobbyPage(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Rakip seçim lobisini getirir
        /// </summary>
        /// <param name="account"></param>
        public int OpenLobby(Account account)
        {
            Console.WriteLine("Seçilebilir Rakipler");
            Console.WriteLine("0 - Yapay Zeka");
            _otherAccounts = _accountService.GetOtherAccounts(account.Id);
            _otherAccounts.ForEach(x =>
            {
                Console.WriteLine($"{x.Id} - {x.Name} - Kazanma Oranı: {x.WinRate()}");
            });
            Console.WriteLine("");

            return SelectRival();
        }

        /// <summary>
        /// Rakip seçimini yapar
        /// </summary>
        /// <returns></returns>
        public int SelectRival()
        {
            Console.WriteLine("");
            var unSuccessful = true;
            var showError = false;
            var selectedId = 0;
            do
            {
                if (showError)
                {
                    GeneralExtensions.ClearLine();
                    Console.WriteLine("Seçimi Hatalı Yaptınız!!");
                }
                Console.Write("Rakibini Seçmek İçin Başındaki Sayıyı Yaz : ");
                var selectedIdString = Console.ReadLine();
                if (int.TryParse(selectedIdString, out selectedId))
                {
                    if (selectedId == 0 || _otherAccounts.Any(x => x.Id == selectedId))
                        unSuccessful = false;
                    else showError = true;
                }
                else showError = true;

                GeneralExtensions.ClearLine();
            } while (unSuccessful);

            return selectedId;
        }
    }
}
