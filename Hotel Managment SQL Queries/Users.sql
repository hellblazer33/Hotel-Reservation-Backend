
CREATE TABLE Users (
    UserId int IDENTITY(1,1) PRIMARY KEY,
    Fullname varchar(255),
    Email varchar(255),
    Password varchar(255),
    MobileNumber varchar(255)
);

GO

-----------------------------------------------------------------------------------

CREATE PROCEDURE UserRegister(
@Fullname varchar(255),
@Email varchar(255),
@Password varchar(255),
@MobileNumber varchar(255)
) AS
BEGIN  
    INSERT INTO Users(Fullname,Email,Password,MobileNumber)
VALUES (@Fullname,@Email,@Password,@MobileNumber);   
END  
 
--------------------------------------------------------------------------------------------


GO
 
CREATE PROCEDURE UserLogin(
@Email varchar(255),
@Password varchar(255)
) AS
BEGIN  
    SELECT * FROM Users WHERE Email=@Email AND Password=@Password;    
END

GO
----------------------------------------------------------------------------------------------------

 
CREATE PROCEDURE UserForgotPassword(
@Email varchar(255)
) AS
BEGIN  
	UPDATE Users 
	SET 
		Password ='Null'
	WHERE 
		Email = @Email;
    SELECT * FROM Users WHERE Email=@Email;    
END 

GO

-------------------------------------------------------------------------------------------------------------


  
CREATE PROCEDURE UserResetPassword(
@Email varchar(255),
@Password varchar(255)
) AS
BEGIN  
	UPDATE Users 
	SET 
		Password = @Password
	WHERE 
		Email = @Email;
END &&  
DELIMITER ;