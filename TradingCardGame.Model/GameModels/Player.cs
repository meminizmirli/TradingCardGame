using System.Collections.Generic;
using TradingCardGame.Model.Base;
using TradingCardGame.Model.SocialModels;

namespace TradingCardGame.Model.GameModels
{
    public class Player : ModelBase
    {
        public Player()
        {
            PlayerCardDeck = new List<Card>();
            PlayerHandCards = new List<Card>();
        }

        /// <summary>
        /// <see cref="SocialModels.Account.Id"/>
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// Yaşam Puanı
        /// </summary>
        public int Hp { get; set; }
        /// <summary>
        /// Mevcut Mana Slot Sayısı
        /// </summary>
        public int ManaSlotCount { get; set; }
        /// <summary>
        /// Mevcut Mana Sayısı
        /// </summary>
        public int ManaCount { get; set; }

        /// <summary>
        /// Oyuncunun elindekilerin bilgisi
        /// </summary>
        public List<Card> PlayerHandCards { get; set; }

        /// <summary>
        /// Oyuncunun Çekilebilir Kartlarını Tutar
        /// </summary>
        public List<Card> PlayerCardDeck { get; set; }
        public Account Account { get; set; }
    }
}
