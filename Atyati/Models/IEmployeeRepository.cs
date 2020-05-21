using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atyati.Models
{
   public interface IEmployeeRepository
    {
        Employees GetEmployee(int Id);
        IEnumerable<Employees> GetAllEmployee();
        Employees Add(Employees employee);
        Employees Update(Employees employeeChanges);
        Employees Delete(int Id);

        IEnumerable<Product> GetAllProducts();
        Product AddProduct(Product prod);
        Product Update(Product ProductChanges);
        ////Category GetAllCat();
        //void Add(List<Product> tempProd);
        //IEnumerable<Product> GetCart();
        IEnumerable<TempSales> GetAllTemp();
        TempSales AddTemp(TempSales tempProd);
        TempSales DeleteTemp(int Pid);
        TempSales GetTemp(int Pid);
        void AddSales(Sold sold);
        IEnumerable<Sold> GetAllSold();
    }
}
