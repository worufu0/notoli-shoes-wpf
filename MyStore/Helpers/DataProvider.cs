namespace MyStore.Helpers
{
    public class DataProvider
    {
        public MyStoreEntities database { get; set; }
        private static string _connectionString;
        private static DataProvider _instance;
        public static DataProvider instance
        {
            get { return _instance == null ? _instance = new DataProvider(_connectionString) : _instance; }
            set { _instance = value; }
        }

        public DataProvider(string connectionString)
        {
            _connectionString = connectionString;
            database = new MyStoreEntities(_connectionString);
        }
    }
}
