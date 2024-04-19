using FireSharp.Response;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Xml.Linq;
using Wallet.Database.FirebaseRealTimeDatabase;
using Wallet.Models;
using Wallet.Models.Users;
using Wallet.Repositories.IRepositories;

namespace Wallet.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private FirebaseSetup _firebase = new FirebaseSetup();
        public void DeleteData(string nameTable, int id)
        {
            try
            {
               var DataToBeDelted = _firebase.client.Delete(nameTable + "/" + id);
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
                FirebaseResponse recordList = _firebase.client.Get(nameTable);
                Dictionary<string, T> dictionaryData = JsonConvert.DeserializeObject<Dictionary<string, T>>(recordList.Body.ToString());
               
                List<T> convertedData = new List<T>();

                /*foreach (var item in dictionaryData)
                {

                    convertedData
                }*/

                return convertedData;
            }
            catch (Exception)
            {
                Console.WriteLine("Wyjatek z dostaniem listy danych firebase");
                return null;
            }
        }

        public void SetData(string nameTable, T entity)
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
