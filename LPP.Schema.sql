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
nombre VARCHAR(255) NOT NULL,
apellido VARCHAR(255) NOT NULL,
id_tipo_doc NUMERIC(18, 0),
num_doc DECIMAL(20, 0), --RR: me parece que esto se podría sacar FF: el enunciado pide num de doc, al no aparecer en la maestra deberiamos ver como manejarlo. Podriamos inventar un num_doc de migracion, y a los que vienen de la maestra le ponemos ese. O que sea nullable y lo dejamos en null.
fecha_nac DATETIME,
mail VARCHAR(255),
id_domicilio INTEGER,
id_pais NUMERIC(18, 0),
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
id_pais NUMERIC(18,0),
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
costo_mantenimiento DECIMAL,
costo_transaccion DECIMAL,
estado INTEGER,
PRIMARY KEY(id_tipocuenta));

CREATE TABLE [LPP].ESTADOS_CUENTA(
id_estadocuenta NUMERIC(18, 0) NOT NULL IDENTITY(1,1),
descripcion VARCHAR(255),
PRIMARY KEY(id_estadocuenta));

CREATE TABLE [LPP].CUENTAS(
num_cuenta NUMERIC(18,0) NOT NULL IDENTITY(1,1),
id_cliente INTEGER NOT NULL,
id_banco INTEGER NOT NULL,
saldo DECIMAL,
id_moneda INTEGER NOT NULL,
fecha_apertura DATETIME,
fecha_cierre DATETIME,
id_tipo INTEGER NOT NULL,
id_estado NUMERIC(18, 0),
id_pais NUMERIC(18, 0), 
PRIMARY KEY(num_cuenta, id_banco));

CREATE TABLE [LPP].BANCOS(
id_banco NUMERIC(18, 0) NOT NULL IDENTITY(1,1),
nombre VARCHAR(255),
costo_apertura NUMERIC(18, 0) NOT NULL DEFAULT 100, --RR: Saque los not null, porque costo apertura y cambio no estan en la tabla maestra FF: agrego valores default, porque todos los bancos deben tener costos si o si
costo_cambio NUMERIC(18, 0) NOT NULL DEFAULT 100, 
id_domicilio INTEGER, -- FF: en maestra la direccion del banco es un varchar de 255, rever si no conviene dejarlo como un varchar en vez de partir el char para insertar en tabla domicilio
PRIMARY KEY(id_banco))

CREATE TABLE [LPP.EMISORES](
id_emisor INTEGER NOT NULL IDENTITY(1,1),
emisor_descr VARCHAR(255),
PRIMARY KEY(id_emisor));

CREATE TABLE [LPP].TARJETAS(
num_tarjeta VARCHAR(16) NOT NULL,
id_emisor NUMERIC(18,0) NOT NULL,
id_banco INTEGER NOT NULL,
num_cuenta NUMERIC(18,0) NOT NULL,--FF: borre marca, ya que en maestra=emisor
cod_seguridad VARCHAR(3) NOT NULL,
fecha_emision DATETIME,
fecha_vencimiento DATETIME,
PRIMARY KEY(num_tarjeta, id_emisor));

CREATE TABLE [LPP].DEPOSITOS(
num_deposito NUMERIC(18,0) NOT NULL IDENTITY(1,1),
num_cuenta NUMERIC(18,0) NOT NULL,--FF: en la maestra no hay datos de cuenta en los depositos, que hacemos?
importe NUMERIC(18,2) NOT NULL,
id_moneda NUMERIC(18,0) DEFAULT 1,
num_tarjeta VARCHAR(16),
fecha_deposito DATETIME,
id_banco NUMERIC(18,0), --ni de bancos
PRIMARY KEY(num_deposito));

CREATE TABLE [LPP].TRANSACCIONES(
id_transaccion INTEGER NOT NULL IDENTITY(1,1),
id_retiro INTEGER,
ID_deposito INTEGER,
id_transferencia INTEGER,
PRIMARY KEY(id_transaccion));

