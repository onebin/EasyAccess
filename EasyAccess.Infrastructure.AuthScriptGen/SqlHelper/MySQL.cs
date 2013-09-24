namespace EasyAccess.Infrastructure.AuthScriptGen.SqlHelper
{
    public class MySQL : ISqlStatement
    {

        public string BeforeGen()
        {
            return
                "DELIMITER //\r\n\r\n" +
                "CREATE PROCEDURE GenMenuPermission() \r\n" +
                "BEGIN \r\n\r\n";
        }

        public string AfterGen()
        {
            return 
                "END;// \r\n" +
                "DELIMITER ; \r\n\r\n" +
                "CALL GenMenuPermission(); \r\n\r\n" +
                "DROP PROCEDURE GenMenuPermission;";
        }

        public string GenMenu()
        {
            return
                "IF EXISTS (SELECT 1 FROM `Menu` WHERE `Id` = '{0}') \r\n" +
                "THEN \r\n" +
                "UPDATE `Menu` SET `ParentId` = '{1}', `Name`='{2}', `Url`='{3}', `System`='{4}', `Depth`='{5}', `Index`='{6}' WHERE `Id` = '{0}'; \r\n" +
                "ELSE \r\n" +
                "INSERT `Menu` (`Id`,`ParentId`,`Name`,`Url`,`System`,`Depth`,`Index`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}'); \r\n" +
                "END IF; \r\n\r\n";
        }

        public string DeleteMenu()
        {
            return
                "DELETE FROM `Menu` WHERE `Id` NOT IN ({0}); \r\n\r\n\r\n\r\n";
        }

        public string GenPermission()
        {
            return
                "IF EXISTS (SELECT 1 FROM `Permission` WHERE `Id` = '{0}') \r\n" +
                "THEN \r\n" +
                "UPDATE `Permission` SET `MenuId` = '{1}', `Name`='{2}', `ActionUrl`='{3}' WHERE `Id` = '{0}'; \r\n" +
                "ELSE \r\n" +
                "INSERT `Permission` (`Id`,`MenuId`,`Name`,`ActionUrl`) VALUES ('{0}','{1}','{2}','{3}'); \r\n" +
                "END IF; \r\n\r\n";
        }

        public string DeletePermission()
        {
            return
                "DELETE FROM `Permission` WHERE `Id` NOT IN ({0}); \r\n\r\n\r\n\r\n";
        }
    }
}
