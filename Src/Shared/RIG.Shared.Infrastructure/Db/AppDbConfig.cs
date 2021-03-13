namespace RIG.Shared.Infrastructure.Db
{
    public class AppDbConfig
    {
        public string ConnectionStr { get; private set; }

        public AppDbConfig(string connectionStr)
        {
            ConnectionStr = connectionStr;
        }
    }
}