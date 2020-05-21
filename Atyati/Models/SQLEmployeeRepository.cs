using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atyati.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AtyatiContext context;
        ViewModel vm;
        public SQLEmployeeRepository(AtyatiContext context)
        {
            this.context = context;
            vm = new ViewModel();
        }

        public Employees Add(Employees employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employees Delete(int Id)
        {
            Employees employee = context.Employees.Find(Id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employees> GetAllEmployee()
        {
          return context.Employees;
          
        }
       //public Category GetAllCat()
       // {
       //     return context.Category;
       // }
        public Employees GetEmployee(int Id)
        {
            return context.Employees.Find(Id);
        }

        public Employees Update(Employees employeeChanges)
        {
            var employee = context.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeeChanges;
        }
        public Product Update(Product productChanges)
        {
            if (productChanges.Quantity == 0)
            {
                productChanges.IsOutOfStock = true;
            }
            else
            {
                productChanges.IsOutOfStock = false;
            }
            var p = context.Product.Attach(productChanges);
            p.State= Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return productChanges;
        }


        public IEnumerable<Product> GetAllProducts()
        {
            var result = from p in context.Product
                         join ct in context.Category
                         on p.CategoryId equals ct.CategoryId
                         select new Product
                         {
                             Pid = p.Pid,
                             Category = ct,
                             Name = p.Name,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             IsOutOfStock = p.IsOutOfStock,
                             Brand = p.Brand,
                             CategoryId = ct.CategoryId

                         };
            return result;
        }



        public Product AddProduct(Product prod)
        {
            if(prod.Quantity==0)
            {
                prod.IsOutOfStock = true;
            }
            else
            {
                prod.IsOutOfStock = false;
            }
            context.Product.Add(prod);
            context.SaveChanges();
            return prod;
        }
        //public void  Add(List<Product> tempProd) {
           
        //    vm.ProductsInCart = tempProd;
        //    //return vm.ProductsToBeSold;
        //    //context.Employees.Add(employee);
        //    //context.SaveChanges();
        //    //return employee;
        //}

        //public IEnumerable<Product> GetCart()
        //{            
        //    return vm.ProductsInCart;
          
        //}

        public TempSales AddTemp(TempSales tempProd)
        {

            context.TempSales.Add(tempProd);
            context.SaveChanges();
            return tempProd;
        }
        public TempSales DeleteTemp(int Pid)
        {
            TempSales tempSales = context.TempSales.Find(Pid);
            if (tempSales != null)
            {
                context.TempSales.Remove(tempSales);
                context.SaveChanges();
            }
            return tempSales;
        }
        public TempSales GetTemp(int Pid)
        {
            return context.TempSales.Find(Pid);
        }


        public IEnumerable<TempSales> GetAllTemp()
        {
            var result = from p in context.TempSales
                         join ct in context.Category
                         on p.CategoryId equals ct.CategoryId
                         select new TempSales
                         {
                             Pid = p.Pid,
                             Category = ct,
                             Name = p.Name,
                             Price = p.Price,
                             Quantity = p.Quantity,
                             IsOutOfStock = p.IsOutOfStock,
                             Brand = p.Brand,
                             CategoryId = ct.CategoryId

                         };
            return result;

        }


        public IEnumerable<Sold>GetAllSold()
        {
            var result = from p in context.Sold
                         join ct in context.Category
                         on p.CategoryId equals ct.CategoryId
                         select new Sold
                         {
                             Pid = p.Pid,
                             Category = ct,
                             Name = p.Name,
                             Price = p.Price,
                             Quantity = p.Quantity,                    
                             Brand = p.Brand,
                             CategoryId = ct.CategoryId

                         };
            return result;

        }


        public void AddSales(Sold sold)
        {
            context.Sold.Add(sold);
            context.SaveChanges();
            
        }
    }
    }

