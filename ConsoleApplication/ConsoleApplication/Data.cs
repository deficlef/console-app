using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Data
{
    public class DBConnection
    {
        private DBConnection()
        {
        }

        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            bool result = true;
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(databaseName))
                    result = false;
                    string connstring = string.Format("Server=umeboluDB.db.6941838.hostedresource.com; database={0}; UID=umeboluDB; password=Deficlef@123", databaseName);
                    connection = new MySqlConnection(connstring);
                    connection.Open();
                    result = true;

            }

            return result;
        }

        public void Open() {
            if(connection.State == ConnectionState.Closed)
                connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }
    }
}