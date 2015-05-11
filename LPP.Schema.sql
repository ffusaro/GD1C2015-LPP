USE [GD1C2015] 
--GO
--CREATE SCHEMA [LPP]; --AUTHORIZATION [gd]
--GO

IF NOT EXISTS (
SELECT  schema_name
FROM    information_schema.SCHEMATA
WHERE   schema_name = 'LPP' ) 

BEGIN
EXEC sp_executesql N'CREATE SCHEMA LPP AUTHORIZATION gd'
END

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

IF OBJECT_ID('LPP.TARJETAS ') IS NOT NULL
BEGIN
	DROP TABLE LPP.TARJETAS ;
END;

IF OBJECT_ID('LPP.EMISORES ') IS NOT NULL
BEGIN
	DROP TABLE LPP.EMISORES ;
END;

IF OBJECT_ID('LPP.TRANSFERENCIAS ') IS NOT NULL
BEGIN
	DROP TABLE LPP.TRANSFERENCIAS ;
END;

IF OBJECT_ID('LPP.ITEMS_FACTURA') IS NOT NULL
BEGIN
	DROP TABLE LPP.ITEMS_FACTURA;
END;

IF OBJECT_ID('LPP.FACTURAS') IS NOT NULL
BEGIN
	DROP TABLE LPP.FACTURAS;
END;

IF OBJECT_ID('LPP.RETIROS') IS NOT NULL
BEGIN
	DROP TABLE LPP.RETIROS;
END;

IF OBJECT_ID('LPP.ROLES ') IS NOT NULL
BEGIN
	DROP TABLE LPP.ROLES ;
END;

IF OBJECT_ID('LPP.ITEMS_PENDIENTES ') IS NOT NULL
BEGIN
	DROP TABLE LPP.ITEMS_PENDIENTES ;
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

IF OBJECT_ID('LPP.NACIONALIDADES') IS NOT NULL
BEGIN
	DROP TABLE LPP.NACIONALIDADES;
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



IF OBJECT_ID('LPP.TRANSACCIONES ') IS NOT NULL
BEGIN
	DROP TABLE LPP.TRANSACCIONES ;
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

--CREATE TABLE [LPP].();

CREATE TABLE [LPP].USUARIOS(
/*id_usuario INTEGER NOT NULL, IDENTITY(1,1),*/
username VARCHAR(20) NOT NULL,
pass VARCHAR(20) NOT NULL,
pregunta_secreta VARCHAR(50),
respuesta_secreta VARCHAR(50),
fecha_creacion DATETIME,
fecha_ultimamodif DATETIME,
intentos INTEGER,
habilitado BIT DEFAULT 1,
PRIMARY KEY(username));

CREATE TABLE [LPP].ROLESXUSUARIO(
id_rolxusuario INTEGER NOT NULL IDENTITY(1,1),
username VARCHAR(20)NOT NULL,
rol VARCHAR(50) NOT NULL,
PRIMARY KEY(id_rolxusuario));

CREATE TABLE [LPP].ROLES(
nombre VARCHAR(50) NOT NULL,
habilitado BIT DEFAULT 1,
PRIMARY KEY(nombre));

CREATE TABLE [LPP].LOGSXUSUARIO(
id_log INTEGER NOT NULL IDENTITY(1,1),
username VARCHAR(20) NOT NULL,
fecha DATETIME, -- INCLUYE LA HORA Y FECHA
num_intento BIT, -- 1 ES LOGIN CORRECTO Y 0 ES LOGIN INCORRECTO
PRIMARY KEY(id_log),
);

CREATE TABLE [LPP].CLIENTES(
id_cliente INTEGER NOT NULL IDENTITY(1,1),
username VARCHAR(20),
nombre VARCHAR(50) NOT NULL,
apellido VARCHAR(50) NOT NULL,
id_tipo_doc INTEGER,
num_doc DECIMAL(20, 0), --RR: me parece que esto se podría sacar
fecha_nac DATETIME,
mail VARCHAR(50),
id_domicilio INTEGER,
id_nacionalidad INTEGER,
PRIMARY KEY(id_cliente),
UNIQUE(id_tipo_doc, num_doc, apellido, nombre, fecha_nac));

