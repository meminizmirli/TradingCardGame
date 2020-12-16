
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using TradingCardGame.Interface.Models.GameModels;
using TradingCardGame.Interface.Models.SocialModels;
using TradingCardGame.Interface.ServiceInterfaces;
using TradingCardGame.Pages;

namespace TradingCardGame
{
    class Program
    {
        private static Account _loginAccount;
        private static int _rivalId;
        private static bool _returnLobby;
        static void Main(string[] args)
        {
            DependencyServices.RegisterService();
            IServiceScope scope = DependencyServices.ServiceProvider.CreateScope();
            _loginAccount = scope.ServiceProvider.GetRequiredService<AccountPage>().LoginOrRegister();
            do
            {
                _rivalId = scope.ServiceProvider.GetRequiredService<LobbyPage>().OpenLobby(_loginAccount);
                _returnLobby = scope.ServiceProvider.GetRequiredService<GamePage>().StartGame(_loginAccount, _rivalId);
            } while (_returnLobby);

            Console.Write("Hoşçakal..");
            DependencyServices.DisposeServices();
        }
    }
}