CREATE TABLE [LPP].RETIROS(
id_retiro NUMERIC(18,0) NOT NULL IDENTITY(1,1), -- hago cambios para que coincida con la maestra
num_cuenta NUMERIC(18, 0) NOT NULL,
id_banco NUMERIC(18,0) NOT NULL,
importe NUMERIC(18,2),
fecha DATETIME,
PRIMARY KEY(id_retiro));

CREATE TABLE [LPP].CHEQUES(
cheque_num NUMERIC(18,0) NOT NULL,
id_retiro NUMERIC(18, 0) NOT NULL,
importe NUMERIC(18, 2),
fecha DATETIME,
id_banco NUMERIC(18,0) NOT NULL,
PRIMARY KEY(cheque_num, id_banco));

CREATE TABLE [LPP].TRANSFERENCIAS(
id_transferencia INTEGER NOT NULL IDENTITY(1,1),
num_cuenta_origen NUMERIC(18,0) NOT NULL,
id_banco_origen NUMERIC(18,0) NOT NULL,
num_cuenta_destino NUMERIC(18,0) NOT NULL,
id_banco_destino NUMERIC(18,0) NOT NULL,
importe NUMERIC(18,2),
fecha DATETIME,
costo_trans NUMERIC(18,2),
PRIMARY KEY(id_transferencia));

CREATE TABLE [LPP].ITEMS_PENDIENTES(
id_item INTEGER NOT NULL IDENTITY(1,1),
num_cuenta NUMERIC(18,0) NOT NULL,
monto NUMERIC(18,2),
id_transaccion INTEGER,
estado BIT DEFAULT 0, --bit 0: no ha sido cobrado todavia
id_banco NUMERIC(18,0)
PRIMARY KEY(id_item));


CREATE TABLE [LPP].ITEMS_FACTURA(
id_items_factura INTEGER NOT NULL IDENTITY(1,1),
id_factura INTEGER NOT NULL,
id_item_pendiente INTEGER NOT NULL,
descr VARCHAR(255),
importe NUMERIC(18, 2),
PRIMARY KEY(id_items_factura));

CREATE TABLE [LPP].FACTURAS(
id_factura NUMERIC(18,0) NOT NULL IDENTITY (1,1),
num_cuenta NUMERIC(18,0) NOT NULL,
id_banco NUMERIC(18,0) NOT NULL,
fecha DATETIME, 
total DECIMAL,
PRIMARY KEY(id_factura));



/*---------Definiciones de Relaciones-------*/

ALTER TABLE LPP.ROLESXUSUARIO ADD
							FOREIGN KEY (username) references LPP.USUARIOS,
							FOREIGN KEY (rol) references LPP.ROLES;
								
ALTER TABLE LPP.LOGSXUSUARIO ADD
							FOREIGN KEY (username) references LPP.USUARIOS;
								
ALTER TABLE LPP.CLIENTES ADD
							FOREIGN KEY (username) references LPP.USUARIOS,
							FOREIGN KEY (tipo_cod) references LPP.TIPO_DOCS,
							FOREIGN KEY (id_domicilio) references LPP.DOMICILIOS,
							FOREIGN KEY (id_pais) references LPP.PAISES;
							
ALTER TABLE LPP.DOMICILIOS ADD
							FOREIGN KEY (id_pais) references LPP.PAISES;
								
ALTER TABLE LPP.CUENTAS ADD
							FOREIGN KEY (id_cliente) references LPP.CLIENTES,
							FOREIGN KEY (id_banco) references LPP.BANCOS,
							FOREIGN KEY (id_moneda) references LPP.MONEDAS,
							FOREIGN KEY (id_tipocuenta) references LPP.TIPOS_CUENTA,
							FOREIGN KEY (id_estadocuenta) references LPP.ESTADOS_CUENTA,
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
							FOREIGN KEY (num_cuenta, id_banco) references LPP.CUENTAS;

ALTER TABLE LPP.CHEQUES ADD
							FOREIGN KEY (id_banco) references LPP.BANCOS;
																					

ALTER TABLE LPP.TRANSFERENCIAS ADD
							FOREIGN KEY (num_cuenta_origen, id_banco_origen) references LPP.CUENTAS,
							FOREIGN KEY (num_cuenta_destino, id_banco_destino) references LPP.CUENTAS;
							
