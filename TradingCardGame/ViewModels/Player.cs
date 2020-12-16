using System;
using System.Collections.Generic;
using System.Linq;
using TradingCardGame.Interface.Models.GameModels;
using TradingCardGame.Interface.Models.SocialModels;

namespace TradingCardGame.ViewModels
{
    public class Player
    {
        public Player(Player basePlayer, Account account)
        {
            Hp = basePlayer.Hp;
            ManaSlotCount = basePlayer.ManaSlotCount;
            ManaCount = basePlayer.ManaCount;
            PlayerHandCards = new List<Card>(basePlayer.PlayerHandCards);
            PlayerCardDeck = new List<Card>(basePlayer.PlayerCardDeck);
            Account = account;
        }
        public Player()
        {
            PlayerCardDeck = new List<Card>();
            PlayerHandCards = new List<Card>();
        }

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

        /// <summary>
        /// Oyuncunun hesap bilgileri
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Son turda verilen hasar
        /// </summary>
        public int LastDamegeDone { get; set; }

        /// <summary>
        /// Desteden kart çeker eğer elinde 5 kartan az varsa eline alır yoksa yok eder
        /// </summary>
        public void DrawCard()
        {
            if (!PlayerCardDeck.Any()) return;

            var random = new Random();
            var drawingCard = PlayerCardDeck[random.Next(0, PlayerCardDeck.Count)];
            PlayerCardDeck.Remove(drawingCard);
            if (PlayerHandCards.Count <= 5)
                PlayerHandCards.Add(drawingCard);
        }

        /// <summary>
        /// Mana slot sayısı 10dan azsa arttırır
        /// </summary>
        public void UpdateManaSlot()
        {
            if (ManaSlotCount < 10)
                ManaSlotCount++;
        }
    }
}
