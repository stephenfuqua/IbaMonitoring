/*
This script was created by Visual Studio on 4/10/2010 at 7:48 PM.
Run this script on [qantaqa\sqlexpress.IbaUnitTest] to make it the same as [QANTAQA\sqlexpress.IBA].
This script performs its actions in the following order:
1. Disable foreign-key constraints.
2. Perform DELETE commands. 
3. Perform UPDATE commands.
4. Perform INSERT commands.
5. Re-enable foreign-key constraints.
Please back up your target database before running this script.
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
/*Pointer used for text / image updates. This might not be needed, but is declared here just in case*/
DECLARE @pv binary(16)
BEGIN TRANSACTION
ALTER TABLE [dbo].[Location] DROP CONSTRAINT [FK_Location__Lookup]
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'0046ffde-e465-4241-8307-84e892f42556', N'MHA11', 44.905550, -93.196250, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'9ae2fc4a-c09b-4882-8be7-ce2385d6525a', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'006a5efc-5e6f-4bfb-a3e7-bd32edf5f635', N'LSL06', 44.758610, -92.988400, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'd07b9a86-6776-464a-a748-604b0efc432f', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'0496690f-eddd-4364-b00a-9a4c0a1dedc7', N'MHA10', 44.907550, -93.199660, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'9ae2fc4a-c09b-4882-8be7-ce2385d6525a', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'07776b5f-5f8a-45af-80dc-bb59f3733460', N'BCW12', 44.937580, -93.018040, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'0b98b506-534b-4e1f-b0a6-439f0a07c563', N'CF04', 44.899400, -93.152150, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'671f5340-13d1-4eff-a5a3-573fe524326e', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'0d89a7b1-ddc9-4bbe-9547-4f58eb56099a', N'GC01', 44.789710, -92.955670, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'7770787c-7ba8-46e4-bf08-f8bc34ead934', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'0e6fe691-99f0-48d2-a45e-95ba3310ca9d', N'GC08', 44.796490, -92.960920, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'7770787c-7ba8-46e4-bf08-f8bc34ead934', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'0fffc446-d0f9-42f3-a811-668eeebc7b5f', N'LD08', 44.920590, -93.121370, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'843ebc01-6888-4085-91b4-f02b13433805', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'1361b16b-94a1-4ac3-8064-4df15d16b6a1', N'CF11', 44.900780, -93.160040, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'671f5340-13d1-4eff-a5a3-573fe524326e', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'14160e8f-8bd8-451e-befc-7e20abd2ce18', N'BCE12', 44.937660, -93.000170, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'1545d98e-3cde-4a76-947f-e99e38473eca', N'BCW04', 44.935200, -93.020740, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'16f8aeaa-ce4a-4c95-84d5-55b37eb2befc', N'SB04', 44.769590, -92.928690, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'e07ed0e1-23f3-4fe7-af47-78e43ea23988', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'17657cf1-6338-4dca-b220-3aebab826277', N'BCW10', 44.934190, -93.015910, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'176f8c0f-58a2-4dc5-a16f-a701348a470a', N'HF01', 44.904450, -93.189680, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'1f5079dc-8071-47a8-98ac-e0e761612cae', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'18cb8d56-eba4-4d18-a3cd-4299a91a7770', N'MHA08', 44.910140, -93.204890, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'9ae2fc4a-c09b-4882-8be7-ce2385d6525a', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'1945474d-f4af-4fbc-a7dc-d3ebbf7014e2', N'CF07', 44.904590, -93.144220, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'671f5340-13d1-4eff-a5a3-573fe524326e', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'1a9e59c1-e407-4500-9850-a7b7df81f067', N'BCE10', 44.937600, -92.995660, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'1c4ed15a-2e9e-4fb2-9e93-b22dc74d2a8d', N'SB06', 44.766010, -92.933010, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'e07ed0e1-23f3-4fe7-af47-78e43ea23988', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'1d13b49e-c291-422f-bec9-253d630c3c59', N'LSL08', 44.759490, -92.984610, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'd07b9a86-6776-464a-a748-604b0efc432f', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'1f5079dc-8071-47a8-98ac-e0e761612cae', N'Hidden Falls', 44.909550, -93.199450, N'265f303c-f442-4c3b-a35c-0ec54aa65b70', NULL, N'HF')
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'1f9b4404-443e-45b1-867b-f3ade9a582e3', N'BCE03', 44.945830, -93.003970, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'2564739b-5058-4a0e-8c3b-f579c1a6beb1', N'BCE07', 44.940330, -92.996060, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'2619834e-a58d-4c1f-9a52-41a90b9a17fe', N'MPG08', 44.937260, -93.202930, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4fbdebae-9d08-4463-a210-37f5dd37874c', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'26ca4989-de47-4363-b5ca-c4bab381561c', N'BCW09', 44.934620, -93.012630, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'281045c8-d21a-48fd-b7fe-85190caf2c88', N'CF06', 44.902530, -93.143490, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'671f5340-13d1-4eff-a5a3-573fe524326e', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'2c36733f-2144-44f8-94ae-54e5c4e8db20', N'MHA09', 44.908920, -93.201630, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'9ae2fc4a-c09b-4882-8be7-ce2385d6525a', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'2d07bf92-622e-4aab-96a9-c4cc4a7ab4d7', N'MPG11', 44.928660, -93.202350, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4fbdebae-9d08-4463-a210-37f5dd37874c', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'30676374-792a-43ac-b3f0-075153fac5fc', N'LD03', 44.910840, -93.131230, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'843ebc01-6888-4085-91b4-f02b13433805', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'30c55512-930b-43e3-b74a-6f546d2c310c', N'BCE02', 44.947350, -93.001080, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'3275e104-7667-4caa-b540-ca43a7eaf41d', N'GC03', 44.787950, -92.957750, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'7770787c-7ba8-46e4-bf08-f8bc34ead934', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'379b6463-18fb-4014-9646-3bb59fd30a80', N'CF13', 44.898590, -93.165340, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'671f5340-13d1-4eff-a5a3-573fe524326e', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'38613745-6036-4487-baf6-387aa01c6e1a', N'CF01', 44.896400, -93.164540, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'671f5340-13d1-4eff-a5a3-573fe524326e', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'3d733fba-2169-43ac-961d-17ba901dfc06', N'LSL02', 44.754860, -92.983960, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'd07b9a86-6776-464a-a748-604b0efc432f', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'3eccb6fa-c6df-484e-a60c-4fd145760573', N'LSL03', 44.756930, -92.986350, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'd07b9a86-6776-464a-a748-604b0efc432f', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'3ece30b4-2392-4d10-b3c2-8887f7f324fa', N'HF08', 44.895960, -93.183020, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'1f5079dc-8071-47a8-98ac-e0e761612cae', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'41330609-479a-4cb2-87ce-51e7c575ef60', N'LD06', 44.918260, -93.126680, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'843ebc01-6888-4085-91b4-f02b13433805', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'419739ec-a3c0-40b1-bfa7-a0dd20a44950', N'BCE04', 44.943240, -93.004100, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'42ab6847-eaac-4bd1-ae34-e8f5f13a7558', N'MHA04', 44.910800, -93.204770, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'9ae2fc4a-c09b-4882-8be7-ce2385d6525a', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'4805691e-3331-4360-b0ad-2f3179cf6434', N'LD02', 44.912980, -93.129620, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'843ebc01-6888-4085-91b4-f02b13433805', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'48c255cc-e5e5-4b37-91d2-f1224b0e5e89', N'GC07', 44.793300, -92.959910, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'7770787c-7ba8-46e4-bf08-f8bc34ead934', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'4ae5fd62-d43c-4ecd-a144-6074a30055e4', N'SB01', 44.768320, -92.935060, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'e07ed0e1-23f3-4fe7-af47-78e43ea23988', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'4af590bc-3373-4250-94cd-97231ec2e5be', N'LSL10', 44.755880, -92.980780, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'd07b9a86-6776-464a-a748-604b0efc432f', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'4b47713f-874b-4a93-bbf1-b3511235dadf', N'LD04', 44.912130, -93.128140, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'843ebc01-6888-4085-91b4-f02b13433805', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'4be9a454-568c-4d4d-937c-36f2ee69125e', N'CF05', 44.901240, -93.147620, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'671f5340-13d1-4eff-a5a3-573fe524326e', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', N'Battle Creek East Park', 44.947350, -93.004100, N'265f303c-f442-4c3b-a35c-0ec54aa65b70', NULL, N'BCE')
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'4fbdebae-9d08-4463-a210-37f5dd37874c', N'Mississippi River Gorge', 44.957270, -93.217240, N'265f303c-f442-4c3b-a35c-0ec54aa65b70', NULL, N'MPG')
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'559e2e05-300a-42d3-a211-69cfe98479e8', N'MPG03', 44.952450, -93.209300, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4fbdebae-9d08-4463-a210-37f5dd37874c', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'57d3b6df-4da1-427a-bfec-2b07fe2ca2ae', N'BCE05', 44.942090, -93.000000, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'587f30f7-9e8f-45bb-a9a1-db730a45b659', N'BCE11', 44.935330, -92.999610, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'58908029-5ae2-4007-bc36-2abaccefa072', N'LD05', 44.914770, -93.126660, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'843ebc01-6888-4085-91b4-f02b13433805', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'5a4813ed-ba85-4b52-bb1c-06ca8b2d66dd', N'BCW14', 44.943550, -93.013470, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'5a63aee1-f278-48cd-bc14-737bab8737a5', N'SB10', 44.758420, -92.933940, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'e07ed0e1-23f3-4fe7-af47-78e43ea23988', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'5adebd6e-c9ae-4860-b57f-daa32e5007d3', N'BCW11', 44.935340, -93.018380, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'5ae21f2d-1e07-4f02-8827-096e1e9e54f2', N'CF09', 44.903450, -93.147740, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'671f5340-13d1-4eff-a5a3-573fe524326e', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'5aea676f-f1f5-4d97-979a-c78fc479f93c', N'SB08', 44.762280, -92.938220, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'e07ed0e1-23f3-4fe7-af47-78e43ea23988', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'5b1f483f-5620-4dd2-ac6d-184c1c97542d', N'MPG04', 44.950920, -93.206900, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4fbdebae-9d08-4463-a210-37f5dd37874c', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'5f46d84b-bf4f-426c-aa18-12955a6e63c6', N'MPG06', 44.942150, -93.202780, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4fbdebae-9d08-4463-a210-37f5dd37874c', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'649cfb33-a7be-4cef-81ff-86830057a264', N'LD12', 44.919810, -93.114000, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'843ebc01-6888-4085-91b4-f02b13433805', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'659f8058-e78e-4f8e-9342-d3a3067c2ca4', N'SB03', 44.771140, -92.925310, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'e07ed0e1-23f3-4fe7-af47-78e43ea23988', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'65b307fc-44eb-4924-b159-aa2b5ae63e81', N'CF08', 44.906070, -93.146070, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'671f5340-13d1-4eff-a5a3-573fe524326e', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'671f5340-13d1-4eff-a5a3-573fe524326e', N'Crosby Farm Park', 44.906070, -93.165340, N'265f303c-f442-4c3b-a35c-0ec54aa65b70', NULL, N'CF')
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'6a5803f2-cbff-4149-8f6b-d3b26e152b72', N'LSL09', 44.757190, -92.983090, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'd07b9a86-6776-464a-a748-604b0efc432f', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'6b19cca0-2bae-48bc-9b55-3cfa06876f88', N'BCE06', 44.939870, -93.000500, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'6ed6e461-39f2-4951-8c1a-f42d4cb632f0', N'LD01', 44.915370, -93.130360, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'843ebc01-6888-4085-91b4-f02b13433805', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'6ed80188-7a9f-41c8-bba9-8c14c8146dca', N'SB07', 44.766700, -92.937160, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'e07ed0e1-23f3-4fe7-af47-78e43ea23988', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'6f9ca1ec-0c49-4f14-b7e8-2fb1f84c1957', N'SB02', 44.769380, -92.932070, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'e07ed0e1-23f3-4fe7-af47-78e43ea23988', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'74edd68b-e288-4d4b-9be7-a04360ce0180', N'GC10', 44.797000, -92.964550, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'7770787c-7ba8-46e4-bf08-f8bc34ead934', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'75e440cd-5904-42ab-b856-e4bd0dad2b53', N'BCW08', 44.936940, -93.012860, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'7770787c-7ba8-46e4-bf08-f8bc34ead934', N'Gray Cloud SNA', 44.798870, -92.964550, N'265f303c-f442-4c3b-a35c-0ec54aa65b70', NULL, N'GC')
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'7ca5abe5-d154-4092-ac5a-017d78dbdda7', N'BCE09', 44.938800, -92.993230, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'80a97750-48c8-4f83-b51c-652aa629e1c5', N'BCW03', 44.933040, -93.022320, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'81a7f493-b41b-4397-9100-65cb6381fa85', N'BCW01', 44.936460, -93.025810, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'843ebc01-6888-4085-91b4-f02b13433805', N'Lilydale Park', 44.924950, -93.131230, N'265f303c-f442-4c3b-a35c-0ec54aa65b70', NULL, N'LD')
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'88b5b3f3-092f-422d-9447-edab6eba4675', N'BCW07', 44.938260, -93.025470, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'89451935-c2ff-4e02-96b8-1dff135c51a1', N'GC11', 44.795450, -92.958270, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'7770787c-7ba8-46e4-bf08-f8bc34ead934', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'8e70258e-f1a4-4800-aca2-c24d6644cb54', N'BCW06', 44.939510, -93.022740, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'8f2af3b8-8778-4ff0-b82a-8fb8f94081a5', N'MHA02', 44.916770, -93.210700, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'9ae2fc4a-c09b-4882-8be7-ce2385d6525a', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'9355b989-d413-46db-b721-9913fe962982', N'LD07', 44.920120, -93.124770, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'843ebc01-6888-4085-91b4-f02b13433805', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'936184cc-61f5-44a9-8081-2e3a4f3742f9', N'LSL01', 44.752330, -92.984570, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'd07b9a86-6776-464a-a748-604b0efc432f', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'959bece3-b505-4419-833e-db42b3e87d59', N'MPG01', 44.957270, -93.217240, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4fbdebae-9d08-4463-a210-37f5dd37874c', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'96876116-b0d7-4540-9693-c02862280580', N'MHA03', 44.918140, -93.207920, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'9ae2fc4a-c09b-4882-8be7-ce2385d6525a', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'97d36e39-b65a-412f-9458-37234687e205', N'MPG09', 44.923850, -93.203530, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4fbdebae-9d08-4463-a210-37f5dd37874c', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'996d4408-7560-40a5-95d0-fdebc6c47974', N'HF04', 44.909550, -93.199450, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'1f5079dc-8071-47a8-98ac-e0e761612cae', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'9ac8e3b7-d959-443c-8272-4a713a09cde2', N'MHA07', 44.912180, -93.206360, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'9ae2fc4a-c09b-4882-8be7-ce2385d6525a', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'9ae2fc4a-c09b-4882-8be7-ce2385d6525a', N'Minnehaha Park', 44.918140, -93.210700, N'265f303c-f442-4c3b-a35c-0ec54aa65b70', NULL, N'MHA')
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'9ce34b29-cbb4-4c98-a595-bfc7a7e16e70', N'HF09', 44.897330, -93.185650, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'1f5079dc-8071-47a8-98ac-e0e761612cae', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'a756e954-9c8d-4acd-8dc5-b1eec3908084', N'CF03', 44.899590, -93.155480, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'671f5340-13d1-4eff-a5a3-573fe524326e', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'ad9fcf27-16b9-4500-8621-da022e272b0e', N'MPG07', 44.939690, -93.202740, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4fbdebae-9d08-4463-a210-37f5dd37874c', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'ae835b44-9692-42fc-b7b7-7846bb99a87d', N'BCE08', 44.939130, -92.989850, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'b34d2eef-331f-4fb2-91bf-bc4a7f9cecc9', N'SB05', 44.766270, -92.928630, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'e07ed0e1-23f3-4fe7-af47-78e43ea23988', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'b38a0621-50ae-4c62-94d1-872f8e099ed7', N'GC09', 44.798870, -92.963710, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'7770787c-7ba8-46e4-bf08-f8bc34ead934', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'b4c3df67-ef6b-4829-a5e8-9270c93b76fb', N'BCW02', 44.934510, -93.024350, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'b7fe67ee-a5b4-4f21-8954-604a5bc9f073', N'CF12', 44.899110, -93.162450, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'671f5340-13d1-4eff-a5a3-573fe524326e', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'b95e466a-4806-473c-9e68-0231a1eb2378', N'LSL05', 44.756120, -92.989330, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'd07b9a86-6776-464a-a748-604b0efc432f', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'bd00dcec-e4e7-4fc2-b187-e1c6290c7643', N'LD11', 44.923240, -93.111690, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'843ebc01-6888-4085-91b4-f02b13433805', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'c3cad9bd-3968-4e7b-b102-62dc5902303b', N'BCE14', 44.938320, -93.003600, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'c4c050ce-f9c2-4bb1-a61e-4c3343f94f31', N'LD09', 44.922080, -93.118410, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'843ebc01-6888-4085-91b4-f02b13433805', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'c4ecde54-1ad1-46d4-a31a-955d0fb3d2fb', N'MHA06', 44.915330, -93.206550, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'9ae2fc4a-c09b-4882-8be7-ce2385d6525a', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'ccdf6085-e8a6-4abf-981c-08c61f30f6e8', N'BCW05', 44.937050, -93.022930, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'cd9f33d0-f69b-400a-87ed-d2c2338bab08', N'CF10', 44.902070, -93.157510, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'671f5340-13d1-4eff-a5a3-573fe524326e', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'cf607f69-9340-4b03-baca-78168232b9b3', N'HF10', 44.897550, -93.188740, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'1f5079dc-8071-47a8-98ac-e0e761612cae', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'd054c8aa-508a-4b90-9fa3-4529248f004b', N'GC04', 44.788280, -92.960960, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'7770787c-7ba8-46e4-bf08-f8bc34ead934', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'd07b9a86-6776-464a-a748-604b0efc432f', N'Lower Spring Lake Park', 44.760760, -92.989330, N'265f303c-f442-4c3b-a35c-0ec54aa65b70', NULL, N'LSL')
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'd1ab2c93-af34-49d7-ad9d-1b35ad72a6b1', N'HF06', 44.909320, -93.192220, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'1f5079dc-8071-47a8-98ac-e0e761612cae', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'd2c40293-f3d3-45fa-b68c-5fc14b92ef80', N'HF03', 44.907830, -93.197030, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'1f5079dc-8071-47a8-98ac-e0e761612cae', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'd495e46d-cb3f-4dc8-87a4-63e86fc8ea36', N'MPG10', 44.926110, -93.202770, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4fbdebae-9d08-4463-a210-37f5dd37874c', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'd5903f61-2262-4ed3-ab1c-e3096f982353', N'LD10', 44.924950, -93.109920, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'843ebc01-6888-4085-91b4-f02b13433805', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'd90c0c03-fc8a-460e-8170-b185bdd8d43f', N'BCE13', 44.936150, -93.003010, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'd9ed9f8b-6e72-4670-8191-809e1ad5eb8e', N'SB11', 44.760450, -92.932890, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'e07ed0e1-23f3-4fe7-af47-78e43ea23988', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'ddc828a9-b432-430c-9e8d-95c68af6b7ea', N'GC02', 44.790380, -92.958270, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'7770787c-7ba8-46e4-bf08-f8bc34ead934', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'def4a4ac-5855-4a74-9781-a95744b80af8', N'BCE01', 44.944340, -93.001110, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4f34dc94-add2-4eab-b6d1-ef5344fd9f19', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'e0792270-3b3a-4398-be86-c89e2206079a', N'HF05', 44.908340, -93.194750, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'1f5079dc-8071-47a8-98ac-e0e761612cae', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'e07ed0e1-23f3-4fe7-af47-78e43ea23988', N'Schaar''s Bluff Park', 44.771140, -92.938220, N'265f303c-f442-4c3b-a35c-0ec54aa65b70', NULL, N'SB')
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'e142d291-7d2b-40ab-9ddd-f98ee1cd070b', N'SB09', 44.762310, -92.934650, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'e07ed0e1-23f3-4fe7-af47-78e43ea23988', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'e41a284e-00d6-4e96-b64f-5c85fe257f71', N'MHA01', 44.914060, -93.209380, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'9ae2fc4a-c09b-4882-8be7-ce2385d6525a', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'e4460600-71af-4ee1-b1a4-7177524d468a', N'SB12', 44.764470, -92.936740, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'e07ed0e1-23f3-4fe7-af47-78e43ea23988', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'e71568c1-7b4d-44a3-9d93-2b9685655c21', N'LSL04', 44.754510, -92.987230, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'd07b9a86-6776-464a-a748-604b0efc432f', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'e85d0c1c-af32-4f66-abaf-684e83de05d9', N'HF11', 44.899920, -93.189950, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'1f5079dc-8071-47a8-98ac-e0e761612cae', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'f0d65bd0-4dd3-45e4-9313-437d57950804', N'GC12', 44.792300, -92.956860, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'7770787c-7ba8-46e4-bf08-f8bc34ead934', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'f20b82e5-a410-4523-8ecb-449021330929', N'GC05', 44.790170, -92.962980, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'7770787c-7ba8-46e4-bf08-f8bc34ead934', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'f25b3936-b3fc-4999-bdc2-654e85a2a2de', N'MPG05', 44.945060, -93.202760, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4fbdebae-9d08-4463-a210-37f5dd37874c', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'f48f280d-31d4-4caa-bbc1-5c605bb989b0', N'MHA05', 44.916920, -93.204090, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'9ae2fc4a-c09b-4882-8be7-ce2385d6525a', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', N'Battle Creek West Park', 44.943550, -93.025810, N'265f303c-f442-4c3b-a35c-0ec54aa65b70', NULL, N'BCW')
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'f6f93e20-2da9-452d-b81e-780af39fe8b0', N'HF07', 44.906680, -93.190610, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'1f5079dc-8071-47a8-98ac-e0e761612cae', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'f8b09999-b0bf-4c8b-b411-5e0920197834', N'MPG02', 44.954190, -93.212100, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'4fbdebae-9d08-4463-a210-37f5dd37874c', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'f9d31d93-b7b2-4929-b6e9-da8442bf486a', N'HF02', 44.906540, -93.194010, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'1f5079dc-8071-47a8-98ac-e0e761612cae', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'fd21ecc3-e600-455f-a96a-e18579cf05ef', N'CF02', 44.897690, -93.159870, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'671f5340-13d1-4eff-a5a3-573fe524326e', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'fd95a9d9-eb79-436d-b742-4d62e55327b0', N'BCW13', 44.939280, -93.018170, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'f678fecc-50bd-4dbc-9403-cc1b73d046b3', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'fe28d9cf-2c6b-41ab-bb61-a2a0e4f00bf4', N'LSL07', 44.760760, -92.987270, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'd07b9a86-6776-464a-a748-604b0efc432f', NULL)
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'ff4c1766-52a2-4493-b15b-4b49878ce7ee', N'GC06', 44.792000, -92.962310, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'7770787c-7ba8-46e4-bf08-f8bc34ead934', NULL)
ALTER TABLE [dbo].[Location] ADD CONSTRAINT [FK_Location__Lookup] FOREIGN KEY ([LocationTypeId]) REFERENCES [dbo].[Lookup] ([LookupId])
COMMIT TRANSACTION;

