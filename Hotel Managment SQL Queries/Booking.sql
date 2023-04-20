create table BookingTable
(
	BookingId int IDENTITY(1,1) not null primary key,
	Price int not null,
	BookingDate Date not null,
	StartDate Date not null,
	EndDate Date not null,
	UserId int not null, 
    CONSTRAINT fk_UserId3 FOREIGN KEY (UserId) REFERENCES Users(UserId),
	roomId int not null, 
    CONSTRAINT fk_RoomId3 FOREIGN KEY (roomId) REFERENCES Rooms(RoomId),
	
)

GO

Create Procedure AddBooking
(
	
	@UserId int,
	@RoomId int,
	@StartDate Date,
	@EndDate Date
	
) AS

BEGIN

declare @Price AS int;

set @Price = (select discountPrice from Rooms where roomId = @RoomId);

	
		If(Exists(Select * from Rooms where roomId = @RoomId))
		BEGIN
			If(Exists (Select * from Users where UserId = @UserId))
            BEGIN
				BEGIN Transaction;
						Insert Into BookingTable (Price, BookingDate, UserId,StartDate,EndDate,roomId)
						Values(@Price, GETDATE(), @UserId,@StartDate,@EndDate,@RoomId);
						select * from BookingTable;
                        COMMIT Transaction ;
			END
			ELSE
				BEGIN
				Select 3;
				END 
		END
		ELSE
		BEGIN
			Select 2;
		END
	END



GO

drop procedure AddBooking



GO


CREATE PROCEDURE AddBooking
(
	@UserId int,
	@RoomId int,
	@StartDate Date,
	@EndDate Date
) AS
BEGIN
	declare @Price AS int;
	declare @AlreadyBookedStartDate AS Date;
	declare @AlreadyBookedEndDate AS Date;

	set @Price = (select discountPrice from Rooms where roomId = @RoomId);
	
	If(Exists (Select StartDate from BookingTable where roomId = @RoomId))
	BEGIN
		SELECT TOP 1 @AlreadyBookedStartDate=StartDate
FROM BookingTable order by StartDate desc
	END
	
	If(Exists (Select EndDate from BookingTable where roomId = @RoomId))
	BEGIN
		SELECT TOP 1 @AlreadyBookedEndDate=EndDate
FROM BookingTable order by EndDate desc
	END
	
	IF((@AlreadyBookedStartDate IS NOT NULL) AND (@AlreadyBookedEndDate IS NOT NULL))
	BEGIN
		IF((@EndDate<@AlreadyBookedStartDate) OR (@StartDate>@AlreadyBookedEndDate))
		BEGIN
			If(Exists(Select * from Rooms where roomId = @RoomId))
			BEGIN
				If(Exists (Select * from Users where UserId = @UserId))
				BEGIN
					BEGIN Transaction;
						Insert Into BookingTable (Price, BookingDate, StartDate,EndDate, UserId, roomId)
						Values(@Price, GETDATE(), @StartDate,@EndDate,@UserId, @RoomId);
						select * from BookingTable;
						COMMIT Transaction ;
				END
				ELSE
					BEGIN
						Select 3;
				END 
			END
			ELSE
			BEGIN
					Select 2;
			END

	END
	ELSE
	BEGIN
		select 2
	END
	END
	ELSE
	BEGIN
		If(Exists(Select * from Rooms where roomId = @RoomId))
			BEGIN
				If(Exists (Select * from Users where UserId = @UserId))
				BEGIN
					BEGIN Transaction;
						Insert Into BookingTable (Price, BookingDate, StartDate,EndDate, UserId, roomId)
						Values(@Price, GETDATE(), @StartDate,@EndDate,@UserId, @RoomId);
						select * from BookingTable;
						COMMIT Transaction ;
				END
				ELSE
					BEGIN
						Select 3;
				END 
			END
			ELSE
			BEGIN
					Select 2;
			END
	END
END



drop procedure AddBooking

GO

Create Procedure GetBookings
(
	@UserId int
) AS
BEGIN
		Select 
		O.BookingId, O.UserId, b.roomId,
		O.Price, O.BookingDate,O.StartDate,O.EndDate,
		b.roomNumber
		FROM Rooms b
		inner join BookingTable O on O.roomId = b.roomId 
		where 
			O.UserId = @UserId;
END

GO

Create Procedure DeleteBooking
(
		@bookingId int
) AS
BEGIN
	Delete from BookingTable where BookingId = @bookingId
END