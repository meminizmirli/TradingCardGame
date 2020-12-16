using TradingCardGame.Model.Base;

namespace TradingCardGame.Model.GameModels
{
    public class Card : ModelBase
    {
        /// <summary>
        /// Kartın Adı
        /// </summary>
        public string Name { get; set; }
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
