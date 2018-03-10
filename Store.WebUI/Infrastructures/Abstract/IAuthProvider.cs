namespace Store.WebUI.Infrastructures.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}