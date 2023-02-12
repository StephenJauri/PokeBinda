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
    public class LookupAccessor : ILookupAccessor
    {
        public List<Pokemon> SelectAllActiveReleasedPokemon()
        {
            List<Pokemon> pokemons = new List<Pokemon>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_pokemon_by_active_and_released";

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
                            Pokemon newPokemon = new Pokemon()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Active = true,
                                Released = true
                            };
                            if (!reader.IsDBNull(2))
                            {
                                newPokemon.Gen = reader.GetByte(2);
                            }
                            else
                            {
                                newPokemon.Gen = null;
                            }
                            if (!reader.IsDBNull(3))
                            {
                                newPokemon.PokedexNumber = reader.GetInt16(3);
                            }
                            else
                            {
                                newPokemon.PokedexNumber = null;
                            }
                            pokemons.Add(newPokemon);
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
            return pokemons;

        }

        public List<string> SelectAllCardTypes()
        {
            List<string> tags = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_types";

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
                            tags.Add(reader.GetString(0));
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
            return tags;
        }

        public List<string> SelectAllTags()
        {
            List<string> types = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_tags";

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
                            types.Add(reader.GetString(0));
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
            return types;
        }
        public List<string> SelectAllStatuses()
        {
            List<string> statuses = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_card_statuses";

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
                            statuses.Add(reader.GetString(0));
                        }
                        reader.Close();
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
            return statuses;
        }
        public List<int> SelectAllPokemonGens()
        {
            List<int> gens = new List<int>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_gens_by_active_and_released";

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
                            if (!reader.IsDBNull(0))
                            {
                                gens.Add(reader.GetByte(0));
                            }
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
            return gens;
        }
    }
}
