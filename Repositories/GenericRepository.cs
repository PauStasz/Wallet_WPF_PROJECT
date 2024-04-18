using System.Xml.Linq;
using Wallet.Database.FirebaseRealTimeDatabase;
using Wallet.Models.Users;
using Wallet.Repositories.IRepositories;

namespace Wallet.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private FirebaseSetup _firebase = new FirebaseSetup();
        public void DeleteData(string nameTable, T entity)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAllData(string nameTable)
        {
            throw new NotImplementedException();
        }

        public T GetData(string nameTable, int id)
        {
            throw new NotImplementedException();
        }

        public void SetData(string nameTable, T entity)
        {
            try
            {
                int Id = 0;
                var SetData = _firebase.client.Set(nameTable + "/" + Id, entity);

            }
            catch (Exception)
            {
                Console.WriteLine("Wyjatek z dodaniem danych firebase");
            }
        }

        public void UpdateData(string nameTable, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
