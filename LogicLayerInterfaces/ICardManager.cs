using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface ICardManager
    {
        void LoadCardAbilities(PokemonCard card);
        void LoadCardPokemon(PokemonCard card);
        void LoadCardTags(PokemonCard card);
        void LoadCardTypes(PokemonCard card);
        void LoadCardInformation(PokemonCard card);
        void LoadAllCardsInformation(IEnumerable<PokemonCard> cards);
        List<PokemonCard> LoadAllActiveReleasedCards();
        List<PokemonCard> LoadAllPokemonCards();
        void UpdatePokemonCard(PokemonCard pokemonCard);
        void CreatePokemonCard(PokemonCard pokemonCard);
        void CopyImageIntoApplication(string fullPath, string fileName);
    }
}
