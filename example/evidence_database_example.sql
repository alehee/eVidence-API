CREATE DATABASE  IF NOT EXISTS `evidence` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `evidence`;
-- MySQL dump 10.13  Distrib 8.0.30, for Win64 (x86_64)
--
-- Host: localhost    Database: evidence
-- ------------------------------------------------------
-- Server version	8.0.30

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20230815141957_init','6.0.21'),('20230821143744_entrance_history','6.0.21'),('20230903155128_accounts-update','6.0.21'),('20230907182847_entrance-model-fix','6.0.21'),('20230913155038_new-process-2','6.0.21'),('20230913155758_new-process-history','6.0.21'),('20230919185053_process-update','6.0.21'),('20230922162732_processhistory-update','6.0.21'),('20230926175910_administrators','6.0.21');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `accounts`
--

DROP TABLE IF EXISTS `accounts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accounts` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `Surname` longtext NOT NULL,
  `Keycard` longtext,
  `DeletedAt` datetime(6) DEFAULT NULL,
  `DepartmentId` int NOT NULL,
  `CreatedAt` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`),
  KEY `IX_Accounts_DepartmentId` (`DepartmentId`),
  CONSTRAINT `FK_Accounts_Departments_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `departments` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accounts`
--

LOCK TABLES `accounts` WRITE;
/*!40000 ALTER TABLE `accounts` DISABLE KEYS */;
INSERT INTO `accounts` VALUES (3,'Czesław','Baranowski','928471',NULL,4,'2023-12-26 00:00:00.000000'),(4,'Kamil','Kwiatkowski','489321',NULL,5,'2023-12-26 00:00:00.000000'),(5,'Ksawery','Laskowski','432790',NULL,6,'2023-12-26 00:00:00.000000'),(6,'Lucjan','Makowski','593082',NULL,7,'2023-12-26 00:00:00.000000'),(7,'Arkadiusz','Brzeziński','901582',NULL,4,'2023-12-26 00:00:00.000000'),(8,'Jolanta','Maciejewska','194857',NULL,5,'2023-12-26 00:00:00.000000'),(9,'Amelia','Witkowska','999051',NULL,6,'2023-12-26 00:00:00.000000'),(10,'Irena','Górecka','157839',NULL,7,'2023-12-26 00:00:00.000000'),(11,'Ewelina','Krupa','571000',NULL,6,'2023-12-26 00:00:00.000000'),(12,'Karolina','Mazurek','577782',NULL,7,'2023-12-26 00:00:00.000000');
/*!40000 ALTER TABLE `accounts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `administrators`
--

DROP TABLE IF EXISTS `administrators`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `administrators` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Login` longtext NOT NULL,
  `Password` longtext NOT NULL,
  `PermissionAdministrator` tinyint(1) NOT NULL,
  `PermissionUser` tinyint(1) NOT NULL,
  `PermissionProcess` tinyint(1) NOT NULL,
  `PermissionReport` tinyint(1) NOT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `administrators`
--

LOCK TABLES `administrators` WRITE;
/*!40000 ALTER TABLE `administrators` DISABLE KEYS */;
INSERT INTO `administrators` VALUES (3,'Administrator','Rv+qXpWoAAz93THPteHE7iaHHLU9yc7bzt1jQ7K+x7Un5D2H',1,1,1,1,'2023-12-16 13:59:02.160012'),(4,'Martyna','GSvZnEiMKs4zvnX/73StqZ2A6bvUKnAIwG0TZMRU7qxTl9IT',0,1,1,1,'2023-12-16 13:59:50.213054'),(5,'Robert','dNZOZm5Jvkk3xsPfIJLqJwUerIVTpSVlf7lKA7X0R/w6pvlI',0,1,0,0,'2023-12-16 14:00:12.329061'),(6,'Raporty','fs0N+iwu9G/duJI+P3YPk4H36sfsUUtTUcg2oycWHEha4XGU',0,0,0,1,'2023-12-16 14:00:23.986102');
/*!40000 ALTER TABLE `administrators` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `departments`
--

DROP TABLE IF EXISTS `departments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `departments` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `departments`
--

