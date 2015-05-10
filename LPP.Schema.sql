USE [GD1C2015] 
GO
CREATE SCHEMA [LPP] AUTHORIZATION [gd]
GO

/*---------Definiciones de Tabla-------------*/

CREATE TABLE [LPP].();

CREATE TABLE [LPP].USUARIOS(
/*id_usuario INTEGER NOT NULL, IDENTITY(1,1),*/
username VARCHAR(20) NOT NULL,
pass VARCHAR(20) NOT NULL,
pregunta_secreta VARCHAR(50),
respuesta_secreta VARCHAR(50),
fecha_creacion DATE,
fecha_ultimamodif DATE,
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
tipo_doc VARCHAR(10),
num_doc DECIMAL(20, 0),
fecha_nac DATE,
mail VARCHAR(50),
id_domicilio INTEGER,
id_nacionalidad INTEGER,
PRIMARY KEY(id_cliente),
UNIQUE(tipo_doc, num_doc, apellido, nombre, fecha_nac));

CREATE TABLE [LPP].TIPO_DOCS(
tipo VARCHAR(10) NOT NULL,
PRIMARY KEY(tipo));

CREATE TABLE [LPP].DOMICILIOS(
id_domicilio INTEGER NOT NULL IDENTITY(1,1),
calle VARCHAR(100),
num INTEGER,
depto VARCHAR(5),
piso INTEGER, 
localidad VARCHAR(100),
id_pais INTEGER,
PRIMARY KEY(id_domicilio));

CREATE TABLE [LPP].NACIONALIDADES(
id_nac INTEGER NOT NULL IDENTITY(1,1),
nacionalidad VARCHAR(50),
PRIMARY KEY(id_nac));

CREATE TABLE [LPP].CUENTAS(
num_cuenta INTEGER NOT NULL IDENTITY(1,1),
id_cliente INTEGER NOT NULL,
id_banco INTEGER NOT NULL,
saldo DECIMAL,
id_moneda INTEGER NOT NULL,
fecha_apertura DATE,
id_tipo INTEGER NOT NULL,
id_estado INTEGER,
id_pais INTEGER, 
PRIMARY KEY(num_cuenta, id_banco));

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
costo_apertura INTEGER NOT NULL,
costo_cambio INTEGER NOT NULL, 
id_domicilio INTEGER NOT NULL,
PRIMARY KEY(id_banco));

CREATE TABLE [LPP].TARJETAS(
num_tarjeta INTEGER NOT NULL,
id_banco INTEGER NOT NULL,
num_cuenta INTEGER NOT NULL,
marca VARCHAR(20),
cod_seguridad INTEGER NOT NULL,
fecha_emision DATE,
fecha_vencimiento DATE,
PRIMARY KEY(num_tarjeta));

CREATE TABLE [LPP].DEPOSITOS(
num_deposito INTEGER NOT NULL IDENTITY(1,1),
num_cuenta INTEGER NOT NULL,
importe DECIMAL NOT NULL,
id_moneda INTEGER,
num_tarjeta INTEGER,
fecha_deposito DATE,
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
fecha DATE,
PRIMARY KEY(id_retiro));

CREATE TABLE [LPP].TRANSFERENCIAS(
id_transferencia INTEGER NOT NULL IDENTITY(1,1),
id_origen INTEGER NOT NULL,
id_banco_origen INTEGER NOT NULL,
id_destino INTEGER NOT NULL,
id_banco_destino INTEGER NOT NULL,
importe decimal,
PRIMARY KEY(id_transferencia));

CREATE TABLE [LPP].ITEMS_PENDIENTES(
id_item INTEGER NOT NULL IDENTITY(1,1),
num_cuenta INTEGER NOT NULL,
monto DECIMAL,
id_transaccion INTEGER,
estado BIT, 
PRIMARY KEY(id_item));

CREATE TABLE [LPP].ITEMS_FACTURA(
id_items_factura INTEGER NOT NULL IDENTITY(1,1),
id_factura INTEGER NOT NULL,
id_item_pendiente INTEGER NOT NULL,
PRIMARY KEY(id_items_factura));

CREATE TABLE [LPP].FACTURAS(
id_factura INTEGER NOT NULL IDENTITY (1,1),
id_cliente INTEGER NOT NULL,
id_banco INTEGER NOT NULL,
fecha DATE, 
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
							FOREIGN KEY (tipo_doc) references LPP.TIPO_DOCS,
							FOREIGN KEY (id_domicilio) references LPP.DOMICILIOS,
							FOREIGN KEY (id_nacionalidad) references LPP.NACIONALIDADES;
							
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
							FOREIGN KEY (num_cuenta, id_banco) references LPP.CUENTAS;
							
ALTER TABLE LPP.DEPOSITOS ADD
							FOREIGN KEY (id_moneda) references LPP.MONEDAS,
							FOREIGN KEY (num_tarjeta) references LPP.TARJETAS;
-- TODO: Tiene un campo numero de cuenta pero no un id_banco, por lo cual se complica la referencia con la tabla
-- de cuentas, revisar si hay que agregar un campo de id_banco para hacer una FK compuesta

ALTER TABLE LPP.RETIROS ADD
							FOREIGN KEY (id_cliente) references LPP.CLIENTES,
							FOREIGN KEY (id_banco) references LPP.BANCOS;

ALTER TABLE LPP.TRANSFERENCIAS ADD
							FOREIGN KEY (id_origen, id_banco_origen) references LPP.CUENTAS,
							FOREIGN KEY (id_destino, id_banco_destino) references LPP.CUENTAS;
							
ALTER TABLE LPP.ITEMS_PENDIENTES ADD
							FOREIGN KEY (id_transaccion) references LPP.TRANSACCIONES;
--TODO: El mismo problema con la tabla de cuentas

ALTER TABLE LPP.ITEMS_FACTURA ADD
							FOREIGN KEY (id_factura) references LPP.FACTURAS,
							FOREIGN KEY (id_item_pendiente) references LPP.ITEMS_PENDIENTES;
							
ALTER TABLE LPP.FACTURAS ADD
							FOREIGN KEY (id_cliente) references LPP.CLIENTES,
							FOREIGN KEY (id_banco) references LPP.BANCOS;


/*---------Carga de datos--------------------*/

/*---------Migracion-------------------------*/

/*---------Definicioners de Vistas-----------*/

/*---------Definicioners de Triggers---------*/

/*---------Definicioners de Procedures-------*/
