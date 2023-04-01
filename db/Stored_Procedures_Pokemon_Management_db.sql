print '' print '*** using database pokemon_management_db'
GO
USE [pokemon_management_db]
GO

print '' print '*** creating sp_authenticate_user'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
(
	@Email			[nvarchar](128),
	@PasswordHash	[char](64)
)
AS
	BEGIN
		SELECT 	COUNT([UserID]) AS 'Authenticated'
		FROM	[Users]
		WHERE	@Email = [UserEmail]
			AND	@PasswordHash = [UserPasswordHash]
			AND	[UserActive] = 1
	END
GO

print '' print '*** creating sp_select_user_by_email'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
(
	@Email			[nvarchar](128)
)
AS
	BEGIN
		SELECT 	[UserID], [UserEmail], [UserGivenName], [UserFamilyName], [UserCreationDate], [UserBirthday], [UserActive]
		FROM	[Users]
		WHERE @Email = [UserEmail]
	END
GO

print '' print '*** creating sp_select_card_groups_by_userid'
GO
CREATE PROCEDURE [dbo].[sp_select_card_groups_by_userid]
(
	@UserID		[int]
)
AS
	BEGIN
		SELECT 	[PokemonCardGroup].[PokemonCardGroupID], [PokemonCardGroupName], 
		CAST(CASE WHEN [FavoriteGroup].[UserID] IS NOT NULL THEN 1 ELSE 0 END AS [BIT]) AS Favorite
		, [PokemonCardGroupActive]
		FROM	[PokemonCardGroup]
		LEFT JOIN	[FavoriteGroup] ON [PokemonCardGroup].[PokemonCardGroupID] = [FavoriteGroup].[PokemonCardGroupID]
		WHERE 	@UserID = [PokemonCardGroup].[UserID]
			AND [PokemonCardGroupActive] = 1
		ORDER BY CAST(CASE WHEN [FavoriteGroup].[UserID] IS NOT NULL THEN 1 ELSE 0 END AS [BIT]) DESC, PokemonCardGroupName
	END
GO

print '' print '*** creating sp_create_user_group'
GO
CREATE PROCEDURE [dbo].[sp_create_user_group]
(
	@UserID		[int],
	@GroupName	[nvarchar](16)
)
AS
	BEGIN
		INSERT INTO [dbo].[PokemonCardGroup]
		([PokemonCardGroupName],[UserID])
		VALUES
		(@GroupName, @UserID)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** creating sp_change_favorite_group'
GO
CREATE PROCEDURE [dbo].[sp_change_favorite_group]
(
	@OldFavoriteGroupID	[int],
	@NewFavoriteGroupID	[int],
	@UserID				[int]
)
AS
	BEGIN TRY
		BEGIN TRANSACTION
			DELETE FROM [dbo].[FavoriteGroup]
				WHERE [PokemonCardGroupID] = @OldFavoriteGroupID 
					AND [UserID] = @UserID
			INSERT INTO [dbo].[FavoriteGroup]
				([PokemonCardGroupID],[UserID])
				VALUES
				(@NewFavoriteGroupID,@UserID)
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH
GO

print '' print '*** creating sp_delete_user_group'
GO
CREATE PROCEDURE [dbo].[sp_delete_user_group]
(
	@GroupID	[INT]
)
AS
	BEGIN
		UPDATE [PokemonCardGroup]
		SET [PokemonCardGroupActive] = 0
		WHERE [PokemonCardGroupID] = @GroupID
	END
GO

print '' print '*** creating sp_update_user_group_name_by_groupid'
GO
CREATE PROCEDURE [dbo].[sp_update_user_group_name_by_groupid]
(
	@OldName	[nvarchar](16),
	@NewName	[nvarchar](16),
	@GroupID	[int]
)
AS
	BEGIN
		UPDATE [PokemonCardGroup]
		SET [PokemonCardGroupName] = @NewName
		WHERE @GroupID = [PokemonCardGroupID]
		AND	@OldName = [PokemonCardGroupName]
	END
