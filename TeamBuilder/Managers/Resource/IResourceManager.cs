namespace TeamBuilder.Managers.Resource
{
    public interface IResourceManager
    {
        T GetResourceValue<T>(string name);
        string GetResourceString(string key);
    }
}