LOCK TABLES `departments` WRITE;
/*!40000 ALTER TABLE `departments` DISABLE KEYS */;
INSERT INTO `departments` VALUES (4,'Przyjęcie'),(5,'Kompletacja'),(6,'Utrzymanie'),(7,'Wysyłka');
/*!40000 ALTER TABLE `departments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `entrancehistory`
--

DROP TABLE IF EXISTS `entrancehistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `entrancehistory` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AccountId` int NOT NULL,
  `Exit` datetime(6) DEFAULT NULL,
  `Enter` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`),
  KEY `IX_EntranceHistory_AccountId` (`AccountId`),
  CONSTRAINT `FK_EntranceHistory_Accounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `entrancehistory`
--

LOCK TABLES `entrancehistory` WRITE;
/*!40000 ALTER TABLE `entrancehistory` DISABLE KEYS */;
INSERT INTO `entrancehistory` VALUES (11,3,'2023-12-13 15:58:44.000000','2023-12-13 07:33:12.000000'),(12,4,'2023-12-13 16:01:00.000000','2023-12-13 07:43:11.000000'),(13,5,'2023-12-13 16:05:45.000000','2023-12-13 07:58:40.000000'),(14,6,'2023-12-13 16:02:55.000000','2023-12-13 07:59:59.000000'),(15,3,'2023-12-14 16:05:50.000000','2023-12-14 07:02:32.000000'),(16,8,'2023-12-14 16:11:32.000000','2023-12-14 07:54:31.000000'),(17,9,'2023-12-14 16:00:14.000000','2023-12-14 07:56:44.000000'),(18,10,'2023-12-14 16:31:00.000000','2023-12-14 08:02:01.000000'),(19,11,'2023-12-14 16:00:18.000000','2023-12-14 08:02:17.000000'),(20,7,'2023-12-15 16:00:00.000000','2023-12-15 07:38:00.000000'),(21,12,'2023-12-15 16:01:09.000000','2023-12-15 07:41:06.000000'),(22,5,'2023-12-15 16:02:18.000000','2023-12-15 07:43:59.000000'),(23,4,'2023-12-15 16:03:27.000000','2023-12-15 07:49:15.000000'),(24,9,'2023-12-15 16:04:36.000000','2023-12-15 07:57:01.000000');
/*!40000 ALTER TABLE `entrancehistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `groupdepartments`
--

DROP TABLE IF EXISTS `groupdepartments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `groupdepartments` (
  `DepartmentsId` int NOT NULL,
  `GroupsId` int NOT NULL,
  PRIMARY KEY (`DepartmentsId`,`GroupsId`),
  KEY `IX_GroupDepartments_GroupsId` (`GroupsId`),
  CONSTRAINT `FK_GroupDepartments_Departments_DepartmentsId` FOREIGN KEY (`DepartmentsId`) REFERENCES `departments` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_GroupDepartments_Groups_GroupsId` FOREIGN KEY (`GroupsId`) REFERENCES `groups` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `groupdepartments`
--

LOCK TABLES `groupdepartments` WRITE;
/*!40000 ALTER TABLE `groupdepartments` DISABLE KEYS */;
INSERT INTO `groupdepartments` VALUES (5,5),(4,6),(5,6),(6,6),(7,6),(4,7),(7,7),(6,8);
/*!40000 ALTER TABLE `groupdepartments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `groups`
--

DROP TABLE IF EXISTS `groups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `groups` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `groups`
--

LOCK TABLES `groups` WRITE;
/*!40000 ALTER TABLE `groups` DISABLE KEYS */;
INSERT INTO `groups` VALUES (5,'Zamówienia'),(6,'Inne'),(7,'Wózki widłowe'),(8,'Utrzymanie');
/*!40000 ALTER TABLE `groups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `processes`
--

DROP TABLE IF EXISTS `processes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `processes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `GroupId` int NOT NULL,
  `Name` longtext NOT NULL,
  `ShortName` longtext NOT NULL,
  `Color` longtext NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Processes_GroupId` (`GroupId`),
  CONSTRAINT `FK_Processes_Groups_GroupId` FOREIGN KEY (`GroupId`) REFERENCES `groups` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `processes`
--

LOCK TABLES `processes` WRITE;
/*!40000 ALTER TABLE `processes` DISABLE KEYS */;
INSERT INTO `processes` VALUES (13,5,'Kompletowanie','K','#84f726'),(14,5,'Kontrola jakości','KJ','#ff1c15'),(15,5,'Korekty dostaw','KD','#3de4b7'),(16,6,'Przerwa','P','#a7a7a7'),(17,6,'Sprzątanie','SP','#a382a6'),(18,7,'Rampa załadunkowa','R','#f79d09'),(19,7,'Utrzymanie wózka','UW','#ab65da'),(20,7,'Wysokie składowanie','W','#5b87fb'),(21,8,'Usterki','U','#fce129'),(22,8,'Optymalizacja','O','#3deaf3'),(23,8,'Inwentaryzacja','I','#e344dc');
/*!40000 ALTER TABLE `processes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `processeshistory`
--

DROP TABLE IF EXISTS `processeshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `processeshistory` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AccountId` int DEFAULT NULL,
  `TemporaryEntranceId` int DEFAULT NULL,
  `DepartmentId` int NOT NULL,
  `ProcessId` int NOT NULL,
  `Start` datetime(6) NOT NULL,
  `Stop` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ProcessesHistory_AccountId` (`AccountId`),
  KEY `IX_ProcessesHistory_DepartmentId` (`DepartmentId`),
  KEY `IX_ProcessesHistory_ProcessId` (`ProcessId`),
  KEY `IX_ProcessesHistory_TemporaryEntranceId` (`TemporaryEntranceId`),
  CONSTRAINT `FK_ProcessesHistory_Accounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`Id`),
  CONSTRAINT `FK_ProcessesHistory_Departments_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `departments` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_ProcessesHistory_Processes_ProcessId` FOREIGN KEY (`ProcessId`) REFERENCES `processes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_ProcessesHistory_TemporaryEntranceHistory_TemporaryEntranceId` FOREIGN KEY (`TemporaryEntranceId`) REFERENCES `temporaryentrancehistory` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `processeshistory`
--

LOCK TABLES `processeshistory` WRITE;
/*!40000 ALTER TABLE `processeshistory` DISABLE KEYS */;
INSERT INTO `processeshistory` VALUES (1,3,NULL,4,18,'2023-12-13 07:54:43.000000','2023-12-13 08:59:01.000000'),(2,3,NULL,4,17,'2023-12-13 08:59:01.000000','2023-12-13 09:12:34.000000'),(3,3,NULL,4,19,'2023-12-13 09:12:34.000000','2023-12-13 10:01:01.000000'),(4,3,NULL,4,18,'2023-12-13 10:01:01.000000','2023-12-13 12:00:16.000000'),(5,3,NULL,4,16,'2023-12-13 12:00:16.000000','2023-12-13 12:30:46.000000'),(6,3,NULL,4,20,'2023-12-13 12:30:46.000000','2023-12-13 15:56:00.000000'),(7,4,NULL,5,15,'2023-12-13 08:01:01.000000','2023-12-13 11:11:00.000000'),(8,4,NULL,5,13,'2023-12-13 11:11:00.000000','2023-12-13 12:00:35.000000'),(9,4,NULL,5,16,'2023-12-13 12:00:35.000000','2023-12-13 12:31:01.000000'),(10,4,NULL,5,13,'2023-12-13 12:31:01.000000','2023-12-13 16:00:52.000000'),(11,5,NULL,6,22,'2023-12-13 07:59:32.000000','2023-12-13 12:02:19.000000'),(12,5,NULL,6,16,'2023-12-13 12:02:19.000000','2023-12-13 12:32:01.000000'),(13,5,NULL,6,23,'2023-12-13 12:33:51.000000','2023-12-13 14:11:09.000000'),(14,5,NULL,6,21,'2023-12-13 14:11:09.000000','2023-12-13 16:18:05.000000'),(15,6,NULL,7,18,'2023-12-13 08:04:01.000000','2023-12-13 11:59:06.000000'),(16,6,NULL,7,16,'2023-12-13 11:59:06.000000','2023-12-13 12:35:00.000000'),(17,6,NULL,7,18,'2023-12-13 12:35:00.000000','2023-12-13 16:00:40.000000'),(18,3,NULL,4,19,'2023-12-14 07:12:34.000000','2023-12-14 07:45:00.000000'),(19,3,NULL,4,18,'2023-12-14 07:45:00.000000','2023-12-14 12:00:54.000000'),(20,3,NULL,4,16,'2023-12-14 12:00:54.000000','2023-12-14 12:43:02.000000'),(21,3,NULL,4,18,'2023-12-14 12:43:02.000000','2023-12-14 16:04:32.000000'),(22,8,NULL,5,14,'2023-12-14 07:58:55.000000','2023-12-14 12:00:00.000000'),(23,8,NULL,5,16,'2023-12-14 12:00:00.000000','2023-12-14 12:30:10.000000'),(24,8,NULL,5,13,'2023-12-14 12:30:10.000000','2023-12-14 15:32:11.000000'),(25,8,NULL,5,17,'2023-12-14 15:32:11.000000','2023-12-14 16:07:08.000000'),(26,9,NULL,6,21,'2023-12-14 07:57:01.000000','2023-12-14 11:54:49.000000'),(27,9,NULL,6,16,'2023-12-14 11:54:49.000000','2023-12-14 12:28:19.000000'),(28,9,NULL,6,23,'2023-12-14 12:28:19.000000','2023-12-14 15:58:59.000000'),(29,10,NULL,7,17,'2023-12-14 08:03:56.000000','2023-12-14 08:46:15.000000'),(30,10,NULL,7,20,'2023-12-14 08:46:15.000000','2023-12-14 12:10:14.000000'),(31,10,NULL,7,16,'2023-12-14 12:10:14.000000','2023-12-14 12:42:54.000000'),(32,10,NULL,7,20,'2023-12-14 12:42:54.000000','2023-12-14 16:28:31.000000'),(33,11,NULL,6,21,'2023-12-14 08:05:18.000000','2023-12-14 12:02:02.000000'),(34,11,NULL,6,16,'2023-12-14 12:02:02.000000','2023-12-14 12:41:56.000000'),(35,11,NULL,6,21,'2023-12-14 12:41:56.000000','2023-12-14 15:55:55.000000'),(36,7,NULL,4,18,'2023-12-15 07:40:40.000000','2023-12-15 11:34:56.000000'),(37,7,NULL,4,17,'2023-12-15 11:34:56.000000','2023-12-15 12:01:06.000000'),(38,7,NULL,4,16,'2023-12-15 12:01:06.000000','2023-12-15 12:32:07.000000'),(39,7,NULL,4,18,'2023-12-15 12:32:07.000000','2023-12-15 15:54:00.000000'),(40,7,NULL,4,20,'2023-12-15 15:54:00.000000','2023-12-15 15:59:01.000000'),(41,12,NULL,7,20,'2023-12-15 07:42:01.000000','2023-12-15 12:00:01.000000'),(42,12,NULL,7,16,'2023-12-15 12:00:01.000000','2023-12-15 12:30:01.000000'),(43,12,NULL,7,20,'2023-12-15 12:30:01.000000','2023-12-15 16:00:01.000000'),(44,5,NULL,6,22,'2023-12-15 07:50:50.000000','2023-12-15 12:02:01.000000'),(45,5,NULL,6,16,'2023-12-15 12:02:01.000000','2023-12-15 12:29:43.000000'),(46,5,NULL,6,21,'2023-12-15 12:29:43.000000','2023-12-15 14:17:05.000000'),(47,5,NULL,6,23,'2023-12-15 14:17:05.000000','2023-12-15 16:01:57.000000'),(48,4,NULL,5,13,'2023-12-15 07:53:37.000000','2023-12-15 11:54:20.000000'),(49,4,NULL,5,16,'2023-12-15 11:54:20.000000','2023-12-15 12:21:53.000000'),(50,4,NULL,5,13,'2023-12-15 12:21:53.000000','2023-12-15 15:55:06.000000'),(51,9,NULL,6,23,'2023-12-15 07:59:29.000000','2023-12-15 09:15:35.000000'),(52,9,NULL,6,21,'2023-12-15 09:15:35.000000','2023-12-15 11:51:01.000000'),(53,9,NULL,6,17,'2023-12-15 11:51:01.000000','2023-12-15 12:17:47.000000'),(54,9,NULL,6,16,'2023-12-15 12:17:47.000000','2023-12-15 12:50:13.000000'),(55,9,NULL,6,21,'2023-12-15 12:50:13.000000','2023-12-15 16:04:00.000000');
/*!40000 ALTER TABLE `processeshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `temporarycards`
--

DROP TABLE IF EXISTS `temporarycards`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `temporarycards` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Keycard` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `temporarycards`
--

LOCK TABLES `temporarycards` WRITE;
/*!40000 ALTER TABLE `temporarycards` DISABLE KEYS */;
INSERT INTO `temporarycards` VALUES (5,'100120'),(6,'100121'),(7,'100122');
/*!40000 ALTER TABLE `temporarycards` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `temporaryentrancehistory`
--

DROP TABLE IF EXISTS `temporaryentrancehistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `temporaryentrancehistory` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TemporaryCardId` int NOT NULL,
  `Name` longtext NOT NULL,
  `Surname` longtext NOT NULL,
  `Exit` datetime(6) DEFAULT NULL,
  `Enter` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`),
  KEY `IX_TemporaryEntranceHistory_TemporaryCardId` (`TemporaryCardId`),
  CONSTRAINT `FK_TemporaryEntranceHistory_TemporaryCards_TemporaryCardId` FOREIGN KEY (`TemporaryCardId`) REFERENCES `temporarycards` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `temporaryentrancehistory`
--

LOCK TABLES `temporaryentrancehistory` WRITE;
/*!40000 ALTER TABLE `temporaryentrancehistory` DISABLE KEYS */;
INSERT INTO `temporaryentrancehistory` VALUES (3,5,'Tymon','Rozerski','2023-12-13 13:11:35.000000','2023-12-13 11:56:49.000000'),(4,7,'Agnieszka','Turnał','2023-12-14 15:03:29.000000','2023-12-14 14:34:15.000000');
/*!40000 ALTER TABLE `temporaryentrancehistory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-12-16 23:23:40
