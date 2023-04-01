using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IEmployeeAccessor
    {
        int AuthenticateEmployeeWithEmailAndPasswordHash(string email, string passwordHash);
        Employee SelectEmployeeByEmail(string email);
        List<string> SelectRolesByEmployeeID(Employee employee);
        int UpdatePasswordHash(Account employee, string oldPasswordHash, string newPasswordHash);
        int UpdateAccountInformation(Account oldAccountInformation, Account newAccountInformation);
        List<Employee> SelectAllEmployees();
        int CreateEmployee(Employee employee, string passwordHash);
        int SelectCountOfEmail(string email);
        int SelectCountOfEmailMinusEmployee(Employee employee);
        int UpdatePasswordAdmin(Employee employee, string passwordHash);
        int UpdateEmployeeAdmin(Employee employee);
        List<string> SelectAllRoles();
        int DeleteEmployeeRole(int employeeId, string roleId);
        int InsertEmployeeRole(int employeeId, string roleId);
    }
}
