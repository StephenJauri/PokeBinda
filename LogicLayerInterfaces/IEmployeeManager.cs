using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IEmployeeManager
    {
        Employee LoginEmployee(string email, string password);
        string HashSha256(string source);
        void ResetPassword(Account user, string password, string newPassword);
        void ChangeEmployeeInformation(Account oldAccountInfo, Account newAccountInfo);
        List<Employee> GetAllEmployees();
        bool FindUser(string email);
        void ChangeEmployeePasswordAdmin(Employee employee, string password);
        void CreateEmployee(Employee employee, string password);
        void ChangeEmployeeAdmin(Employee employee);
        List<string> GetAllRoles();
        int RetrieveEmployeeIDFromEmail(string email);
        bool DeleteEmployeeRole(int employeeID, string roleName);
        bool AddEmployeeRole(int employeeID, string roleName);
    }
}
