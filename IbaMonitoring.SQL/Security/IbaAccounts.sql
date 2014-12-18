CREATE ROLE [IbaAccounts]
    AUTHORIZATION [iba_web];


GO
ALTER ROLE [IbaAccounts] ADD MEMBER [iba_web];