GO

print '' print '*** creating sp_select_types'
GO
CREATE PROCEDURE [dbo].[sp_select_types]
AS
	BEGIN
		SELECT [TypeID]
		FROM [Type]
	END
GO


print '' print '*** creating sp_select_tags'
GO
CREATE PROCEDURE [dbo].[sp_select_tags]
AS
	BEGIN
		SELECT [TagID]
		FROM [Tag]
	END
GO



print '' print '*** creating sp_select_card_statuses'
GO
CREATE PROCEDURE [dbo].[sp_select_card_statuses]
AS
	BEGIN
		SELECT [CardStatusID]
		FROM [CardStatus]
	END
GO


print '' print '*** creating sp_select_gens_by_active_and_released'
GO
CREATE PROCEDURE [dbo].[sp_select_gens_by_active_and_released]
AS
	BEGIN
		SELECT DISTINCT [PokemonGen]
		FROM [Pokemon]
		WHERE [PokemonReleased] = 1
		AND	[PokemonActive] = 1
		AND [PokemonGen] IS NOT NULL
		ORDER BY [PokemonGen]
	END
GO

print '' print '*** creating sp_select_pokemon_by_active_and_released'
GO
CREATE PROCEDURE [dbo].[sp_select_pokemon_by_active_and_released]
AS
	BEGIN
		SELECT [PokemonID],[PokemonName],[PokemonGen],[PokemonPokedexNumber]
		FROM [Pokemon]
		WHERE [PokemonReleased] = 1
		AND	[PokemonActive] = 1
		ORDER BY [PokemonPokedexNumber]
	END
GO

print '' print '*** creating sp_select_user_pokemon_cards_by_userid'
GO
CREATE PROCEDURE [dbo].[sp_select_active_user_pokemon_cards_by_userid]
(
	@UserID	[int]
)
AS
	BEGIN
		SELECT [PokemonCard].[PokemonCardID],[PokemonCardName],[PokemonCardNote],[PokemonCardHP],[PokemonCardReleaseYear],[PokemonCardSetNumber],	
               [PokemonCardImageName],[PokemonCardReleased],[PokemonCardActive],[UserPokemonCardID],[CardStatusID],
			   [UserPokemonCardDateAdded],[UserPokemonCardActive]	
		FROM [UserPokemonCard]
		JOIN [PokemonCard]
			ON [UserPokemonCard].[PokemonCardID] = [PokemonCard].[PokemonCardID]
		WHERE [UserID] = @UserID
		AND [UserPokemonCardActive] = 1
		ORDER BY [UserPokemonCard].[UserPokemonCardID]
	END
GO

print '' print '*** creating sp_select_user_cardids_by_groupid'
GO
CREATE PROCEDURE [dbo].[sp_select_user_cardids_by_groupid]
(
	@GroupID	[int]
)
AS
	BEGIN
		SELECT [UserPokemonCardID]
		FROM [PokemonCardGroupCard]
		WHERE [PokemonCardGroupID] = @GroupID
		ORDER BY [UserPokemonCardID]
	END
GO

print '' print '*** creating sp_select_tags_by_pokemon_cardid'
GO
CREATE PROCEDURE [dbo].[sp_select_tags_by_pokemon_cardid]
(
	@PokemonCardID	[int]
)
AS
	BEGIN
		SELECT [TagID]
		FROM [PokemonCardTag]
		WHERE [PokemonCardID] = @PokemonCardID
		ORDER BY [TagID]
	END
GO


print '' print '*** creating sp_select_abilities_by_pokemon_cardid'
GO
CREATE PROCEDURE [dbo].[sp_select_abilities_by_pokemon_cardid]
(
	@PokemonCardID	[int]
)
AS
	BEGIN
		SELECT [Ability].[AbilityID], [AbilityName], [AbilityDescription]
		FROM [PokemonCardAbility]
		JOIN [Ability]
		ON	[Ability].[AbilityID] = [PokemonCardAbility].[AbilityID]
		WHERE [PokemonCardID] = @PokemonCardID
		ORDER BY [AbilityName]
	END
