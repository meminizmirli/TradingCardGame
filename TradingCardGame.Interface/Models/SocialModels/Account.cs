using TradingCardGame.Interface.Models.Base;

namespace TradingCardGame.Interface.Models.SocialModels
{
    public class Account : ModelBase
    {
        /// <summary>
        /// Galibiyet Sayısı
        /// </summary>
        public int WinScore { get; set; }
        /// <summary>
        /// Kaybetme Sayısı
        /// </summary>
        public int LoseScore { get; set; }
    }
}
