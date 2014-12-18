CREATE PROCEDURE [dbo].[Person_Save]
	@Id UNIQUEIDENTIFIER,
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@OpenId VARCHAR(1000),
	@EmailAddress VARCHAR(250),
	@PhoneNumber VARCHAR(15),	
	@Address1 varchar(50),
	@Address2 varchar (50),
	@City varchar(50),
	@State varchar(50),
	@ZipCode varchar(10),
	@Country varchar(50),
	@HasBeenTrained BIT,
	@HasClipboard BIT,
	@PersonStatus smallint,
	@PersonRole smallint

AS
	SET NOCOUNT ON;

	UPDATE dbo.Person SET EmailAddress = @EmailAddress,
								FirstName = @FirstName,
								LastName = @LastName,
								OpenId = @OpenId,
								PersonId = @Id,
								PhoneNumber = @PhoneNumber,								
								Address1=@Address1,
								Address2=@Address2,
								City=@City,
								[State]=@State,
								ZipCode=@ZipCode,
								Country=@Country,
								HasBeenTrained=@HasBeenTrained,
								HasClipboard=@HasClipboard,
								PersonStatus=@PersonStatus,
								[PersonRole]=@PersonRole
	WHERE PersonId = @Id;

	IF @@ROWCOUNT = 0
		INSERT INTO dbo.Person (PersonId, EmailAddress, FirstName, LastName, OpenId, PhoneNumber,Address1,Address2,City,[State],ZipCode,Country,HasBeenTrained,HasClipboard,PersonStatus,PersonRole)
		VALUES (@Id, @EmailAddress, @FirstName, @LastName, @OpenId, @PhoneNumber,@Address1,@Address2,@City,@State,@ZipCode,@Country,@HasBeenTrained,@HasClipboard,@PersonStatus,@PersonRole);		
RETURN 0

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Person_Save] TO [IbaAccounts]
    AS [dbo];

