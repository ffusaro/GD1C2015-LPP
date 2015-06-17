USE [GD1C2015] 

IF NOT EXISTS (
SELECT  schema_name
FROM    information_schema.SCHEMATA
WHERE   schema_name = 'LPP' ) 

BEGIN
EXEC sp_executesql N'CREATE SCHEMA LPP '
END

/**********Limpieza************************/
/*---------Limpieza de Funciones----------*/
IF OBJECT_ID('LPP.FUNC_encriptar_tarjeta') IS NOT NULL
DROP FUNCTION LPP.FUNC_encriptar_tarjeta
GO


/*---------Limpieza de Procedures---------*/

IF OBJECT_ID('LPP.PRC_inhabilitar_cuentas') IS NOT NULL
DROP PROCEDURE LPP.PRC_inhabilitar_cuentas
GO

IF OBJECT_ID('LPP.PRC_estadistico_cuentas_inhabilitadas') IS NOT NULL
DROP PROCEDURE LPP.PRC_estadistico_cuentas_inhabilitadas
GO

IF OBJECT_ID('LPP.PRC_estadistico_comisiones_facturadas') IS NOT NULL
DROP PROCEDURE LPP.PRC_estadistico_comisiones_facturadas
GO

IF OBJECT_ID('LPP.PRC_estadistico_transacciones_cuentas_propias') IS NOT NULL
DROP PROCEDURE LPP.PRC_estadistico_transacciones_cuentas_propias
GO

IF OBJECT_ID('LPP.PRC_estadistico_pais_mas_movimientos') IS NOT NULL
DROP PROCEDURE LPP.PRC_estadistico_pais_mas_movimientos
GO

IF OBJECT_ID('LPP.PRC_estadistico_facturado_tipo_cuentas') IS NOT NULL
DROP PROCEDURE LPP.PRC_estadistico_facturado_tipo_cuentas
GO

IF OBJECT_ID('LPP.PRC_realizar_transferencia') IS NOT NULL
DROP PROCEDURE LPP.PRC_realizar_transferencia
GO

IF OBJECT_ID('LPP.PRC_cuentas_habilitadas_e_inhabilitadas') IS NOT NULL
DROP PROCEDURE LPP.PRC_cuentas_habilitadas_e_inhabilitadas
GO

IF OBJECT_ID('LPP.PRC_cuentas_de_un_cliente') IS NOT NULL
DROP PROCEDURE LPP.PRC_cuentas_de_un_cliente
GO

IF OBJECT_ID('LPP.PRC_items_factura_pendientes_de_un_cliente') IS NOT NULL
DROP PROCEDURE LPP.PRC_items_factura_pendientes_de_un_cliente
GO

IF OBJECT_ID('LPP.PRC_cuentas_deudoras') IS NOT NULL
DROP PROCEDURE LPP.PRC_cuentas_deudoras
GO

IF OBJECT_ID('LPP.PRC_inhabilitar_cuenta_por_deudor') IS NOT NULL
DROP PROCEDURE LPP.PRC_inhabilitar_cuenta_por_deudor
GO

IF OBJECT_ID('LPP.PRC_obtener_factura') IS NOT NULL
DROP PROCEDURE LPP.PRC_obtener_factura
GO

IF OBJECT_ID('LPP.PRC_facturar_item_factura') IS NOT NULL
DROP PROCEDURE LPP.PRC_facturar_item_factura
GO

IF OBJECT_ID('LPP.PRC_items_de_una_factura') IS NOT NULL
DROP PROCEDURE LPP.PRC_items_de_una_factura
GO

IF OBJECT_ID('LPP.PRC_insertar_nueva_tarjeta') IS NOT NULL
DROP PROCEDURE LPP.PRC_insertar_nueva_tarjeta
GO

IF OBJECT_ID('LPP.PRC_modificar_tarjeta') IS NOT NULL
DROP PROCEDURE LPP.PRC_modificar_tarjeta
GO

IF OBJECT_ID('LPP.PRC_desasociar_tarjeta') IS NOT NULL
DROP PROCEDURE LPP.PRC_desasociar_tarjeta
GO

IF OBJECT_ID('LPP.PRC_obtener_saldo_de_una_cuenta') IS NOT NULL
DROP PROCEDURE LPP.PRC_obtener_saldo_de_una_cuenta
GO

IF OBJECT_ID('LPP.PRC_ultimos_5_depositos_de_una_cuenta') IS NOT NULL
DROP PROCEDURE LPP.PRC_ultimos_5_depositos_de_una_cuenta
GO

IF OBJECT_ID('LPP.PRC_ultimos_5_retiros_de_una_cuenta') IS NOT NULL
DROP PROCEDURE LPP.PRC_ultimos_5_retiros_de_una_cuenta
GO

IF OBJECT_ID('LPP.PRC_ultimas_10_transferencias_de_una_cuenta') IS NOT NULL
DROP PROCEDURE LPP.PRC_ultimas_10_transferencias_de_una_cuenta
GO

IF OBJECT_ID ('LPP.PRC_cuenta_es_deudora') IS NOT NULL
DROP PROCEDURE LPP.PRC_cuenta_es_deudora
GO

IF OBJECT_ID('LPP.PRC_cambiar_costo_apertura_cuenta') IS NOT NULL
DROP PROCEDURE LPP.PRC_cambiar_costo_apertura_cuenta
GO


/*---------Limpieza de Triggers-----------*/
IF OBJECT_ID('LPP.TRG_ItemFactura_x_AperturaCuenta') IS NOT NULL
DROP TRIGGER LPP.TRG_ItemFactura_x_AperturaCuenta
GO

IF OBJECT_ID('LPP.TRG_CambioCuenta') IS NOT NULL
DROP TRIGGER LPP.TRG_CambioCuenta
GO

IF OBJECT_ID('LPP.TRG_ItemFactura_x_Transferencia') IS NOT NULL
DROP TRIGGER LPP.TRG_ItemFactura_x_Transferencia
GO

IF OBJECT_ID('LPP.TRG_cuenta_pendientedeactivacion_a_activada') IS NOT NULL
DROP TRIGGER LPP.TRG_cuenta_pendientedeactivacion_a_activada
GO

IF OBJECT_ID('LPP.TRG_inserta_tarjeta_encriptada') IS NOT NULL
DROP TRIGGER LPP.TRG_inserta_tarjeta_encriptada
GO

IF OBJECT_ID('LPP.TRG_inhabilita_cuentas_vencidas') IS NOT NULL
DROP TRIGGER LPP.TRG_inhabilita_cuentas_vencidas
GO

/*---------Limpieza de Views--------------*/

/*---------Limpieza de Tablas-------------*/

IF OBJECT_ID('LPP.ROLESXUSUARIO ') IS NOT NULL
BEGIN
	DROP TABLE LPP.ROLESXUSUARIO ;
END;

IF OBJECT_ID('LPP.LOGSXUSUARIO ') IS NOT NULL
BEGIN
	DROP TABLE LPP.LOGSXUSUARIO ;
END;

IF OBJECT_ID('LPP.DEPOSITOS ') IS NOT NULL
BEGIN
	DROP TABLE LPP.DEPOSITOS ;
END;

IF OBJECT_ID('LPP.TRANSFERENCIAS ') IS NOT NULL
BEGIN
	DROP TABLE LPP.TRANSFERENCIAS ;
END;

IF OBJECT_ID('LPP.ITEMS_FACTURA') IS NOT NULL
BEGIN
	DROP TABLE LPP.ITEMS_FACTURA;
END;

IF OBJECT_ID('LPP.ITEMS') IS NOT NULL
BEGIN
	DROP TABLE LPP.ITEMS;
END;

IF OBJECT_ID('LPP.FACTURAS') IS NOT NULL
BEGIN
	DROP TABLE LPP.FACTURAS;
END;

IF OBJECT_ID('LPP.CHEQUES ') IS NOT NULL
BEGIN
	DROP TABLE LPP.CHEQUES ;
