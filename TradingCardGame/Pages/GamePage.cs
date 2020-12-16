using System;
using TradingCardGame.Business.Services;
using TradingCardGame.Interface.Models.GameModels;
using TradingCardGame.Interface.Models.SocialModels;
using TradingCardGame.Interface.ServiceInterfaces;
using TradingCardGame.ViewModels;

namespace TradingCardGame.Pages
{
    public class GamePage
    {
        private readonly IGameService _gameService;
        private readonly IAccountService _accountService;
        public GamePage(IGameService gameService, IAccountService accountService)
        {
            _gameService = gameService;
            _accountService = accountService;
        }

        /// <summary>
        /// Oyunu başlatır
        /// </summary>
        /// <param name="loginAccount"></param>
        /// <param name="rivalId"></param>
        public void StartGame(Account loginAccount, int rivalId)
        {
            var rivalAccount = _accountService.GetAccountById(rivalId) ?? new Account { Id = 0, Name = "Yapay Zeka" };
            var cardList = _gameService.GetCardList();
            var game = new Game
            {
                PlayerOneAccount = loginAccount,
                PlayerTwoAccount = rivalAccount,
                PlayerCardDeck = cardList,
                StartingHp = 30,
                StartingMana = 0,
                StartingManaSlot = 0,
                StartingCardCount = 3
            };
            game.SetPlayers();
            game.DrawFirstCards();

            do
            {
                if (game.Played.Account.Id == 0) // Yapay zeka
                    game.AIPlayed();
                else
                    game.StartTurn();
                game.Turn++;
            } while (game.PlayerOne.Hp > 0 && game.PlayerTwo.Hp > 0);

            Finish(game);
        }

        /// <summary>
        /// Oyunu bitirir
        /// </summary>
        /// <param name="game"></param>
        private void Finish(Game game)
        {
            Console.Clear();
            Console.WriteLine($"{game.PlayerOne.Account.Name} Canı {game.PlayerOne.Hp} - {game.PlayerTwo.Account.Name} Canı {game.PlayerTwo.Hp}");
            Console.WriteLine($"Kazanan Oyuncu {game.Winner.Name}");

            _accountService.UpdateWinScore(game.Winner.Id);
            _accountService.UpdateLoseScore(game.Losser.Id);
        }
    }
}
