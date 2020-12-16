using System;
using Microsoft.Extensions.DependencyInjection;
using TradingCardGame.Business.Services;
using TradingCardGame.Interface.Models.GameModels;
using TradingCardGame.Interface.Models.SocialModels;
using TradingCardGame.Interface.ServiceInterfaces;
using TradingCardGame.Pages;

namespace TradingCardGame
{
    public static class DependencyServices
    {
        private static ServiceProvider _serviceProvider;
        public static ServiceProvider ServiceProvider => _serviceProvider;

        public static void RegisterService()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IGameService, GameService>()
                .AddSingleton<IAccountService, AccountService>()
                .AddSingleton<IDataService<Account>, DataService<Account>>()
                .AddSingleton<IDataService<Card>, DataService<Card>>()
                .AddSingleton<AccountPage>()
                .AddSingleton<LobbyPage>()
                .AddSingleton<GamePage>()
                .BuildServiceProvider(true);
        }

        public static void DisposeServices()
        {
            if (_serviceProvider == null) return;
            if (_serviceProvider is IDisposable) ((IDisposable)_serviceProvider).Dispose();
        }
    }
}
