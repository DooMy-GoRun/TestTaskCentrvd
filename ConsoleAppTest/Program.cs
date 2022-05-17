using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ConsoleAppTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int Zero_Salary = 0;

            int salarySumTemp;
            int salaryChiefTemp;
            int salaryMax = Zero_Salary;
            string departamentIdWithMaxSalary = "";
            List<int> salaryChiefList = new List<int>();

            Database.SetInitializer(new DropCreateDatabaseAlways<DbEntity>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DbEntity>());


            using (DbEntity db = new DbEntity())
            {
                Departament d1 = new Departament { Id = 1, Name = "D1" };
                Departament d2 = new Departament { Id = 2, Name = "D2" };
                Departament d3 = new Departament { Id = 3, Name = "D3" };

                db.Departaments.AddRange(new List<Departament> { d1, d2, d3 });
                db.SaveChanges();

                var departaments = db.Departaments.ToList();

                Employee employee1 = new Employee
                    { Id = 1, DepartamentId = 1, ChiefId = 5, Name = "John", Salary = 100 };
                Employee employee2 = new Employee
                    { Id = 2, DepartamentId = 1, ChiefId = 5, Name = "Misha", Salary = 600 };
                Employee employee3 = new Employee
                    { Id = 3, DepartamentId = 2, ChiefId = 6, Name = "Eugen", Salary = 300 };
                Employee employee4 = new Employee
                    { Id = 4, DepartamentId = 2, ChiefId = 6, Name = "Tolya", Salary = 400 };
                Employee employee5 = new Employee
                    { Id = 5, DepartamentId = 3, ChiefId = 7, Name = "Stepan", Salary = 500 };
                Employee employee6 = new Employee
                    { Id = 6, DepartamentId = 3, ChiefId = 7, Name = "Alex", Salary = 1000 };
                Employee employee7 = new Employee
                    { Id = 7, DepartamentId = 3, ChiefId = null, Name = "Ivan", Salary = 1100 };

                db.Employees.AddRange(new List<Employee> { employee1, employee2, employee3, employee4, employee5, employee6, employee7 });
                db.SaveChanges();

                var empoloyeesList = db.Employees.ToList();

                foreach (var dep in departaments)
                {
                    salarySumTemp = Zero_Salary;
                    salaryChiefTemp = Zero_Salary;
                    foreach (var emp in empoloyeesList)
                    {
                        var salary = emp.Salary;
                        if (dep.Id == emp.DepartamentId)
                        {
                            salarySumTemp += salary;
                        }

                        if (dep.Id == emp.DepartamentId && emp.ChiefId == null)
                        {
                            salaryChiefTemp += salary;
                            salaryChiefList.Add(salary);
                        }

                        if (salaryMax < salary)
                        {
                            salaryMax = salary;
                            departamentIdWithMaxSalary = emp.DepartamentId.ToString();
                        }
                    }

                    Console.WriteLine("Сумма зарплат сотрудников и руководителей: в {0} - {1}", dep.Name, salarySumTemp);
                    Console.WriteLine("Сумма зарплат сотрудников без руководителей: в {0} - {1}", dep.Name, salarySumTemp - salaryChiefTemp);
                    Console.WriteLine("*****************************************");
                }

                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("Департамент с максимальной зарплатой - {0}", departamentIdWithMaxSalary);

                int temp = 0;
                for (int write = 0; write < salaryChiefList.Count; write++)
                {
                    for (int sort = 0; sort < salaryChiefList.Count - 1; sort++)
                    {
                        if (salaryChiefList[sort] < salaryChiefList[sort + 1])
                        {
                            temp = salaryChiefList[sort + 1];
                            salaryChiefList[sort + 1] = salaryChiefList[sort];
                            salaryChiefList[sort] = temp;
                        }
                    }
                }

                Console.WriteLine("-----------------------------------------");
                Console.Write("Зарплаты руководителей по убыванию: ");
                foreach (var sortSalary in salaryChiefList)
                {
                    Console.Write("{0}; ", sortSalary);
                }
                    

                Console.WriteLine();
                Console.WriteLine("-----------------------------------------");
                Console.ReadKey();
            }
        }
    }
}
