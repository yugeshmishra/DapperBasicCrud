USE [dbo]
GO

/****** Object:  Table [dbo].[Friends]    Script Date: 16-11-2021 16:18:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Friends](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
 CONSTRAINT [PK_Friends] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


---sp create--
GO
ALTER procedure [dbo].[sp_Friends_Create]

@Name nvarchar(300),
@City nvarchar(300),
@PhoneNumber nvarchar(300)
as begin
insert into Friends
(
        Name,
		City,
		PhoneNumber
)
values
(
@Name,
@City,
@PhoneNumber
)
set @Id=(select SCOPE_IDENTITY ())
select 
Name=@Name,
City=@City,
PhoneNumber=@PhoneNumber
from Friends
where Id=@Id
end

--sp delete
GO
ALTER procedure [dbo].[sp_Friends_Delete]
@Id int
as begin
delete  from Friends
where      
 Id=@Id
end 

--sp get
GO
ALTER procedure [dbo].[sp_Friends_Get]
@Id int
as begin
select Id,
      Name,
      City,
      PhoneNumber
from Friends
where @Id=Id
end

--sp getall
GO
ALTER procedure [dbo].[sp_Friends_GetAll]
@Id int
as begin
select Id,
      Name,
      City,
      PhoneNumber
from Friends
where @Id=Id
end

--sp update
GO
ALTER procedure [dbo].[sp_Friends_Update]
@Id int,
@Name nvarchar(300),
@City nvarchar(300),
@PhoneNumber nvarchar(300)
as begin
update  Friends
set 
        Name=@Name,
		City=@City,
		PhoneNumber=@PhoneNumber
where Id=@Id
end 