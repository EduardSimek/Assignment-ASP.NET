namespace MyApp.Interfaces
{
    public class ExternalClass
    {
        public static string GetConnection()
        {
            string connectionToDB = @"Data Source=C:\Atom\MyDatabase.db";
            return connectionToDB;
        }
    }
}
