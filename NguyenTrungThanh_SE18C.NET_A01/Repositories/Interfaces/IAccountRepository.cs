using BusinessObjects;
using System.Collections.Generic;

namespace Repositories.Interfaces
{
    public interface IAccountRepository
    {
        SystemAccount? GetAccountByEmail(string email);
        List<SystemAccount> GetAccounts();
        void CreateAccount(SystemAccount account);
        void UpdateAccount(SystemAccount account);
        void DeleteAccount(SystemAccount account);
        SystemAccount? GetAccountById(short id);
    }
}