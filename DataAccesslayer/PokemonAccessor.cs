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
    public class PokemonAccessor : IPokemonAccessor
    {
        public int InsertPokemon(Pokemon pokemon)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_insert_pokemon";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Gen", SqlDbType.TinyInt);
            cmd.Parameters.Add("@PokedexNumber", SqlDbType.SmallInt);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 16);
            cmd.Parameters.Add("@Released", SqlDbType.Bit);

            if (pokemon.Gen != null)
            {
                cmd.Parameters["@Gen"].Value = pokemon.Gen;
            }
            else
            {
                cmd.Parameters["@Gen"].Value = DBNull.Value;
            }

            if (pokemon.PokedexNumber != null)
            {
                cmd.Parameters["@PokedexNumber"].Value = pokemon.PokedexNumber;
            }
            else
            {
                cmd.Parameters["@PokedexNumber"].Value = DBNull.Value;
            }
            cmd.Parameters["@Name"].Value = pokemon.Name;
            cmd.Parameters["@Released"].Value = pokemon.Released;

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
        public List<Pokemon> SelectAllPokemon()
        {
            List<Pokemon> pokemons = new List<Pokemon>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_all_pokemon";

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
                                Active = reader.GetBoolean(4),
                                Released = reader.GetBoolean(5)
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

        public int UpdatePokemon(Pokemon pokemon)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_update_pokemon";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters.Add("@Gen", SqlDbType.TinyInt);
            cmd.Parameters.Add("@PokedexNumber", SqlDbType.SmallInt);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 16);
            cmd.Parameters.Add("@Released", SqlDbType.Bit);
            cmd.Parameters.Add("@Active", SqlDbType.Bit);

            cmd.Parameters["@ID"].Value = pokemon.ID;
            if (pokemon.Gen != null)
            {
                cmd.Parameters["@Gen"].Value = pokemon.Gen;
            }
            else
            {
                cmd.Parameters["@Gen"].Value = DBNull.Value;
            }
            if (pokemon.PokedexNumber != null)
            {
                cmd.Parameters["@PokedexNumber"].Value = pokemon.PokedexNumber;
            }
            else
            {
                cmd.Parameters["@PokedexNumber"].Value = DBNull.Value;
            }
            cmd.Parameters["@Name"].Value = pokemon.Name;
            cmd.Parameters["@Released"].Value = pokemon.Released;
            cmd.Parameters["@Active"].Value = pokemon.Active;

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
