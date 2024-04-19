using Wallet.Models;

namespace Wallet.Repositories.IRepositories
{
    internal interface IGenericRepository<T> where T : BaseEntity
    {
        public List<T> GetAllData(string nameTable);
        public T GetOneData(string nameTable, int id);
        public void SetData(string nameTable, T entity);
        public void UpdateData(string nameTable, T entity);
        public void DeleteData(string nameTable, int id);

    }
}
