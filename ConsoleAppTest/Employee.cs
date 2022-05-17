using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    public class Employee
    {
        public int Id { get; set; }
        public int DepartamentId{ get; set; }
        public int? ChiefId { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public Departament Departament { get; set; }
    }
}
