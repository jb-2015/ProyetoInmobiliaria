-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 24-09-2024 a las 20:45:38
-- Versión del servidor: 10.4.28-MariaDB
-- Versión de PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `auditoria`
--

CREATE TABLE `auditoria` (
  `idAuditoria` int(11) NOT NULL,
  `idUsuario` int(11) NOT NULL,
  `accion` varchar(100) NOT NULL,
  `observacion` text NOT NULL,
  `fechaYHora` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `auditoria`
--

INSERT INTO `auditoria` (`idAuditoria`, `idUsuario`, `accion`, `observacion`, `fechaYHora`) VALUES
(1, 2, 'Crear Contrato', 'Contrato creado para inquilino ID: 1 en inmueble ID: 3.', '2024-09-22 03:57:09'),
(2, 2, 'Crear Contrato', 'Contrato creado para inquilino ID: 2 en inmueble ID: 9.', '2024-09-22 04:02:14'),
(3, 2, 'Crear Contrato', 'Contrato creado para inquilino ID: 2 en inmueble ID: 10.', '2024-09-22 04:13:16'),
(4, 2, 'Crear Pago', 'Pago creado para contrato ID: 27. Importe: 100.', '2024-09-22 04:17:04'),
(5, 2, 'Crear Pago', 'Pago creado para contrato ID: 42. Importe: 1500.', '2024-09-22 04:20:23'),
(6, 2, 'Crear Pago', 'Pago creado para contrato ID: 42. Importe: 2000.', '2024-09-22 04:37:15'),
(7, 2, 'Crear Pago', 'Pago creado para contrato ID: 40. Importe: 1000.', '2024-09-22 04:44:35'),
(8, 2, 'Crear Contrato', 'Contrato creado para inquilino ID: 2 en inmueble ID: 13.', '2024-09-23 22:46:04'),
(9, 2, 'Crear Contrato', 'Contrato creado para inquilino ID: 3 en inmueble ID: 13.', '2024-09-24 00:43:36'),
(10, 4, 'Crear Contrato', 'Contrato creado para inquilino ID: 1 en inmueble ID: 4.', '2024-09-24 01:17:26'),
(11, 2, 'Crear Pago', 'Pago creado para contrato ID: 27. Importe: 4199900.', '2024-09-24 12:17:26'),
(12, 2, 'Crear Pago', 'Pago creado para contrato ID: 42. Importe: 11314.', '2024-09-24 12:18:21'),
(14, 2, 'Crear Pago', 'Pago creado para contrato ID: 46. Importe: 36900000.', '2024-09-24 12:20:41'),
(15, 2, 'Crear Pago', 'Pago creado para contrato ID: 28. Importe: -501234.', '2024-09-24 12:22:57'),
(16, 2, 'Crear Pago', 'Pago creado para contrato ID: 42. Importe: 0.', '2024-09-24 12:23:30'),
(17, 2, 'Crear Contrato', 'Contrato creado para inquilino ID: 5 en inmueble ID: 14.', '2024-09-24 15:30:26'),
(18, 2, 'Crear Pago', 'Pago creado para contrato ID: 47. Importe: 100000.', '2024-09-24 15:30:42');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `idContrato` int(11) NOT NULL,
  `idInquilino` int(11) NOT NULL,
  `idInmueble` int(11) NOT NULL,
  `monto` decimal(10,0) NOT NULL,
  `fechaInicio` date NOT NULL,
  `fechaFin` date NOT NULL,
  `fechaAnulacion` date DEFAULT NULL,
  `estado` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`idContrato`, `idInquilino`, `idInmueble`, `monto`, `fechaInicio`, `fechaFin`, `fechaAnulacion`, `estado`) VALUES
