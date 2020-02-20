namespace FSL.Framework.Core.Configuration.Models
{
    public interface IDefaultConfiguration
    {
        string ConnectionStringId { get; set; }

        string GetConnectionString(
            string connectionStringId);
    }
}
