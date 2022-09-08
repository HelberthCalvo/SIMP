create database SIMPDB;

use SIMPDB;

create table Rol(
	id int identity not null,
	descripcion varchar(25) not null,
);

alter table Rol add constraint PK_Rol primary key(id);

create table Estado(
	id int identity not null,
	descripcion varchar(25) not null,
);

alter table Estado add constraint PK_Estado primary key(id);

create table Usuario (
	id int identity not null,
	idRol int not null,
	idEstado int not null,
	nombre varchar(50) not null,
	primer_apellido varchar(50) not null,
	segundo_apellido varchar(50) not null,
	usuario varchar(50) not null,
	contrasena varbinary(max) not null,
	
);

alter table Usuario add constraint PK_Usuario primary key(id);
alter table Usuario add constraint FK_Rol_Usuario foreign key(idRol) references Rol(id);
alter table Usuario add constraint FK_Estado_Usuario foreign key(idEstado) references Estado(id);

create table Cliente (
	id int identity not null,
	nombre varchar(50) not null,
	primer_apellido varchar(50) not null,
	segundo_apellido varchar(50) not null,
	correo_electronico varchar(50) not null,
	telefono varchar(15) not null,
);

alter table Cliente add constraint PK_Cliente primary key(id);

create table Proyecto (
	id int identity not null,
	idCliente int not null,
	idEstado int not null,
	nombre varchar(50) not null,
	descripcion varchar(250),
	fecha_inicio date not null,
	fecha_estimada date not null,
	fecha_finalizacion date
);

alter table Proyecto add constraint PK_Proyecto primary key(id);
alter table Proyecto add constraint FK_Cliente_Proyecto foreign key(idCliente) references Cliente(id);
alter table Proyecto add constraint FK_Estado_Proyecto foreign key(idEstado) references Estado(id);

create table UsuarioxProyecto (
	idUsuario int not null,
	idProyecto int not null,
	fecha date
);

alter table UsuarioxProyecto add constraint PK_Usuario_Proyecto primary key(idUsuario,idProyecto);
alter table UsuarioxProyecto add constraint FK_Usuario_UsuarioxProyecto foreign key(idUsuario) references Usuario(id);
alter table UsuarioxProyecto add constraint FK_Proyecto_UsuarioxProyecto foreign key(idProyecto) references Proyecto(id);

create table Fase (
	id int identity not null,
	idProyecto int not null,
	idEstado int not null,
	nombre varchar(50) not null,
	descripcion varchar(250)
);

alter table Fase add constraint PK_Fase primary key(id);
alter table Fase add constraint FK_Proyecto_Fase foreign key(idProyecto) references Proyecto(id);
alter table Fase add constraint FK_Estado_Fase foreign key(idEstado) references Estado(id);

create table Actividad (
	id int identity not null,
	idFase int not null,
	idUsuario int not null,
	idEstado int not null,
	descripcion varchar(250) not null,
	fecha_inicio date not null,
	fecha_estimada date not null,
	fecha_finalizacion date,
);

alter table Actividad add constraint PK_Actividad primary key(id);
alter table Actividad add constraint FK_Fase_Actividad foreign key(idFase) references Fase(id);
alter table Actividad add constraint FK_Usuario_Actividad foreign key(idUsuario) references Usuario(id);
alter table Actividad add constraint FK_Estado_Actividad foreign key(idEstado) references Estado(id);
