USE [GD1C2015] 

IF NOT EXISTS (
SELECT  schema_name
FROM    information_schema.SCHEMATA
WHERE   schema_name = 'LPP' ) 

BEGIN
EXEC sp_executesql N'CREATE SCHEMA LPP '
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
rol VARCHAR(50) NOT NULL,
PRIMARY KEY(id_rolxusuario));

CREATE TABLE [LPP].ROLES(
nombre VARCHAR(50) NOT NULL,
habilitado BIT DEFAULT 1,
PRIMARY KEY(nombre));

CREATE TABLE [LPP].FUNCIONALIDAD(
id_funcionalidad SMALLINT NOT NULL,
descripcion VARCHAR(50),
PRIMARY KEY(id_funcionalidad));
 
CREATE TABLE [LPP].FUNCIONALIDADXROL(
id BIGINT IDENTITY(1,1) NOT NULL,
rol VARCHAR(50) NOT NULL,
funcionalidad SMALLINT NOT NULL,
PRIMARY KEY(id)); 

CREATE TABLE [LPP].LOGSXUSUARIO(
id_log INTEGER NOT NULL IDENTITY(1,1),
username VARCHAR(255) NOT NULL,
fecha DATETIME, -- INCLUYE LA HORA Y FECHA
num_intento BIT, -- 1 ES LOGIN CORRECTO Y 0 ES LOGIN INCORRECTO
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
id_cliente INTEGER NOT NULL,
fecha_emision DATETIME,
fecha_vencimiento DATETIME,
PRIMARY KEY(num_tarjeta));

CREATE TABLE [LPP].DEPOSITOS(
num_deposito NUMERIC(18,0) NOT NULL IDENTITY(1,1),
num_cuenta NUMERIC(18,0) NOT NULL,--FF: en la maestra no hay datos de cuenta en los depositos, que hacemos?
importe NUMERIC(18,2) NOT NULL, --RR TODO: Poner bancos default
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
cheque_num NUMERIC(18,0) NOT NULL,
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
id_item_factura INTEGER NOT NULL IDENTITY(1,1),
descripcion VARCHAR(255) NOT NULL,
num_cuenta NUMERIC(18,0) NOT NULL,
monto NUMERIC(18,2),
facturado BIT, 
id_factura NUMERIC(18,0),
PRIMARY KEY(id_item_factura));

CREATE TABLE [LPP].FACTURAS(
id_factura NUMERIC(18,0) NOT NULL IDENTITY (1,1),
id_cliente NUMERIC(18, 0) NOT NULL,
fecha DATETIME, 
PRIMARY KEY(id_factura));

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
							FOREIGN KEY (id_pais) references LPP.PAISES;
								
ALTER TABLE LPP.CUENTAS ADD
							FOREIGN KEY (id_cliente) references LPP.CLIENTES,
							FOREIGN KEY (id_moneda) references LPP.MONEDAS,
							FOREIGN KEY (id_tipo) references LPP.TIPOS_CUENTA,
							FOREIGN KEY (id_estado) references LPP.ESTADOS_CUENTA,
							FOREIGN KEY (id_pais) references LPP.PAISES;
							
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
							FOREIGN KEY (num_cuenta) references LPP.CUENTAS;
												
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
BEGIN TRANSACTION
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Administrador', 1);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Administrador', 2);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Administrador', 3);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Administrador', 4);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Administrador', 5);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Administrador', 6);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Administrador', 7);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Administrador', 8);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Administrador', 9);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Administrador', 10);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Administrador', 11);


INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Cliente', 3);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Cliente', 4);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Cliente', 5);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Cliente', 7);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Cliente', 8);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Cliente', 10);
INSERT INTO LPP.FUNCIONALIDADXROL (rol, funcionalidad) VALUES ('Cliente', 11);
COMMIT

