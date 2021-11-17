use dbo
go

create table Friends
(
	Id int primary key identity(1,1) not null,
	Name nvarchar(50) not null,
	City nvarchar(50) not null,
	PhoneNumber nvarchar(50) not null,	
)
go


--sp for create
create proc [dbo].[usp_Friends_Create]
	@Id int,
	@Name nvarchar(50),
	@City nvarchar(50),
	@PhoneNumber nvarchar(50)
		 
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

	select SCOPE_IDENTITY() InsertedId
end
go


--sp for GetAll
create proc [dbo].[usp_Friends_GetAll]
	@Id int = null,
	@Search nvarchar(50) = null,
	@OrderBy varchar(100) = 'name',
	@IsDescending bit = 0
as begin
	select 
		Id,
		Name,
		City,
		PhoneNumber
	from 
		Friends
	where
		Id = coalesce(@Id, Id)
		and
		(
			(@Search is null or @Search = '')
			or
			(
				@Search is not null
				and
				(
					Name like '%' + @Search + '%'
					or
					City like '%' + @Search + '%'
					
				)
			)
		)
	order by
		case when @OrderBy = 'name' and @IsDescending = 0 then Name end asc
		, case when @OrderBy = 'name' and @IsDescending = 1 then Name end desc
		, case when @OrderBy = 'city' and @IsDescending = 0 then city end asc
		, case when @OrderBy = 'city' and @IsDescending = 1 then city end desc
		
end
go

SET IDENTITY_INSERT [dbo].[Friends] ON 
GO
INSERT [dbo].[Friends] ([Id], [Name], [City], [PhoneNumber]) VALUES (2, N'John', N'Mumbai', N'123456')
GO
INSERT [dbo].[Friends] ([Id], [Name], [City], [PhoneNumber]) VALUES (3, N'Rock', N'Chennai', N'987456')
GO
INSERT [dbo].[Friends] ([Id], [Name], [City], [PhoneNumber]) VALUES (4, N'Abhi', N'Pune', N'4561789')
GO
SET IDENTITY_INSERT [dbo].[Friends] OFF
GO

--sp for Get
create proc [dbo].[usp_Friends_Get]
	@Id int
as begin
	select 
		Id,
		Name,
		City,
		PhoneNumber
	from 
		Friends
	where 
		Id = @Id
end
go

--sp for Update
create proc [dbo].[usp_Friends_Update]
	@Id int,
	@Name nvarchar(50),
	@City nvarchar(50),
	@PhoneNumber nvarchar(50)
as begin
	update 
		Friends
	set
		Name = @Name,
		City = @City,
		PhoneNumber = @PhoneNumber		
	where 
		Id=@Id
end
go

--sp for Delete
create proc [dbo].[usp_Friends_Delete]
	@Id int
as begin
	delete 
	from 
		Friends
	where 
		Id = @Id
end
go