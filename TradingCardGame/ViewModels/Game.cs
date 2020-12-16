using System;
using System.Collections.Generic;
using System.Linq;
using TradingCardGame.Core.Extensions;
using TradingCardGame.Interface.Models.GameModels;
using TradingCardGame.Interface.Models.SocialModels;

namespace TradingCardGame.ViewModels
{
    public class Game
    {
        public Game()
        {
            Turn = 1;
        }

        /// <summary>
        /// Oyuncuların başlangıç canı
        /// </summary>
        public int StartingHp { get; set; }
        /// <summary>
        /// Oyuncuların başlangıç mana sayısı
        /// </summary>
        public int StartingMana { get; set; }
        /// <summary>
        /// Oyuncuların başlangıç mana slot sayısı
        /// </summary>
        public int StartingManaSlot { get; set; }
        /// <summary>
        /// Oyuncuların oyuna elinde kaç kartla başlayacağının sayısı
        /// </summary>
        public int StartingCardCount { get; set; }
        /// <summary>
        /// Login olan kullanıcının hesap bilgisi
        /// </summary>
        public Account PlayerOneAccount { get; set; }
        /// <summary>
        /// Rakip olan kullanıcının hesap bilgisi
        /// </summary>
        public Account PlayerTwoAccount { get; set; }
        /// <summary>
        /// Oyuncuların başlangıçtaki destelerindeki kartlar
        /// </summary>
        public List<Card> PlayerCardDeck { get; set; }

        /// <summary>
        /// Login olan kullanıcının oyun özelinde oyun bilgisi
        /// </summary>
        public Player PlayerOne { get; set; }
        /// <summary>
        /// Rakip olan kullanıcının oyun özelinde oyun bilgisi
        /// </summary>
        public Player PlayerTwo { get; set; }
        /// <summary>
        /// Bulundukları tur
        /// </summary>
        public int Turn { get; set; }

        /// <summary>
        /// Kazanan oyuncunun hesap bilgisi
        /// </summary>
        public Account Winner => PlayerOne.Hp > PlayerTwo.Hp ? PlayerOne.Account : PlayerTwo.Account;
        /// <summary>
        /// Kaybeden oyuncunun hesap bilgisi
        /// </summary>
        public Account Losser => PlayerTwo.Hp > PlayerOne.Hp ? PlayerOne.Account : PlayerTwo.Account;

        /// <summary>
        /// Turu gelen oyuncu
        /// </summary>
        public Player Played => Turn % 2 == 1 ? PlayerOne : PlayerTwo;
        /// <summary>
        /// Turu gelmemiş olan oyuncu
        /// </summary>
        public Player Rival => Turn % 2 == 0 ? PlayerOne : PlayerTwo;

        /// <summary>
        /// Oyuncuları tanımlar
        /// </summary>
        public void SetPlayers()
        {
            var newPlayer = new Player
            {
                Hp = StartingHp,
                ManaSlotCount = StartingMana,
                ManaCount = StartingManaSlot,
                PlayerHandCards = new List<Card>(),
                PlayerCardDeck = PlayerCardDeck
            };
            PlayerOne = new Player(newPlayer, PlayerOneAccount);
            PlayerTwo = new Player(newPlayer, PlayerTwoAccount);
        }

        /// <summary>
        /// Başlangıç kartlarını çeker
        /// </summary>
        public void DrawFirstCards()
        {
            for (var i = 0; i < StartingCardCount; i++)
            {
                PlayerOne.DrawCard();
                PlayerTwo.DrawCard();
            }
        }

        /// <summary>
        /// Turu oynatır
        /// </summary>
        public void StartTurn()
        {
            Played.UpdateManaSlot();
            Played.ManaCount = Played.ManaSlotCount;
            Console.Clear();
            if (Turn > 1) Console.WriteLine($"Rakibiniz size toplam {Rival.LastDamegeDone} hasasr verdi");
            Console.WriteLine($"{Played.Account.Name} Sırası");
            Console.WriteLine("");
            Console.WriteLine($"Senin Canın {Played.Hp} - Rakibinin Canı {Rival.Hp}");
            Console.WriteLine($"Mana Slot Sayın {Played.ManaSlotCount}");
            Console.WriteLine($"Mana Sayın {Played.ManaCount}");
            Played.DrawCard();

            if (!Played.PlayerHandCards.Any() && !Played.PlayerCardDeck.Any())
            {
                Console.WriteLine("Elinde ve destende hiç kart bulunmuyor rakibine sıra geçti!! Devam Etmek için bir tuşa basınız.");
                Console.ReadKey();
                return;
            }

            WriteHandCards();

            var continueTurn = true;
            do
            {
                if (Rival.Hp <= 0)
                {
                    continueTurn = false;
                }
                else if (!Played.PlayerHandCards.Any(x => x.ManaCost <= Played.ManaCount))
                {
                    Console.WriteLine("Elinde oynayabileceğiniz kart kalmadığı için turun geçti. Devam Etmek için bir tuşa basınız.");
                    Console.ReadKey();
                    continueTurn = false;
                }
                else
                {
                    var selectedCardId = SelectedCard();
                    PlayedCard(selectedCardId);
                }
            } while (continueTurn);
        }

