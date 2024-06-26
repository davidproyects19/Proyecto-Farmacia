USE FARMACIABD
GO
--USUARIO
create or alter proc pa_encontrar_usuario
@login_usu varchar(25),
@clave_usu varchar(25)
as
   select count(*) as conta 
   from Usuarios
	where login_usu=@login_usu and
		clave_usu=@clave_usu
go

exec pa_encontrar_usuario 'david19','123456'
GO

SELECT * FROM Usuarios
GO
--REPORTE VENTAS-WEB API
CREATE OR ALTER PROCEDURE ReporteVentas
AS
BEGIN
    SELECT vc.num_vta, vc.fec_vta, c.nombre AS nombre_cliente, v.nom_ven AS nombre_vendedor, vc.tot_vta
    FROM Ventas_Cab vc
    INNER JOIN Clientes c ON vc.cod_cli = c.id
    INNER JOIN Vendedor v ON vc.cod_ven = v.cod_ven
    ORDER BY vc.fec_vta ASC;
END
GO
--
EXEC ReporteVentas
--

--
CREATE OR ALTER PROCEDURE ReporteVentasPorCliente
@Id INT
AS
BEGIN
    SELECT c.nombre AS nombre_cliente,
           vc.num_vta,
           vc.fec_vta,          
           p.Nombre AS nombre_producto,
           vd.cantidad,
           vd.precio,
		   vc.tot_vta
    FROM Ventas_Cab vc
    INNER JOIN Clientes c ON vc.cod_cli = c.id
    INNER JOIN Vendedor v ON vc.cod_ven = v.cod_ven
    INNER JOIN Ventas_Deta vd ON vc.num_vta = vd.num_vta
    INNER JOIN Productos p ON vd.cod_pro = p.id
    WHERE c.id = @Id
    ORDER BY c.nombre, vc.fec_vta ASC;
END
GO
--
EXEC ReporteVentasPorCliente 1 



--REALIZAR VENTAS
SELECT TOP(1) VC.*, C.nombre AS nom_cli 
FROM Ventas_Cab VC 
INNER JOIN Clientes C ON VC.cod_cli = C.id
ORDER BY VC.num_vta DESC
GO
--
SELECT TOP(1) WITH TIES VD.*, P.nombre AS nom_pro
FROM Ventas_Deta VD 
INNER JOIN Productos P ON VD.cod_pro = P.id
ORDER BY VD.num_vta DESC
GO
--
CREATE OR ALTER PROC PA_GRABAR_WEB_VENTAS_CAB
@COD_CLI INT, @TOT_VTA DECIMAL(10,2)
AS
	DECLARE @NUMERO VARCHAR(5) 
	SELECT @NUMERO = RIGHT(MAX(num_vta), 4) + 1 FROM Ventas_Cab
	SELECT @NUMERO = 'V' + RIGHT('000' + @NUMERO, 4)

	INSERT INTO Ventas_Cab VALUES(@NUMERO, GETDATE(), @COD_CLI, 1, @TOT_VTA, 'No')
	SELECT @NUMERO AS NUMERO
GO
--
CREATE OR ALTER PROC PA_GRABAR_WEB_VENTAS_DETA
@NUM_VTA CHAR(5), @COD_PRO INT, @CANTIDAD INT, @PRECIO DECIMAL(7,2)
AS
	INSERT INTO Ventas_Deta 
	VALUES(@NUM_VTA, @COD_PRO, @CANTIDAD, @PRECIO, 'No')

	UPDATE Productos SET cantidad = cantidad - @CANTIDAD 
	WHERE id = @COD_PRO
GO
--