END;

IF OBJECT_ID('LPP.RETIROS') IS NOT NULL
BEGIN
	DROP TABLE LPP.RETIROS;
END;

IF OBJECT_ID('LPP.FUNCIONALIDADXROL') IS NOT NULL
BEGIN 
	DROP TABLE LPP.FUNCIONALIDADXROL;
END;

IF OBJECT_ID('LPP.FUNCIONALIDAD') IS NOT NULL
BEGIN
	DROP TABLE LPP.FUNCIONALIDAD;
END;		

IF OBJECT_ID('LPP.ROLES ') IS NOT NULL
BEGIN
	DROP TABLE LPP.ROLES ;
END;

IF OBJECT_ID('LPP.TARJETAS ') IS NOT NULL
BEGIN
	DROP TABLE LPP.TARJETAS ;
END;

IF OBJECT_ID('LPP.EMISORES ') IS NOT NULL
BEGIN
	DROP TABLE LPP.EMISORES ;
END;

IF OBJECT_ID('LPP.CAMBIOS_CUENTA') IS NOT NULL
BEGIN
	DROP TABLE LPP.CAMBIOS_CUENTA ;
END;

IF OBJECT_ID('LPP.CUENTAS ') IS NOT NULL
BEGIN
	DROP TABLE LPP.CUENTAS ;
END;

IF OBJECT_ID('LPP.CLIENTES') IS NOT NULL
BEGIN
	DROP TABLE LPP.CLIENTES;
END;

IF OBJECT_ID('LPP.TIPO_DOCS') IS NOT NULL
BEGIN
	DROP TABLE LPP.TIPO_DOCS;
END;

IF OBJECT_ID('LPP.BANCOS ') IS NOT NULL
BEGIN
	DROP TABLE LPP.BANCOS ;
END;

IF OBJECT_ID('LPP.DOMICILIOS') IS NOT NULL
BEGIN
	DROP TABLE LPP.DOMICILIOS;
END;	

IF OBJECT_ID('LPP.PAISES ') IS NOT NULL
BEGIN
	DROP TABLE LPP.PAISES ;
END;

IF OBJECT_ID('LPP.MONEDAS ') IS NOT NULL
BEGIN
	DROP TABLE LPP.MONEDAS ;
END;

IF OBJECT_ID('LPP.TIPOS_CUENTA ') IS NOT NULL
BEGIN
	DROP TABLE LPP.TIPOS_CUENTA ;
END;

IF OBJECT_ID('LPP.ESTADOS_CUENTA ') IS NOT NULL
BEGIN
	DROP TABLE LPP.ESTADOS_CUENTA ;
END;

IF OBJECT_ID('LPP.USUARIOS ') IS NOT NULL
BEGIN
	DROP TABLE LPP.USUARIOS ;
END;

/*---------Definiciones de Tabla-------------*/

CREATE TABLE [LPP].USUARIOS(
/*id_usuario INTEGER NOT NULL, IDENTITY(1,1),*/
username VARCHAR(255) NOT NULL,
pass VARCHAR(255) NOT NULL,
pregunta_secreta VARCHAR(50),
respuesta_secreta VARCHAR(50),
fecha_creacion DATETIME,
fecha_ultimamodif DATETIME,
intentos INTEGER DEFAULT 0,
habilitado BIT DEFAULT 1,
PRIMARY KEY(username));

CREATE TABLE [LPP].ROLESXUSUARIO(
id_rolxusuario INTEGER NOT NULL IDENTITY(1,1),
username VARCHAR(255)NOT NULL,
rol INTEGER NOT NULL,
PRIMARY KEY(id_rolxusuario));

CREATE TABLE [LPP].ROLES(
id_rol INTEGER NOT NULL IDENTITY(1,1),
nombre VARCHAR(50) NOT NULL,
habilitado BIT DEFAULT 1,
PRIMARY KEY(id_rol));

CREATE TABLE [LPP].FUNCIONALIDAD(
id_funcionalidad SMALLINT NOT NULL,
descripcion VARCHAR(50),
PRIMARY KEY(id_funcionalidad));


CREATE TABLE [LPP].FUNCIONALIDADXROL(
id BIGINT IDENTITY(1,1) NOT NULL,
rol INTEGER,
funcionalidad SMALLINT NOT NULL,
PRIMARY KEY(id)); 

CREATE TABLE [LPP].LOGSXUSUARIO(
id_log INTEGER NOT NULL IDENTITY(1,1),
username VARCHAR(255) NOT NULL,
fecha DATETIME, -- INCLUYE LA HORA Y FECHA
num_intento INTEGER,
logueo BIT, -- 1 ES LOGIN CORRECTO Y 0 ES LOGIN INCORRECTO
PRIMARY KEY(id_log));

CREATE TABLE [LPP].CLIENTES(
id_cliente INTEGER NOT NULL IDENTITY(1,1),
username VARCHAR(255),
nombre VARCHAR(255) NOT NULL,
apellido VARCHAR(255) NOT NULL,
id_tipo_doc NUMERIC(18, 0),
num_doc DECIMAL(20, 0), 
fecha_nac DATETIME,
mail VARCHAR(255),
id_domicilio INTEGER,
id_pais NUMERIC(18, 0),
habilitado BIT DEFAULT 1,
PRIMARY KEY(id_cliente),
UNIQUE(id_tipo_doc, num_doc, apellido, nombre, fecha_nac));

CREATE TABLE [LPP].TIPO_DOCS(
tipo_cod NUMERIC(18,0) NOT NULL IDENTITY(1,1),
tipo_descr VARCHAR(255) NOT NULL,
PRIMARY KEY(tipo_cod));


CREATE TABLE [LPP].DOMICILIOS(
id_domicilio INTEGER NOT NULL IDENTITY(1,1),
calle VARCHAR(255),
num NUMERIC(18, 0),
depto VARCHAR(10),
piso NUMERIC(18, 0), 
localidad VARCHAR(255),
PRIMARY KEY(id_domicilio));

CREATE TABLE [LPP].PAISES(
id_pais NUMERIC(18, 0) NOT NULL IDENTITY(1,1),
pais VARCHAR(250),
PRIMARY KEY(id_pais),
);

CREATE TABLE [LPP].MONEDAS(
id_moneda NUMERIC(18, 0) NOT NULL IDENTITY(1,1),
descripcion VARCHAR(255),
PRIMARY KEY(id_moneda));

CREATE TABLE [LPP].TIPOS_CUENTA(
id_tipocuenta INTEGER NOT NULL IDENTITY(1,1),
descripcion VARCHAR(50) NOT NULL,
duracion INTEGER,
costo_apertura NUMERIC(18, 2), --FF: el costo de apertura debe ser respecto del tipo de cuenta que se abre
costo_transaccion NUMERIC(18, 2),
estado BIT DEFAULT 1,
PRIMARY KEY(id_tipocuenta));

CREATE TABLE [LPP].CAMBIOS_CUENTA(
id_cambio_cuenta NUMERIC(18,0) NOT NULL IDENTITY(1,1),
num_cuenta NUMERIC(18,0) NOT NULL,
tipocuenta_origen INTEGER NOT NULL,
tipocuenta_final INTEGER NOT NULL, 
fecha DATETIME,
PRIMARY KEY(id_cambio_cuenta));

CREATE TABLE [LPP].ESTADOS_CUENTA(
id_estadocuenta NUMERIC(18, 0) NOT NULL IDENTITY(1,1),
descripcion VARCHAR(255),
PRIMARY KEY(id_estadocuenta));

CREATE TABLE [LPP].CUENTAS(
num_cuenta NUMERIC(18,0) NOT NULL IDENTITY(1,1),
id_cliente INTEGER NOT NULL,
saldo NUMERIC(18, 2),
id_moneda NUMERIC(18,0) NOT NULL,
fecha_apertura DATETIME,
fecha_cierre DATETIME,
id_tipo INTEGER NOT NULL,
id_estado NUMERIC(18, 0),
id_pais NUMERIC(18, 0), 
PRIMARY KEY(num_cuenta));