CREATE TABLE [LPP].TIPO_DOCS(
tipo_cod INTEGER NOT NULL IDENTITY(1,1),
tipo VARCHAR(10) NOT NULL,
PRIMARY KEY(tipo_cod));


CREATE TABLE [LPP].DOMICILIOS(
id_domicilio INTEGER NOT NULL IDENTITY(1,1),
calle VARCHAR(100),
num INTEGER,
depto VARCHAR(5),
piso INTEGER, 
localidad VARCHAR(100),
id_pais INTEGER,
PRIMARY KEY(id_domicilio));

CREATE TABLE [LPP].PAISES(
id_pais INTEGER NOT NULL IDENTITY(1,1),
pais VARCHAR(50),
PRIMARY KEY(id_pais),
);

CREATE TABLE [LPP].MONEDAS(
id_moneda INTEGER NOT NULL IDENTITY(1,1),
descripcion VARCHAR(50),
PRIMARY KEY(id_moneda));

CREATE TABLE [LPP].TIPOS_CUENTA(
id_tipocuenta INTEGER NOT NULL IDENTITY(1,1),
descripcion VARCHAR(50) NOT NULL,
duracion INTEGER,
costo_mantenimiento DECIMAL,
costo_transaccion DECIMAL,
estado INTEGER,
PRIMARY KEY(id_tipocuenta));

CREATE TABLE [LPP].ESTADOS_CUENTA(
id_estadocuenta INTEGER NOT NULL IDENTITY(1,1),
descripcion VARCHAR(50),
PRIMARY KEY(id_estadocuenta));

CREATE TABLE [LPP].BANCOS(
id_banco INTEGER NOT NULL IDENTITY(1,1),
nombre VARCHAR(50),
costo_apertura INTEGER, --RR: Saque los not null, porque costo apertura y cambio no estan en la tabla maestra
costo_cambio INTEGER, 
id_domicilio INTEGER,
PRIMARY KEY(id_banco));

CREATE TABLE [LPP].TARJETAS(
num_tarjeta INTEGER NOT NULL,
id_emisor varchar(30) NOT NULL,
id_banco INTEGER NOT NULL,
num_cuenta NUMERIC(17,0) NOT NULL,
marca VARCHAR(20),
cod_seguridad INTEGER NOT NULL,
fecha_emision DATETIME,
fecha_vencimiento DATETIME,
PRIMARY KEY(num_tarjeta));

CREATE TABLE [LPP].DEPOSITOS(
num_deposito INTEGER NOT NULL IDENTITY(1,1),
num_cuenta NUMERIC(17,0) NOT NULL,
importe DECIMAL NOT NULL,
id_moneda INTEGER,
num_tarjeta INTEGER,
fecha_deposito DATETIME,
id_banco INTEGER,
PRIMARY KEY(num_deposito));

CREATE TABLE [LPP].TRANSACCIONES(
id_transaccion INTEGER NOT NULL IDENTITY(1,1),
/*id_retiro INTEGER,
ID_deposito INTEGER,
id_transferencia INTEGER,*/
PRIMARY KEY(id_transaccion));

CREATE TABLE [LPP].RETIROS(
id_retiro INTEGER NOT NULL IDENTITY(1,1),
id_cliente INTEGER NOT NULL, 
id_banco INTEGER NOT NULL,
importe DECIMAL,
fecha DATETIME,
PRIMARY KEY(id_retiro));

CREATE TABLE [LPP].TRANSFERENCIAS(
id_transferencia INTEGER NOT NULL IDENTITY(1,1),
id_origen NUMERIC(17,0) NOT NULL,
id_banco_origen INTEGER NOT NULL,
id_destino NUMERIC(17,0) NOT NULL,
id_banco_destino INTEGER NOT NULL,
importe decimal,
PRIMARY KEY(id_transferencia));

CREATE TABLE [LPP].ITEMS_PENDIENTES(
id_item INTEGER NOT NULL IDENTITY(1,1),
num_cuenta NUMERIC(17,0) NOT NULL,
monto DECIMAL,
id_transaccion INTEGER,
estado BIT, 
id_banco INTEGER,
PRIMARY KEY(id_item));

