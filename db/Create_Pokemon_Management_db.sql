IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
			WHERE name = 'pokemon_management_db')
BEGIN
	DROP DATABASE pokemon_management_db
	print '' print '*** dropping database pokemon_management_db'
END
GO


print '' print '*** creating database pokemon_management_db'
GO
CREATE DATABASE pokemon_management_db
GO

print '' print '*** using database pokemon_management_db'
GO
USE [pokemon_management_db]
GO


-- Role Table
print '' print '*** creating Role table'
GO
CREATE TABLE [dbo].[Role] (
    [RoleID]        [nvarchar](16)        	not null,
    [RoleDescription]	[nvarchar](256)			not null,
	
    constraint [pk_RoleID]    Primary Key ([RoleID])
    )
GO



-- Tag Table
print '' print '*** creating Tag table'
GO
CREATE TABLE [dbo].[Tag] (
    [TagID]        [nvarchar](32)        		not null,
    [TagDescription]	[nvarchar](256)			not null,
	
    constraint [pk_TagID]    Primary Key ([TagID])
    )
GO



-- Pokemon Card Table
print '' print '*** creating PokemonCard table'
GO
CREATE TABLE [dbo].[PokemonCard] (
	[PokemonCardID]				[int]	IDENTITY(100000, 1)		not null,
	[PokemonCardName]			[nvarchar](32)					not null,
	[PokemonCardNote]			[nvarchar](64)					not null,
	[PokemonCardHP]				[int]							null,
	[PokemonCardReleaseYear]	[date]							not null,
	[PokemonCardSetNumber]		[nvarchar](32)					not null,
	[PokemonCardImageName]		[nvarchar](64)		 			not null,
	[PokemonCardReleased]		[bit]							not null,
	[PokemonCardActive]			[bit]	DEFAULT 1				not null,
	
	CONSTRAINT [pk_PokemonCardID]			PRIMARY KEY ([PokemonCardID])
)
GO

-- Index Pokemon Card Table
print '' print '*** adding indexes to PokemonCard table'
GO
CREATE INDEX ix_PokemonCardReleaseYear 
	ON [dbo].[PokemonCard] ([PokemonCardReleaseYear])
GO



-- Card Tag Table
print '' print '*** creating PokemonCardTag table'
GO
CREATE TABLE [dbo].[PokemonCardTag] (
	[PokemonCardID]	[int]			not null,
	[TagID]		[nvarchar](32)	not null,
	
	CONSTRAINT [fk_TagTablePokemonCardID] 				FOREIGN KEY ([PokemonCardID]) REFERENCES [PokemonCard]([PokemonCardID]),
	CONSTRAINT [fk_TagTableTagID] 					FOREIGN KEY ([TagID]) REFERENCES [Tag]([TagID]),
	CONSTRAINT [uc_PokemonCardTag] 								UNIQUE ([PokemonCardID],[TagID])
)
GO

-- Index Card Tag Table
print '' print '*** adding indexes to PokemonCardTag table'
GO
CREATE INDEX ix_PokemonCardTagTagID
	ON [dbo].[PokemonCardTag] ([TagID])
GO
CREATE INDEX ix_PokemonCardTagPokemonCardID
	ON [dbo].[PokemonCardTag] ([PokemonCardID])
GO


-- Type Table
print '' print '*** creating Type table'
GO
CREATE TABLE [dbo].[Type] (
    [TypeID]        	[nvarchar](16)        	not null,
    [TypeDescription]    	[nvarchar](256)			not null,
	
    constraint [pk_TypeID]    Primary Key ([TypeID])
    )
GO

-- Ability Table
print '' print '*** creating Ability table'
GO
CREATE TABLE [dbo].[Ability] (
    [AbilityID]        		[int] IDENTITY(100000,1)not null,
    [AbilityName]    		[nvarchar](32)			not null,
    [AbilityDescription]	[nvarchar](512)			not null,
	
    constraint [pk_AbilityID]    Primary Key ([AbilityID])
    )
GO


