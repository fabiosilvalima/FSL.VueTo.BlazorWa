namespace FSL.Framework.Core.Cryptography.Provider
{
    public interface ICryptographyProvider
    {
        string Encrypt(
            string info);

        string DeCrypt(
            string info);
    }
}