GO



print '' print '*** creating sp_select_types_by_pokemon_cardid'
GO
CREATE PROCEDURE [dbo].[sp_select_types_by_pokemon_cardid]
(
	@PokemonCardID	[int]
)
AS
	BEGIN
		SELECT [TypeID]
		FROM [PokemonCardType]
		WHERE [PokemonCardID] = @PokemonCardID
		ORDER BY [TypeID]
	END
GO



print '' print '*** creating sp_select_pokemon_by_pokemon_cardid'
GO
CREATE PROCEDURE [dbo].[sp_select_pokemon_by_pokemon_cardid]
(
	@PokemonCardID	[int]
)
AS
	BEGIN
		SELECT [Pokemon].[PokemonID],[PokemonName],[PokemonGen],
		[PokemonPokeDexNumber],[PokemonReleased],[PokemonActive]
		FROM [PokemonCardPokemon]
		JOIN [Pokemon]
		ON	[PokemonCardPokemon].[PokemonID] = [Pokemon].[PokemonID]
		WHERE [PokemonCardID] = @PokemonCardID
		ORDER BY [Pokemon].[PokemonPokedexNumber]
	END
GO	

print '' print '*** creating sp_insert_card_into_group'
GO
CREATE PROCEDURE [dbo].[sp_insert_card_into_group]
(
	@GroupID	[int],
	@UserPokemonCardID	[int]
)
AS
	BEGIN
		INSERT INTO [dbo].[PokemonCardGroupCard]
		([PokemonCardGroupID],[UserPokemonCardID])
		VALUES
		(@GroupID,@UserPokemonCardID)
	END
GO

print '' print '*** creating sp_delete_card_from_group'
GO
CREATE PROCEDURE [dbo].[sp_delete_card_from_group]
(
	@GroupID	[int],
	@UserPokemonCardID	[int]
)
AS
	BEGIN
		DELETE FROM [dbo].[PokemonCardGroupCard]
		WHERE [PokemonCardGroupID] = @GroupID
			AND	[UserPokemonCardID] = @UserPokemonCardID
	END
GO


print '' print '*** creating sp_select_active_released_pokemon_cards'
GO
CREATE PROCEDURE [dbo].[sp_select_active_released_pokemon_cards]
AS
	BEGIN
		SELECT [PokemonCardID],[PokemonCardName],[PokemonCardNote],[PokemonCardHP],[PokemonCardReleaseYear],[PokemonCardSetNumber],	
               [PokemonCardImageName],[PokemonCardReleased],[PokemonCardActive]	
		FROM [PokemonCard]
		WHERE [PokemonCardActive] = 1
			AND [PokemonCardReleased] = 1
		ORDER BY [PokemonCardReleaseYear]
	END
GO


print '' print '*** creating sp_update_card_status_by_statusid_and_user_cardid'
GO
CREATE PROCEDURE [dbo].[sp_update_card_status_by_statusid_and_user_cardid]
(
	@OldStatus	[nvarchar](16),
	@NewStatus	[nvarchar](16),
	@UserPokemonCardID	[int]
)
AS
	BEGIN
		UPDATE [UserPokemonCard]
		SET [CardStatusID] = @NewStatus
		WHERE @UserPokemonCardID = [UserPokemonCardID]
		AND	@OldStatus = [CardStatusID]
	END
GO


print '' print '*** creating sp_insert_user_pokemon_card'
GO
CREATE PROCEDURE [dbo].[sp_insert_user_pokemon_card]
(
	@UserID	[int],
	@PokemonCardID	[int],
	@StatusID	[nvarchar](16)
)
AS
	BEGIN
		INSERT INTO [dbo].[UserPokemonCard]
		([UserID],[PokemonCardID],[CardStatusID])
		VALUES
		(@UserID,@PokemonCardID,@StatusID)
		SELECT SCOPE_IDENTITY()
	END
