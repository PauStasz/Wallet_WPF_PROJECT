using FireSharp.Response;
using Wallet.Services.FirebaseRealTimeDatabase;
using Wallet.Models;
using Wallet.Repositories.IRepositories;
using Wallet.Models.Users;
using System.Diagnostics;

namespace Wallet.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private FirebaseSetup _firebase = new FirebaseSetup();
        public void DeleteData(string nameTable, int id)
        {
            try
            {
               var DataToBeDeleted = _firebase.client.Delete(nameTable + "/" + id);
            }
            catch (Exception)
            {
                Console.WriteLine("Wyjatek z usunieciem danych firebase");
            }
        }

        public List<T> GetAllData(string nameTable)
        {
            try
            {
                FirebaseResponse response = _firebase.client.Get(nameTable);
                Dictionary<string, T> data = response.ResultAs<Dictionary<string, T>>();

                List<T> getList = new List<T>();

                foreach (var item in data)
                {
                    getList.Add(item.Value);
                }

                

                return getList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyjatek z dostaniem listy danych firebase: " + ex.Message);
                return null;
            }
        }

        public T GetOneData(string nameTable, int id)
        {
            try
            {
                FirebaseResponse response = _firebase.client.Get(nameTable + "/" + id);
                T data = response.ResultAs<T>();

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyjatek z dostaniem listy danych firebase: " + ex.Message);
                return null;
            }
        }

        public virtual void SetData(string nameTable, T entity)
        {
            try
            {
                var SetData = _firebase.client.Set(nameTable + "/" + entity.Id, entity);
            }
            catch (Exception)
            {
                Console.WriteLine("Wyjatek z dodaniem danych firebase");
            }
        }

        public void UpdateData(string nameTable, T entity)
        {
            try
            {
                var DataToBeUpdated = _firebase.client.Update(nameTable + "/" + entity.Id, entity);
            }
            catch (Exception)
            {
                Console.WriteLine("Wyjatek z aktulizacja danych firebase");
            }
        }
    }
}