--select NEWID()
--INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'55778686-31B0-4B50-87D0-DEFE5A264A66', N'Fake Park', 44.906070, -93.165340, N'265f303c-f442-4c3b-a35c-0ec54aa65b706', NULL, N'FK')
--INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'55778686-31B0-4B50-87D0-DEFE5A264A65', N'FK1', 44.906070, -93.165340, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'55778686-31B0-4B50-87D0-DEFE5A264A66',  N'FK1')
--INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'55778686-31B0-4B50-87D0-DEFE5A264A64', N'FK2', 44.906070, -93.165340, N'501888ac-26af-43bd-a47d-ec7d61d42a7d',N'55778686-31B0-4B50-87D0-DEFE5A264A66',  N'FK2')
--INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) VALUES (N'55778686-31B0-4B50-87D0-DEFE5A264A63', N'FK3', 44.906070, -93.165340, N'501888ac-26af-43bd-a47d-ec7d61d42a7d',N'55778686-31B0-4B50-87D0-DEFE5A264A66',  N'FK3')




/* Boundary Coordinates */
-- Crosby
WITH coords as (
	SELECT 44.898137 as Lat, -93.166396 as Lon, 1 as Seq UNION
	SELECT 44.895526 as Lat, -93.163384 as Lon, 2 as Seq UNION
	SELECT 44.898158 as Lat, -93.157350 as Lon, 3 as Seq UNION
	SELECT 44.898529 as Lat, -93.150566 as Lon, 4 as Seq UNION
	SELECT 44.905141 as Lat, -93.140353 as Lon, 5 as Seq UNION
	SELECT 44.909970 as Lat, -93.144555 as Lon, 6 as Seq 
)
INSERT INTO dbo.SiteBoundary (SiteId, Latitude, Longitude, VertexSequence)
SELECT '671F5340-13D1-4EFF-A5A3-573FE524326E', Lat, Lon, Seq FROM coords;

