using Wallet.Repositories.IRepositories;
using Wallet.Repositories;

namespace Wallet.Models
{
    internal class Category : BaseEntity
    {
        public string Name { get; set; }

        public int IdUser { get; set; }

        private IGenericRepository<Category> _repository = new GenericRepository<Category>();

        public void Create(string name, int idUser)
        {
            Name = name;
            IdUser = idUser;

            _repository.SetData("categories", this);
        }

        public List<Category> GetCategoriesById(int idUser)
        {

            List<Category> category = _repository.GetAllData("categories");

            if (category is not null)
            {
                return category.Where(a => a.IdUser == idUser).ToList();
            }

            return new List<Category>();
        }
    }
}
