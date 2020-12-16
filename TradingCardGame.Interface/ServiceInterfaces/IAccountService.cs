using System.Collections.Generic;
using TradingCardGame.Interface.Models.SocialModels;

namespace TradingCardGame.Interface.ServiceInterfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Kullanıcı listesini döndürür
        /// </summary>
        /// <returns></returns>
        List<Account> GetList();
        /// <summary>
        /// Kullanıcıyı Id ye göre döndürür
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Account GetAccountById(int id);
        /// <summary>
        /// Kullanıcıyı Name e göre döndürür yok ise yeni oluşturup döndürür
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Account LoginOrRegister(string name);
        /// <summary>
        /// Kullanıcıyı kaydeder
        /// </summary>
        /// <param name="account"></param>
        void SaveAccount(Account account);
        /// <summary>
        /// Giriş yapan kullanıcı dışındaki kullanıcıları getirir
        /// </summary>
        /// <param name="loginAccountId"></param>
        /// <returns></returns>
        List<Account> GetOtherAccounts(int loginAccountId);
        /// <summary>
        /// Id ye göre kullanıcının WinScore unu arttırır
        /// </summary>
        /// <param name="id"></param>
        void UpdateWinScore(int id);
        /// <summary>
        /// Id ye göre kullanıcının LoseScore unu arttırır
        /// </summary>
        /// <param name="id"></param>
        void UpdateLoseScore(int id);
    }
}