(27, 2, 7, 3000000, '2024-01-02', '2024-04-02', '2024-09-24', 1),
(28, 3, 11, 200000, '2024-01-10', '2024-03-08', '2024-09-24', 1),
(40, 1, 3, 10000, '2024-09-20', '2024-10-09', '0001-01-01', 1),
(42, 2, 9, 12345, '2024-01-20', '2024-11-19', '2024-09-24', 1),
(43, 2, 10, 11111, '2010-10-10', '2011-11-11', '0001-01-01', 1),
(44, 2, 13, 200, '2024-09-11', '2024-11-28', '0001-01-01', 1),
(45, 3, 13, 200000, '2024-09-09', '2024-10-11', '0001-01-01', 1),
(46, 1, 4, 12300000, '2024-09-13', '2024-10-04', '2024-09-24', 1),
(47, 5, 14, 2412321312, '2024-09-06', '2024-09-28', '0001-01-01', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `direccion`
--

CREATE TABLE `direccion` (
  `idDireccion` int(11) NOT NULL,
  `calle` varchar(200) NOT NULL,
  `altura` int(11) NOT NULL,
  `cp` varchar(12) NOT NULL,
  `ciudad` varchar(100) NOT NULL,
  `coordenadas` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `direccion`
--

INSERT INTO `direccion` (`idDireccion`, `calle`, `altura`, `cp`, `ciudad`, `coordenadas`) VALUES
(1, 'Carlos perez', 12, '5324', 'Carlos paz', '123123213123.42'),
(2, 'Mitre', 123, '5012', 'San Jose', '123123 12'),
(3, 'Mitre', 123, '5012', 'San Luis', '123123 12'),
(4, 'Mitre', 123, '5012', 'San Luisss', '123123 12'),
(5, 'Mitre', 123, '5012', 'Buenos Aires', '123123 12'),
(6, 'Lord Baden Powell', 10, '2000', 'San Jose', '123123 12'),
(7, 'Lord Baden Powell', 10, '2000', 'San Jose', '123123 12'),
(8, 'Lord Baden Powell', 10, '2000', 'San Jose', '123123 12'),
(9, 'Lord Baden Powell', 10, '2000d', 'San Jose', '123123 12'),
(10, 'Lord Baden Powell', 10, '2000', 'San Jose', '123123 12'),
(11, 'Lord Baden Powell', 10, '2000', 'San Jose', '123123 12'),
(12, 'Lord Baden Powell', 10, '2000', 'San Jose', '123123 12'),
(13, 'Lord Baden Powell', 10, '2000', 'San Jose', '123123 12'),
(14, 'Guemes ', 20, '5715', 'Los Angeles', '1234 1232'),
(15, 'Guemes ', 20, '5715', 'Los Angeles', '1234 1232');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `idInmueble` int(11) NOT NULL,
  `idPropietario` int(11) NOT NULL,
  `idDireccion` int(11) NOT NULL,
  `idTipo` int(11) NOT NULL,
  `metros2` varchar(100) NOT NULL,
  `cantidadAmbientes` int(11) NOT NULL,
  `disponible` tinyint(60) NOT NULL,
  `precio` decimal(10,0) NOT NULL,
  `descripcion` varchar(300) NOT NULL,
  `cochera` tinyint(4) NOT NULL,
  `piscina` tinyint(4) NOT NULL,
  `mascotas` tinyint(4) NOT NULL,
  `urlImagen` varchar(150) NOT NULL,
  `estado` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`idInmueble`, `idPropietario`, `idDireccion`, `idTipo`, `metros2`, `cantidadAmbientes`, `disponible`, `precio`, `descripcion`, `cochera`, `piscina`, `mascotas`, `urlImagen`, `estado`) VALUES
(3, 2, 1, 8, '123', 2123, 1, 1000, 'Lindo inmueble', 1, 1, 1, 'https://imgar.zonapropcdn.com/avisos/1/00/50/31/01/85/360x266/1930226378.jpg?isFirstImage=true', 0),
(4, 2, 1, 14, '2', 2, 1, 2, 'Departamento en nueva cordoba', 1, 0, 0, 'https://pics.nuroa.com/departamento_en_venta_en_villa_carlos_paz_centrico_cordoba_37_m2_2360009724864991483.jpg', 1),
(7, 10, 1, 10, '12', 3, 1, 1, 'Cabaña a las afuera de Villa Carlos Paz', 1, 1, 1, 'https://cf.bstatic.com/xdata/images/hotel/max1024x768/440749717.jpg?k=83f268be156bdbded33925621e94b30433fe9b2ab892a8e8f1b0e9b010975549&o=&hp=1', 1),
(8, 2, 1, 14, '1000', 2, 0, 500000, 'Departamento en Villa Carlos Paz', 0, 0, 0, 'https://cf.bstatic.com/xdata/images/hotel/max1024x768/315048793.jpg?k=84f3d87ae94b55202a0a589b18ce58131fa536e97cf1eacdc3e6cca53ae643e3&o=&hp=1', 1),
(9, 9, 1, 8, '2000', 3, 0, 410000, 'Departamento en villa carlos paz', 0, 0, 1, 'https://cf.bstatic.com/xdata/images/hotel/max1024x768/315048793.jpg?k=84f3d87ae94b55202a0a589b18ce58131fa536e97cf1eacdc3e6cca53ae643e3&o=&hp=1', 1),
(10, 2, 2, 6, '1,450', 2, 1, 111, 'Depto de juan', 0, 1, 0, 'https://photos.zillowstatic.com/fp/f9bbd7d817a6e6a324e32373cfde69b8-sc_768_512.webp', 1),
(11, 2, 5, 6, '1,450', 2, 1, 111, 'Depto de juan becerra', 0, 1, 0, 'https://photos.zillowstatic.com/fp/f9bbd7d817a6e6a324e32373cfde69b8-sc_768_512.webp', 1),
(12, 2, 4, 6, '1,450', 2, 1, 111, 'Depto de juan', 0, 1, 0, 'https://photos.zillowstatic.com/fp/f9bbd7d817a6e6a324e32373cfde69b8-sc_768_512.webp', 1),
(13, 2, 13, 11, '323', 12, 1, 800, 'Casa con una buena pileta', 0, 1, 0, 'https://storage.googleapis.com/portales-prod-images/204/property-images/2023/4/b04e4c24-5564-405d-a427-f40aab7b924c.jpeg', 1),
(14, 1, 15, 11, '1000', 16, 1, 30000000, 'Mansion en LA', 1, 1, 1, 'https://st3.idealista.com/news/archivos/styles/fullwidth_xl/public/2020-06/im-191825.jpg?VersionId=H30j04J4vZFBf8nX6Otwwe0l4Vjfo96b&itok=lgCc-eBK', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `idInquilino` int(11) NOT NULL,
  `dni` varchar(10) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `telefono` varchar(60) NOT NULL,
  `correo` varchar(150) NOT NULL,
  `estado` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`idInquilino`, `dni`, `apellido`, `nombre`, `telefono`, `correo`, `estado`) VALUES
(1, '12', 'Fernandez', 'Diego', '246685845', 'diego@gmail.com', 1),
(2, '44480378', 'Moyano Guzmán', 'Ignacio', '2657356970', 'nachomoyag@gmail.com', 1),
(3, '12345678', 'García', 'Laura', '600123456', 'laura.garcia@example.com', 1),
(4, '23456789B', 'Martínez', 'Javier', '600234567', 'javier.martinez@example.com', 1),
(5, '34567890C', 'López', 'María', '600345678', 'maria.lopez@example.com', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `idPago` int(11) NOT NULL,
  `idContrato` int(11) NOT NULL,
  `fechaPago` date NOT NULL,
  `importe` decimal(10,0) NOT NULL,
  `numeroPago` varchar(50) NOT NULL,
  `detalle` varchar(200) NOT NULL,
  `estado` tinyint(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`idPago`, `idContrato`, `fechaPago`, `importe`, `numeroPago`, `detalle`, `estado`) VALUES
(2, 27, '2024-09-20', 300000, '123456', 'Pago a 3 dias del vencimiento', 1),
(3, 27, '2024-09-23', 500000, '123', 'Pago a 3 dias del vencimiento', 1),
(4, 28, '2024-09-21', 900000, '1', 'Pago a 3 dias del vencimiento', 1),
(5, 28, '2024-09-26', 1234, '2', 'asdasd', 1),
(6, 27, '2231-02-20', 100, '4', '1231ADSASDA', 1),
(7, 42, '2024-09-22', 1500, '2', 'PRIMER PAGO', 1),
(8, 42, '2024-09-20', 2000, '2', 'pago nuevo', 0),
(9, 40, '2024-09-20', 1000, '1231', '121312', 1),
(10, 27, '2024-09-24', 4199900, '0', 'Multa por contrato anulado', 1),
(11, 42, '2024-09-24', 11314, '0', 'Multa por contrato anulado', 1),
(12, 42, '2024-09-24', 0, '0', 'Multa por contrato anulado', 1),
(13, 46, '2024-09-24', 36900000, '0', 'Multa por contrato anulado', 1),
(14, 28, '2024-09-24', -501234, '0', 'Multa por contrato anulado', 1),
(15, 42, '2024-09-24', 0, '0', 'Multa por contrato anulado', 1),
(16, 47, '2024-09-24', 10000, '1', 'Primer pago', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `idPropietario` int(11) NOT NULL,
  `dni` varchar(10) NOT NULL,
  `apellido` varchar(60) NOT NULL,
  `nombre` varchar(60) NOT NULL,
  `telefono` varchar(15) NOT NULL,
  `correo` varchar(100) NOT NULL,
  `estado` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`idPropietario`, `dni`, `apellido`, `nombre`, `telefono`, `correo`, `estado`) VALUES
(1, '12345', 'Moyano', 'Nacho', '234526', 'nacho@gmail.com', 1),
(2, '12123123', 'Becerrra', 'Juan', '37245235', 'juan@gmail.com', 1),
(3, '123', 'Escudero', 'Marilena', '1354321', 'mari@', 1),
(8, '11100001', 'Bertolini', 'Maria Ines', '124431212', 'ines@gmail.com', 1),
(9, '56789012E', 'González', 'Ana', '600567890', 'ana.gonzalez@example.com', 1),
(10, '67890123F', 'Pérez', 'Luis', '600678901', 'luis.perez@example.com', 1),
(11, '41245732', 'Rudiger', 'Antonio', '265878265', 'rudiger@gmail.com', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo`
--

CREATE TABLE `tipo` (
  `idTipo` int(11) NOT NULL,
  `observacion` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipo`
--

INSERT INTO `tipo` (`idTipo`, `observacion`) VALUES
(10, 'Cabaña'),
(6, 'Campo'),
(11, 'Casa'),
(14, 'Departamento'),
(8, 'Edificio');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `idUsuario` int(11) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `rol` varchar(50) NOT NULL,
  `avatar` varchar(150) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `estado` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`idUsuario`, `email`, `password`, `rol`, `avatar`, `nombre`, `apellido`, `estado`) VALUES
(2, 'nachomoyag@gmail.com', '$2a$12$WNaPCMdtqQLv5FKruY3iHeEYz6UFBx6sC3i/tfazxyusK5vOh0bea', 'Administrador', '/img/Avatar/default.jpg', 'Ignacio', 'Moyano Guzmán', 1),
(3, 'carlos@gmail', '$2a$12$649jxapLpF1Nq3YhtFUa/uGzGDQ3NdWgeDVJj45UzZSMRn2SOhscC', 'Administrador', '/img/Avatar/default.jpg', 'Juan', 'Fernandez', 1),
(4, 'pepe@gmail', '$2a$12$beh4c8g26FYfZZ75bdM4M.XAFJbebyS5lR2j0BPYW2moxYD2Gct3S', 'Empleado', '/img/Avatar/default.jpg', 'pepe', 'pepe', 1),
(5, 'carlitos@carlitos', '$2a$12$6wrIjY9DK8XKNnywJK.TvOBqab9ecimtPoUJbezBCL2/Dfj8bXpkW', 'Administrador', '/img/Avatar/default.jpg', 'carlitos', 'carlitos', 1),
(6, 'juan@gmail.com', '$2a$12$FFuETpPr5K/ISDa1z7RUieVCuigRVV4yvS60R62ts79q1sZBjuHce', 'Empleado', '/img/Avatar/default.jpg', 'Juan', 'Becerrrad', 1),
(8, 'peit@gmail.com', '$2a$12$K0N0zmTnCBPnUhy/5cVS/uFN0s5gDKk6HuRiD6bVD8n5tVZbWW3Vy', 'Empleado', '/img/Avatar/default.jpg', 'Juan', 'Moyano Guzmán', 1),
(9, 'aaa@gmai', '$2a$12$./kCHY789ggueLDVEfLvwusOVvcwzyyOg0ugo/X0GJN6rb.apbYzS', 'Empleado', '/img/Avatar/default.jpg', 'pepe', 'Fernandez', 1),
(10, 'aaa@asdas', '$2a$12$ayciiUF8QtSLIwxXrI6F6OCP1esq.wFfu6Pqqf4bB0UF/KNb/eJOK', 'Empleado', '/img/Avatar/default.jpg', 'Ignacio', 'Becerrrad', 1),
(11, 'nachomoyag@gmail.comsss', '$2a$12$tcr68BAy14PX4/BMLjjiLuZWzqfkwsShpp.i2QXGjsZnHR89j1eme', 'Empleado', '/img/Avatar/Marvel image.jpg', 'Juan', 'Moyano Guzmán', 0),
(12, 'angelito@gmail.com', '$2a$12$0Xpqe3TB/DKFQWVVH7CZ1u8vvIeLK8LJbS8a3hvN7hq.oRVONgvoy', 'Empleado', '/img/Avatar/img_avatar.png', 'Angel', 'Di maria', 1),
(13, 'ana@gmail', '$2a$12$S18CRxVhVtXOUbT0gjv9UeRfmpajzVMmMs1MO02A1uyJflZnsFlPq', 'Empleado', '/img/Avatar/img_avatar.png', 'analia', 'martinez', 1);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `auditoria`
--
ALTER TABLE `auditoria`
  ADD PRIMARY KEY (`idAuditoria`);

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`idContrato`),
  ADD KEY `idInquilino` (`idInquilino`,`idInmueble`),
  ADD KEY `idInmueble` (`idInmueble`);

--
-- Indices de la tabla `direccion`
--
ALTER TABLE `direccion`
  ADD PRIMARY KEY (`idDireccion`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`idInmueble`),
  ADD KEY `idDireccion` (`idDireccion`,`idTipo`),
  ADD KEY `idPropietario` (`idPropietario`),
  ADD KEY `idTipo` (`idTipo`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`idInquilino`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`idPago`),
  ADD KEY `idContrato` (`idContrato`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`idPropietario`);

--
-- Indices de la tabla `tipo`
--
ALTER TABLE `tipo`
  ADD PRIMARY KEY (`idTipo`),
  ADD UNIQUE KEY `observacion` (`observacion`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`idUsuario`),
  ADD UNIQUE KEY `email` (`email`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `auditoria`
--
ALTER TABLE `auditoria`
  MODIFY `idAuditoria` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=48;

--
-- AUTO_INCREMENT de la tabla `direccion`
--
ALTER TABLE `direccion`
  MODIFY `idDireccion` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `tipo`
--
ALTER TABLE `tipo`
  MODIFY `idTipo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`idInquilino`) REFERENCES `inquilino` (`idInquilino`),
  ADD CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`idInmueble`) REFERENCES `inmueble` (`idInmueble`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`idTipo`) REFERENCES `tipo` (`idTipo`),
  ADD CONSTRAINT `inmueble_ibfk_2` FOREIGN KEY (`idDireccion`) REFERENCES `direccion` (`idDireccion`),
  ADD CONSTRAINT `inmueble_ibfk_3` FOREIGN KEY (`idPropietario`) REFERENCES `propietario` (`idPropietario`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`idContrato`) REFERENCES `contrato` (`idContrato`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
