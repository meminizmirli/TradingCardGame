using System.Collections.Generic;
using TradingCardGame.Interface.Models.GameModels;
using TradingCardGame.Interface.ServiceInterfaces;

namespace TradingCardGame.Business.Services
{
    public class GameService : IGameService
    {
        private readonly IDataService<Card> _cardDataService;
        public GameService(IDataService<Card> cardDataService)
        {
            _cardDataService = cardDataService;
        }

        public List<Card> GetCardList()
        {
            return _cardDataService.GetJsonData();
        }
    }
}
