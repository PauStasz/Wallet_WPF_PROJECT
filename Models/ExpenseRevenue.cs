using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Repositories.IRepositories;
using Wallet.Repositories;
using System.Diagnostics;

namespace Wallet.Models
{
    internal class ExpenseRevenue : BaseEntity
    {
        public int IdAccount { get; set; }

        public int IdCategory { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public double Salary { get; set; }

        public Category Category { get; set; }


        private IGenericRepository<ExpenseRevenue> _repository = new GenericRepository<ExpenseRevenue>();
        private IGenericRepository<Category> _categoryRepository = new GenericRepository<Category>();
        internal List<ExpenseRevenue> GetAllExpensesByAccountId(int id)
        {
            List<ExpenseRevenue> expenses = _repository.GetAllData("expenses");
            List<Category> categories = _categoryRepository.GetAllData("categories");
            List<ExpenseRevenue> result = new List<ExpenseRevenue>();


            if (expenses != null)
            {
                foreach (ExpenseRevenue expense in expenses)
                {
                    if (expense.IdAccount == id)
                    {
                        expense.Category = categories.FirstOrDefault(c => c.Id == expense.IdCategory);

                        result.Add(expense);
                    }

                }
            }

            return result;
        }
        
        internal List<ExpenseRevenue> GetAllRevenuesByAccountId(int id)
        {
            List<ExpenseRevenue> revenues = _repository.GetAllData("revenues");
            List<Category> categories = _categoryRepository.GetAllData("categories");
            List<ExpenseRevenue> result = new List<ExpenseRevenue>();

            if (revenues != null)
            {
                foreach (ExpenseRevenue revenue in revenues)
                {
                    if (revenue.IdAccount == id)
                    {
                        revenue.Category = categories.FirstOrDefault(c => c.Id == revenue.IdCategory);

                        result.Add(revenue);
                    }

                }
            }

            return result;
        }
    }
}