CREATE TABLE [LPP].BANCOS(
id_banco NUMERIC(18, 0) NOT NULL IDENTITY(1,1),
nombre VARCHAR(255),
domicilio VARCHAR(255), -- FF: en maestra la direccion del banco es un varchar de 255, rever si no conviene dejarlo como un varchar en vez de partir el char para insertar en tabla domicilio
PRIMARY KEY(id_banco))

CREATE TABLE [LPP].EMISORES(
id_emisor NUMERIC(18,0) NOT NULL IDENTITY(1,1),
emisor_descr VARCHAR(255),
PRIMARY KEY(id_emisor));

CREATE TABLE [LPP].TARJETAS(
num_tarjeta VARCHAR(16) NOT NULL,
id_emisor NUMERIC(18,0) NOT NULL,
cod_seguridad VARCHAR(3) NOT NULL,
id_cliente INTEGER,
fecha_emision DATETIME,
fecha_vencimiento DATETIME,
PRIMARY KEY(num_tarjeta));

CREATE TABLE [LPP].DEPOSITOS(
num_deposito NUMERIC(18,0) NOT NULL IDENTITY(1,1),
num_cuenta NUMERIC(18,0) NOT NULL,
importe NUMERIC(18,2) NOT NULL, 
id_moneda NUMERIC(18,0) DEFAULT 1,
num_tarjeta VARCHAR(16),
id_emisor NUMERIC(18,0),
fecha_deposito DATETIME
PRIMARY KEY(num_deposito));

CREATE TABLE [LPP].RETIROS(
id_retiro NUMERIC(18,0) NOT NULL IDENTITY(1,1), 
num_cuenta NUMERIC(18, 0) NOT NULL,
importe NUMERIC(18,2),
fecha DATETIME,
id_moneda NUMERIC(18,0),
PRIMARY KEY(id_retiro));

CREATE TABLE [LPP].CHEQUES(
cheque_num NUMERIC(18,0) NOT NULL IDENTITY(1,1),
id_retiro NUMERIC(18, 0) NOT NULL,
importe NUMERIC(18, 2),
fecha DATETIME,
id_banco NUMERIC(18,0) NOT NULL,
cliente_receptor INTEGER,
PRIMARY KEY(cheque_num));

CREATE TABLE [LPP].TRANSFERENCIAS(
id_transferencia INTEGER NOT NULL IDENTITY(1,1),
num_cuenta_origen NUMERIC(18,0) NOT NULL,
num_cuenta_destino NUMERIC(18,0) NOT NULL,
importe NUMERIC(18,2),
fecha DATETIME,
costo_trans NUMERIC(18,2),
PRIMARY KEY(id_transferencia));

CREATE TABLE [LPP].ITEMS_FACTURA(
id_item_factura NUMERIC(18,0) NOT NULL IDENTITY(1,1),
id_item NUMERIC(18,0) NOT NULL,
num_cuenta NUMERIC(18,0) NOT NULL,
monto NUMERIC(18,2),
facturado BIT, 
id_factura NUMERIC(18,0),
fecha DATETIME,
PRIMARY KEY(id_item_factura));

CREATE TABLE [LPP].ITEMS(
id_item NUMERIC(18,0) NOT NULL IDENTITY(1,1),
descripcion VARCHAR(255),
PRIMARY KEY(id_item));

CREATE TABLE [LPP].FACTURAS(
id_factura NUMERIC(18,0) NOT NULL IDENTITY (1,1),
id_cliente INTEGER NOT NULL,
fecha DATETIME, 
PRIMARY KEY(id_factura));

/*---------Definiciones de Vistas-----------*/

/*---------Definiciones de Relaciones-------*/

ALTER TABLE LPP.ROLESXUSUARIO ADD
							FOREIGN KEY (username) references LPP.USUARIOS,
							FOREIGN KEY (rol) references LPP.ROLES;
								
ALTER TABLE LPP.LOGSXUSUARIO ADD
							FOREIGN KEY (username) references LPP.USUARIOS;
							
ALTER TABLE LPP.FUNCIONALIDADXROL ADD
							FOREIGN KEY (rol) references LPP.ROLES,
							FOREIGN KEY (funcionalidad) references LPP.FUNCIONALIDAD;							
								
ALTER TABLE LPP.CLIENTES ADD
							FOREIGN KEY (username) references LPP.USUARIOS,
							FOREIGN KEY (id_tipo_doc) references LPP.TIPO_DOCS,
							FOREIGN KEY (id_domicilio) references LPP.DOMICILIOS,
							FOREIGN KEY (id_pais) references LPP.PAISES;
								
ALTER TABLE LPP.CUENTAS ADD
							FOREIGN KEY (id_cliente) references LPP.CLIENTES,
							FOREIGN KEY (id_moneda) references LPP.MONEDAS,
							FOREIGN KEY (id_tipo) references LPP.TIPOS_CUENTA,
							FOREIGN KEY (id_estado) references LPP.ESTADOS_CUENTA,
							FOREIGN KEY (id_pais) references LPP.PAISES;

ALTER TABLE LPP.CAMBIOS_CUENTA ADD 
							FOREIGN KEY(num_cuenta) references LPP.CUENTAS,
							FOREIGN KEY(tipocuenta_origen) references LPP.TIPOS_CUENTA,
							FOREIGN KEY(tipocuenta_final) references LPP.TIPOS_CUENTA;
							
ALTER TABLE LPP.TARJETAS ADD
							FOREIGN KEY (id_emisor) references LPP.EMISORES,
							FOREIGN KEY (id_cliente) references LPP.CLIENTES;
							
ALTER TABLE LPP.DEPOSITOS ADD
							FOREIGN KEY (id_moneda) references LPP.MONEDAS,
							FOREIGN KEY (num_cuenta) references LPP.CUENTAS,
							FOREIGN KEY (num_tarjeta) references LPP.TARJETAS;	

ALTER TABLE LPP.RETIROS ADD
							FOREIGN KEY (num_cuenta) references LPP.CUENTAS,
							FOREIGN KEY (id_moneda) references LPP.MONEDAS;

ALTER TABLE LPP.CHEQUES ADD
							FOREIGN KEY (id_banco) references LPP.BANCOS,
							FOREIGN KEY (id_retiro) references LPP.RETIROS,
							FOREIGN KEY (cliente_receptor) references LPP.CLIENTES;
																					
ALTER TABLE LPP.TRANSFERENCIAS ADD
							FOREIGN KEY (num_cuenta_origen) references LPP.CUENTAS,
							FOREIGN KEY (num_cuenta_destino) references LPP.CUENTAS;
							
ALTER TABLE LPP.ITEMS_FACTURA ADD
							FOREIGN KEY (id_factura) references LPP.FACTURAS,
							FOREIGN KEY(id_item) references LPP.ITEMS,
							FOREIGN KEY (num_cuenta) references LPP.CUENTAS;

ALTER TABLE LPP.FACTURAS ADD
							FOREIGN KEY(id_cliente) references LPP.CLIENTES;

										
/*---------Carga de datos--------------------*/

INSERT INTO LPP.MONEDAS (descripcion) VALUES ('Dólares');

/*Creacion de Roles*/
BEGIN TRANSACTION
INSERT INTO LPP.ROLES (nombre) VALUES ('Administrador');
INSERT INTO LPP.ROLES (nombre) VALUES('Cliente');
COMMIT

