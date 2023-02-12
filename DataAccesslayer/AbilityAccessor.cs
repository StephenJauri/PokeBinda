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
    public class AbilityAccessor : IAbilityAccessor
    {
        public List<Ability> SelectAllAbilities()
        {
            List<Ability> abilities = new List<Ability>();

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_select_all_abilities";

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
                            Ability newAbility = new Ability()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2)
                            };
                            abilities.Add(newAbility);
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
            return abilities;
        }

        public int InsertAbility(Ability ability)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_insert_ability";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 32);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 512);

            cmd.Parameters["@Name"].Value = ability.Name;
            cmd.Parameters["@Description"].Value = ability.Description;

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

        public int UpdateAbility(Ability ability)
        {
            int result = 0;

            DBConnection connectionFactory = new DBConnection();
            SqlConnection conn = connectionFactory.GetConnection();

            string cmdText = "sp_update_ability";

            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 32);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 512);

            cmd.Parameters["@ID"].Value = ability.ID;
            cmd.Parameters["@Name"].Value = ability.Name;
            cmd.Parameters["@Description"].Value = ability.Description;

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
