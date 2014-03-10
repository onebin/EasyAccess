namespace EasyAccess.Infrastructure.Util.EnumDescription
{
    public interface IEnumDescription
    {
        string Description { get; }

        int Index { get; }

        int Value { get; }

        string Name { get; }
    }
}