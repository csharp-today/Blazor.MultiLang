namespace CSharpToday.Blazor.MultiLang
{
    public interface ITypedLanguage : ILanguage
    {
        string TypePrefix { get; }
    }
}