GO


print '' print '*** creating sp_remove_user_pokemon_card'
GO
CREATE PROCEDURE [dbo].[sp_remove_user_pokemon_card]
(
	@UserPokemonCardID	[int]
)
AS
	BEGIN
		UPDATE [dbo].[UserPokemonCard]
		SET [UserPokemonCardActive] = 0
		WHERE @UserPokemonCardID = [UserPokemonCardID]
	END
GO

print '' print '*** creating sp_update_user_account_by_userid'
GO
CREATE PROCEDURE [dbo].[sp_update_user_account_by_userid]
(
	@UserID	[int],
	@OldGivenName	[nvarchar](64),
	@OldFamilyName	[nvarchar](64),
	@OldBirthday	[date],
	@NewGivenName	[nvarchar](64),
	@NewFamilyName	[nvarchar](64),
	@NewBirthday	[date]
)
AS
	BEGIN
		UPDATE [dbo].[Users]
		SET	[UserGivenName] = @NewGivenName,
			[UserFamilyName] = @NewFamilyName,
			[UserBirthday] = @NewBirthday
		WHERE [UserID] = @UserID
			AND	[UserGivenName] = @OldGivenName
			AND	[UserFamilyName] = @OldFamilyName
			AND	[UserBirthday] = @OldBirthday
	END
GO


print '' print '*** creating sp_update_employee_account_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_update_employee_account_by_employeeid]
(
	@EmployeeID	[int],
	@OldGivenName	[nvarchar](64),
	@OldFamilyName	[nvarchar](64),
	@OldBirthday	[date],
	@NewGivenName	[nvarchar](64),
	@NewFamilyName	[nvarchar](64),
	@NewBirthday	[date]
)
AS
	BEGIN
		UPDATE [dbo].[Employee]
		SET	[EmployeeGivenName] = @NewGivenName,
			[EmployeeFamilyName] = @NewFamilyName,
			[EmployeeBirthday] = @NewBirthday
		WHERE [EmployeeID] = @EmployeeID
			AND	[EmployeeGivenName] = @OldGivenName
			AND	[EmployeeFamilyName] = @OldFamilyName
			AND	[EmployeeBirthday] = @OldBirthday
	END
GO


print '' print '*** creating sp_update_user_passwordhash'
GO
CREATE PROCEDURE [dbo].[sp_update_user_passwordhash]
(
	@UserID	[int],
	@OldPasswordHash	[char](64),
	@NewPasswordHash	[char](64)
)
AS
	BEGIN
		UPDATE [dbo].[Users]
		SET	[UserPasswordHash] = @NewPasswordHash
		WHERE	[UserID] = @UserID
			AND	[UserPasswordHash] = @OldPasswordHash
	END
GO



print '' print '*** creating sp_update_employee_passwordhash'
GO
CREATE PROCEDURE [dbo].[sp_update_employee_passwordhash]
(
	@EmployeeID	[int],
	@OldPasswordHash	[char](64),
	@NewPasswordHash	[char](64)
)
AS
	BEGIN
		UPDATE [dbo].[Employee]
		SET	[EmployeePasswordHash] = @NewPasswordHash
		WHERE	[EmployeeID] = @EmployeeID
			AND	[EmployeePasswordHash] = @OldPasswordHash
	END
GO

print '' print '*** creating sp_deactivate_user_account'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_user_account]
(
	@UserID	[int],
	@PasswordHash	[char](64)
)
AS
	BEGIN
		UPDATE [dbo].[Users]
		SET [UserActive] = 0
		WHERE [UserID] = @UserID
			AND	[UserPasswordHash] = @PasswordHash
	END
GO

print '' print '*** creating sp_authenticate_employee'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_employee]
(
	@Email			[nvarchar](128),
	@PasswordHash	[char](64)
)
AS
	BEGIN
		SELECT 	COUNT([EmployeeID]) AS 'Authenticated'
		FROM	[Employee]
		WHERE	@Email = [EmployeeEmail]
			AND	@PasswordHash = [EmployeePasswordHash]
			AND	[EmployeeActive] = 1
	END
