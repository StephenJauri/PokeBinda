using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccesslayer;
using DataObjects;
using System.Security.Cryptography;

namespace LogicLayer
{
    public class EmployeeManager : IEmployeeManager
    {
        private IEmployeeAccessor _employeeAccessor = null;

        public EmployeeManager()
        {
            _employeeAccessor = new EmployeeAccessor();
        }

        public string HashSha256(string source)
        {

            if (source == "" || source == null)
            {
                throw new ArgumentNullException("Missing Input");
            }
            // create a byte array
            byte[] data;

            // create a .NET hash provider object
            using (SHA256 sha256hasher = SHA256.Create())
            {
                data = sha256hasher.ComputeHash(
                    Encoding.UTF8.GetBytes(source));
            }

            // create output with a stringbuilder object
            StringBuilder s = new StringBuilder();

            // loop through the hashed output making characters from the values in the byte array
            Array.ForEach(data, (byteData) => s.Append(byteData.ToString("x2")));

            return s.ToString();
        }
        public Employee LoginEmployee(string email, string password)
        {
            Employee employee = null;
            email = email.ToLower();
            try
            {
                password = HashSha256(password);
                if (1 == _employeeAccessor.AuthenticateEmployeeWithEmailAndPasswordHash(email, password))
                {
                    employee = _employeeAccessor.SelectEmployeeByEmail(email);
                    employee.Roles = _employeeAccessor.SelectRolesByEmployeeID(employee);
                }
                else
                {
                    throw new ApplicationException("Bad Username or Password.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Something went wrong logging you in.", ex);
            }
            return employee;
        }
        public void ResetPassword(Account employee, string password, string newPassword)
        {
            password = HashSha256(password);
            newPassword = HashSha256(newPassword);
            try
            {
                if (0 == _employeeAccessor.UpdatePasswordHash(employee, password, newPassword))
                {
                    throw new ApplicationException("Incorrect password provided");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update the password", ex);
            }
        }
        public void ChangeEmployeeInformation(Account oldAccountInfo, Account newAccountInfo)
        {
            try
            {
                if (1 == _employeeAccessor.UpdateAccountInformation(oldAccountInfo, newAccountInfo))
                {
                    oldAccountInfo.GivenName = newAccountInfo.GivenName;
                    oldAccountInfo.FamilyName = newAccountInfo.FamilyName;
                    oldAccountInfo.Birthday = newAccountInfo.Birthday;
                }
                else
                {
                    throw new ApplicationException("Failed to update information");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to Update account information", ex);
            }
        }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = null;
            try
            {
                employees = _employeeAccessor.SelectAllEmployees();
                foreach (Employee employee in employees)
                {
                    employee.Roles = _employeeAccessor.SelectRolesByEmployeeID(employee);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load employees", ex);
            }
            return employees;
        }
        public void ChangeEmployeePasswordAdmin(Employee employee, string password)
        {
            password = HashSha256(password);
            try
            {
                if (0 == _employeeAccessor.UpdatePasswordAdmin(employee, password))
                {
                    throw new ApplicationException("No passwords updated");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update employee password!", ex);
            }
        }
        public void CreateEmployee(Employee employee, string password)
        {
            employee.Email = employee.Email.ToLower();
            password = HashSha256(password);
            try
            {
                if (0 != _employeeAccessor.SelectCountOfEmail(employee.Email))
                {
                    throw new ApplicationException("An account with that email already exists");
                }
                if (0 == _employeeAccessor.CreateEmployee(employee, password))
                {
                    throw new ApplicationException("No Employee added");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create account", ex);
            }
        }
        public void ChangeEmployeeAdmin(Employee employee)
        {
            employee.Email = employee.Email.ToLower();
            try
            {
                if (0 != _employeeAccessor.SelectCountOfEmailMinusEmployee(employee))
                {
                    throw new ApplicationException("An account with that email already exists");
                }
                if (0 == _employeeAccessor.UpdateEmployeeAdmin(employee))
                {
                    throw new ApplicationException("No Employee modified");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update account", ex);
            }
        }
        public List<string> GetAllRoles()
        {
            List<string> roles;
            try
            {
                roles = _employeeAccessor.SelectAllRoles();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load roles", ex);
            }
            return roles;
        }
        public bool FindUser(string email)
        {
            try
            {
                return _employeeAccessor.SelectCountOfEmail(email) == 1;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to search for that email", ex);
            }
        }

        public int RetrieveEmployeeIDFromEmail(string email)
        {
            try
            {
                return _employeeAccessor.SelectEmployeeByEmail(email).ID;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }

        public bool DeleteEmployeeRole(int employeeID, string roleName)
        {
            try
            {
                return _employeeAccessor.DeleteEmployeeRole(employeeID, roleName) != 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }

        public bool AddEmployeeRole(int employeeID, string roleName)
        {
            try
            {
                return _employeeAccessor.InsertEmployeeRole(employeeID, roleName) != 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database Error", ex);
            }
        }
    }
}
