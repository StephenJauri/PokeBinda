using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using System.Data;
using System.Data.SqlClient;
using DataObjects;

namespace DataAccesslayer
{
    public class UserGroupAccessor : IUserGroupAccessor
    {
        public int ChangeFavoriteGroup(int userID, int groupID, int currentFavorite)
        {
            int result = 0;

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();


            // command text
            string cmdText = "sp_change_favorite_group";

            // use the command 
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command
            cmd.Parameters.Add("@NewFavoriteGroupID", SqlDbType.Int);
            cmd.Parameters.Add("@OldFavoriteGroupID", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            // set the values for the parameter object
            cmd.Parameters["@NewFavoriteGroupID"].Value = groupID;
            cmd.Parameters["@OldFavoriteGroupID"].Value = currentFavorite;
            cmd.Parameters["@UserID"].Value = userID;

            // now that the cammond is set up, we can invoke it in a try-catch block

            try
            {
                // open the connection
                conn.Open();

                // execute the command appropriately and capture the results
                // you can ExecuteScalar, ExecuteNonQuery, or ExecuteReader depending on whether yo uexpect a single value, an int for rows affected, or rows and columns
                // aggregate, action, select

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
        public int UpdateGroupName(int groupID, string oldName, string newName)
        {
            int result = 0;

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();


            // command text
            string cmdText = "sp_update_user_group_name_by_groupid";

            // use the command 
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command
            cmd.Parameters.Add("@OldName", SqlDbType.NVarChar, 16);
            cmd.Parameters.Add("@NewName", SqlDbType.NVarChar, 16);
            cmd.Parameters.Add("@GroupID", SqlDbType.Int);

            // set the values for the parameter object
            cmd.Parameters["@OldName"].Value = oldName;
            cmd.Parameters["@NewName"].Value = newName;
            cmd.Parameters["@GroupID"].Value = groupID;

            // now that the cammond is set up, we can invoke it in a try-catch block

            try
            {
                // open the connection
                conn.Open();

                // execute the command appropriately and capture the results
                // you can ExecuteScalar, ExecuteNonQuery, or ExecuteReader depending on whether yo uexpect a single value, an int for rows affected, or rows and columns
                // aggregate, action, select

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
        public int CreateUserGroup(int userID, PokemonCardGroup group)
        {
            int result = 0;

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();


            // command text
            string cmdText = "sp_create_user_group";

            // use the command 
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@GroupName", SqlDbType.NVarChar, 16);

            // set the values for the parameter object
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@GroupName"].Value = group.Name;

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

        public List<PokemonCardGroupVM> GetPokemonCardGroupsByUserID(int userID)
        {
            List<PokemonCardGroupVM> userGroups = new List<PokemonCardGroupVM>();

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();


            // command text
            string cmdText = "sp_select_card_groups_by_userid";

            // use the command 
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            // set the values for the parameter object
            cmd.Parameters["@UserID"].Value = userID;

            // now that the cammond is set up, we can invoke it in a try-catch block
            try
            {
                // open the connection
                conn.Open();

                // execute the command appropriately and capture the results
                // you can ExecuteScalar, ExecuteNonQuery, or ExecuteReader depending on whether yo uexpect a single value, an int for rows affected, or rows and columns
                // aggregate, action, select

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PokemonCardGroupVM pokemonCardGroup = new PokemonCardGroupVM()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Favorite = reader.GetBoolean(2),
                                Active = reader.GetBoolean(3),
                                Cards = new List<UserPokemonCard>()
                            };
                            userGroups.Add(pokemonCardGroup);
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
            return userGroups;
        }
        public int DeleteUserGroup(int groupID)
        {
            int result = 0;


            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();


            // command text
            string cmdText = "sp_delete_user_group";

            // use the command 
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command
            cmd.Parameters.Add("@GroupID", SqlDbType.Int);

            // set the values for the parameter object
            cmd.Parameters["@GroupID"].Value = groupID;

            // now that the cammond is set up, we can invoke it in a try-catch block

            try
            {
                // open the connection
                conn.Open();

                // execute the command appropriately and capture the results
                // you can ExecuteScalar, ExecuteNonQuery, or ExecuteReader depending on whether yo uexpect a single value, an int for rows affected, or rows and columns
                // aggregate, action, select

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