GO

print '' print '*** creating sp_select_employee_by_email'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_by_email]
(
	@Email			[nvarchar](128)
)
AS
	BEGIN
		SELECT 	[EmployeeID], [EmployeeEmail], [EmployeeGivenName], [EmployeeFamilyName],
		[EmployeeCreationDate], [EmployeeBirthday], [EmployeeActive]
		FROM	[Employee]
		WHERE @Email = [EmployeeEmail]
	END
GO

print '' print '*** creating sp_select_employee_roles_by_employeeid'
GO
CREATE PROCEDURE [dbo].[sp_select_employee_roles_by_employeeid]
(
	@EmployeeID	[int]
)
AS
	BEGIN
		SELECT	[RoleID]
		FROM [EmployeeRole]
		WHERE	[EmployeeID] = @EmployeeID
	END
GO



print '' print '*** creating sp_select_all_pokemon_cards'
GO
CREATE PROCEDURE [dbo].[sp_select_all_pokemon_cards]
AS
	BEGIN
		SELECT [PokemonCardID],[PokemonCardName],[PokemonCardNote],[PokemonCardHP],[PokemonCardReleaseYear],[PokemonCardSetNumber],	
               [PokemonCardImageName],[PokemonCardReleased],[PokemonCardActive]	
		FROM [PokemonCard]
		ORDER BY [PokemonCardReleaseYear]
	END
GO

print '' print '*** creating sp_select_all_pokemon'
GO
CREATE PROCEDURE [dbo].[sp_select_all_pokemon]
AS
	BEGIN
		SELECT [PokemonID],[PokemonName],[PokemonGen],[PokemonPokedexNumber],[PokemonActive],[PokemonReleased]
		FROM [Pokemon]
		ORDER BY [PokemonActive] DESC, [PokemonReleased] DESC,[PokemonPokedexNumber]
	END
GO


print '' print '*** creating sp_insert_pokemon'
GO
CREATE PROCEDURE [dbo].[sp_insert_pokemon]
(
	@Gen	[tinyint],
	@PokedexNumber	[smallint],
	@Name	[nvarchar](16),
	@Released	[bit]
)
AS
	BEGIN
		INSERT INTO [dbo].[Pokemon]
		([PokemonGen],[PokemonPokedexNumber],[PokemonName],[PokemonReleased])
		VALUES
		(@Gen,@PokedexNumber,@Name,@Released)
	END
GO


print '' print '*** creating sp_update_pokemon'
GO
CREATE PROCEDURE [dbo].[sp_update_pokemon]
(
	@ID	[int],
	@Gen	[tinyint],
	@PokedexNumber	[smallint],
	@Name	[nvarchar](16),
	@Released [bit],
	@Active [bit]
)
AS
	BEGIN
		Update [dbo].[Pokemon]
		SET [PokemonGen] = @Gen, [PokemonPokedexNumber] = @PokedexNumber, [PokemonName] = @Name, [PokemonActive] = @Active, [PokemonReleased] = @Released
		WHERE [PokemonID] = @ID
	END
GO


print '' print '*** creating sp_select_all_abilities'
GO
CREATE PROCEDURE [dbo].[sp_select_all_abilities]
AS
	BEGIN
		SELECT [AbilityID],[AbilityName],[AbilityDescription]
		FROM [Ability]
		ORDER BY [AbilityID]
	END
GO


print '' print '*** creating sp_insert_ability'
GO
CREATE PROCEDURE [dbo].[sp_insert_ability]
(
	@Name	[nvarchar](32),
	@Description	[nvarchar](512)
)
AS
	BEGIN
		INSERT INTO [dbo].[Ability]
		([AbilityName],[AbilityDescription])
		VALUES
		(@Name,@Description)
	END
