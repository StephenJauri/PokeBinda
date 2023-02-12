using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccesslayer
{
    public class EmployeeAccessor : IEmployeeAccessor
    {
        public int AuthenticateEmployeeWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_authenticate_employee";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 128);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.Char, 64);

            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int CreateEmployee(Employee employee, string passwordHash)
        {
            int ID = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();
            SqlTransaction trans = null;

            string addEmployeeText = "sp_insert_employee";
            string addRoleText = "sp_insert_role_for_employee";


            try
            {
                // open the connection
                conn.Open();
                // begin the transaction
                trans = conn.BeginTransaction();

                // add the pokemon card
                SqlCommand cmdAddEmployee = new SqlCommand(addEmployeeText, conn, trans);
                cmdAddEmployee.CommandType = CommandType.StoredProcedure;
                cmdAddEmployee.Parameters.Add("@GivenName", SqlDbType.NVarChar, 64);
                cmdAddEmployee.Parameters.Add("@FamilyName", SqlDbType.NVarChar, 64);
                cmdAddEmployee.Parameters.Add("@Email", SqlDbType.NVarChar, 128);
                cmdAddEmployee.Parameters.Add("@Birthday", SqlDbType.Date);
                cmdAddEmployee.Parameters.Add("@PasswordHash", SqlDbType.Char, 64);

                cmdAddEmployee.Parameters["@GivenName"].Value = employee.GivenName;
                cmdAddEmployee.Parameters["@FamilyName"].Value = employee.FamilyName;
                cmdAddEmployee.Parameters["@Email"].Value = employee.Email;
                cmdAddEmployee.Parameters["@Birthday"].Value = employee.Birthday;
                cmdAddEmployee.Parameters["@PasswordHash"].Value = passwordHash;

                ID = Convert.ToInt32(cmdAddEmployee.ExecuteScalar());

                // add all tags to the card
                foreach (string role in employee.Roles)
                {
                    SqlCommand cmdAddTag = new SqlCommand(addRoleText, conn, trans);
                    cmdAddTag.CommandType = CommandType.StoredProcedure;
                    cmdAddTag.Parameters.Add("@RoleID", SqlDbType.NVarChar, 32);
                    cmdAddTag.Parameters.Add("@EmployeeID", SqlDbType.Int);
                    cmdAddTag.Parameters["@RoleID"].Value = role;
                    cmdAddTag.Parameters["@EmployeeID"].Value = ID;
                    cmdAddTag.ExecuteNonQuery();
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    trans.Rollback();
                }
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return ID;
        }


        public List<Employee> SelectAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_all_employees";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee()
                            {
                                ID = reader.GetInt32(0),
                                Email = reader.GetString(1),
                                GivenName = reader.GetString(2),
                                FamilyName = reader.GetString(3),
                                CreationDate = reader.GetDateTime(4),
                                Birthday = reader.GetDateTime(5),
                                Active = reader.GetBoolean(6)
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return employees;
        }

        public List<string> SelectAllRoles()
        {
            List<string> roles = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_all_roles";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            roles.Add(reader.GetString(0));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return roles;
        }

        public int SelectCountOfEmail(string email)
        {
            int count = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string checkEmailText = "sp_check_email_availablity";


            SqlCommand cmdCheckEmail = new SqlCommand(checkEmailText, conn);
            cmdCheckEmail.CommandType = CommandType.StoredProcedure;
            cmdCheckEmail.Parameters.Add("@Email", SqlDbType.NVarChar, 128);

            cmdCheckEmail.Parameters["@Email"].Value = email;
            try
            {
                conn.Open();

                count = Convert.ToInt32(cmdCheckEmail.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        public int SelectCountOfEmailMinusEmployee(Employee employee)
        {
            int count = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string checkEmailText = "sp_check_email_availablity_minus_employee";


            SqlCommand cmdCheckEmail = new SqlCommand(checkEmailText, conn);
            cmdCheckEmail.CommandType = CommandType.StoredProcedure;
            cmdCheckEmail.Parameters.Add("@Email", SqlDbType.NVarChar, 128);
            cmdCheckEmail.Parameters.Add("@EmployeeID", SqlDbType.Int);

            cmdCheckEmail.Parameters["@Email"].Value = employee.Email;
            cmdCheckEmail.Parameters["@EmployeeID"].Value = employee.ID;

            try
            {
                conn.Open();

                count = Convert.ToInt32(cmdCheckEmail.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        public Employee SelectEmployeeByEmail(string email)
        {
            Employee employee = null;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_employee_by_email";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 128);

            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        employee = new Employee()
                        {
                            ID = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            GivenName = reader.GetString(2),
                            FamilyName = reader.GetString(3),
                            CreationDate = reader.GetDateTime(4),
                            Birthday = reader.GetDateTime(5),
                            Active = reader.GetBoolean(6)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return employee;
        }

        public List<string> SelectRolesByEmployeeID(Employee employee)
        {
            List<string> roles = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_employee_roles_by_employeeid";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EmployeeID", SqlDbType.NVarChar, 128);

            cmd.Parameters["@EmployeeID"].Value = employee.ID;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            roles.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return roles;
        }

        public int UpdateAccountInformation(Account oldAccountInfo, Account newAccountInfo)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();


            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_update_employee_account_by_employeeid";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@OldGivenName", SqlDbType.NVarChar, 64);
            cmd.Parameters.Add("@OldFamilyName", SqlDbType.NVarChar, 64);
            cmd.Parameters.Add("@OldBirthday", SqlDbType.Date);
            cmd.Parameters.Add("@NewGivenName", SqlDbType.NVarChar, 64);
            cmd.Parameters.Add("@NewFamilyName", SqlDbType.NVarChar, 64);
            cmd.Parameters.Add("@NewBirthday", SqlDbType.Date);

            cmd.Parameters["@EmployeeID"].Value = oldAccountInfo.ID;
            cmd.Parameters["@OldGivenName"].Value = oldAccountInfo.GivenName;
            cmd.Parameters["@OldFamilyName"].Value = oldAccountInfo.FamilyName;
            cmd.Parameters["@OldBirthday"].Value = oldAccountInfo.Birthday;
            cmd.Parameters["@NewGivenName"].Value = newAccountInfo.GivenName;
            cmd.Parameters["@NewFamilyName"].Value = newAccountInfo.FamilyName;
            cmd.Parameters["@NewBirthday"].Value = newAccountInfo.Birthday;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int UpdateEmployeeAdmin(Employee employee)
        {
            int success = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();
            SqlTransaction trans = null;

            string removeRolesText = "sp_delete_roles_for_employee";
            string updateEmployeeText = "sp_update_employee_account_by_employeeid_for_admin";
            string addRoleText = "sp_insert_role_for_employee";


            try
            {
                conn.Open();
                trans = conn.BeginTransaction();

                SqlCommand cmdUpdateEmployee = new SqlCommand(updateEmployeeText, conn, trans);
                cmdUpdateEmployee.CommandType = CommandType.StoredProcedure;
                cmdUpdateEmployee.Parameters.Add("@EmployeeID", SqlDbType.Int);
                cmdUpdateEmployee.Parameters.Add("@NewGivenName", SqlDbType.NVarChar, 64);
                cmdUpdateEmployee.Parameters.Add("@NewFamilyName", SqlDbType.NVarChar, 64);
                cmdUpdateEmployee.Parameters.Add("@NewEmail", SqlDbType.NVarChar, 128);
                cmdUpdateEmployee.Parameters.Add("@NewBirthday", SqlDbType.Date);
                cmdUpdateEmployee.Parameters.Add("@Active", SqlDbType.Bit);

                cmdUpdateEmployee.Parameters["@EmployeeID"].Value = employee.ID;
                cmdUpdateEmployee.Parameters["@NewGivenName"].Value = employee.GivenName;
                cmdUpdateEmployee.Parameters["@NewFamilyName"].Value = employee.FamilyName;
                cmdUpdateEmployee.Parameters["@NewEmail"].Value = employee.Email;
                cmdUpdateEmployee.Parameters["@NewBirthday"].Value = employee.Birthday;
                cmdUpdateEmployee.Parameters["@Active"].Value = employee.Active;

                success = cmdUpdateEmployee.ExecuteNonQuery();

                SqlCommand cmdRemoveRoles = new SqlCommand(removeRolesText, conn, trans);
                cmdRemoveRoles.CommandType = CommandType.StoredProcedure;
                cmdRemoveRoles.Parameters.Add("@ID", SqlDbType.Int);
                cmdRemoveRoles.Parameters["@ID"].Value = employee.ID;

                cmdRemoveRoles.ExecuteNonQuery();

                foreach (string role in employee.Roles)
                {
                    SqlCommand cmdAddTag = new SqlCommand(addRoleText, conn, trans);
                    cmdAddTag.CommandType = CommandType.StoredProcedure;
                    cmdAddTag.Parameters.Add("@RoleID", SqlDbType.NVarChar, 32);
                    cmdAddTag.Parameters.Add("@EmployeeID", SqlDbType.Int);
                    cmdAddTag.Parameters["@RoleID"].Value = role;
                    cmdAddTag.Parameters["@EmployeeID"].Value = employee.ID;
                    cmdAddTag.ExecuteNonQuery();
                }


                trans.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    trans.Rollback();
                }
                catch (Exception ex2)
                {
                    throw ex2;
                }
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        public int UpdatePasswordAdmin(Employee employee, string passwordHash)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();


            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_update_employee_password_admin";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.Char, 64);

            cmd.Parameters["@EmployeeID"].Value = employee.ID;
            cmd.Parameters["@NewPasswordHash"].Value = passwordHash;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int UpdatePasswordHash(Account employee, string oldPasswordHash, string newPasswordHash)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();


            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_update_employee_passwordhash";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.Char, 64);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.Char, 64);

            cmd.Parameters["@EmployeeID"].Value = employee.ID;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
