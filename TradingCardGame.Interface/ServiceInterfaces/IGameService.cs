using System.Collections.Generic;
using TradingCardGame.Interface.Models.GameModels;

namespace TradingCardGame.Interface.ServiceInterfaces
{
    public interface IGameService
    {
        /// <summary>
        /// Kart listesini döndürür
        /// </summary>
        /// <returns></returns>
        List<Card> GetCardList();
    }
}