/*Creacion de Funcionallidades*/
BEGIN TRANSACTION
INSERT INTO LPP.FUNCIONALIDAD (id_funcionalidad, descripcion) VALUES (1, 'ABM Cliente');
INSERT INTO LPP.FUNCIONALIDAD (id_funcionalidad, descripcion) VALUES (2, 'ABM Rol');
INSERT INTO LPP.FUNCIONALIDAD (id_funcionalidad, descripcion) VALUES (3,'ABM Cuenta');
INSERT INTO LPP.FUNCIONALIDAD (id_funcionalidad, descripcion) VALUES (4, 'Depositos');
INSERT INTO LPP.FUNCIONALIDAD (id_funcionalidad, descripcion) VALUES (5, 'Consulta Saldos');
INSERT INTO LPP.FUNCIONALIDAD (id_funcionalidad, descripcion) VALUES (6, 'Facturar');
INSERT INTO LPP.FUNCIONALIDAD (id_funcionalidad, descripcion) VALUES (7, 'Retiros');
INSERT INTO LPP.FUNCIONALIDAD (id_funcionalidad, descripcion) VALUES (8, 'Transferencias');
INSERT INTO LPP.FUNCIONALIDAD (id_funcionalidad, descripcion) VALUES (9, 'Listados');
INSERT INTO LPP.FUNCIONALIDAD (id_funcionalidad, descripcion) VALUES (10, 'ABM Usuarios');
INSERT INTO LPP.FUNCIONALIDAD (id_funcionalidad, descripcion) VALUES (11, 'Asociar/Desasociar Tarjetas');
COMMIT

/*Asignacion de Funcionalidades por Rol*/

GO
DECLARE @ID INTEGER;
SET @ID = (SELECT id_rol FROM LPP.ROLES WHERE nombre='Administrador');

BEGIN TRANSACTION
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 1);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 2);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 3);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 5);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 6);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 9);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 10);

SET @ID = (SELECT id_rol FROM LPP.ROLES WHERE nombre='Cliente');

INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 1);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 3);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 4);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 5);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 6);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 7);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 8);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES (@ID, 11);
COMMIT

/*Creacion de Usuarios Admin -HASH del password w23e: 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0dc9be7'*/
BEGIN TRANSACTION 
INSERT INTO LPP.USUARIOS (username, pass, fecha_creacion) VALUES('admin', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', GETDATE());
INSERT INTO LPP.USUARIOS (username, pass, fecha_creacion) VALUES('admin2','e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', GETDATE());

SET @ID = (SELECT id_rol FROM LPP.ROLES WHERE nombre='Administrador');
INSERT INTO LPP.ROLESXUSUARIO (rol, username) VALUES (@ID, 'admin');
INSERT INTO LPP.ROLESXUSUARIO (rol, username)	VALUES (@ID, 'admin2');
COMMIT

/*Creacion de Tipos de Documento*/
BEGIN TRANSACTION
INSERT INTO LPP.TIPO_DOCS (tipo_descr) VALUES ('DNI');
INSERT INTO LPP.TIPO_DOCS (tipo_descr) VALUES ('Cedula');
INSERT INTO LPP.TIPO_DOCS (tipo_descr) VALUES ('Libreta de Enrolamiento');
COMMIT

/*Creacion de los tipos de cuenta*/
BEGIN TRANSACTION
INSERT INTO LPP.TIPOS_CUENTA (descripcion, duracion, costo_apertura, costo_transaccion, estado) VALUES('Oro', 66, 200, 200, 1);
INSERT INTO LPP.TIPOS_CUENTA (descripcion, duracion, costo_apertura, costo_transaccion, estado) VALUES('Plata', 55, 100, 100,1);
INSERT INTO LPP.TIPOS_CUENTA (descripcion, duracion, costo_apertura, costo_transaccion, estado) VALUES('Bronce', 33, 50, 50, 1);
INSERT INTO LPP.TIPOS_CUENTA (descripcion, duracion, costo_apertura, costo_transaccion, estado) VALUES('Gratuita', 20, 0, 1, 1);
COMMIT

/*Creacion de estados de cuenta*/
BEGIN TRANSACTION 
INSERT INTO LPP.ESTADOS_CUENTA (descripcion)VALUES ('Habilitada');
INSERT INTO LPP.ESTADOS_CUENTA (descripcion)VALUES ('Pendiente de activacion');
INSERT INTO LPP.ESTADOS_CUENTA (descripcion)VALUES ('Cerrada');
INSERT INTO LPP.ESTADOS_CUENTA (descripcion)VALUES ('Inhabilitada');
COMMIT 

/*Creacion de descripcion de Items*/
BEGIN TRANSACTION
INSERT INTO LPP.ITEMS (descripcion) VALUES ('Comision por apertura de cuenta.');
INSERT INTO LPP.ITEMS (descripcion) VALUES ('Comision por cambio de tipo de cuenta.');
COMMIT


/*---------Definiciones de Funciones--------*/
GO
CREATE FUNCTION LPP.FUNC_encriptar_tarjeta (@num_tarjeta VARCHAR(16))
RETURNS VARCHAR(16)
WITH SCHEMABINDING
AS
BEGIN
	DECLARE @ret VARCHAR(16);
	SET @ret = CONVERT(VARCHAR(12), HASHBYTES('SHA1', SUBSTRING(@num_tarjeta, 1, (LEN(@num_tarjeta)-4 ))), 2)+ SUBSTRING(@num_tarjeta, (LEN(@num_tarjeta)-4 ), LEN(@num_tarjeta));
	RETURN @ret;	
END;
GO


/*---------Migracion-------------------------*/

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].TIPO_DOCS ON;
INSERT INTO LPP.TIPO_DOCS(tipo_cod, tipo_descr) 
			SELECT DISTINCT Cli_Tipo_Doc_Cod, Cli_Tipo_Doc_Desc FROM gd_esquema.Maestra;
SET IDENTITY_INSERT [LPP].TIPO_DOCS OFF;
COMMIT;

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].PAISES ON;			
INSERT INTO LPP.PAISES(id_pais, pais)
			SELECT DISTINCT Cli_Pais_Codigo, Cli_Pais_Desc FROM gd_esquema.Maestra;
INSERT INTO LPP.PAISES(id_pais, pais)
			SELECT DISTINCT Cuenta_Pais_Codigo, Cuenta_Pais_Desc FROM gd_esquema.Maestra
				WHERE (Cuenta_Pais_Codigo not in (select id_pais from LPP.PAISES));
INSERT INTO LPP.PAISES(id_pais, pais)
			SELECT DISTINCT Cuenta_Dest_Pais_Codigo, Cuenta_Dest_Pais_Desc FROM gd_esquema.Maestra
				WHERE (Cuenta_Dest_Pais_Codigo not in (select id_pais from LPP.PAISES));
SET IDENTITY_INSERT [LPP].PAISES OFF;
COMMIT; 

BEGIN TRANSACTION
INSERT INTO LPP.DOMICILIOS (calle, num, piso, depto)	
			SELECT DISTINCT Cli_Dom_Calle, Cli_Dom_Nro, Cli_Dom_Piso, Cli_Dom_Depto from gd_esquema.Maestra;
COMMIT;

BEGIN TRANSACTION
INSERT INTO LPP.EMISORES (emisor_descr)
			SELECT DISTINCT Tarjeta_Emisor_Descripcion FROM gd_esquema.Maestra WHERE Tarjeta_Emisor_Descripcion is not null;
COMMIT; 

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].BANCOS ON;
INSERT INTO LPP.BANCOS (id_banco, nombre, domicilio)
			SELECT DISTINCT Banco_Cogido, Banco_Nombre, Banco_Direccion FROM gd_esquema.Maestra WHERE Banco_Cogido is not null;
SET IDENTITY_INSERT [LPP].BANCOS OFF;
COMMIT; 


--TODO: el enunciado pide generar los nombres de usuarios y pass de los usuarios que ya existen en la maestra
BEGIN TRANSACTION
	INSERT INTO LPP.USUARIOS (username, pass, fecha_creacion) 
		SELECT DISTINCT REPLACE(Cli_Nombre+Cli_Apellido,' ',''), 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7' , GETDATE() FROM gd_esquema.Maestra; --HASH sha56: pass
	INSERT INTO LPP.CLIENTES (username, nombre, apellido, fecha_nac, id_pais, id_tipo_doc, num_doc, id_domicilio, mail) 
		SELECT DISTINCT REPLACE(Cli_Nombre+Cli_Apellido,' ',''), Cli_Nombre, Cli_Apellido, CONVERT(DATETIME, Cli_Fecha_Nac, 103), Cli_Pais_Codigo, Cli_Tipo_Doc_Cod, Cli_Nro_Doc,
				(SELECT id_domicilio FROM LPP.DOMICILIOS WHERE num= Cli_Dom_Nro AND calle = Cli_Dom_Calle AND depto = Cli_Dom_Depto ), Cli_Mail
		FROM gd_esquema.Maestra;
