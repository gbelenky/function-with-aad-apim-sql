CREATE TABLE dbo.ToDo (
    id uniqueidentifier primary key,
    [url] nvarchar(200) not null,
    completed bit not null
);
GO