CREATE TABLE [LPP].CUENTAS(
num_cuenta NUMERIC(17,0) NOT NULL IDENTITY(1,1),
id_cliente INTEGER NOT NULL,
id_banco INTEGER NOT NULL,
saldo DECIMAL,
id_moneda INTEGER NOT NULL,
fecha_apertura DATETIME,
fecha_cierre DATETIME,
id_tipo INTEGER NOT NULL,
id_estado INTEGER,
id_pais INTEGER, 
PRIMARY KEY(num_cuenta, id_banco));

CREATE TABLE [LPP].ITEMS_FACTURA(
id_items_factura INTEGER NOT NULL IDENTITY(1,1),
id_factura INTEGER NOT NULL,
id_item_pendiente INTEGER NOT NULL,
PRIMARY KEY(id_items_factura));

CREATE TABLE [LPP].FACTURAS(
id_factura INTEGER NOT NULL IDENTITY (1,1),
id_cliente INTEGER NOT NULL,
id_banco INTEGER NOT NULL,
fecha DATETIME, 
total DECIMAL,
PRIMARY KEY(id_factura));

CREATE TABLE [LPP].EMISORES(
id_emisor varchar(30) NOT NULL,
PRIMARY KEY(id_emisor),
);

/*---------Definiciones de Relaciones-------*/

ALTER TABLE LPP.ROLESXUSUARIO ADD
							FOREIGN KEY (username) references LPP.USUARIOS,
							FOREIGN KEY (rol) references LPP.ROLES;
								
ALTER TABLE LPP.LOGSXUSUARIO ADD
							FOREIGN KEY (username) references LPP.USUARIOS;
								
ALTER TABLE LPP.CLIENTES ADD
							FOREIGN KEY (username) references LPP.USUARIOS,
							FOREIGN KEY (id_tipo_doc) references LPP.TIPO_DOCS,
							FOREIGN KEY (id_domicilio) references LPP.DOMICILIOS,
							FOREIGN KEY (id_nacionalidad) references LPP.PAISES;
							
ALTER TABLE LPP.DOMICILIOS ADD
							FOREIGN KEY (id_pais) references LPP.PAISES;
								
ALTER TABLE LPP.CUENTAS ADD
							FOREIGN KEY (id_cliente) references LPP.CLIENTES,
							FOREIGN KEY (id_banco) references LPP.BANCOS,
							FOREIGN KEY (id_moneda) references LPP.MONEDAS,
							FOREIGN KEY (id_tipo) references LPP.TIPOS_CUENTA,
							FOREIGN KEY (id_estado) references LPP.ESTADOS_CUENTA,
							FOREIGN KEY (id_pais) references LPP.PAISES;
							

ALTER TABLE LPP.BANCOS ADD
							FOREIGN KEY (id_domicilio) references LPP.DOMICILIOS;
							
ALTER TABLE LPP.TARJETAS ADD
							FOREIGN KEY (id_emisor) references LPP.EMISORES,
							FOREIGN KEY (num_cuenta, id_banco) references LPP.CUENTAS;
							
ALTER TABLE LPP.DEPOSITOS ADD
							FOREIGN KEY (id_moneda) references LPP.MONEDAS,
							FOREIGN KEY (num_cuenta, id_banco) references LPP.CUENTAS,
							FOREIGN KEY (num_tarjeta) references LPP.TARJETAS;

ALTER TABLE LPP.RETIROS ADD
							FOREIGN KEY (id_cliente) references LPP.CLIENTES,
							FOREIGN KEY (id_banco) references LPP.BANCOS;

ALTER TABLE LPP.TRANSFERENCIAS ADD
							FOREIGN KEY (id_origen, id_banco_origen) references LPP.CUENTAS,
							FOREIGN KEY (id_destino, id_banco_destino) references LPP.CUENTAS;
							
ALTER TABLE LPP.ITEMS_PENDIENTES ADD
							FOREIGN KEY (id_transaccion) references LPP.TRANSACCIONES,
							FOREIGN KEY (num_cuenta, id_banco) references LPP.CUENTAS;

ALTER TABLE LPP.ITEMS_FACTURA ADD
							FOREIGN KEY (id_factura) references LPP.FACTURAS,
							FOREIGN KEY (id_item_pendiente) references LPP.ITEMS_PENDIENTES;
							
