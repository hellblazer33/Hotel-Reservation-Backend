create Table Admins
(
	AdminId int IDENTITY(1,1) primary key not null,
	FullName varchar(255) not null,
	Email varchar(255) not null,
	Password varchar(255) not null,
	MobileNumber varchar(255) not null
);

GO

create Procedure LoginAdmin
(
	@Email varchar(255),
	@Password varchar(255)
) AS
BEGIN
	If(Exists(select * from Admins where Email = @Email and Password = @Password)) 
	BEGIN		
			select AdminId, FullName, Email, MobileNumber from Admins;
	END
	else 
	BEGIN 
		select 2;
	End 
END

GO

Insert into Admins(FullName,Email,Password,MobileNumber) 
values('Admin','pk110495@gmail.com', 'Pankaj@123', '9747388481');


select * from Admins