/*Creacion de Usuarios Admin -HASH del password w23e: 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0dc9be7'*/
BEGIN TRANSACTION 
INSERT INTO LPP.USUARIOS (username, pass, fecha_creacion) VALUES('admin', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0dc9be7', GETDATE());
INSERT INTO LPP.USUARIOS (username, pass, fecha_creacion) VALUES('admin2', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0dc9be7', GETDATE());


INSERT INTO LPP.ROLESXUSUARIO (rol, username) VALUES ('Administrador', 'admin');
INSERT INTO LPP.ROLESXUSUARIO (rol, username)	VALUES ('Administrador', 'admin2');
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
		SELECT DISTINCT REPLACE(Cli_Nombre+Cli_Apellido,' ',''), 'd74ff0ee8da3b9806b18c877dbf29bbde50b5bd8e4dad7a3a725000feb82e8f1' , GETDATE() FROM gd_esquema.Maestra; --HASH sha56: pass
	INSERT INTO LPP.CLIENTES (username, nombre, apellido, fecha_nac, id_pais, id_tipo_doc, num_doc, id_domicilio, mail) 
		SELECT DISTINCT REPLACE(Cli_Nombre+Cli_Apellido,' ',''), Cli_Nombre, Cli_Apellido, Cli_Fecha_Nac, Cli_Pais_Codigo, Cli_Tipo_Doc_Cod, Cli_Nro_Doc,
				(SELECT id_domicilio FROM LPP.DOMICILIOS WHERE num= Cli_Dom_Nro AND calle = Cli_Dom_Calle AND depto = Cli_Dom_Depto ), Cli_Mail
		FROM gd_esquema.Maestra;
COMMIT; 

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].CUENTAS ON;
INSERT INTO LPP.CUENTAS (id_cliente, num_cuenta, saldo, fecha_apertura, id_pais, id_moneda, id_tipo, id_estado) 
			SELECT DISTINCT (SELECT id_cliente FROM LPP.CLIENTES WHERE nombre=Cli_Nombre and apellido=Cli_Apellido) as id_cliente, 
			Cuenta_Numero, 0,Cuenta_Fecha_Creacion, Cuenta_Pais_Codigo,(SELECT id_moneda from LPP.MONEDAS where descripcion='Dólares'),
			(SELECT id_tipocuenta FROM LPP.TIPOS_CUENTA WHERE descripcion = 'Gratuita'), 4 FROM gd_esquema.Maestra; --id_estado = 4 cuenta habilitada
SET IDENTITY_INSERT [LPP].CUENTAS OFF;
--RR: Asumí que las cuentas son gratuitas, ya que el tipo de cuenta no está definida en la tabla maestra
COMMIT; 
	
BEGIN TRANSACTION
	INSERT INTO [LPP].TARJETAS (num_tarjeta, id_emisor, cod_seguridad, fecha_emision, fecha_vencimiento, id_cliente)
		SELECT DISTINCT [Tarjeta_Numero],(SELECT DISTINCT [id_emisor] FROM [LPP].EMISORES WHERE [emisor_descr] = m.[Tarjeta_Emisor_Descripcion])'idemisor',
		[Tarjeta_Codigo_Seg],[Tarjeta_Fecha_Emision],[Tarjeta_Fecha_Vencimiento],(SELECT id_cliente FROM LPP.CLIENTES WHERE nombre=Cli_Nombre and apellido=Cli_Apellido)
        FROM [GD1C2015].[gd_esquema].[Maestra] m  WHERE [Tarjeta_Numero] IS NOT NULL;   
COMMIT;      

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].DEPOSITOS ON;
	INSERT INTO [LPP].DEPOSITOS (num_deposito, num_cuenta, importe, id_moneda,num_tarjeta, fecha_deposito)
		SELECT [Deposito_Codigo],[Cuenta_Numero],[Deposito_Importe], 1, [Tarjeta_Numero],[Deposito_Fecha]
	    FROM [GD1C2015].[gd_esquema].[Maestra] WHERE Deposito_Codigo IS NOT NULL
SET IDENTITY_INSERT [LPP].DEPOSITOS OFF;    		
COMMIT;

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].RETIROS ON;
	INSERT INTO [LPP].RETIROS (id_retiro, num_cuenta, importe,fecha)
		SELECT [Retiro_Codigo],[Cuenta_Numero],[Retiro_Importe], [Retiro_Fecha]
		FROM [GD1C2015].[gd_esquema].[Maestra] WHERE Retiro_Codigo is not null
	INSERT INTO [LPP].CHEQUES (cheque_num, id_retiro,importe, fecha, id_banco, cliente_receptor)
		SELECT [Cheque_Numero], [Retiro_Codigo],[Cheque_Importe],[Cheque_Fecha],[Banco_Cogido], (SELECT id_cliente FROM LPP.CLIENTES WHERE nombre=Cli_Nombre and apellido=Cli_Apellido)
				FROM [GD1C2015].[gd_esquema].[Maestra] WHERE Retiro_Codigo IS NOT NULL AND Cheque_Numero IS NOT NULL
SET IDENTITY_INSERT [LPP].RETIROS OFF;
COMMIT;

BEGIN TRANSACTION
	INSERT INTO [LPP].TRANSFERENCIAS (num_cuenta_origen, num_cuenta_destino, importe , fecha, costo_trans)
		SELECT [Cuenta_Numero], [Cuenta_Dest_Numero], [Trans_Importe], [Transf_Fecha], [Trans_Costo_Trans]
		FROM [GD1C2015].[gd_esquema].[Maestra] WHERE Transf_Fecha IS NOT NULL
COMMIT;

BEGIN TRANSACTION
SET IDENTITY_INSERT [LPP].FACTURAS ON;
	INSERT INTO [LPP].FACTURAS (id_factura, fecha, id_cliente)
		SELECT distinct [Factura_Numero], [Factura_Fecha], (SELECT id_cliente FROM LPP.CLIENTES WHERE nombre=Cli_Nombre and apellido=Cli_Apellido)
		FROM [GD1C2015].[gd_esquema].[Maestra] where [Factura_Numero] IS NOT NULL
SET IDENTITY_INSERT [LPP].FACTURAS OFF;
COMMIT;

BEGIN TRANSACTION
	INSERT INTO [LPP].ITEMS_FACTURA (id_factura, descripcion, num_cuenta,monto,facturado)
	SELECT [Factura_Numero], [Item_Factura_Descr],[Cuenta_Numero], [Item_Factura_Importe], 1
	FROM [GD1C2015].gd_esquema.Maestra WHERE Item_Factura_Descr IS NOT NULL
COMMIT;

-- FALTAN HACER LAS MIGRACIONES EN LAS TABLAS ITEMS_ PENDIENTES, TRANSACCIONES.

/*---------Definiciones de Funciones--------*/

--CREATE FUNCTION encriptarTarjeta ();
--CREATE FUNCTION desencriptarTarjeta();


/*---------Definiciones de Vistas-----------*/

/*---------Definiciones de Triggers---------*/

-- cada vez que hay una transferencia insertar item de factura con descripcion costo por transferencia
	
--cada vez que hay un cambio de cuenta insertar un item de factura
--cada vez que hay un cambio en el tipo de cuenta insertar item de factura
--cada vez que ay una apartura de una cuenta insertar item de factura

/*---------Definiciones de Procedures-------*/

