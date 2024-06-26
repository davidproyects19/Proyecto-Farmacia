-- CREACION DE BASE DE DATOS  (DATA y LOG)
USE master
GO

IF db_id('FARMACIABD') is not null
begin
	ALTER DATABASE FARMACIABD
	SET SINGLE_USER WITH ROLLBACK IMMEDIATE

	DROP DATABASE FARMACIABD
end
go

CREATE DATABASE FARMACIABD
COLLATE Modern_Spanish_CI_AI
GO

SET LANGUAGE SPANISH
SET NOCOUNT ON
GO

USE FARMACIABD
GO

CREATE TABLE Productos (
    id INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    precio DECIMAL(10, 2) NOT NULL,
    cantidad INT NOT NULL
)
GO
--
CREATE TABLE Vendedor(
	cod_ven int identity(1,1) not null primary key,
	nom_ven varchar (50) NULL ,
	fecing_ven date NULL --fecha de ingreso 
)
GO
-- Creación de la tabla Clientes
CREATE TABLE Clientes (
    id INT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(255) NULL,
    telefono VARCHAR(20) NULL
)
GO

-- Creación de la tabla Ventas_Cab
CREATE TABLE Ventas_Cab (
    num_vta CHAR(5) NOT NULL PRIMARY KEY,
    fec_vta DATE NULL,
    cod_cli INT NULL,
    cod_ven INT,
    tot_vta DECIMAL(8,2) DEFAULT 0,
    anulado CHAR(2) DEFAULT 'No'
)
GO

-- Creación de la tabla Ventas_Deta
CREATE TABLE Ventas_Deta (
    num_vta CHAR(5) NOT NULL,
    cod_pro int ,
    cantidad INT NULL,
    precio DECIMAL(7,2),
    anulado CHAR(2) DEFAULT 'No',
    PRIMARY KEY (num_vta, cod_pro),
    FOREIGN KEY (num_vta) REFERENCES Ventas_Cab (num_vta),
    FOREIGN KEY (cod_pro) REFERENCES Productos (id)
)
GO
--

CREATE TABLE Usuarios (
    login_usu VARCHAR(50) PRIMARY KEY,
    clave_usu VARCHAR(100) NOT NULL,
	nombre varchar(50) not null,
    rol varchar(20) not null
)
GO
--
-- Insertar datos en la tabla Productos
INSERT INTO Productos (id, nombre, descripcion, precio, cantidad)
VALUES
    (1, 'Aspirina', 'Medicamento para aliviar el dolor y la fiebre', 5.00, 100),
    (2, 'Paracetamol', 'Analgésico y antipirético para aliviar el dolor y la fiebre', 3.50, 150),
    (3, 'Ibuprofeno', 'Antiinflamatorio no esteroideo para aliviar el dolor y la fiebre', 4.00, 120),
    (4, 'Omeprazol', 'Medicamento para reducir la producción de ácido estomacal', 6.50, 80),
    (5, 'Loratadina', 'Antihistamínico para aliviar los síntomas de alergia', 8.00, 60),
    (6, 'Amoxicilina', 'Antibiótico para tratar infecciones bacterianas', 10.00, 50),
    (7, 'Salbutamol', 'Broncodilatador para tratar problemas respiratorios como el asma', 15.00, 40),
    (8, 'Vitamina C', 'Suplemento vitamínico para fortalecer el sistema inmunológico', 7.50, 70),
    (9, 'Crema de Caléndula', 'Crema para aliviar irritaciones de la piel y quemaduras leves', 9.50, 90),
    (10, 'Alcohol en Gel', 'Desinfectante de manos para prevenir la propagación de gérmenes', 4.00, 200)
--
-- Insertar datos en la tabla Vendedor
INSERT INTO Vendedor (nom_ven, fecing_ven)
VALUES
    ('David', '2022-01-01')