COMMIT; 

BEGIN TRANSACTION
	INSERT INTO LPP.ROLESXUSUARIO (username, rol)
		SELECT DISTINCT REPLACE(Cli_Nombre+Cli_Apellido,' ',''), 2 FROM gd_esquema.Maestra;
COMMIT;	

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].CUENTAS ON;
INSERT INTO LPP.CUENTAS (id_cliente, num_cuenta, saldo, fecha_apertura, id_pais, id_moneda, id_tipo, id_estado) 
			SELECT DISTINCT (SELECT id_cliente FROM LPP.CLIENTES WHERE nombre=Cli_Nombre and apellido=Cli_Apellido) as id_cliente, 
			Cuenta_Numero, 0,CONVERT(DATETIME, Cuenta_Fecha_Creacion, 103), Cuenta_Pais_Codigo,(SELECT id_moneda from LPP.MONEDAS where descripcion='Dólares'),
			(SELECT id_tipocuenta FROM LPP.TIPOS_CUENTA WHERE descripcion = 'Gratuita'), 1 FROM gd_esquema.Maestra; --id_estado = 1 cuenta habilitada
SET IDENTITY_INSERT [LPP].CUENTAS OFF;
--RR: Asumí que las cuentas son gratuitas, ya que el tipo de cuenta no está definida en la tabla maestra
COMMIT; 


BEGIN TRANSACTION
INSERT INTO [LPP].TARJETAS (num_tarjeta, id_emisor, cod_seguridad, fecha_emision, fecha_vencimiento, id_cliente)
	SELECT DISTINCT (dbo.FUNC_encriptar_tarjeta([Tarjeta_Numero])),(SELECT DISTINCT [id_emisor] FROM [LPP].EMISORES WHERE [emisor_descr] = m.[Tarjeta_Emisor_Descripcion])'id_emisor',
		[Tarjeta_Codigo_Seg],CONVERT(DATETIME,[Tarjeta_Fecha_Emision], 103), CONVERT(DATETIME,[Tarjeta_Fecha_Vencimiento], 103),(SELECT id_cliente FROM LPP.CLIENTES WHERE nombre=Cli_Nombre and apellido=Cli_Apellido) 'id_cliente'
        FROM [GD1C2015].[gd_esquema].[Maestra] m WHERE [Tarjeta_Numero] IS NOT NULL;  
COMMIT

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].DEPOSITOS ON;
	INSERT INTO [LPP].DEPOSITOS (num_deposito, num_cuenta, importe, id_moneda,num_tarjeta, fecha_deposito)
		SELECT [Deposito_Codigo],[Cuenta_Numero],[Deposito_Importe], 1, (dbo.FUNC_encriptar_tarjeta([Tarjeta_Numero])),CONVERT(DATETIME, [Deposito_Fecha], 103)
	    FROM [GD1C2015].[gd_esquema].[Maestra] WHERE Deposito_Codigo IS NOT NULL 
SET IDENTITY_INSERT [LPP].DEPOSITOS OFF; 
COMMIT;      

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].RETIROS ON;
	INSERT INTO [LPP].RETIROS (id_retiro, num_cuenta, importe,fecha)
		SELECT [Retiro_Codigo],[Cuenta_Numero],[Retiro_Importe], CONVERT(DATETIME, [Retiro_Fecha], 103)
		FROM [GD1C2015].[gd_esquema].[Maestra] WHERE Retiro_Codigo is not null
SET IDENTITY_INSERT [LPP].RETIROS OFF;	
COMMIT;	

BEGIN TRANSACTION		
SET IDENTITY_INSERT [LPP].CHEQUES ON;
	INSERT INTO [LPP].CHEQUES (cheque_num, id_retiro,importe, fecha, id_banco, cliente_receptor)
		SELECT [Cheque_Numero], [Retiro_Codigo],[Cheque_Importe],CONVERT(DATETIME, [Cheque_Fecha], 103),[Banco_Cogido], (SELECT id_cliente FROM LPP.CLIENTES WHERE nombre=Cli_Nombre and apellido=Cli_Apellido)
				FROM [GD1C2015].[gd_esquema].[Maestra] WHERE Retiro_Codigo IS NOT NULL AND Cheque_Numero IS NOT NULL
SET IDENTITY_INSERT [LPP].CHEQUES OFF;
COMMIT;

BEGIN TRANSACTION
	INSERT INTO [LPP].TRANSFERENCIAS (num_cuenta_origen, num_cuenta_destino, importe , fecha, costo_trans)
		SELECT [Cuenta_Numero], [Cuenta_Dest_Numero], [Trans_Importe], CONVERT(DATETIME, [Transf_Fecha], 103), [Trans_Costo_Trans]
		FROM [GD1C2015].[gd_esquema].[Maestra] WHERE Transf_Fecha IS NOT NULL
COMMIT;

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].FACTURAS ON;
	INSERT INTO [LPP].FACTURAS (id_factura, fecha, id_cliente)
		SELECT DISTINCT [Factura_Numero], CONVERT(DATETIME, [Factura_Fecha], 103), (SELECT id_cliente FROM LPP.CLIENTES WHERE nombre=Cli_Nombre and apellido=Cli_Apellido)
		FROM [GD1C2015].[gd_esquema].[Maestra] WHERE [Factura_Numero] IS NOT NULL
SET IDENTITY_INSERT [LPP].FACTURAS OFF;
COMMIT;

BEGIN TRANSACTION
	INSERT INTO [LPP].ITEMS(descripcion)
		SELECT DISTINCT [Item_Factura_Descr]
		FROM [GD1C2015].[gd_esquema].[Maestra] WHERE [Item_Factura_Descr] IS NOT NULL
COMMIT;		

BEGIN TRANSACTION
	INSERT INTO [LPP].ITEMS_FACTURA (id_factura, id_item, num_cuenta, monto,facturado, fecha)
	SELECT [Factura_Numero],(SELECT id_item FROM LPP.ITEMS WHERE descripcion = [Item_Factura_Descr]) 'id_item',[Cuenta_Numero], [Item_Factura_Importe], 1 'facturado', CONVERT(DATETIME, [Transf_Fecha], 103)
	FROM [GD1C2015].gd_esquema.Maestra WHERE Item_Factura_Descr IS NOT NULL
COMMIT;
GO
/*---------Definiciones de Triggers---------*/

--cada vez que ingresa el usuario se inhabilitan las cuentas vencidas
CREATE TRIGGER LPP.TRG_inhabilita_cuentas_vencidas
ON LPP.LOGSXUSUARIO
AFTER INSERT
AS
BEGIN
	IF((SELECT logueo FROM inserted) = 1)
		BEGIN
		DECLARE @id_cliente INTEGER
		DECLARE @fecha DATETIME
		SET @id_cliente = (SELECT i.username FROM inserted i JOIN LPP.CLIENTES c ON c.username = i.username)
		
		DECLARE @num_cuenta NUMERIC(18,0), @id_estado NUMERIC(18, 0), @id_tipo INTEGER, @fecha_apertura DATETIME
		 
		DECLARE cuentas CURSOR FOR
		SELECT num_cuenta, id_estado, id_tipo, fecha_apertura FROM LPP.CUENTAS WHERE id_cliente = @id_cliente	
		OPEN cuentas
		FETCH NEXT FROM cuentas INTO @num_cuenta, @id_estado, @id_tipo, @fecha_apertura
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF((SELECT @fecha - (@fecha_apertura + duracion) FROM LPP.TIPOS_CUENTA WHERE id_tipocuenta = @id_tipo) >= 0 )
				BEGIN
					UPDATE LPP.CUENTAS SET id_estado = 4 WHERE num_cuenta = @num_cuenta AND id_estado <> 2 AND id_estado <>3	
				END
			FETCH NEXT FROM cuentas INTO @num_cuenta, @id_estado, @id_tipo, @fecha_apertura
		END
		CLOSE cuentas
		DEALLOCATE cuentas 
		
		END
