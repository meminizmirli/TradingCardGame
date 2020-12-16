using TradingCardGame.Model.Base;

namespace TradingCardGame.Model.SocialModels
{
    public class Account : ModelBase
    {
        /// <summary>
        /// Kullanıcı Adı
        /// </summary>
        public string UserName { get; set; }
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
