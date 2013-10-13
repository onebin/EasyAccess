namespace EasyAccess.Authorization.ScriptGenerator.SqlHelper
{
    public interface ISqlStatement
    {
        string BeforeGen();

        string AfterGen();

        string GenMenu();

        string DeleteMenu();

        string GenPermission();

        string DeletePermission();
    }
}
