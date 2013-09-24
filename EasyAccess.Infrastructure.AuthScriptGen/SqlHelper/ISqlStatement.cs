namespace EasyAccess.Infrastructure.AuthScriptGen.SqlHelper
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
