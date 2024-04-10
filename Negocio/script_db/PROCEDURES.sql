USE [CATALOGO_WEB_DB]
GO

create procedure [dbo].[insertarNuevo]
    @nombre VARCHAR(50),
    @apellido VARCHAR(50),
    @email VARCHAR(100),
    @pass VARCHAR(20),
    @admin BIT
	as

    INSERT INTO USERS (nombre, apellido, email, pass, admin)
    OUTPUT inserted.id
    VALUES (@nombre, @apellido, @email, @pass, @admin)

go

create procedure [dbo].[insertarNuevoFavorito]
    @idUser INT,
    @IdArticulo INT
AS
BEGIN
    INSERT INTO FAVORITOS (idUser, IdArticulo)
    VALUES (@idUser, @IdArticulo)
END

go

create procedure [dbo].[storedAltaArticulo] 
@Codigo varchar(10),
@Nombre varchar(50),
@Descripcion varchar(100),
@Precio money,
@idMarca int,
@idCategoria  int,
@ImagenUrl varchar(300)
as

INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria, ImagenUrl) 
VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @idMarca, @idCategoria, @ImagenUrl)

go

create procedure [dbo].[storedListar] as
Select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.ImagenUrl, A.Precio, A.IdMarca, A.IdCategoria, C.Descripcion AS CategoriaDescripcion, M.Descripcion AS MarcaDescripcion 
From ARTICULOS A, CATEGORIAS C, MARCAS M
Where A.IdMarca = M.Id And A.IdCategoria = C.Id;

go

create procedure [dbo].[storedListarFavoritos]
AS
BEGIN
    SELECT idUser, IdArticulo
    FROM Favoritos;
END

go

create procedure [dbo].[storedModificarArticulo]
    @Codigo varchar(10),
    @Nombre varchar(50),
    @Descripcion varchar(100),
    @Precio money,
    @idMarca int,
    @idCategoria int,
    @ImagenUrl varchar(300),
    @id int
AS
BEGIN
    UPDATE ARTICULOS 
    SET Codigo = @Codigo,
        Nombre = @Nombre,
        Descripcion = @Descripcion,
        Precio = @Precio,
        idMarca = @idMarca,
        idCategoria = @idCategoria,
        ImagenUrl = @ImagenUrl
    WHERE Id = @id
	end