-- BCW
WITH coords as (
	SELECT 44.935616 as Lat, -93.029039 as Lon, 1 as Seq UNION
	SELECT 44.929373 as Lat, -93.021238 as Lon, 2 as Seq UNION
	SELECT 44.933403 as Lat, -93.009359 as Lon, 3 as Seq UNION
	SELECT 44.938821 as Lat, -93.009999 as Lon, 4 as Seq UNION
	SELECT 44.938600 as Lat, -93.017361 as Lon, 5 as Seq UNION
	SELECT 44.942512 as Lat, -93.023050 as Lon, 6 as Seq 
)
INSERT INTO dbo.SiteBoundary (SiteId, Latitude, Longitude, VertexSequence)
SELECT 'F678FECC-50BD-4DBC-9403-CC1B73D046B3', Lat, Lon, Seq FROM coords;

-- Lilydale
WITH coords as (
	SELECT 44.905185 as Lat, -93.135293 as Lon, 1 as Seq UNION
	SELECT 44.905079 as Lat, -93.135379 as Lon, 2 as Seq UNION
	SELECT 44.922136 as Lat, -93.107378 as Lon, 3 as Seq UNION
	SELECT 44.926169 as Lat, -93.106958 as Lon, 4 as Seq UNION
	SELECT 44.927253 as Lat, -93.105053 as Lon, 5 as Seq UNION
	SELECT 44.930286 as Lat, -93.104344 as Lon, 6 as Seq UNION
	SELECT 44.924711 as Lat, -93.110482 as Lon, 7 as Seq UNION
	SELECT 44.921069 as Lat, -93.123981 as Lon, 8 as Seq UNION
	SELECT 44.917396 as Lat, -93.129519 as Lon, 9 as Seq 
)
INSERT INTO dbo.SiteBoundary (SiteId, Latitude, Longitude, VertexSequence)
SELECT '843EBC01-6888-4085-91B4-F02B13433805', Lat, Lon, Seq FROM coords;

