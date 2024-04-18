namespace Wallet.Repositories.IRepositories
{
    internal interface IGenericRepository<T> where T : class
    {
        public List<T> GetAllData(string nameTable);
        public T GetData(string nameTable, int id);
        public void SetData(string nameTable, T entity);
        public void UpdateData(string nameTable, T entity);
        public void DeleteData(string nameTable,T entity);

    }
}
