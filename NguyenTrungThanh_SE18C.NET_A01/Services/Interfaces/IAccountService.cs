using BusinessObjects;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IAccountService
    {
        SystemAccount? GetAccountByEmail(string email);
        List<SystemAccount> GetAccounts();
        void CreateAccount(SystemAccount account);
        void UpdateAccount(SystemAccount account);
        void DeleteAccount(SystemAccount account);
        SystemAccount? GetAccountById(short id);
    }
}