ALTER TABLE LPP.ITEMS_PENDIENTES ADD
							FOREIGN KEY (id_transaccion) references LPP.TRANSACCIONES,
							FOREIGN KEY (num_cuenta, id_banco) references LPP.CUENTAS;

ALTER TABLE LPP.ITEMS_FACTURA ADD
							FOREIGN KEY (id_factura) references LPP.FACTURAS,
							FOREIGN KEY (id_item) references LPP.ITEMS_PENDIENTES;
							
ALTER TABLE LPP.FACTURAS ADD
							FOREIGN KEY (num_cuenta) references LPP.CUENTAS,
							FOREIGN KEY (id_banco) references LPP.BANCOS;


/*---------Carga de datos--------------------*/

INSERT INTO LPP.MONEDAS (id_moneda, descripcion) VALUES (1, 'Dólares');

BEGIN TRANSACTION
INSERT INTO LPP.ROLES (nombre) VALUES ('Administrador');
INSERT INTO LPP.ROLES (nombre) VALUES('Cliente');
COMMIT

BEGIN TRANSACTION
INSERT INTO LPP.TIPOS_CUENTA (descripcion) VALUES('Oro');
INSERT INTO LPP.TIPOS_CUENTA (descripcion) VALUES('Plata');
INSERT INTO LPP.TIPOS_CUENTA (descripcion) VALUES('Bronce');
INSERT INTO LPP.TIPOS_CUENTA (descripcion) VALUES('Gratuita');
COMMIT


/*---------Migracion-------------------------*/

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].TIPO_DOCS ON;
INSERT INTO LPP.TIPO_DOCS(tipo_cod, tipo) 
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
SET IDENTITY_INSERT [LPP].DOMICILIOS ON;
INSERT INTO LPP.DOMICILIOS (id_pais, calle, id_domicilio, piso, depto)	
			SELECT DISTINCT Cli_Pais_Codigo, Cli_Dom_Calle, Cli_Dom_Nro, Cli_Dom_Piso, Cli_Dom_Depto from gd_esquema.Maestra;
SET IDENTITY_INSERT [LPP].DOMICILIOS OFF;
COMMIT;

BEGIN TRANSACTION
INSERT INTO LPP.EMISORES (id_emisor)
			SELECT DISTINCT Tarjeta_Emisor_Descripcion FROM gd_esquema.Maestra WHERE Tarjeta_Emisor_Descripcion is not null;
COMMIT;

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].BANCOS ON;
INSERT INTO LPP.BANCOS (id_banco, nombre)
			SELECT DISTINCT Banco_Cogido, Banco_Nombre FROM gd_esquema.Maestra WHERE Banco_Cogido is not null;
SET IDENTITY_INSERT [LPP].BANCOS OFF;
COMMIT;

BEGIN TRANSACTION
INSERT INTO LPP.CLIENTES (nombre, apellido, fecha_nac, id_nacionalidad, id_tipo_doc, id_domicilio, mail )
			SELECT DISTINCT Cli_Nombre, Cli_Apellido, Cli_Fecha_Nac, Cli_Pais_Codigo, Cli_Tipo_Doc_Cod, Cli_Dom_Nro, Cli_Mail
				FROM gd_esquema.Maestra; -- Hay que ver si se hace con distinct porque no se pueden perder filas en la migracion, y asi se estarian perdiendo las repetidas. Habria que manejarlo de otro manera. Se me ocurre insertar todas las filas pero dejar habilitada sola una de las repetidas.
COMMIT;


BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].CUENTAS ON;
INSERT INTO LPP.CUENTAS (id_cliente, num_cuenta, fecha_apertura, id_pais, id_banco, id_moneda, id_tipo) 
			SELECT DISTINCT (SELECT id_cliente FROM LPP.CLIENTES WHERE nombre=Cli_Nombre and apellido=Cli_Apellido) as id_cliente, 
			Cuenta_Numero, Cuenta_Fecha_Creacion, Cuenta_Pais_Codigo, Banco_Cogido,
			(SELECT id_moneda from LPP.MONEDAS where descripcion='Dólares'),
			(SELECT id_tipocuenta FROM LPP.TIPOS_CUENTA WHERE descripcion = 'Gratuita') FROM gd_esquema.Maestra where Banco_Cogido is not null;
