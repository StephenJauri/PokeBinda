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
    public class PokemonCardGroupAccessor : IPokemonCardGroupCardAccessor
    {
        public int AddCardToGroup(UserPokemonCard card, PokemonCardGroup group)
        {
            int result = 0;

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();


            // command text
            string cmdText = "sp_insert_card_into_group";

            // use the command 
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command
            cmd.Parameters.Add("@GroupID", SqlDbType.Int);
            cmd.Parameters.Add("@UserPokemonCardID", SqlDbType.Int);

            // set the values for the parameter object
            cmd.Parameters["@GroupID"].Value = group.ID;
            cmd.Parameters["@UserPokemonCardID"].Value = card.UserCardID;

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

        public List<int> GetPokemonCardIDsInGroup(PokemonCardGroup group)
        {
            List<int> cardIDs = new List<int>();

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();


            // command text
            string cmdText = "sp_select_user_cardids_by_groupid";

            // use the command 
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command
            cmd.Parameters.Add("@GroupID", SqlDbType.Int);

            // set the values for the parameter object
            cmd.Parameters["@GroupID"].Value = group.ID;

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
                            cardIDs.Add(reader.GetInt32(0));
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
            return cardIDs;
        }

        public int RemoveCardFromGroup(UserPokemonCard card, PokemonCardGroup group)
        {
            int result = 0;

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();


            // command text
            string cmdText = "sp_delete_card_from_group";

            // use the command 
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command
            cmd.Parameters.Add("@GroupID", SqlDbType.Int);
            cmd.Parameters.Add("@UserPokemonCardID", SqlDbType.Int);

            // set the values for the parameter object
            cmd.Parameters["@GroupID"].Value = group.ID;
            cmd.Parameters["@UserPokemonCardID"].Value = card.UserCardID;

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
