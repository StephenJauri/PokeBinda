using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccesslayer
{
    public class UserPokemonCardAccessor : IUserPokemonCardAccessor
    {
        public int CreateUserPokemonCard(UserPokemonCard card, User user)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_insert_user_pokemon_card";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@PokemonCardID", SqlDbType.Int);
            cmd.Parameters.Add("@StatusID", SqlDbType.NVarChar, 16);

            cmd.Parameters["@UserID"].Value = user.ID;
            cmd.Parameters["@PokemonCardID"].Value = card.ID;
            cmd.Parameters["@StatusID"].Value = card.Status;

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

        public List<UserPokemonCard> GetAllUserPokemonCards(User user)
        {
            List<UserPokemonCard> userPokemonCards = new List<UserPokemonCard>();

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();


            // command text
            string cmdText = "sp_select_active_user_pokemon_cards_by_userid";

            // use the command 
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            // set the command type
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameter objects to the command
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            // set the values for the parameter object
            cmd.Parameters["@UserID"].Value = user.ID;

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
                            UserPokemonCard userPokemonCard = new UserPokemonCard()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Note = reader.GetString(2),
                                ReleaseYear = reader.GetDateTime(4),
                                SetNumber = reader.GetString(5),
                                ImageName = reader.GetString(6),
                                Released = reader.GetBoolean(7),
                                Active = reader.GetBoolean(8),
                                UserCardID = reader.GetInt32(9),
                                Status = reader.GetString(10),
                                DateAdded = reader.GetDateTime(11),
                                UserCardActive = reader.GetBoolean(12),
                                Groups = new List<PokemonCardGroupVM>(),
                                Abilities = new List<Ability>(),
                                Tags = new List<string>(),
                                Types = new List<string>(),
                                Pokemon = new List<Pokemon>()
                            };
                            if (!reader.IsDBNull(3))
                            {
                                userPokemonCard.HP = reader.GetInt32(3);
                            }
                            else
                            {
                                userPokemonCard.HP = null;
                            }
                            userPokemonCards.Add(userPokemonCard);
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
            return userPokemonCards;
        }

        public int RemoveUserPokemonCard(UserPokemonCard card)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_remove_user_pokemon_card";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserPokemonCardID", SqlDbType.Int);

            cmd.Parameters["@UserPokemonCardID"].Value = card.UserCardID;
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

        public int UpdateCardStatus(UserPokemonCard card, string newStatus)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_update_card_status_by_statusid_and_user_cardid";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@OldStatus", SqlDbType.NVarChar, 16);
            cmd.Parameters.Add("@NewStatus", SqlDbType.NVarChar, 16);
            cmd.Parameters.Add("@UserPokemonCardID", SqlDbType.Int);

            cmd.Parameters["@OldStatus"].Value = card.Status;
            cmd.Parameters["@NewStatus"].Value = newStatus;
            cmd.Parameters["@UserPokemonCardID"].Value = card.UserCardID;
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