-- Pokemon Table
print '' print '*** creating Pokemon table'
GO
CREATE TABLE [dbo].[Pokemon] (
	[PokemonID]					[int]	IDENTITY(1, 1)			not null,
	[PokemonName]				[nvarchar](16)					not null,
	/*[PokemonDescription]		[nvarchar](256)					not null,*/
	[PokemonGen]				[tinyint]						null,
	[PokemonPokedexNumber]		[smallint]						null,
	[PokemonReleased]		[bit]	DEFAULT 1					not null,
	[PokemonActive]			[bit]	DEFAULT 1					not null,
	
	CONSTRAINT [pk_PokemonID]	PRIMARY KEY ([PokemonID]),
	CONSTRAINT [uc_PokemonName]	UNIQUE ([PokemonName])
)
GO


-- Index Pokemon Table
print '' print '*** adding indexes to Pokemon table'
GO
CREATE INDEX ix_PokemonGen
	ON [dbo].[Pokemon] ([PokemonGen])
GO
CREATE INDEX ix_PokemonPokedexNumber
	ON [dbo].[Pokemon] ([PokemonPokedexNumber])
GO


-- User Table
print '' print '*** creating Users table'
GO
CREATE TABLE [dbo].[Users] (
	[UserID]			[int]	IDENTITY(100000, 1)		not null,
	[UserEmail]			[nvarchar](128)					not null,
	[UserPasswordHash]	[char](64)						not null,
	[UserGivenName]		[nvarchar](64)					not null,
	[UserFamilyName]	[nvarchar](64)					not null,
	[UserCreationDate]	[date]		DEFAULT GETDATE()	not null,
	[UserBirthday]		[date]							not null,
	[UserActive]		[bit]		DEFAULT 1			not null,
	
	CONSTRAINT [pk_UserID]	PRIMARY KEY ([UserID]),
	CONSTRAINT [uc_UserEmail]	UNIQUE ([UserEmail])
)
GO

-- Index Users Table
print '' print '*** adding indexes to Users table'
GO
CREATE INDEX ix_UserEmail
	ON [dbo].[Users] ([UserEmail])
GO


-- Employee Table
print '' print '*** creating Employee table'
GO
CREATE TABLE [dbo].[Employee] (
	[EmployeeID]			[int]	IDENTITY(100000, 1)		not null,
	[EmployeeEmail]			[nvarchar](128)					not null,
	[EmployeePasswordHash]	[char](64)						not null,
	[EmployeeGivenName]		[nvarchar](64)					not null,
	[EmployeeFamilyName]	[nvarchar](64)					not null,
	[EmployeeCreationDate]	[date]		DEFAULT GETDATE()	not null,
	[EmployeeBirthday]		[date]							not null,
	[EmployeeActive]		[bit]		DEFAULT 1			not null,
	
	CONSTRAINT [pk_EmployeeID]	PRIMARY KEY ([EmployeeID]),
	CONSTRAINT [uc_EmployeeEmail]	UNIQUE ([EmployeeEmail])
)
GO

-- Index Employee Table
print '' print '*** adding indexes to Employee table'
GO
CREATE INDEX ix_EmployeeEmail
	ON [dbo].[Employee] ([EmployeeEmail])
GO


-- Card Status Table
print '' print '*** creating Card Status table'
GO
CREATE TABLE [dbo].[CardStatus] (
    [CardStatusID]        [nvarchar](16)        	not null
	
    constraint [pk_CardStatusID]    Primary Key ([CardStatusID])
    )
GO


-- Pokemon Card Group Table
print '' print '*** creating PokemonCardGroup table'
GO
CREATE TABLE [dbo].[PokemonCardGroup] (
	[PokemonCardGroupID]	[int]	IDENTITY(100000, 1)		not null,
	[UserID]				[int]							not null,
	[PokemonCardGroupName]	[nvarchar](16)					not null,
	[PokemonCardGroupActive]		[bit]		DEFAULT 1			not null,
	
	CONSTRAINT [pk_PokemonCardGroupID]	PRIMARY KEY ([PokemonCardGroupID]),
	CONSTRAINT [fk_PokemonCardGroupUserID] FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID])
)
GO

-- Index Pokemon Card Group Table
print '' print '*** adding indexes to PokemonCardGroup table'
GO
CREATE INDEX ix_PokemonCardGroupUserID
	ON [dbo].[PokemonCardGroup] ([UserID])
GO