-- Hidden Falls
WITH coords as (
	SELECT 44.912655 as Lat, -93.197621 as Lon, 1 as Seq UNION
	SELECT 44.910747 as Lat, -93.200218 as Lon, 2 as Seq UNION
	SELECT 44.907967 as Lat, -93.198428 as Lon, 3 as Seq UNION
	SELECT 44.906633 as Lat, -93.194874 as Lon, 4 as Seq UNION
	SELECT 44.902680 as Lat, -93.190139 as Lon, 5 as Seq UNION
	SELECT 44.895719 as Lat, -93.187397 as Lon, 6 as Seq UNION
	SELECT 44.894585 as Lat, -93.181449 as Lon, 7 as Seq UNION
	SELECT 44.896308 as Lat, -93.180216 as Lon, 8 as Seq UNION
	SELECT 44.894746 as Lat, -93.185235 as Lon, 9 as Seq UNION
	SELECT 44.902253 as Lat, -93.189154 as Lon, 10 as Seq 
)
INSERT INTO dbo.SiteBoundary (SiteId, Latitude, Longitude, VertexSequence)
SELECT '1F5079DC-8071-47A8-98AC-E0E761612CAE', Lat, Lon, Seq FROM coords;

-- BCE
WITH coords as (
	SELECT 44.933150 as Lat, -93.004468 as Lon, 1 as Seq UNION
	SELECT 44.933150 as Lat, -92.994785 as Lon, 2 as Seq UNION
	SELECT 44.936317 as Lat, -92.994785 as Lon, 3 as Seq UNION
	SELECT 44.936317 as Lat, -92.988622 as Lon, 4 as Seq UNION
	SELECT 44.935276 as Lat, -92.988622 as Lon, 5 as Seq UNION
	SELECT 44.935276 as Lat, -92.984786 as Lon, 6 as Seq UNION
	SELECT 44.941250 as Lat, -92.984786 as Lon, 7 as Seq UNION
	SELECT 44.933150 as Lat, -92.984786 as Lon, 8 as Seq UNION
	SELECT 44.933150 as Lat, -92.998511 as Lon, 9 as Seq UNION
	SELECT 44.944336 as Lat, -92.999533 as Lon, 10 as Seq UNION
	SELECT 44.948097 as Lat, -93.00450 as Lon, 11 as Seq UNION
	SELECT 44.948097 as Lat, -93.004468 as Lon, 12 as Seq
)
INSERT INTO dbo.SiteBoundary (SiteId, Latitude, Longitude, VertexSequence)
SELECT '4F34DC94-ADD2-4EAB-B6D1-EF5344FD9F19', Lat, Lon, Seq FROM coords;

--4FBDEBAE-9D08-4463-A210-37F5DD37874C	Mississippi River Gorge
--D07B9A86-6776-464A-A748-604B0EFC432F	Lower Spring Lake Park
--E07ED0E1-23F3-4FE7-AF47-78E43EA23988	Schaar's Bluff Park
--9AE2FC4A-C09B-4882-8BE7-CE2385D6525A	Minnehaha Park
--7770787C-7BA8-46E4-BF08-F8BC34EAD934	Gray Cloud SNA