SET IDENTITY_INSERT [LPP].CUENTAS OFF;
--RR: Asumí que las cuentas son gratuitas, ya que el tipo de cuenta no está definida en la tabla maestra
COMMIT;

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].EMISORES ON;
	INSERT INTO [LPP].EMISORES (id_emisor, emisor_descr)
		SELECT [Tarjeta_Emisor_Descripcion] FROM [GD1C2015].[gd_esquema].[Maestra] WHERE [Tarjeta_Numero] IS NOT NULL;
SET IDENTITY_INSERT [LPP].EMISORES OFF;        
COMMIT;    

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].TARJETAS ON;
	INSERT INTO [LPP].TARJETAS (num_tarjeta, id_emisor, id_banco, num_cuenta, cod_seguridad, fecha_emision, fecha_vencimiento)
		SELECT [Tarjeta_Numero],(SELECT DISTINCT [id_emisor] FROM [LPP.EMISORES] WHERE [emisor_descr] = [Tarjeta_Emisor_Descripcion])[Cuenta_Numero]),
		[Banco_Cogido], [Cuenta_Numero],[Tarjeta_Codigo_Seg],[Tarjeta_Fecha_Emision],[Tarjeta_Fecha_Vencimiento]
        FROM [GD1C2015].[gd_esquema].[Maestra] WHERE [Tarjeta_Numero] IS NOT NULL;
SET IDENTITY_INSERT [LPP].TARJETAS OFF;        
COMMIT;        

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].DEPOSITOS ON;
	INSERT INTO [LPP.DEPOSITOS] (num_deposito, num_cuenta, importe, id_moneda,num_tarjeta, fecha_deposito, id_banco)
		SELECT [Deposito_Codigo],[Cuenta_Numero],[Deposito_Importe], 1, [Tarjeta_Numero],[Deposito_Fecha],[Banco_Cogido]
	    FROM [GD1C2015].[gd_esquema].[Maestra] WHERE Deposito_Codigo IS NOT NULL
SET IDENTITY_INSERT [LPP].DEPOSITOS OFF;    		
COMMIT;

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].RETIROS ON;
	INSERT INTO [LPP.RETIROS] (id_retiro, num_cuenta, id_banco, importe,fecha)
		SELECT [Retiro_Codigo],[Cuenta_Numero],[Banco_Cogido], [Retiro_Importe], [Retiro_Fecha]
		FROM [GD1C2015].[gd_esquema].[Maestra] WHERE Retiro_Codigo is not null
	INSERT INTO [LPP.CHEQUES] (cheque_num, id_retiro,importe, fecha, id_banco)
		SELECT [Cheque_Numero], [Retiro_Codigo],[Cheque_Importe],[Cheque_Fecha],[Banco_Cogido]
		FROM [GD1C2015].[gd_esquema].[Maestra] WHERE Deposito_Codigo IS NOT NULL AND Cheque_Numero IS NOT NULL
SET IDENTITY_INSERT [LPP].RETIROS OFF;
COMMIT;

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].TRANSFERENCIAS ON;
	INSERT INTO [LPP].TRANSFERENCIAS (num_cuenta_origen, num_cuenta_destino, id_banco_destino, importe , fecha, costo_trans)
		SELECT [Cuenta_Numero], [Cuenta_Dest_Numero], [Banco_Cogido], [Trans_Importe], [Transf_Fecha], [Trans_Costo_Trans]
		FROM [GD1C2015].[gd_esquema].[Maestra] WHERE Transf_Fecha IS NOT NULL
SET IDENTITY_INSERT [LPP].TRANSFERENCIAS OFF;
COMMIT;


-- FALTAN HACER LAS MIGRACIONES EN LAS TABLAS MONEDAS, ITEMS_ PENDIENTES, TRANSACCIONES, ITEM_ FACTURA, FACTURAS


/*---------Definiciones de Vistas-----------*/

/*---------Definiciones de Triggers---------*/

/*---------Definiciones de Procedures-------*/