GO


print '' print '*** creating sp_update_ability'
GO
CREATE PROCEDURE [dbo].[sp_update_ability]
(
	@ID	[int],
	@Name	[nvarchar](16),
	@Description	[nvarchar](512)
)
AS
	BEGIN
		Update [dbo].[Ability]
		SET [AbilityName] = @Name, [AbilityDescription] = @Description
		WHERE [AbilityID] = @ID
	END
GO


print '' print '*** creating sp_insert_pokemon_card'
GO
CREATE PROCEDURE [dbo].[sp_insert_pokemon_card]
(
	@Name	[nvarchar](32),
	@Note	[nvarchar](64),
	@HP		[int],
	@ReleaseYear	[date],
	@SetNumber	[nvarchar](32),
	@ImageName	[nvarchar](64),
	@Released	[bit],
	@Active		[bit]
)
AS
	BEGIN 
		INSERT INTO [dbo].[PokemonCard]
		([PokemonCardName],[PokemonCardNote],[PokemonCardHP],[PokemonCardReleaseYear],[PokemonCardSetNumber],
		[PokemonCardImageName],[PokemonCardReleased],[PokemonCardActive])
		VALUES
		(@Name,@Note,@HP,@ReleaseYear,@SetNumber,@ImageName,@Released,@Active)
		SELECT SCOPE_IDENTITY()
	END
GO



print '' print '*** creating sp_insert_ability_for_card'
GO
CREATE PROCEDURE [dbo].[sp_insert_ability_for_card]
(
	@CardID	[int],
	@AbilityID	[int]
)
AS
	BEGIN
		INSERT INTO [dbo].[PokemonCardAbility]
		([PokemonCardID],[AbilityID])
		VALUES
		(@CardID,@AbilityID)
	END
GO


print '' print '*** creating sp_insert_pokemon_for_card'
GO
CREATE PROCEDURE [dbo].[sp_insert_pokemon_for_card]
(
	@CardID	[int],
	@PokemonID	[int]
)
AS
	BEGIN
		INSERT INTO [dbo].[PokemonCardPokemon]
		([PokemonCardID],[PokemonID])
		VALUES
		(@CardID,@PokemonID)
	END
GO


print '' print '*** creating sp_insert_tag_for_card'
GO
CREATE PROCEDURE [dbo].[sp_insert_tag_for_card]
(
	@CardID	[int],
	@TagID	[nvarchar](32)
)
AS
	BEGIN
		INSERT INTO [dbo].[PokemonCardTag]
		([PokemonCardID],[TagID])
		VALUES
		(@CardID,@TagID)
	END
GO


print '' print '*** creating sp_insert_type_for_card'
GO
CREATE PROCEDURE [dbo].[sp_insert_type_for_card]
(
	@CardID	[int],
	@TypeID	[nvarchar](16)
)
AS
	BEGIN
		INSERT INTO [dbo].[PokemonCardType]
		([PokemonCardID],[TypeID])
		VALUES
		(@CardID,@TypeID)
	END
GO


print '' print '*** creating sp_update_pokemon_card'
GO
CREATE PROCEDURE [dbo].[sp_update_pokemon_card]
(
	@ID	[int],
	@Name	[nvarchar](32),
	@Note	[nvarchar](64),
	@HP		[int],
	@ReleaseYear	[date],
	@SetNumber	[nvarchar](32),
	@ImageName	[nvarchar](64),
	@Released	[bit],
	@Active		[bit]
)
AS
	BEGIN 
		UPDATE [dbo].[PokemonCard]
		SET [PokemonCardName] = @Name,
		[PokemonCardNote] = @Note,
		[PokemonCardHP] = @HP,
		[PokemonCardReleaseYear] = @ReleaseYear,
		[PokemonCardSetNumber] = @SetNumber,
		[PokemonCardImageName] = @ImageName,
		[PokemonCardReleased] = @Released,
		[PokemonCardActive] = @Active
		WHERE [PokemonCardID] = @ID
	END