-- User Favorite Group Table
print '' print '*** creating FavoriteGroup table'
GO
CREATE TABLE [dbo].[FavoriteGroup] (
	[UserID]				[int]		not null,
	[PokemonCardGroupID]	[int]		not null,
	
	CONSTRAINT [fk_FavoriteGroupTableUserID] 				FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID]),
	CONSTRAINT [fk_FavoriteGroupTablePokemonCardGroupID] 	FOREIGN KEY ([PokemonCardGroupID]) REFERENCES [PokemonCardGroup]([PokemonCardGroupID]),
	CONSTRAINT [uc_FavoriteGroupTableUserID] 				UNIQUE ([UserID]),
	CONSTRAINT [uc_FavoriteGroupTablePokemonCardGroupID] 	UNIQUE ([PokemonCardGroupID])
)
GO

-- Index Favorite Group Table
print '' print '*** adding indexes to FavoriteGroup table'
GO
CREATE INDEX ix_FavoriteGroupUserID
	ON [dbo].[FavoriteGroup] ([UserID])
GO
CREATE INDEX ix_FavoriteGroupPokemonCardGroupID
	ON [dbo].[FavoriteGroup] ([PokemonCardGroupID])
GO


-- User Pokemon Card Table
print '' print '*** creating UserPokemonCard table'
GO
CREATE TABLE [dbo].[UserPokemonCard] (
	[UserPokemonCardID]			[int]	IDENTITY(100000,1)		not null,
	[UserID]					[int]							not null,
	[PokemonCardID]				[int]							not null,
	[CardStatusID]				[nvarchar](16)					not null,
	[UserPokemonCardDateAdded]	[datetime]	DEFAULT GETDATE()	not null,
	[UserPokemonCardActive]		[bit]		DEFAULT 1			not null
	
	CONSTRAINT [pk_UserPokemonCardID] 						PRIMARY KEY ([UserPokemonCardID]),
	CONSTRAINT [fk_UserPokemonCardUserID] 					FOREIGN KEY ([UserID]) REFERENCES [Users]([UserID]),
	CONSTRAINT [fk_UserPokemonCardPokemonCardID] 			FOREIGN KEY ([PokemonCardID]) REFERENCES [PokemonCard]([PokemonCardID]),
	CONSTRAINT [fk_UserPokemonCardCardStatusID] 			FOREIGN KEY ([CardStatusID]) REFERENCES [CardStatus]([CardStatusID])
)
GO

-- Index User Pokemon Card Table
print '' print '*** adding indexes to UserPokemonCard table'
GO
CREATE INDEX ix_UserPokemonCardUserID
	ON [dbo].[UserPokemonCard] ([UserID])
GO
CREATE INDEX ix_UserPokemonCardPokemonCardID
	ON [dbo].[UserPokemonCard] ([PokemonCardID])
GO
CREATE INDEX ix_UserPokemonCardCardStatusID
	ON [dbo].[UserPokemonCard] ([CardStatusID])
GO


-- Pokemon Card Group Card Table
print '' print '*** creating PokemonCardGroupCard table'
GO
CREATE TABLE [dbo].[PokemonCardGroupCard] (
	[PokemonCardGroupID]	[int]	not null,
	[UserPokemonCardID]		[int]	not null,
	
	CONSTRAINT [uc_PokemonCardGroupCard]	UNIQUE ([PokemonCardGroupID],[UserPokemonCardID]),
	CONSTRAINT [fk_PokemonCardGroupCardPokemonCardGroupID] FOREIGN KEY ([PokemonCardGroupID]) REFERENCES [PokemonCardGroup]([PokemonCardGroupID]),
	CONSTRAINT [fk_PokemonCardGroupCardUserPokemonCardID] FOREIGN KEY ([UserPokemonCardID]) REFERENCES [UserPokemonCard]([UserPokemonCardID])
)
GO

-- Index Pokemon Card Group Card Table
print '' print '*** adding indexes to Pokemon Card Group Card table'
GO
CREATE INDEX ix_PokemonCardGroupCardPokemonCardGroupID
	ON [dbo].[PokemonCardGroupCard] ([PokemonCardGroupID])
GO
CREATE INDEX ix_UserPokemonCardPokemonCardID
	ON [dbo].[PokemonCardGroupCard] ([UserPokemonCardID])
GO




