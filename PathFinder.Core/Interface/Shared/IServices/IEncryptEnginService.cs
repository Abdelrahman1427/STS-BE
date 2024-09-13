namespace PathFinder.Core.Interface.Shared.IServices
{
    public interface IEncryptEnginService
    {
        public string Encrypt(string toEncrypt, string key, bool useHashing);
        public string Decrypt(string cipherString, string key, bool useHashing);
    }
}
