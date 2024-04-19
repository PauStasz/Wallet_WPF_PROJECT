﻿using FireSharp.Extensions;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Reflection;
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
                FirebaseResponse response = _firebase.client.Get(nameTable);
                List<T> data = response.ResultAs<List<T>>();

                return data;
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