-- Pokemon Card Type Table
print '' print '*** creating PokemonCardType table'
GO
CREATE TABLE [dbo].[PokemonCardType] (
	[TypeID]		[nvarchar](16)	not null,
	[PokemonCardID]	[int]			not null,
	
	CONSTRAINT [uc_PokemonCardType]	UNIQUE ([TypeID],[PokemonCardID]),
	CONSTRAINT [fk_PokemonCardTypeTypeID] 	FOREIGN KEY ([TypeID]) 		REFERENCES [Type]([TypeID]),
	CONSTRAINT [fk_PokemonCardTypePokemonCardID] FOREIGN KEY ([PokemonCardID])	REFERENCES [PokemonCard]([PokemonCardID])
)
GO

-- Index Pokemon Card Type Table
print '' print '*** adding indexes to Pokemon Card Type table'
GO
CREATE INDEX ix_PokemonCardTypeTypeID
	ON [dbo].[PokemonCardType] ([TypeID])
GO
CREATE INDEX ix_PokemonCardTypePokemonCardID
	ON [dbo].[PokemonCardType] ([PokemonCardID])
GO




-- Pokemon Card Pokemon Table
print '' print '*** creating PokemonCardPokemon table'
GO
CREATE TABLE [dbo].[PokemonCardPokemon] (
	[PokemonID]		[int]			not null,
	[PokemonCardID]	[int]			not null,
	
	CONSTRAINT [uc_PokemonCardPokemon]	UNIQUE ([PokemonID],[PokemonCardID]),
	CONSTRAINT [fk_PokemonCardPokemonPokemonID] 	FOREIGN KEY ([PokemonID]) 		REFERENCES [Pokemon]([PokemonID]),
	CONSTRAINT [fk_PokemonCardPokemonPokemonCardID] FOREIGN KEY ([PokemonCardID])	REFERENCES [PokemonCard]([PokemonCardID])
)
GO

-- Index Pokemon Card Type Table
print '' print '*** adding indexes to Pokemon Card Type table'
GO
CREATE INDEX ix_PokemonCardPokemonPokemonID
	ON [dbo].[PokemonCardPokemon] ([PokemonID])
GO
CREATE INDEX ix_PokemonCardPokemonPokemonCardID
	ON [dbo].[PokemonCardPokemon] ([PokemonCardID])
GO


-- Employee Role Table
print '' print '*** creating EmployeeRole table'
GO
CREATE TABLE [dbo].[EmployeeRole] (
	[EmployeeID]	[int]				not null,
	[RoleID]		[nvarchar](16)		not null,
	
	CONSTRAINT [uc_EmployeeRole]	UNIQUE ([EmployeeID],[RoleID]),
	CONSTRAINT [fk_EmployeeRoleEmployeeID] 	FOREIGN KEY ([EmployeeID]) 	REFERENCES [Employee]([EmployeeID]),
	CONSTRAINT [fk_EmployeeRoleRoleID] 		FOREIGN KEY ([RoleID])		REFERENCES [Role]([RoleID])
)
GO

-- Index Pokemon Card Type Table
print '' print '*** adding indexes to EmployeeRole table'
GO
CREATE INDEX ix_EmployeeRoleRoleID
	ON [dbo].[EmployeeRole] ([RoleID])
GO
CREATE INDEX ix_EmployeeRoleEmployeeID
	ON [dbo].[EmployeeRole] ([EmployeeID])
GO



-- Pokemon Card Ability Table
print '' print '*** creating PokemonCardAbility table'
GO
CREATE TABLE [dbo].[PokemonCardAbility] (
	[AbilityID]		[int]			not null,
	[PokemonCardID]	[int]			not null,
	
	CONSTRAINT [uc_PokemonCardAbility]	UNIQUE ([AbilityID],[PokemonCardID]),
	CONSTRAINT [fk_PokemonCardAbilityAbilityID] 	FOREIGN KEY ([AbilityID]) 		REFERENCES [Ability]([AbilityID]),
	CONSTRAINT [fk_PokemonCardAbilityPokemonCardID] FOREIGN KEY ([PokemonCardID])	REFERENCES [PokemonCard]([PokemonCardID])
)
GO

-- Index Pokemon Card Type Table
print '' print '*** adding indexes to Pokemon Card Type table'
GO
CREATE INDEX ix_PokemonCardAbilityAbilityID
	ON [dbo].[PokemonCardAbility] ([AbilityID])
GO
CREATE INDEX ix_PokemonCardAbilityPokemonCardID
	ON [dbo].[PokemonCardAbility] ([PokemonCardID])
GO