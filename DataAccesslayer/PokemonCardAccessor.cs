using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;
using System.Data;
using System.Data.SqlClient;

namespace DataAccesslayer
{
    public class PokemonCardAccessor : IPokemonCardAccessor
    {
        public List<Ability> GetAllAbilitiesForPokemonCard(PokemonCard card)
        {
            List<Ability> pokemonCardAbilities = new List<Ability>();

            DBConnection connectionFactory = new DBConnection();

            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_abilities_by_pokemon_cardid";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonCardID", SqlDbType.Int);

            cmd.Parameters["@PokemonCardID"].Value = card.ID;
            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Ability ability = new Ability()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2)
                            };
                            pokemonCardAbilities.Add(ability);
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
            return pokemonCardAbilities;
        }
        public List<PokemonCard> GetAllPokemonCards()
        {
            List<PokemonCard> pokemonCards = new List<PokemonCard>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_all_pokemon_cards";

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
                            PokemonCard pokemonCard = new PokemonCard()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Note = reader.GetString(2),
                                ReleaseYear = reader.GetDateTime(4),
                                SetNumber = reader.GetString(5),
                                ImageName = reader.GetString(6),
                                Released = reader.GetBoolean(7),
                                Active = reader.GetBoolean(8),
                                Abilities = new List<Ability>(),
                                Tags = new List<string>(),
                                Types = new List<string>(),
                                Pokemon = new List<Pokemon>()
                            };
                            if (!reader.IsDBNull(3))
                            {
                                pokemonCard.HP = reader.GetInt32(3);
                            }
                            else
                            {
                                pokemonCard.HP = null;
                            }
                            pokemonCards.Add(pokemonCard);
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
            return pokemonCards;
        }
        public List<Pokemon> GetAllPokemonForPokemonCard(PokemonCard card)
        {

            List<Pokemon> pokemonCardPokemon = new List<Pokemon>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_pokemon_by_pokemon_cardid";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonCardID", SqlDbType.Int);

            cmd.Parameters["@PokemonCardID"].Value = card.ID;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Pokemon pokemon = new Pokemon()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Released = reader.GetBoolean(4),
                                Active = reader.GetBoolean(5)
                            };
                            if (reader.IsDBNull(3))
                            {
                                pokemon.PokedexNumber = null;
                            }
                            else
                            {
                                pokemon.PokedexNumber = reader.GetInt16(3);
                            }
                            if (reader.IsDBNull(2))
                            {
                                pokemon.Gen = null;
                            }
                            else
                            {
                                pokemon.Gen = reader.GetByte(2);
                            }
                            pokemonCardPokemon.Add(pokemon);
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
            return pokemonCardPokemon;
        }
        public List<PokemonCard> GetAllReleasedActivePokemonCards()
        {
            List<PokemonCard> pokemonCards = new List<PokemonCard>();

            // ADO.NET needs a connection
            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_active_released_pokemon_cards";

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
                            PokemonCard pokemonCard = new PokemonCard()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Note = reader.GetString(2),
                                ReleaseYear = reader.GetDateTime(4),
                                SetNumber = reader.GetString(5),
                                ImageName = reader.GetString(6),
                                Released = reader.GetBoolean(7),
                                Active = reader.GetBoolean(8),
                                Abilities = new List<Ability>(),
                                Tags = new List<string>(),
                                Types = new List<string>(),
                                Pokemon = new List<Pokemon>()
                            };
                            if (!reader.IsDBNull(3))
                            {
                                pokemonCard.HP = reader.GetInt32(3);
                            }
                            else
                            {
                                pokemonCard.HP = null;
                            }
                            pokemonCards.Add(pokemonCard);
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
            return pokemonCards;
        }
        public List<string> GetAllTagsForPokemonCard(PokemonCard card)
        {
            List<string> pokemonCardTags = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_tags_by_pokemon_cardid";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonCardID", SqlDbType.Int);

            cmd.Parameters["@PokemonCardID"].Value = card.ID;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            pokemonCardTags.Add(reader.GetString(0));
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
            return pokemonCardTags;
        }
        public List<string> GetAllTypesForPokemonCard(PokemonCard card)
        {

            List<string> pokemonCardTypes = new List<string>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_types_by_pokemon_cardid";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonCardID", SqlDbType.Int);

            cmd.Parameters["@PokemonCardID"].Value = card.ID;

            try
            {
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            pokemonCardTypes.Add(reader.GetString(0));
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
            return pokemonCardTypes;
        }

        public int InsertPokemonCard(PokemonCard pokemonCard)
        {
            int ID = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();
            SqlTransaction trans = null;
            
            string addCardText = "sp_insert_pokemon_card";
            string addAbilityToCardText = "sp_insert_ability_for_card";
            string addPokemonToCardText = "sp_insert_pokemon_for_card";
            string addTagToCardText = "sp_insert_tag_for_card";
            string addTypeToCardText = "sp_insert_type_for_card";

            

            try
            {
                // open the connection
                conn.Open();
                // begin the transaction
                trans = conn.BeginTransaction();

                // add the pokemon card
                SqlCommand cmdAddCard = new SqlCommand(addCardText, conn,trans);
                cmdAddCard.CommandType = CommandType.StoredProcedure;
                cmdAddCard.Parameters.Add("@Name",SqlDbType.NVarChar,32);
                cmdAddCard.Parameters.Add("@Note",SqlDbType.NVarChar,64);
                cmdAddCard.Parameters.Add("@HP",SqlDbType.Int);
                cmdAddCard.Parameters.Add("@ReleaseYear",SqlDbType.Date);
                cmdAddCard.Parameters.Add("@SetNumber",SqlDbType.NVarChar,32);
                cmdAddCard.Parameters.Add("@ImageName",SqlDbType.NVarChar,64);
                cmdAddCard.Parameters.Add("@Released", SqlDbType.Bit);
                cmdAddCard.Parameters.Add("@Active", SqlDbType.Bit);


                cmdAddCard.Parameters["@Name"].Value = pokemonCard.Name;
                cmdAddCard.Parameters["@Note"].Value = pokemonCard.Note;
                if (pokemonCard.HP == null)
                {
                    cmdAddCard.Parameters["@HP"].Value = DBNull.Value;
                }
                else
                {
                    cmdAddCard.Parameters["@HP"].Value = pokemonCard.HP;
                }
                cmdAddCard.Parameters["@ReleaseYear"].Value = pokemonCard.ReleaseYear;
                cmdAddCard.Parameters["@SetNumber"].Value = pokemonCard.SetNumber;
                cmdAddCard.Parameters["@ImageName"].Value = pokemonCard.ImageName;
                cmdAddCard.Parameters["@Released"].Value = pokemonCard.Released;
                cmdAddCard.Parameters["@Active"].Value = pokemonCard.Released;

                ID = Convert.ToInt32(cmdAddCard.ExecuteScalar());

                // add all tags to the card
                foreach (string tag in pokemonCard.Tags)
                {
                    SqlCommand cmdAddTag = new SqlCommand(addTagToCardText, conn, trans);
                    cmdAddTag.CommandType = CommandType.StoredProcedure;
                    cmdAddTag.Parameters.Add("@TagID", SqlDbType.NVarChar, 32);
                    cmdAddTag.Parameters.Add("@CardID", SqlDbType.Int);
                    cmdAddTag.Parameters["@TagID"].Value = tag;
                    cmdAddTag.Parameters["@CardID"].Value = ID;
                    cmdAddTag.ExecuteNonQuery();
                }


                // add all types to the card
                foreach (string type in pokemonCard.Types)
                {
                    SqlCommand cmdAddType = new SqlCommand(addTypeToCardText, conn, trans);
                    cmdAddType.CommandType = CommandType.StoredProcedure;
                    cmdAddType.Parameters.Add("@TypeID", SqlDbType.NVarChar, 16);
                    cmdAddType.Parameters.Add("@CardID", SqlDbType.Int);
                    cmdAddType.Parameters["@TypeID"].Value = type;
                    cmdAddType.Parameters["@CardID"].Value = ID;
                    cmdAddType.ExecuteNonQuery();
                }

                // add all pokemon to the card
                foreach (Pokemon poke in pokemonCard.Pokemon)
                {
                    SqlCommand cmdAddPokemon = new SqlCommand(addPokemonToCardText, conn,trans);
                    cmdAddPokemon.CommandType = CommandType.StoredProcedure;
                    cmdAddPokemon.Parameters.Add("@PokemonID", SqlDbType.Int);
                    cmdAddPokemon.Parameters.Add("@CardID", SqlDbType.Int);
                    cmdAddPokemon.Parameters["@PokemonID"].Value = poke.ID;
                    cmdAddPokemon.Parameters["@CardID"].Value = ID;
                    cmdAddPokemon.ExecuteNonQuery();
                }

                // add all abilities to the card
                foreach (Ability ability in pokemonCard.Abilities)
                {
                    SqlCommand cmdAddAbility = new SqlCommand(addAbilityToCardText, conn,trans);
                    cmdAddAbility.CommandType = CommandType.StoredProcedure;
                    cmdAddAbility.Parameters.Add("@AbilityID", SqlDbType.Int);
                    cmdAddAbility.Parameters.Add("@CardID", SqlDbType.Int);
                    cmdAddAbility.Parameters["@AbilityID"].Value = ability.ID;
                    cmdAddAbility.Parameters["@CardID"].Value = ID;
                    cmdAddAbility.ExecuteNonQuery();
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

        public int UpdatePokemonCard(PokemonCard pokemonCard)
        {
            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();
            SqlTransaction trans = null;

            string addCardText = "sp_update_pokemon_card";
            string deleteAbilityFromCardText = "sp_delete_abilities_for_card";
            string deletePokemonFromCardText = "sp_delete_pokemon_for_card";
            string deleteTagFromCardText = "sp_delete_tags_for_card";
            string deleteTypeFromCardText = "sp_delete_types_for_card";
            string addAbilityToCardText = "sp_insert_ability_for_card";
            string addPokemonToCardText = "sp_insert_pokemon_for_card";
            string addTagToCardText = "sp_insert_tag_for_card";
            string addTypeToCardText = "sp_insert_type_for_card";

            int success = 0;

            try
            {
                // open the connection
                conn.Open();
                // begin the transaction
                trans = conn.BeginTransaction();

                // add the pokemon card
                SqlCommand cmdAddCard = new SqlCommand(addCardText, conn, trans);
                cmdAddCard.CommandType = CommandType.StoredProcedure;
                cmdAddCard.Parameters.Add("@ID", SqlDbType.Int);
                cmdAddCard.Parameters.Add("@Name", SqlDbType.NVarChar, 32);
                cmdAddCard.Parameters.Add("@Note", SqlDbType.NVarChar, 64);
                cmdAddCard.Parameters.Add("@HP", SqlDbType.Int);
                cmdAddCard.Parameters.Add("@ReleaseYear", SqlDbType.Date);
                cmdAddCard.Parameters.Add("@SetNumber", SqlDbType.NVarChar, 32);
                cmdAddCard.Parameters.Add("@ImageName", SqlDbType.NVarChar, 64);
                cmdAddCard.Parameters.Add("@Released", SqlDbType.Bit);
                cmdAddCard.Parameters.Add("@Active", SqlDbType.Bit);


                cmdAddCard.Parameters["@ID"].Value = pokemonCard.ID;
                cmdAddCard.Parameters["@Name"].Value = pokemonCard.Name;
                cmdAddCard.Parameters["@Note"].Value = pokemonCard.Note;
                if (pokemonCard.HP == null)
                {
                    cmdAddCard.Parameters["@HP"].Value = DBNull.Value;
                }
                else
                {
                    cmdAddCard.Parameters["@HP"].Value = pokemonCard.HP;
                }
                cmdAddCard.Parameters["@ReleaseYear"].Value = pokemonCard.ReleaseYear;
                cmdAddCard.Parameters["@SetNumber"].Value = pokemonCard.SetNumber;
                cmdAddCard.Parameters["@ImageName"].Value = pokemonCard.ImageName;
                cmdAddCard.Parameters["@Released"].Value = pokemonCard.Released;
                cmdAddCard.Parameters["@Active"].Value = pokemonCard.Released;

                success = Convert.ToInt32(cmdAddCard.ExecuteNonQuery());

                // remove old tags from card
                SqlCommand cmdRemoveTags = new SqlCommand(deleteTagFromCardText, conn, trans);
                cmdRemoveTags.CommandType = CommandType.StoredProcedure;
                cmdRemoveTags.Parameters.Add("@CardID",SqlDbType.Int);
                cmdRemoveTags.Parameters["@CardID"].Value = pokemonCard.ID;

                cmdRemoveTags.ExecuteNonQuery();

                // remove old types from card
                SqlCommand cmdRemoveTypes = new SqlCommand(deleteTypeFromCardText, conn, trans);
                cmdRemoveTypes.CommandType = CommandType.StoredProcedure;
                cmdRemoveTypes.Parameters.Add("@CardID", SqlDbType.Int);
                cmdRemoveTypes.Parameters["@CardID"].Value = pokemonCard.ID;

                cmdRemoveTypes.ExecuteNonQuery();


                // remove old pokemon from card
                SqlCommand cmdRemovePokemon = new SqlCommand(deletePokemonFromCardText, conn, trans);
                cmdRemovePokemon.CommandType = CommandType.StoredProcedure;
                cmdRemovePokemon.Parameters.Add("@CardID", SqlDbType.Int);
                cmdRemovePokemon.Parameters["@CardID"].Value = pokemonCard.ID;

                cmdRemovePokemon.ExecuteNonQuery();

                // remove old abiliities from card
                SqlCommand cmdRemoveAbilities = new SqlCommand(deleteAbilityFromCardText, conn, trans);
                cmdRemoveAbilities.CommandType = CommandType.StoredProcedure;
                cmdRemoveAbilities.Parameters.Add("@CardID", SqlDbType.Int);
                cmdRemoveAbilities.Parameters["@CardID"].Value = pokemonCard.ID;

                cmdRemoveAbilities.ExecuteNonQuery();

                // add all tags to the card
                foreach (string tag in pokemonCard.Tags)
                {
                    SqlCommand cmdAddTag = new SqlCommand(addTagToCardText, conn, trans);
                    cmdAddTag.CommandType = CommandType.StoredProcedure;
                    cmdAddTag.Parameters.Add("@TagID", SqlDbType.NVarChar, 32);
                    cmdAddTag.Parameters.Add("@CardID", SqlDbType.Int);
                    cmdAddTag.Parameters["@TagID"].Value = tag;
                    cmdAddTag.Parameters["@CardID"].Value = pokemonCard.ID;
                    cmdAddTag.ExecuteNonQuery();
                }


                // add all types to the card
                foreach (string type in pokemonCard.Types)
                {
                    SqlCommand cmdAddType = new SqlCommand(addTypeToCardText, conn, trans);
                    cmdAddType.CommandType = CommandType.StoredProcedure;
                    cmdAddType.Parameters.Add("@TypeID", SqlDbType.NVarChar, 16);
                    cmdAddType.Parameters.Add("@CardID", SqlDbType.Int);
                    cmdAddType.Parameters["@TypeID"].Value = type;
                    cmdAddType.Parameters["@CardID"].Value = pokemonCard.ID;
                    cmdAddType.ExecuteNonQuery();
                }

                // add all pokemon to the card
                foreach (Pokemon poke in pokemonCard.Pokemon)
                {
                    SqlCommand cmdAddPokemon = new SqlCommand(addPokemonToCardText, conn, trans);
                    cmdAddPokemon.CommandType = CommandType.StoredProcedure;
                    cmdAddPokemon.Parameters.Add("@PokemonID", SqlDbType.Int);
                    cmdAddPokemon.Parameters.Add("@CardID", SqlDbType.Int);
                    cmdAddPokemon.Parameters["@PokemonID"].Value = poke.ID;
                    cmdAddPokemon.Parameters["@CardID"].Value = pokemonCard.ID;
                    cmdAddPokemon.ExecuteNonQuery();
                }

                // add all abilities to the card
                foreach (Ability ability in pokemonCard.Abilities)
                {
                    SqlCommand cmdAddAbility = new SqlCommand(addAbilityToCardText, conn, trans);
                    cmdAddAbility.CommandType = CommandType.StoredProcedure;
                    cmdAddAbility.Parameters.Add("@AbilityID", SqlDbType.Int);
                    cmdAddAbility.Parameters.Add("@CardID", SqlDbType.Int);
                    cmdAddAbility.Parameters["@AbilityID"].Value = ability.ID;
                    cmdAddAbility.Parameters["@CardID"].Value = pokemonCard.ID;
                    cmdAddAbility.ExecuteNonQuery();
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
    }
}