END
GO

--cada vez que hay una apartura de una cuenta insertar item de factura
CREATE TRIGGER LPP.TRG_ItemFactura_x_AperturaCuenta 
ON LPP.CUENTAS
AFTER INSERT 
AS
BEGIN
	INSERT INTO LPP.ITEMS_FACTURA (id_item, num_cuenta, monto, facturado, fecha)
	 VALUES (1, (SELECT num_cuenta FROM inserted), (SELECT costo_apertura FROM LPP.TIPOS_CUENTA WHERE id_tipocuenta =(SELECT id_tipo FROM inserted)), 0, (SELECT fecha_apertura FROM inserted))
END 
GO	
/*Test TRG_ItemFactura_x_AperturaCuenta*/
--INSERT INTO LPP.CUENTAS (id_cliente, saldo, id_moneda,fecha_apertura, id_tipo, id_estado, id_pais) VALUES (1, 500, 1, GETDATE(), 1, 2, 8) 
--SELECT * FROM LPP.ITEMS_FACTURA WHERE num_cuenta = (SELECT num_cuenta FROM LPP.CUENTAS WHERE id_cliente = 1 and saldo = 500 and id_tipo =1)

--cada vez que hay un cambio en el tipo de cuenta insertar item de factura
CREATE TRIGGER LPP.TRG_CambioCuenta 
ON LPP.CAMBIOS_CUENTA
AFTER INSERT
AS
BEGIN
	BEGIN TRANSACTION 
		UPDATE LPP.CUENTAS SET id_tipo = (SELECT tipocuenta_final FROM inserted) WHERE num_cuenta = (SELECT num_cuenta FROM inserted)
	
		INSERT INTO LPP.ITEMS_FACTURA (id_item, num_cuenta, monto, facturado, fecha)
			VALUES (2, (SELECT num_cuenta FROM inserted), (SELECT costo_apertura FROM LPP.TIPOS_CUENTA WHERE id_tipocuenta=(SELECT tipocuenta_final FROM inserted)), 0, (SELECT fecha FROM inserted))
	
			IF( (SELECT COUNT(id_item_factura) FROM LPP.ITEMS_FACTURA WHERE facturado= 0) > 5 )
		UPDATE LPP.CUENTAS SET id_estado = 4 WHERE num_cuenta = (SELECT num_cuenta FROM inserted)
	COMMIT 
END 
GO	
/*Test TRG_CambioCuenta*/
--SELECT num_cuenta, id_tipo FROM LPP.CUENTAS WHERE num_cuenta =1111111111111111 
--INSERT INTO LPP.CAMBIOS_CUENTA (num_cuenta, tipocuenta_origen, tipocuenta_final, fecha) VALUES (1111111111111111, 4, 2, GETDATE())
--SELECT num_cuenta, id_tipo FROM LPP.CUENTAS WHERE num_cuenta =1111111111111111
--SELECT * FROM LPP.ITEMS_FACTURA WHERE num_cuenta = (SELECT num_cuenta FROM LPP.CUENTAS WHERE num_cuenta =1111111111111111 and id_tipo = 2) and id_item = 2

-- cada vez que hay una transferencia insertar item de factura con descripcion costo por transferencia
CREATE TRIGGER LPP.TRG_ItemFactura_x_Transferencia 
ON LPP.TRANSFERENCIAS
AFTER INSERT 
AS
BEGIN
	INSERT INTO LPP.ITEMS_FACTURA (id_item, num_cuenta, monto, facturado, fecha)
		VALUES (3, (SELECT num_cuenta_origen FROM inserted), (SELECT costo_trans FROM inserted), 0, (SELECT fecha FROM inserted))
	IF EXISTS(SELECT DISTINCT num_cuenta FROM LPP.ITEMS_FACTURA WHERE facturado = 0 AND num_cuenta = (SELECT num_cuenta_origen FROM inserted)
			  GROUP BY num_cuenta HAVING COUNT(DISTINCT id_item_factura) >5)
		UPDATE LPP.CUENTAS SET id_estado = 4 WHERE num_cuenta = (SELECT num_cuenta_origen FROM inserted)
END 
GO

/*Test TRG_ItemFactura_x_Transferencia*/
--SELECT * FROM LPP.TRANSFERENCIAS	
--INSERT INTO LPP.TRANSFERENCIAS (num_cuenta_origen, num_cuenta_destino, importe, fecha, costo_trans) VALUES (1111111111111111, 1111111111111139, 90, GETDATE(), 1111)
--SELECT * FROM LPP.ITEMS_FACTURA WHERE num_cuenta = 1111111111111111 AND monto = 1111

--cuando se factura los costos de apertura de cuenta cambiar el tipo de cuenta de pendiente de activacion a activada
CREATE TRIGGER LPP.TRG_cuenta_pendientedeactivacion_a_activada
ON LPP.ITEMS_FACTURA
INSTEAD OF UPDATE
AS
BEGIN
	IF ((SELECT DISTINCT id_item FROM inserted) = 1)
		BEGIN
			UPDATE LPP.CUENTAS SET id_estado =1 WHERE (SELECT DISTINCT num_cuenta FROM inserted) = num_cuenta AND (SELECT DISTINCT id_item FROM inserted) = 1
			UPDATE LPP.ITEMS_FACTURA  SET facturado = (SELECT facturado FROM inserted), id_factura = (SELECT id_factura FROM inserted) WHERE id_item_factura = (SELECT id_item_factura FROM inserted)
		END
	ELSE
		BEGIN
			UPDATE LPP.ITEMS_FACTURA  SET facturado = (SELECT facturado FROM inserted), id_factura = (SELECT id_factura FROM inserted) WHERE id_item_factura = (SELECT id_item_factura FROM inserted)
		END 
	
END
GO
/*Test TRG_cuenta_pedienteactivacion_a_activada
SELECT * FROM LPP.ITEMS_FACTURA WHERE id_item = 3
INSERT INTO LPP.CUENTAS (id_cliente, saldo, id_moneda,fecha_apertura, id_tipo, id_estado, id_pais) VALUES (1, 500, 1, GETDATE(), 1, 2, 8) 
SELECT * FROM LPP.ITEMS_FACTURA WHERE id_item = 1
UPDATE LPP.ITEMS_FACTURA SET facturado = 1, id_factura = 1
*/

CREATE TRIGGER LPP.TRG_inserta_tarjeta_encriptada 
ON LPP.TARJETAS
INSTEAD OF INSERT
AS
BEGIN
	INSERT INTO LPP.TARJETAS (num_tarjeta, id_cliente, cod_seguridad, fecha_emision, fecha_vencimiento, id_emisor) VALUES 
		((SELECT dbo.FUNC_encriptar_tarjeta(num_tarjeta) FROM inserted), (SELECT id_cliente FROM inserted), (SELECT cod_seguridad FROM inserted),
		 (SELECT fecha_emision FROM inserted),  (SELECT fecha_vencimiento FROM inserted),  (SELECT  id_emisor FROM inserted) );
END
GO
/*Test TRG_inserta_tarjeta_encriptada*/ 
--INSERT INTO LPP.TARJETAS (num_tarjeta, id_cliente, cod_seguridad, fecha_emision, id_emisor) VALUES (1111111111000000, 1 ,222, GETDATE(), 1);
--SELECT * FROM LPP.TARJETAS WHERE id_cliente = 1 AND cod_seguridad = 222

