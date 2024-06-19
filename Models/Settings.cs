using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Repositories.IRepositories;
using Wallet.Repositories;

namespace Wallet.Models
{
    internal class Settings : BaseEntity
    {
        public int IdUser { get; set; }

        public bool Language {  get; set; } //true - pl, false, - en

        public bool Format { get; set; } //rrr-m-d-h - pl, false, - d-m-r-h

        private IGenericRepository<Settings> repository = new GenericRepository<Settings>();

        internal void GetSettings(int id)
        {
            List<Settings> sets = repository.GetAllData("settings");

            Settings set = sets.FirstOrDefault(s => s.IdUser == id);

            IdUser = id;
            Language = set.Language;
            Format = set.Format;
        }
    }
}
