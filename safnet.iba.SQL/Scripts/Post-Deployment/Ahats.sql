--Point Identifier	Latitude	Longitude


--select NEWID() --478E300F-564D-41C5-9BC2-8CA698421A7B

INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'478E300F-564D-41C5-9BC2-8CA698421A7B', N'AHATS', 45.09586873, -93.1664523, N'265f303c-f442-4c3b-a35c-0ec54aa65b70', NULL, N'ATS');
--ATS03	45.09076678	-93.15772191
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'7DB59D32-01BD-46C2-B17B-E9C5DFD7E08D', N'ATS03', 45.09076678, -93.15772191, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)
--ATS02	45.08427011	-93.15342409
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'81484655-8FB4-46E3-83DD-E8AA2245F26F', N'ATS02', 45.08427011, -93.15342409, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)
--ATS01	45.08244106	-93.15527549
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'4FBFD9DF-BCEC-4BB3-B18C-F5E32BD9E89D', N'ATS01', 45.08244106, -93.15527549, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)
--ATS14	45.08954912	-93.16627924
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'6A3BA4A9-7E85-4D32-A0EC-E1BA36BF1882', N'ATS14', 45.08954912, -93.16627924, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)

--ATS07	45.10546169	-93.17622968
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'55DA78A7-8734-4C8B-A7EB-28007BC1B251', N'ATS07', 45.10546169, -93.17622968, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)
--ATS06	45.1056289	-93.16199279
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'7E0C2D16-AF72-44F6-B950-93A96B3258E8', N'ATS06', 45.1056289, -93.16199279, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)
--ATS05	45.10188176	-93.15063427
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'FD25CF7F-7F30-48FC-8C0F-FC8F28D5CB53', N'ATS05', 45.10188176, -93.15063427, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)
--ATS11	45.09586873	-93.1664523
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'2C9238D1-E378-4BBE-80ED-DB51DCF5BF5D', N'ATS11', 45.09586873, -93.1664523, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)
--ATS13	45.0924343	-93.16680334
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'580A2C92-A654-4A1F-9DC4-8BD39A801C31', N'ATS13', 45.0924343, -93.16680334, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)
--ATS12	45.09729863	-93.16284605
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'0317E0B9-5DA3-4B06-B64A-41B41DC6B533', N'ATS12', 45.09729863, -93.16284605, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)
--ATS04	45.09887049	-93.1577786
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'B686C0A8-3405-44D2-9172-84CBE643AC92', N'ATS04', 45.09887049, -93.1577786, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)
--ATS08	45.10309944	-93.16391138
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'A89FBD8B-5CCC-48B8-B34B-87B50F8436C4', N'ATS08', 45.10309944, -93.16391138, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)
--ATS09	45.10094797	-93.16961348
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'F1BAE94D-F961-4B2B-AEB3-07DA8A2A90E8', N'ATS09', 45.10094797, -93.16961348, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)
--ATS10	45.09597711	-93.17138674
INSERT INTO [dbo].[Location] ([LocationId], [LocationName], [Latitude], [Longitude], [LocationTypeId], [ParentLocationId], [CodeName]) 
VALUES (N'3A696F94-4B48-40D7-BF06-FBDA75A067E6', N'ATS10', 45.09597711, -93.17138674, N'501888ac-26af-43bd-a47d-ec7d61d42a7d', N'478E300F-564D-41C5-9BC2-8CA698421A7B', NULL)