/*---------Definiciones de Procedures-------*/

--inhabilitar cuentas por vencimiento de la duracion de la cuenta
--scheduled stored procedure: se ejecutara una vez por dia
CREATE PROCEDURE LPP.PRC_inhabilitar_cuentas
as
begin
	while 1 = 1
	begin 
		waitfor time '09:00:00'
		begin
			update LPP.CUENTAS set id_estado = 
			 (select id_estadocuenta from LPP.ESTADOS_CUENTA WHERE descripcion = 'Inhabilitada') 
			where num_cuenta IN (
			select num_cuenta from lPP.CUENTAS c
			WHERE 
			 (GETDATE() - fecha_apertura) > (select duracion from lPP.TIPOS_CUENTA T WHERE id_tipo = t.id_tipocuenta)) 
		end
	end
end
go

-- chequear tambien la fecha de cambio de cuenta
--ver si esta es una opcion para que se corra diariamente, yo no tengo el sql server agent para administrar jobs
-- sp_procoption 'PRC_inhabilitar_cuentas','startup', 'on'
-- GO

CREATE PROCEDURE LPP.PRC_cambiar_costo_apertura_cuenta
@costonuevo NUMERIC(18, 2),
@id_tipo NUMERIC(18, 0)
AS
BEGIN
	UPDATE LPP.TIPOS_CUENTA SET costo_apertura = @costonuevo WHERE id_tipocuenta = @id_tipo
END
GO
--alta tarjeta
CREATE PROCEDURE LPP.PRC_insertar_nueva_tarjeta
@num_tarjeta VARCHAR(16),
@id_emisor NUMERIC(18,0),
@cod_seguridad VARCHAR(3),
@id_cliente INTEGER,
@fecha_emision DATETIME,
@fecha_vencimiento DATETIME
AS
BEGIN
	INSERT INTO LPP.TARJETAS (num_tarjeta, id_emisor,cod_seguridad,id_cliente, fecha_emision, fecha_vencimiento) VALUES (dbo.FUNC_encriptar_tarjeta(@num_tarjeta), @id_emisor,@cod_seguridad,@id_cliente, @fecha_emision, @fecha_vencimiento)
END
GO

--modif tarjeta
CREATE PROCEDURE LPP.PRC_modificar_tarjeta
@num_tarjeta VARCHAR(16),
@cod_seguridad VARCHAR(3),
@fecha_emision DATETIME,
@fecha_vencimiento DATETIME
AS
BEGIN
	UPDATE LPP.TARJETAS SET cod_seguridad = @cod_seguridad, fecha_emision =@fecha_emision, fecha_vencimiento= @fecha_vencimiento WHERE num_tarjeta = @num_tarjeta
END
GO

--desasociar tarjeta de cuenta
CREATE PROCEDURE LPP.PRC_desasociar_tarjeta
@num_tarjeta VARCHAR(16),
@id_cliente INTEGER
AS
BEGIN
	UPDATE LPP.TARJETAS SET id_cliente = NULL WHERE num_tarjeta = @num_tarjeta AND id_cliente = @id_cliente
END
GO

--procedures transferencias
--sp que obtiene las cuentas de un cliente, que se puede usar como cuenta origen de una trasnferencia
CREATE PROCEDURE LPP.PRC_cuentas_de_un_cliente
@id_cliente INTEGER
AS
BEGIN
	SELECT * FROM LPP.CUENTAS c WHERE c.id_cliente = @id_cliente	
END
GO

--sp que obtiene las cuentas habilitadas e inhabilitadas a las cuales se le puede trasnferir dinero(cuenta destino de una trasnferencia)
CREATE PROCEDURE LPP.PRC_cuentas_habilitadas_e_inhabilitadas
AS
BEGIN
	SELECT * FROM LPP.CUENTAS c JOIN LPP.ESTADOS_CUENTA e ON c.id_estado = e.id_estadocuenta WHERE (e.id_estadocuenta = 1 or e.id_estadocuenta = 4)
END
GO

--sp que realiza la transferencia de un importe entre dos cuentas, ya sean del mismo usuario o de diferentes.
CREATE PROCEDURE LPP.PRC_realizar_transferencia
@num_cuenta_origen NUMERIC(18,0),
@num_cuenta_destino NUMERIC(18,0),
@importe NUMERIC(18,2),
@fecha DATETIME
AS
BEGIN
	IF(@num_cuenta_origen = @num_cuenta_destino)
		BEGIN
			INSERT INTO LPP.TRANSFERENCIAS (num_cuenta_origen, num_cuenta_destino, importe, fecha, costo_trans) VALUES (@num_cuenta_origen, @num_cuenta_destino, @importe, CONVERT(datetime, @fecha, 103), 0);
		END	
	ELSE 
		BEGIN
			DECLARE @costo NUMERIC(18, 2);
			SET @costo = (SELECT costo_transaccion FROM LPP.TIPOS_CUENTA t JOIN LPP.CUENTAS c ON c.id_tipo =t.id_tipocuenta WHERE C.num_cuenta = @num_cuenta_origen);
			INSERT INTO LPP.TRANSFERENCIAS (num_cuenta_origen, num_cuenta_destino, importe, fecha, costo_trans)
				VALUES (@num_cuenta_origen, @num_cuenta_destino, @importe, CONVERT(datetime, @fecha, 103), @costo);
		END
	UPDATE LPP.CUENTAS SET saldo = saldo - @importe WHERE num_cuenta = @num_cuenta_origen;
	UPDATE LPP.CUENTAS SET saldo = saldo + @importe WHERE num_cuenta = @num_cuenta_destino;	 		
END
GO

--procedures facturacion
--obtener items de factura pendientes de un usuario
CREATE PROCEDURE LPP.PRC_items_factura_pendientes_de_un_cliente
@id_cliente INTEGER
AS
BEGIN
	SELECT * FROM LPP.ITEMS_FACTURA i JOIN LPP.CUENTAS c ON c.num_cuenta = i.num_cuenta
		WHERE c.id_cliente = @id_cliente AND i.facturado = 0		
END
GO

--obtener cuentas que deben mas de 5 transacciones
CREATE PROCEDURE LPP.PRC_cuentas_deudoras
@id_cliente INTEGER
AS
BEGIN
	SELECT i.num_cuenta FROM LPP.ITEMS_FACTURA i JOIN LPP.CUENTAS c ON c.num_cuenta = i.num_cuenta
		WHERE c.id_cliente = @id_cliente AND i.facturado = 0
		GROUP BY i.num_cuenta
		HAVING COUNT(i.id_item_factura) > 5	
END
GO

--inhabilitar cuenta si debe mas 5 transacciones
CREATE PROCEDURE LPP.PRC_inhabilitar_cuenta_por_deudor
@num_cuenta NUMERIC(18, 0)
AS
BEGIN 
	UPDATE LPP.CUENTAS SET id_estado = 4 WHERE num_cuenta = @num_cuenta
END
GO

--generar factura
CREATE PROCEDURE LPP.PRC_obtener_factura
@fecha DATETIME,
@id_cliente INTEGER,
@id_factura NUMERIC(18, 0) OUTPUT
AS
BEGIN
	INSERT INTO LPP.FACTURAS (fecha, id_cliente) VALUES (@fecha, @id_cliente)
	SELECT DISTINCT @id_factura = id_factura FROM LPP.FACTURAS WHERE fecha = CONVERT(datetime, @fecha, 103) AND id_cliente = @id_cliente 
END
GO

--facturar items de factura
CREATE PROCEDURE LPP.PRC_facturar_item_factura
@id_item_factura NUMERIC(18,0),
@id_factura NUMERIC(18,0)
AS
BEGIN
	UPDATE LPP.ITEMS_FACTURA SET facturado = 1, id_factura = @id_factura WHERE id_item_factura = @id_item_factura
END
GO

--obtener items de una factura
CREATE PROCEDURE LPP.PRC_items_de_una_factura
@id_factura NUMERIC(18, 0)
AS
BEGIN
	SELECT * FROM LPP.ITEMS_FACTURA WHERE id_factura = @id_factura