-- Insertar datos en la tabla Clientes
INSERT INTO Clientes (id, nombre, direccion, telefono)
VALUES
    (1, 'Juan Pérez', 'Av. Primavera 123', '987654321'),
    (2, 'María García', 'Jr. Huascarán 456', '963258741'),
    (3, 'Carlos Rodríguez', 'Calle Los Pinos 789', '951357456'),
    (4, 'Ana Martínez', 'Av. Libertadores 321', '925874136'),
    (5, 'Luis Gómez', 'Jr. Lima 654', '947852369'),
    (6, 'Elena Sánchez', 'Calle Ayacucho 987', '968547213'),
    (7, 'Pedro López', 'Av. Tacna 147', '935714826'),
    (8, 'Laura Ramírez', 'Jr. Arequipa 852', '978546213'),
    (9, 'Miguel Torres', 'Calle Progreso 369', '964782513'),
    (10, 'Sofía Fernández', 'Av. Los Ángeles 753', '984651372')
--
-- Insertar datos en la tabla Ventas_Cab
INSERT INTO Ventas_Cab (num_vta, fec_vta, cod_cli, cod_ven, tot_vta, anulado)
VALUES
    ('V0001', '2024-05-01', 1, 1, 0, 'No'),
    ('V0002', '2024-05-02', 2, 1, 0, 'No'),
    ('V0003', '2024-05-03', 3, 1, 0, 'No'),
    ('V0004', '2024-05-04', 4, 1, 0, 'No'),
    ('V0005', '2024-05-05', 5, 1, 0, 'No'),
    ('V0006', '2024-05-06', 6, 1, 0, 'No'),
    ('V0007', '2024-05-07', 7, 1, 0, 'No'),
    ('V0008', '2024-05-08', 8, 1, 0, 'No'),
    ('V0009', '2024-05-09', 9, 1, 0, 'No'),
    ('V0010', '2024-05-10', 10, 1, 0, 'No')

-- Insertar datos en la tabla Ventas_Deta
INSERT INTO Ventas_Deta (num_vta, cod_pro, cantidad, precio, anulado)
VALUES
    ('V0001', 1, 3, 1500.00, 'No'),
    ('V0001', 2, 2, 800.00, 'No'),
    ('V0002', 3, 1, 1200.00, 'No'),
    ('V0002', 4, 4, 700.00, 'No'),
    ('V0003', 5, 2, 300.00, 'No'),
    ('V0003', 6, 1, 600.00, 'No'),
    ('V0004', 7, 2, 250.00, 'No'),
    ('V0004', 8, 3, 180.00, 'No'),
    ('V0005', 9, 1, 350.00, 'No'),
    ('V0005', 10, 2, 1200.00, 'No')
--
INSERT INTO Usuarios (login_usu, clave_usu,nombre,rol)
VALUES
('david19', '123456','David','Administrador'),
('manu20', '123456','Manuel','Administrador')
GO
--
--
SELECT * FROM Productos
SELECT * FROM Clientes
SELECT * FROM Usuarios
SELECT * FROM Ventas_Cab
SELECT * FROM Ventas_Deta
GO

SELECT * FROM Vendedor
GO


UPDATE Ventas_Deta
SET precio = P.precio
FROM Ventas_Deta VD
INNER JOIN Productos P ON VD.cod_pro = P.id
GO

-- Restricciones adicionales para las tablas ARTICULOS y CLIENTES
ALTER TABLE Productos
ADD eli_pro CHAR(2) DEFAULT 'No' WITH VALUES
GO

ALTER TABLE Clientes
ADD eli_cli CHAR(2) DEFAULT 'No' WITH VALUES
GO

-- Restricciones de clave externa para Ventas_Cab y Ventas_Deta
ALTER TABLE Ventas_Cab
ADD CONSTRAINT fk_Ventas_Cab_cod_cli FOREIGN KEY (cod_cli)
REFERENCES Clientes (id)
GO

ALTER TABLE Ventas_Cab
ADD CONSTRAINT fk_Ventas_Cab_cod_ven FOREIGN KEY (cod_ven)
REFERENCES Vendedor (cod_ven)
GO

ALTER TABLE Ventas_Deta
ADD CONSTRAINT fk_Ventas_Deta_cod_pro FOREIGN KEY (cod_pro)
REFERENCES Productos (id)
GO



SET NOCOUNT OFF
SELECT MENSAJE='BASE DE DATOS FARMACIABD CREADA CORRECTAMENTE'
GO
