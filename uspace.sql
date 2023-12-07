-- MySQL dump 10.13  Distrib 5.7.44, for Win64 (x86_64)
--
-- Host: localhost    Database: uspace
-- ------------------------------------------------------
-- Server version	5.7.44-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `contact`
--

DROP TABLE IF EXISTS `contact`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contact` (
  `CID` varchar(50) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Phone` varchar(20) DEFAULT NULL,
  `Email` varchar(255) NOT NULL,
  `ImgPath` varchar(255) DEFAULT NULL,
  `UID` varchar(50) NOT NULL,
  PRIMARY KEY (`CID`),
  KEY `UID` (`UID`),
  CONSTRAINT `contact_ibfk_1` FOREIGN KEY (`UID`) REFERENCES `user` (`UID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contact`
--

LOCK TABLES `contact` WRITE;
/*!40000 ALTER TABLE `contact` DISABLE KEYS */;
/*!40000 ALTER TABLE `contact` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `todo`
--

DROP TABLE IF EXISTS `todo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `todo` (
  `TID` varchar(50) NOT NULL,
  `Content` varchar(255) DEFAULT NULL,
  `Date` date DEFAULT NULL,
  `Priority` int(11) DEFAULT NULL,
  `IsDone` int(11) DEFAULT NULL,
  `Teammate` varchar(255) DEFAULT NULL,
  `UID` varchar(50) NOT NULL,
  PRIMARY KEY (`TID`),
  KEY `UID` (`UID`),
  CONSTRAINT `todo_ibfk_1` FOREIGN KEY (`UID`) REFERENCES `user` (`UID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `todo`
--

LOCK TABLES `todo` WRITE;
/*!40000 ALTER TABLE `todo` DISABLE KEYS */;
INSERT INTO `todo` VALUES ('031d4f76-24ef-47d1-960a-124f8c517f91','联系人增删改查','2023-12-08',0,0,'无','a241898c-a7d9-41bd-b390-6b6194e58d21'),('10181f1f-75eb-4210-ade4-3fd369fe1478','登出按钮','2023-12-08',0,0,'无','f335a91f-dac1-4eee-9091-82781472ade3'),('65ddb831-748b-4d65-b5e0-53c508cdfc97','登录注册UI修改','2023-12-08',0,0,'无','30ac9590-ae09-4167-82cc-a6a158fa93f5'),('98cb3c75-0d90-4e7e-896f-f7114ebc9b2b','关联人图片显示','2023-12-08',0,0,'无','30ac9590-ae09-4167-82cc-a6a158fa93f5'),('a58143e5-0cb5-474e-be68-e3618aff2337','联系人信息格式判断','2023-12-08',0,0,'曾思琦;','30ac9590-ae09-4167-82cc-a6a158fa93f5');
/*!40000 ALTER TABLE `todo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `UID` varchar(50) NOT NULL,
  `Nickname` varchar(255) NOT NULL,
  `DateOfBirth` date NOT NULL,
  `Email` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `JsonFilePath` varchar(255) NOT NULL,
  PRIMARY KEY (`UID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES ('30ac9590-ae09-4167-82cc-a6a158fa93f5','Karen','2003-03-27','2857809611@qq.com','$2a$10$mZuGUkhN6fmR4GFjLzGnEe6AcyIgOPRkwFec3FzPAJOyCCdl5EMSm','D:\\0temp\\USpace\\ToDo\\dashboard.json'),('a241898c-a7d9-41bd-b390-6b6194e58d21','Sandy','2023-12-05','2066892296@qq.com','$2a$10$Yq55dW4.YcfQfwiLuVFlYOt.KG/ec0ffwFE6v8wqSxvh0h7TcQ1tC','../../../AppData/a241898c-a7d9-41bd-b390-6b6194e58d21.json'),('f335a91f-dac1-4eee-9091-82781472ade3','Karim','2023-12-06','2021302111352@whu.edu.cn','$2a$10$fiN/ocU9dYJ6xNn/mdH4a.iabhZwOOWODnMQP26X09pbhz1JYL1UG','D:\\0temp\\USpace\\ToDo\\dashboard.json');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-12-07 21:42:23