ALTER TABLE LPP.FACTURAS ADD
							FOREIGN KEY (id_cliente) references LPP.CLIENTES,
							FOREIGN KEY (id_banco) references LPP.BANCOS;


/*---------Carga de datos--------------------*/

INSERT INTO LPP.MONEDAS (descripcion) VALUES ('Dólares');

INSERT INTO LPP.ROLES (nombre) VALUES ('Administrador');
INSERT INTO LPP.ROLES (nombre) VALUES('Cliente');

INSERT INTO LPP.TIPOS_CUENTA (descripcion) VALUES('Oro');
INSERT INTO LPP.TIPOS_CUENTA (descripcion) VALUES('Plata');
INSERT INTO LPP.TIPOS_CUENTA (descripcion) VALUES('Bronce');
INSERT INTO LPP.TIPOS_CUENTA (descripcion) VALUES('Gratuita');

/*---------Migracion-------------------------*/

SET IDENTITY_INSERT [LPP].TIPO_DOCS ON;
INSERT INTO LPP.TIPO_DOCS(tipo_cod, tipo) 
			SELECT DISTINCT Cli_Tipo_Doc_Cod, Cli_Tipo_Doc_Desc FROM gd_esquema.Maestra;
SET IDENTITY_INSERT [LPP].TIPO_DOCS OFF;


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


SET IDENTITY_INSERT [LPP].DOMICILIOS ON;
INSERT INTO LPP.DOMICILIOS (id_pais, calle, id_domicilio, piso, depto)	
			SELECT DISTINCT Cli_Pais_Codigo, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Dom_Piso, Cli_Dom_Depto from gd_esquema.Maestra;
SET IDENTITY_INSERT [LPP].DOMICILIOS OFF;


INSERT INTO LPP.EMISORES (id_emisor)
			SELECT DISTINCT Tarjeta_Emisor_Descripcion FROM gd_esquema.Maestra WHERE Tarjeta_Emisor_Descripcion is not null;

SET IDENTITY_INSERT [LPP].BANCOS ON;
INSERT INTO LPP.BANCOS (id_banco, nombre)
			SELECT DISTINCT Banco_Cogido, Banco_Nombre FROM gd_esquema.Maestra WHERE Banco_Cogido is not null;
SET IDENTITY_INSERT [LPP].BANCOS OFF;

INSERT INTO LPP.CLIENTES (nombre, apellido, fecha_nac, id_nacionalidad, id_tipo_doc, id_domicilio, mail )
			SELECT DISTINCT Cli_Nombre, Cli_Apellido, Cli_Fecha_Nac, Cli_Pais_Codigo, Cli_Tipo_Doc_Cod, Cli_Dom_Nro, Cli_Mail
				FROM gd_esquema.Maestra;

SET IDENTITY_INSERT [LPP].CUENTAS ON;
INSERT INTO LPP.CUENTAS (id_cliente, num_cuenta, fecha_apertura, id_pais, id_banco, id_moneda, id_tipo) 
			SELECT DISTINCT (SELECT id_cliente FROM LPP.CLIENTES WHERE nombre=Cli_Nombre and apellido=Cli_Apellido) as id_cliente, 
			Cuenta_Numero, Cuenta_Fecha_Creacion, Cuenta_Pais_Codigo, Banco_Cogido,
			(SELECT id_moneda from LPP.MONEDAS where descripcion='Dólares'),
			(SELECT id_tipocuenta FROM LPP.TIPOS_CUENTA WHERE descripcion = 'Gratuita') FROM gd_esquema.Maestra where Banco_Cogido is not null;
SET IDENTITY_INSERT [LPP].CUENTAS OFF;
--RR: Asumí que las cuentas son gratuitas, ya que el tipo de cuenta no está definida en la tabla maestra
-- FALTAN HACER LAS MIGRACIONES EN LAS TABLAS TARJETAS, DEPOSITOS, TRANSACCIONES, RETIROS, TRANSFERENCIAS,
-- ITEMS_PENDIENTES, FACTURAS (SETEAR TAMBIEN LA TABLA INTERMEDIA ITEMS_FACTURA)


/*---------Definiciones de Vistas-----------*/

/*---------Definiciones de Triggers---------*/

/*---------Definiciones de Procedures-------*/