GO



print '' print '*** creating sp_delete_abilities_for_card'
GO
CREATE PROCEDURE [dbo].[sp_delete_abilities_for_card]
(
	@CardID	[int]
)
AS
	BEGIN
		DELETE FROM [dbo].[PokemonCardAbility]
		WHERE [PokemonCardID] = @CardID
	END
GO



print '' print '*** creating sp_delete_pokemon_for_card'
GO
CREATE PROCEDURE [dbo].[sp_delete_pokemon_for_card]
(
	@CardID	[int]
)
AS
	BEGIN
		DELETE FROM [dbo].[PokemonCardPokemon]
		WHERE [PokemonCardID] = @CardID
	END
GO


print '' print '*** creating sp_delete_tags_for_card'
GO
CREATE PROCEDURE [dbo].[sp_delete_tags_for_card]
(
	@CardID	[int]
)
AS
	BEGIN
		DELETE FROM [dbo].[PokemonCardTag]
		WHERE [PokemonCardID] = @CardID
	END
GO


print '' print '*** creating sp_delete_types_for_card'
GO
CREATE PROCEDURE [dbo].[sp_delete_types_for_card]
(
	@CardID	[int]
)
AS
	BEGIN
		DELETE FROM [dbo].[PokemonCardType]
		WHERE [PokemonCardID] = @CardID
	END
GO


print '' print '*** creating sp_select_all_employees'
GO
CREATE PROCEDURE [dbo].[sp_select_all_employees]
AS
	BEGIN
		SELECT 	[EmployeeID], [EmployeeEmail], [EmployeeGivenName], [EmployeeFamilyName],
		[EmployeeCreationDate], [EmployeeBirthday], [EmployeeActive]
		FROM	[Employee]
	END
GO



print '' print '*** creating sp_update_employee_account_by_employeeid_for_admin'
GO
CREATE PROCEDURE [dbo].[sp_update_employee_account_by_employeeid_for_admin]
(
	@EmployeeID	[int],
	@NewGivenName	[nvarchar](64),
	@NewFamilyName	[nvarchar](64),
	@NewEmail		[nvarchar](128),
	@NewBirthday	[date],
	@Active			[bit]
)
AS
	BEGIN
		UPDATE [dbo].[Employee]
		SET	[EmployeeGivenName] = @NewGivenName,
			[EmployeeFamilyName] = @NewFamilyName,
			[EmployeeBirthday] = @NewBirthday,
			[EmployeeEmail] = @NewEmail,
			[EmployeeActive] = @Active
		WHERE [EmployeeID] = @EmployeeID
	END
GO

print '' print '*** creating sp_update_employee_password_admin'
GO
CREATE PROCEDURE [dbo].[sp_update_employee_password_admin]
(
	@EmployeeID	[int],
	@NewPasswordHash	[char](64)
)
AS
	BEGIN
		UPDATE [dbo].[Employee]
		SET	[EmployeePasswordHash] = @NewPasswordHash
		WHERE	[EmployeeID] = @EmployeeID
	END
GO

print '' print '*** creating sp_insert_employee'
GO
CREATE PROCEDURE [dbo].[sp_insert_employee]
(
	@GivenName	[nvarchar](64),
	@FamilyName	[nvarchar](64),
	@Email		[nvarchar](128),
	@Birthday	[date],
	@PasswordHash	[char](64)
)
AS
	BEGIN
		INSERT INTO [dbo].[Employee]
		([EmployeeGivenName],[EmployeeFamilyName],[EmployeeEmail],[EmployeeBirthday],[EmployeePasswordHash])
		VALUES
		(@GivenName,@FamilyName,@Email,@Birthday,@PasswordHash)
		SELECT SCOPE_IDENTITY()
	END
GO


print '' print '*** creating sp_delete_roles_for_employee'
GO
CREATE PROCEDURE [dbo].[sp_delete_roles_for_employee]
(
	@ID	[int]
)
AS
	BEGIN
		DELETE FROM [dbo].[EmployeeRole]
		WHERE [EmployeeID] = @ID
	END
