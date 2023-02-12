print '' print '*** using database pokemon_management_db'
GO
USE [pokemon_management_db]
GO

print '' print '*** creating sp_select_gens_by_active_and_released'
GO

print '' print '*** creating sp_select_pokemon_by_active_and_released'
GO
CREATE PROCEDURE [dbo].[sp_select_pokemon_by_search]
(
	@PokeNameSearch	[nvarchar](16)
)
AS
	BEGIN
		SELECT [PokemonID],[PokemonName],[PokemonGen],[PokemonPokedexNumber]
		FROM [Pokemon]
		WHERE [PokemonReleased] = 1
		AND	[PokemonActive] = 1
		And [PokemonName] LIKE CONCAT('%',@PokeNameSearch,'%')
		ORDER BY [PokemonPokedexNumber]
	END
GO