        public void AIPlayed()
        {
            Played.UpdateManaSlot();
            Played.ManaCount = Played.ManaSlotCount;
            Played.DrawCard();
            if (!Played.PlayerHandCards.Any() && !Played.PlayerCardDeck.Any()) Played.LastDamegeDone = 0;

            if (Played.PlayerHandCards.Any(x => x.ManaCost <= Played.ManaCount))
            {
                var selectedCards = new List<Card>();
                var playableCards = Played.PlayerHandCards.Where(x => x.ManaCost <= Played.ManaCount).ToList();
                var zeroCostCards = playableCards.Where(x => x.ManaCost == 0).ToList();
                zeroCostCards.ForEach(x =>
                {
                    playableCards.Remove(x);
                    selectedCards.Add(x);
                });
                if (playableCards.Any(x => x.ManaCost == Played.ManaCount))
                    selectedCards.Add(playableCards.First(x => x.ManaCost == Played.ManaCount));
                else
                {
                    var orderadCards = playableCards.OrderByDescending(x => x.ManaCost).ToList();

                    var playableCardsList = new Dictionary<int, List<Card>>();
                    var sayac = 0;
                    for (var i = 0; i < orderadCards.Count; i++)
                    {
                        var selectedCard = orderadCards[i];
                        playableCardsList.Add(sayac++, new List<Card> { selectedCard });
                        if (selectedCard.ManaCost == Played.ManaCount)
                            break;

                        var needManaCost = Played.ManaCount - selectedCard.ManaCost;
                        var selectableCards = orderadCards.Where(x => x.ManaCost <= needManaCost).ToList();
                        do
                        {
                            if (playableCardsList[i].Sum(x => x.ManaCost) == Played.ManaCount)
                                break;
                            if (selectableCards.Any())
                            {
                                Try(selectableCards, needManaCost, playableCardsList[i]);
                            }
                            else
                                break;
                        } while (true);
                    }
                }

                selectedCards.ForEach(x =>
                {
                    Played.PlayerHandCards.Remove(x);
                    Rival.Hp -= x.DamageCount;
                    Played.ManaCount -= x.ManaCost;
                });

                Played.LastDamegeDone = selectedCards.Sum(x => x.DamageCount);
            }
            Played.LastDamegeDone = 0;
        }

        public void Try(List<Card> selectableCards, int needManaCost, List<Card> playableCardsList)
        {
            var firstMaybeCard = selectableCards.First();
            if (firstMaybeCard.ManaCost == needManaCost)
            {
                playableCardsList.Add(firstMaybeCard);
            }
            else
                selectableCards.Remove(firstMaybeCard);
        }

        /// <summary>
        /// Oyuncunun elindeki kartları ekrana yazdırır
        /// </summary>
        public void WriteHandCards()
        {
            Console.WriteLine("");
            Console.WriteLine("Elindeki Kartlar");
            Played.PlayerHandCards.ForEach(x =>
            {
                var idText = x.Id.ToString();
                var manaCostText = x.ManaCost.ToString();
                var damageCountText = x.DamageCount.ToString();
                Console.WriteLine($"{idText.PadRight(4 - idText.Length, ' ')} - " +
                                  $"{x.Name.PadRight(10 - x.Name.ToString().Length, ' ')} - " +
                                  $"Mana İhtiyacı {manaCostText.PadRight(4 - manaCostText.Length, ' ')} - " +
                                  $"Hasarı {damageCountText.PadRight(4 - damageCountText.Length, ' ')}");
            });
            Console.WriteLine("");
        }

        /// <summary>
        /// Kart seçim ilemini gerçekleştirir
        /// </summary>
        /// <returns></returns>
        private int SelectedCard()
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
                Console.Write("Oynamak istediğin kartın başında bulunan sayıyı yaz: ");
                var selectedIdString = Console.ReadLine();
                if (int.TryParse(selectedIdString, out selectedId))
                {
                    if (selectedId == 0 || Played.PlayerHandCards.Any(x => x.Id == selectedId))
                        unSuccessful = false;
                    else showError = true;
                }
                else showError = true;

                GeneralExtensions.ClearLine();
            } while (unSuccessful);

            return selectedId;
        }

        /// <summary>
        /// Id ye göre seçilen kartı oynanabilirse oynatır
        /// </summary>
        /// <param name="id"></param>
        public void PlayedCard(int id)
        {
            var selectedCard = Played.PlayerHandCards.First(x => x.Id == id);
            if (Played.ManaCount < selectedCard.ManaCost)
            {
                GeneralExtensions.ClearLine();
                Console.Write("Mana miktarınız bu kartı oynamaya yetmiyor. Başka bir kart seçiniz veya turunuzu geçiniz.");
            }
            else
            {
                Played.PlayerHandCards.Remove(selectedCard);
                Rival.Hp -= selectedCard.DamageCount;
                Played.ManaCount -= selectedCard.ManaCost;
                Console.Clear();
                Console.WriteLine($"{Played.Account.Name} Sırası");
                Console.WriteLine("");
                Console.WriteLine($"Rakibinize {selectedCard.DamageCount} kadar hasar verdiniz.");
                Console.WriteLine("");
                Console.WriteLine($"Senin Canın {Played.Hp} - Rakibinin Canı {Rival.Hp}");
                Console.WriteLine($"Mana Slot Sayın {Played.ManaSlotCount}");
                Console.WriteLine($"Mana Sayın {Played.ManaCount}");

                WriteHandCards();
            }
        }
    }
}