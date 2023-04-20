create table Rooms
(
	roomId int IDENTITY(1,1) PRIMARY KEY,
	roomNumber int,
    rating int,   
	totalRating int,
	discountPrice Decimal,
	originalPrice Decimal,
	description varchar(255) not null,
	
);

GO

-----------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE AddRoom(
	@roomNumber int,
    @rating int,   
	@totalRating int,
	@discountPrice Decimal,
	@originalPrice Decimal,
	@description varchar(255) 
) AS
BEGIN  
    INSERT INTO Rooms(roomNumber,rating,totalRating,discountPrice,originalPrice,description)
VALUES (@roomNumber,@rating,@totalRating,@discountPrice,@originalPrice,@description);   
END

GO
------------------------------------------------------------------------------------------------------------------------------------------


CREATE PROCEDURE UpdateRoom(
	@roomId int,
    @roomNumber int,
    @rating int,   
	@totalRating int,
	@discountPrice Decimal,
	@originalPrice Decimal,
	@description varchar(255)
	 
) AS
BEGIN
   Update Rooms set roomNumber = @roomNumber, 
					rating = @rating, 
					totalRating = @totalRating, 
					discountPrice= @discountPrice,
					originalPrice = @originalPrice,
					description = @description
					
				where 
					roomId = @roomId;
END


GO



---------------------------------------------------------------------------------------------------------------------------------------------------------



CREATE PROCEDURE DeleteRoom(
   @roomId int
) AS
BEGIN
	DELETE FROM Rooms WHERE roomId = @roomId;
END


GO

--------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE GetRoomByRoomId(
   @roomId int
) AS
BEGIN
	SELECT * FROM Rooms WHERE roomId = @roomId;
END


GO

---------------------------------------------------------------------------------------------------------------------------------------


CREATE PROCEDURE GetAllRooms
AS
BEGIN
	SELECT * FROM Rooms;
END