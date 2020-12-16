using TradingCardGame.Interface.Models.Base;

namespace TradingCardGame.Interface.Models.GameModels
{
    public class Card : ModelBase
    {
        /// <summary>
        /// Oynanabilmesi için gerekli Mana miktarı
        /// </summary>
        public int ManaCost { get; set; }
        /// <summary>
        /// Hasar miktarı
        /// </summary>
        public int DamageCount { get; set; }
    }
}