END
GO

--procedures de consultas de saldos
--obtener saldo de una cuenta
CREATE PROCEDURE LPP.PRC_obtener_saldo_de_una_cuenta
@num_cuenta NUMERIC(18, 0),
@saldo NUMERIC(18, 2) OUTPUT
AS
BEGIN
	SELECT @saldo = saldo FROM LPP.CUENTAS WHERE num_cuenta = @num_cuenta
END
GO

--listado ultimos 5 depositos de una cuenta
CREATE PROCEDURE LPP.PRC_ultimos_5_depositos_de_una_cuenta
@num_cuenta NUMERIC(18, 0)
AS
BEGIN
	SELECT TOP 5 * FROM LPP.DEPOSITOS WHERE num_cuenta = @num_cuenta ORDER BY fecha_deposito DESC 
END
GO

--listado ultimos 5 retiros de una cuenta
CREATE PROCEDURE LPP.PRC_ultimos_5_retiros_de_una_cuenta
@num_cuenta NUMERIC(18, 0)
AS
BEGIN
	SELECT TOP 5 * FROM LPP.RETIROS WHERE num_cuenta = @num_cuenta ORDER BY fecha DESC
END
GO

--listados ultimas 1o transferencias
CREATE PROCEDURE LPP.PRC_ultimas_10_transferencias_de_una_cuenta
@num_cuenta NUMERIC(18, 0)
AS
BEGIN
	SELECT TOP 10 * FROM LPP.TRANSFERENCIAS WHERE num_cuenta_origen = @num_cuenta ORDER BY fecha DESC
END
GO

--listado estadistico 1
CREATE PROCEDURE LPP.PRC_estadistico_cuentas_inhabilitadas
@desde INTEGER,
@hasta INTEGER,
@anio INTEGER
AS
BEGIN
	SELECT TOP 5 c.id_cliente, username, nombre, apellido, t.tipo_descr, num_doc, fecha_nac, mail, COUNT(i.id_item_factura) AS items_adeudados
	FROM LPP.CLIENTES c
		JOIN LPP.CUENTAS cu ON cu.id_cliente = c.id_cliente
		JOIN LPP.ESTADOS_CUENTA e ON e.id_estadocuenta = cu.id_estado
		JOIN LPP.TIPO_DOCS t ON t.tipo_cod = c.id_tipo_doc
		JOIN LPP.ITEMS_FACTURA i ON i.num_cuenta= cu.num_cuenta
		JOIN LPP.ITEMS it ON it.id_item = i.id_item
	WHERE e.id_estadocuenta = 4 AND MONTH(i.fecha) BETWEEN @desde AND @hasta AND YEAR(i.fecha) = @anio
	GROUP BY c.id_cliente, username, nombre, apellido, t.tipo_descr, num_doc,  fecha_nac, mail
	ORDER BY COUNT(i.id_item_factura) DESC
END
GO	

--listado estadistico 2
CREATE PROCEDURE LPP.PRC_estadistico_comisiones_facturadas
@desde INTEGER,
@hasta INTEGER,
@anio INTEGER
AS
BEGIN
	SELECT TOP 5 c.id_cliente, username, nombre, apellido, t.tipo_descr, num_doc,  fecha_nac, mail, COUNT(i.id_item_factura)
	FROM LPP.CLIENTES c
		JOIN LPP.CUENTAS cu ON cu.id_cliente = c.id_cliente
		JOIN LPP.ITEMS_FACTURA i ON i.num_cuenta= cu.num_cuenta
		JOIN LPP.TIPO_DOCS t ON t.tipo_cod = c.id_tipo_doc
		WHERE i.facturado = 1
			AND MONTH(i.fecha) BETWEEN @desde AND @hasta AND YEAR(i.fecha) = @anio
		GROUP BY c.id_cliente, username, nombre, apellido, t.tipo_descr, num_doc,  fecha_nac, mail
		ORDER BY COUNT(i.id_item_factura) DESC
END
GO

--listado estadistico 3
CREATE PROCEDURE LPP.PRC_estadistico_transacciones_cuentas_propias
@desde INTEGER,
@hasta INTEGER,
@anio INTEGER
AS
BEGIN
	SELECT TOP 5 c.id_cliente, username, nombre, apellido, t.tipo_descr, num_doc,  fecha_nac, mail, COUNT(tr.id_transferencia)
	FROM LPP.CLIENTES c
		JOIN LPP.CUENTAS c1 ON c1.id_cliente = c.id_cliente
		JOIN LPP.CUENTAS c2 ON c2.id_cliente = c.id_cliente
		JOIN LPP.TIPO_DOCS t ON t.tipo_cod = c.id_tipo_doc
		JOIN LPP.TRANSFERENCIAS tr ON tr.num_cuenta_origen = c1.num_cuenta AND tr.num_cuenta_destino = c2.num_cuenta
		WHERE c1.num_cuenta <> c2.num_cuenta
			AND MONTH(tr.fecha) BETWEEN @desde AND @hasta AND YEAR(tr.costo_trans) = @anio
		GROUP BY c.id_cliente, username, nombre, apellido, t.tipo_descr, num_doc,  fecha_nac, mail
		ORDER BY  COUNT(tr.id_transferencia) DESC
		
END
GO


--listado estadistico 4
--tarda mucho la consulta, luego lo reviso
CREATE PROCEDURE LPP.PRC_estadistico_pais_mas_movimientos
@desde INTEGER,
@hasta INTEGER,
@anio INTEGER
AS
BEGIN
	SELECT TOP 5 pais, COUNT(d.num_deposito)+COUNT(r.id_retiro)+sum(t1.cant_or)+COUNT(t2.cant_dest)
	FROM LPP.PAISES p
		JOIN LPP.CUENTAS c ON c.id_pais = p.id_pais
		JOIN LPP.DEPOSITOS d ON d.num_cuenta = c.num_cuenta
		JOIN LPP.RETIROS r ON r.num_cuenta = c.num_cuenta
		JOIN (select num_cuenta_origen, COUNT(*) as cant_or from LPP.TRANSFERENCIAS group by num_cuenta_origen) t1 ON t1.num_cuenta_origen = c.num_cuenta
		JOIN (select num_cuenta_destino, COUNT(*) as cant_dest from LPP.TRANSFERENCIAS group by num_cuenta_destino) t2 ON t2.num_cuenta_destino = c.num_cuenta
	--WHERE t1.id_transferencia <> t2.id_transferencia
	--AND (MONTH(d.fecha_deposito) BETWEEN @desde AND @hasta AND YEAR(d.fecha_deposito)=@anio)
	--AND(MONTH(r.fecha) BETWEEN @desde AND @hasta AND YEAR(r.fecha)=@anio)
	--AND (MONTH(t1.fecha) BETWEEN @desde AND @hasta AND YEAR(t1.fecha)=@anio)
	--AND (MONTH(t2.fecha) BETWEEN @desde AND @hasta AND YEAR(t2.fecha)=@anio)
	GROUP BY pais
	ORDER BY 2 DESC
END
GO

--listado estadistico 5
CREATE PROCEDURE LPP.PRC_estadistico_facturado_tipo_cuentas
@desde INTEGER,
@hasta INTEGER,
@anio INTEGER
AS
BEGIN
	SELECT TOP 5 id_tipocuenta, descripcion, SUM(monto) AS 'totalFacturado'
	FROM LPP.TIPOS_CUENTA t
		JOIN LPP.CUENTAS c ON c.id_tipo = t.id_tipocuenta
		JOIN LPP.ITEMS_FACTURA i ON i.num_cuenta = c.num_cuenta
	WHERE i.facturado = 1 AND MONTH(i.fecha) BETWEEN @desde AND @hasta AND YEAR(i.fecha) = @anio	
	GROUP BY id_tipocuenta, descripcion
	ORDER BY SUM(monto) DESC	
END
GO


