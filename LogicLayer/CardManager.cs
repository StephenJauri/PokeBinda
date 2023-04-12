using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccesslayer;
using DataAccessInterfaces;


namespace LogicLayer
{
    public class CardManager : ICardManager
    {
        private static CardManager instance;
        public static CardManager Instance {
            get
            {
                if (instance == null)
                {
                    instance = new CardManager();
                }
                return instance;
            }
        } 
        public List<PokemonCard> PokemonCards { get; private set; }
        private IPokemonCardAccessor _pokemonCardAccessor = null;
        private IImageAccessor _imageAccessor = null;
        public CardManager()
        {
            _pokemonCardAccessor = new PokemonCardAccessor();
            _imageAccessor = new ImageAccessor();
        }
        public CardManager(IPokemonCardAccessor pokemonCardAccessor)
        {
            _pokemonCardAccessor = pokemonCardAccessor;
        }
        public void LoadCardPokemon(PokemonCard card)
        {
            List<Pokemon> cardPokemon = null;
            try
            {
                cardPokemon = _pokemonCardAccessor.GetAllPokemonForPokemonCard(card);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load Pokemon card pokemon information", ex);
            }
            card.Pokemon = cardPokemon;
        }
        public void LoadCardTags(PokemonCard card)
        {
            List<string> cardTags = null;
            try
            {
                cardTags = _pokemonCardAccessor.GetAllTagsForPokemonCard(card);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load Pokemon card tag information", ex);
            }
            card.Tags = cardTags;
        }
        public void LoadCardTypes(PokemonCard card)
        {
            List<string> cardTypes = null;
            try
            {
                cardTypes = _pokemonCardAccessor.GetAllTypesForPokemonCard(card);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load Pokemon card type information", ex);
            }
            card.Types = cardTypes;
        }
        public void LoadCardAbilities(PokemonCard card)
        {
            List<Ability> cardAbilities = null;
            try
            {
                cardAbilities = _pokemonCardAccessor.GetAllAbilitiesForPokemonCard(card);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load Pokemon card ability information", ex);
            }
            card.Abilities = cardAbilities;
        }
        public void LoadCardInformation(PokemonCard card)
        {
            try
            {
                LoadCardAbilities(card);
                LoadCardTags(card);
                LoadCardPokemon(card);
                LoadCardTypes(card);
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Failed to load information for " + card.Name, ex);
            }
        }
        public void LoadAllCardsInformation(IEnumerable<PokemonCard> cards)
        {
            foreach (PokemonCard card in cards)
            {
                LoadCardInformation(card);
            }
        }
        public List<PokemonCard> LoadAllActiveReleasedCards()
        {

            List<PokemonCard> pokemonCards = LoadAllActiveReleasedCardsMinimum();
            LoadAllCardsInformation(pokemonCards);
            return pokemonCards;
        }

        public PokemonCard LoadActiveReleasedCard(int id)
        {
            PokemonCard card;
            try
            {
                card = _pokemonCardAccessor.GetPokemonCard(id);
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("Unable to load the card", ex);
            }
            LoadCardInformation(card);
            return card;
        }

        public List<PokemonCard> LoadAllActiveReleasedCardsMinimum()
        {

            List<PokemonCard> pokemonCards = null;
            try
            {
                pokemonCards = _pokemonCardAccessor.GetAllReleasedActivePokemonCards();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Couldn't load cards ", ex);
            }
            return pokemonCards;
        }

        public List<PokemonCard> LoadAllPokemonCards()
        {
            List<PokemonCard> pokemonCards = null;
            try
            {
                pokemonCards = _pokemonCardAccessor.GetAllPokemonCards();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Couldn't load cards ", ex);
            }
            LoadAllCardsInformation(pokemonCards);
            PokemonCards = pokemonCards;
            return PokemonCards;
        }

        public void UpdatePokemonCard(PokemonCard pokemonCard)
        {
            try
            {
                if (0 == _pokemonCardAccessor.UpdatePokemonCard(pokemonCard))
                {
                    throw new ApplicationException("No records were updated");
                }
                PokemonCards[PokemonCards.FindIndex((poke) => poke.ID == pokemonCard.ID)] = pokemonCard;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update the pokemon card", ex);
            }
        }

        public void CreatePokemonCard(PokemonCard pokemonCard)
        {
            try
            {
                pokemonCard.ID = _pokemonCardAccessor.InsertPokemonCard(pokemonCard);
                PokemonCards.Add(pokemonCard);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create the pokemon card", ex);
            }
        }

        public void CopyImageIntoApplication(string fullPath, string fileName)
        {
            try
            {
                _imageAccessor.CopyImageToDirectory(fullPath, fileName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to select file", ex);
            }
        }
    }
}
