CREATE PROCEDURE [dbo].[Person_Get]
	@Id UNIQUEIDENTIFIER = null,
	@OpenId VARCHAR(1000) = null,
	@All BIT = null
AS
	SET NOCOUNT ON;

	SELECT p.EmailAddress,
		p.FirstName,
		p.LastName,
		p.OpenId,
		p.PersonId,
		p.PhoneNumber,	
		p.Address1,
		p.Address2,
		p.City,
		p.State,
		p.ZipCode,
		p.Country,
		p.HasBeenTrained,
		p.HasClipboard,
		p.PersonRole,
		p.PersonStatus
	FROM dbo.Person p
	WHERE PersonId = @Id
		OR OpenId = @OpenId
		OR OpenId = SUBSTRING(@OpenId, 1, LEN(@OpenId) - 1) -- strip off trailing slash
		OR @All = 1
	ORDER BY p.LastName, p.FirstName;
RETURN 0

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[Person_Get] TO [IbaAccounts]
    AS [dbo];