GO



print '' print '*** creating sp_insert_role_for_employee'
GO
CREATE PROCEDURE [dbo].[sp_insert_role_for_employee]
(
	@RoleID	[nvarchar](16),
	@EmployeeID	[int]
)
AS
	BEGIN
		INSERT INTO [dbo].[EmployeeRole]
		([RoleID],[EmployeeID])
		VALUES
		(@RoleID,@EmployeeID)
	END
GO

print '' print '*** creating sp_check_email_availablity'
GO
CREATE PROCEDURE [dbo].[sp_check_email_availablity]
(
	@Email	[nvarchar](128)
)
AS
	BEGIN
		SELECT COUNT([EmployeeEmail])
		FROM [Employee]
		WHERE [EmployeeEmail] = @Email
	END
GO


print '' print '*** creating sp_check_email_availablity_minus_employee'
GO
CREATE PROCEDURE [dbo].[sp_check_email_availablity_minus_employee]
(
	@Email	[nvarchar](128),
	@EmployeeID	[int]
)
AS
	BEGIN
		SELECT COUNT([EmployeeEmail])
		FROM [Employee]
		WHERE [EmployeeEmail] = @Email AND [EmployeeID] <> @EmployeeID
	END
GO

print '' print '*** creating sp_select_all_roles'
GO
CREATE PROCEDURE [dbo].[sp_select_all_roles]
AS
	BEGIN
		SELECT [RoleID]
		FROM [Role]
	END
GO


print '' print '*** creating sp_check_email_availablity_user'
GO
CREATE PROCEDURE [dbo].[sp_check_email_availablity_user]
(
	@Email	[nvarchar](128)
)
AS
	BEGIN
		SELECT COUNT([UserEmail])
		FROM [Users]
		WHERE [UserEmail] = @Email
	END
GO

print '' print '*** creating sp_insert_user'
GO
CREATE PROCEDURE [dbo].[sp_insert_user]
(
	@GivenName	[nvarchar](64),
	@FamilyName	[nvarchar](64),
	@Email		[nvarchar](128),
	@Birthday	[date],
	@PasswordHash	[char](64)
)
AS
	BEGIN TRY
		BEGIN TRANSACTION
		DECLARE @UserID AS int
		DECLARE @GroupID AS int
		INSERT INTO [dbo].[Users]
			([UserGivenName],[UserFamilyName],[UserEmail],[UserBirthday],[UserPasswordHash])
			VALUES
			(@GivenName,@FamilyName,@Email,@Birthday,@PasswordHash)
		SET @UserID = SCOPE_IDENTITY()
		INSERT INTO [dbo].[PokemonCardGroup]
			([UserID],[PokemonCardGroupName])
			VALUES
			(@UserID,'Favorite')
		SET @GroupID = SCOPE_IDENTITY()
		INSERT INTO [dbo].[FavoriteGroup]
			([PokemonCardGroupID],[UserID])
			VALUES
			(@GroupID,@UserID)
		SELECT 1 AS int
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH
GO


print '' print '*** creating sp_delete_employee_role'
GO
CREATE PROCEDURE [dbo].[sp_delete_employee_role]
(
	@EmployeeID	[INT],
	@RoleID		[NVARCHAR](16)
)
AS
	BEGIN
		DELETE FROM [EmployeeRole]
		WHERE [EmployeeID] = @EmployeeID
		AND	[RoleID] = @RoleID
	END
GO

print '' print '*** creating sp_insert_employee_role'
GO
CREATE PROCEDURE [dbo].[sp_insert_employee_role]
(
	@EmployeeID	[INT],
	@RoleID		[NVARCHAR](16)
)
AS
	BEGIN
		INSERT INTO [EmployeeRole]
		([EmployeeID], [RoleID])
		VALUES
		(@EmployeeID, @RoleID)
	END
GO
