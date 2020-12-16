using System.Collections.Generic;
using System.Linq;
using TradingCardGame.Interface.Models.SocialModels;
using TradingCardGame.Interface.ServiceInterfaces;

namespace TradingCardGame.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDataService<Account> _accountDataService;
        public AccountService(IDataService<Account> accountDataService)
        {
            _accountDataService = accountDataService;
        }

        public List<Account> GetList()
        {
            return _accountDataService.GetJsonData();
        }

        public Account GetAccountById(int id)
        {
            return _accountDataService.GetJsonDataById(id);
        }

        public void SaveAccount(Account account)
        {
            _accountDataService.SaveJsomData(new List<Account> { account });
        }

        public Account LoginOrRegister(string name)
        {
            var account = _accountDataService.GetJsonDataByName(name);
            if (account != null) return account;

            var lastId = GetList().Max(x => x.Id);
            account = new Account
            {
                Id = ++lastId,
                Name = name,
            };
            SaveAccount(account);
            return account;
        }

        public List<Account> GetOtherAccounts(int loginAccountId)
        {
            var accountList = GetList();
            return accountList.Where(x => x.Id != loginAccountId).ToList();
        }

        public void UpdateWinScore(int id)
        {
            var account = GetAccountById(id);
            account.WinScore++;
            SaveAccount(account);
        }

        public void UpdateLoseScore(int id)
        {
            var account = GetAccountById(id);
            account.LoseScore++;
            SaveAccount(account);
        }
    }
}
