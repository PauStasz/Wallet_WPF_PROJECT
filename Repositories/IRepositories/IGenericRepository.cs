using Wallet.Models;

namespace Wallet.Repositories.IRepositories
{
    internal interface IGenericRepository<T> where T : BaseObject
    {
        public List<T> GetAllData(string nameTable);
        public void SetData(string nameTable, T entity);
        public void UpdateData(string nameTable, T entity);
        public void DeleteData(string nameTable, int id);

    }
}
