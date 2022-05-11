CREATE TABLE dbo.ToDo (
    id uniqueidentifier primary key,
    title nvarchar(200) not null,
    completed bit not null
);
GO


CREATE PROCEDURE [dbo].[DeleteToDo]
    @Id nvarchar(100)
AS
    DECLARE @UID UNIQUEIDENTIFIER = TRY_CAST(@ID AS uniqueidentifier)
    IF @UId IS NOT NULL AND @Id != ''
    BEGIN
        DELETE FROM dbo.ToDo WHERE Id = @UID
    END
    ELSE
    BEGIN
        DELETE FROM dbo.ToDo WHERE @ID = ''
    END

    SELECT id, title, completed FROM dbo.ToDo
GO
