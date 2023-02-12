using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccesslayer;

namespace LogicLayer
{
    public class AbilityManager : IAbilityManager
    {
        private IAbilityAccessor _abilityAccessor = null;
        public AbilityManager()
        {
            _abilityAccessor = new AbilityAccessor();
        }
        public void CreateAbility(Ability ability)
        {
            try
            {
                if (0 == _abilityAccessor.InsertAbility(ability))
                {
                    throw new ApplicationException("No records were inserted");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add ability", ex);
            }
        }

        public List<Ability> GetAllAbilities()
        {
            List<Ability> abilities = null;
            try
            {
                abilities = _abilityAccessor.SelectAllAbilities();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load pokemon", ex);
            }
            return abilities;
        }

        public void UpdateAbility(Ability ability)
        {
            try
            {
                if (0 == _abilityAccessor.UpdateAbility(ability))
                {
                    throw new ApplicationException("No records were updated");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update pokemon",ex);
            }
        }
    }
}
