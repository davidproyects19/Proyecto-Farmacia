use FARMACIABD
go

select * from Productos
go

select * from Clientes
go

-- CRUD DE PRODUCTOS
-- LISTAR PRODUCTO
CREATE OR ALTER PROCEDURE PA_PRODUCTOS
AS 
	SELECT P.id, P.nombre, P.descripcion, P.precio, P.cantidad, p.eli_pro
	FROM Productos P
	where p.eli_pro = 'No'
GO

EXECUTE PA_PRODUCTOS
GO

-- GRABAR/ACTUALIZAR PRODUCTO
CREATE OR ALTER PROCEDURE PA_GRABAR_PRODUCTO
@id INT, @nombre VARCHAR(100), @descripcion TEXT, @precio DECIMAL, @cantidad INT
AS
	MERGE Productos AS TARGET
	USING (SELECT @id,@nombre,@descripcion,@precio,
	@cantidad,'No') AS SOURCE(id, nombre, descripcion, precio, cantidad, eli_pro)
	ON TARGET.id = SOURCE.id
	WHEN MATCHED THEN
		UPDATE SET TARGET.nombre=SOURCE.nombre,
		           TARGET.descripcion=SOURCE.descripcion,
				   TARGET.precio=SOURCE.precio,
				   TARGET.cantidad=SOURCE.cantidad,
				   TARGET.eli_pro=SOURCE.eli_pro

	WHEN NOT MATCHED THEN
		INSERT VALUES(SOURCE.id,SOURCE.nombre,
		SOURCE.descripcion,SOURCE.precio,SOURCE.cantidad,
		SOURCE.eli_pro);
GO

-- ELIMINAR PRODUCTO
CREATE OR ALTER PROCEDURE PA_ELIMINAR_PRODUCTO
@id INT
AS
	UPDATE Productos
		SET eli_pro='Si'
	WHERE id = @id
GO
-- FIN DE PRODUCTOS


-- CRUD DE CLIENTES
-- LISTAR CLIENTE
CREATE OR ALTER PROCEDURE PA_CLIENTES
AS 
	SELECT C.id, C.nombre, C.direccion, C.telefono, C.eli_cli
	FROM Clientes C
	where c.eli_cli = 'No'
GO

EXECUTE PA_CLIENTES
GO

-- GRABAR/ACTUALIZAR CLIENTE
CREATE OR ALTER PROCEDURE PA_GRABAR_CLIENTE
@id INT, @nombre VARCHAR(100), @direccion VARCHAR(255), @telefono VARCHAR(20)
AS
	MERGE Clientes AS TARGET
	USING (SELECT @id, @nombre, @direccion, @telefono, 'No') 
	AS SOURCE(id, nombre, direccion, telefono, eli_cli)
	ON TARGET.id = SOURCE.id
	WHEN MATCHED THEN
		UPDATE SET TARGET.nombre=SOURCE.nombre,
		           TARGET.direccion=SOURCE.direccion,
				   TARGET.telefono=SOURCE.telefono,
				   TARGET.eli_cli=SOURCE.eli_cli

	WHEN NOT MATCHED THEN
		INSERT VALUES(SOURCE.id,SOURCE.nombre,
		SOURCE.direccion,SOURCE.telefono,SOURCE.eli_cli);
GO

-- ELIMINAR CLIENTE
CREATE OR ALTER PROCEDURE PA_ELIMINAR_CLIENTE
@id INT
AS
	UPDATE Clientes
		SET eli_cli='Si'
	WHERE id = @id
GO
--
CREATE OR ALTER PROCEDURE PA_PRODUCTOS_NOMBRE
    @letraNom CHAR(1)
AS 
BEGIN
    SELECT P.id, P.nombre, p.descripcion, p.precio, p.cantidad, P.eli_pro
    FROM Productos P
    WHERE P.eli_pro = 'NO' AND LEFT(P.nombre, 1) = @letraNom
END
GO
EXEC PA_PRODUCTOS_NOMBRE @letraNom = 'A'
GO
--
CREATE OR ALTER PROCEDURE PA_CLIENTES_NOMBRE
    @letraNom CHAR(1)
AS 
BEGIN
    SELECT C.id, C.nombre, C.direccion, C.telefono, C.eli_cli
    FROM Clientes C
    WHERE C.eli_cli = 'NO' AND LEFT(C.nombre, 1) = @letraNom
END
GO
EXEC PA_CLIENTES_NOMBRE @letraNom = 'A'
GO