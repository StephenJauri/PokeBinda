using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccesslayer
{
    public class UserAccessor : IUserAccessor
    {
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int result = 0;

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();


            // command text
            string cmdText = "sp_authenticate_user";

            // use the command 
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 128);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.Char, 64);

            // set the values for the parameter object
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            // now that the cammond is set up, we can invoke it in a try-catch block

            try
            {
                // open the connection
                conn.Open();

                // execute the command appropriately and capture the results
                // you can ExecuteScalar, ExecuteNonQuery, or ExecuteReader depending on whether yo uexpect a single value, an int for rows affected, or rows and columns
                // aggregate, action, select

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

        public int CheckIfEmailExists(string email)
        {
            int count = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string checkEmailText = "sp_check_email_availablity_user";


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

        public int DeactivateAccount(Account account, string passwordHash)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();


            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_deactivate_user_account";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 64);

            cmd.Parameters["@UserID"].Value = account.ID;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

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

        public int InsertAccount(User account, string passwordHash)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string addEmployeeText = "sp_insert_user";

            try
            {
                // open the connection
                conn.Open();

                SqlCommand cmdAddUser = new SqlCommand(addEmployeeText, conn);
                cmdAddUser.CommandType = CommandType.StoredProcedure;
                cmdAddUser.Parameters.Add("@GivenName", SqlDbType.NVarChar, 64);
                cmdAddUser.Parameters.Add("@FamilyName", SqlDbType.NVarChar, 64);
                cmdAddUser.Parameters.Add("@Email", SqlDbType.NVarChar, 128);
                cmdAddUser.Parameters.Add("@Birthday", SqlDbType.Date);
                cmdAddUser.Parameters.Add("@PasswordHash", SqlDbType.Char, 64);

                cmdAddUser.Parameters["@GivenName"].Value = account.GivenName;
                cmdAddUser.Parameters["@FamilyName"].Value = account.FamilyName;
                cmdAddUser.Parameters["@Email"].Value = account.Email;
                cmdAddUser.Parameters["@Birthday"].Value = account.Birthday;
                cmdAddUser.Parameters["@PasswordHash"].Value = passwordHash;

                result = cmdAddUser.ExecuteNonQuery();
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

        public User SelectUserByEmail(string email)
        {
            User user = null;

            // connection

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            // command text
            string cmdText = "sp_select_user_by_email";

            // command

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // command type

            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 128);

            // value
            cmd.Parameters["@Email"].Value = email;

            // try-catch-finally

            try
            {
                conn.Open();

                // execute and get a SqlDataReader
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        // most of the time there will be a while loop
                        // here we don't need it

                        reader.Read();

                        user = new User()
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

            return user;
        }

        public int UpdateAccountInformation(Account oldAccountInfo, Account newAccountInfo)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();


            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_update_user_account_by_userid";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@OldGivenName", SqlDbType.NVarChar, 64);
            cmd.Parameters.Add("@OldFamilyName", SqlDbType.NVarChar, 64);
            cmd.Parameters.Add("@OldBirthday", SqlDbType.Date);
            cmd.Parameters.Add("@NewGivenName", SqlDbType.NVarChar, 64);
            cmd.Parameters.Add("@NewFamilyName", SqlDbType.NVarChar, 64);
            cmd.Parameters.Add("@NewBirthday", SqlDbType.Date);

            cmd.Parameters["@UserID"].Value = oldAccountInfo.ID;
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

        public int UpdatePasswordHash(int userID, string oldPasswordHash, string newPasswordHash)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();


            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_update_user_passwordhash";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.Char, 64);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.Char, 64);

            cmd.Parameters["@UserID"].Value = userID;
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
