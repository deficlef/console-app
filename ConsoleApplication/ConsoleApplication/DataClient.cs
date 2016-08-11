using MySql.Data.MySqlClient;
using System;

namespace Data
{
    public class DataClient
    {
        string dbName = "umeboluDB";

        public bool InsertToDB(string dir, float total, int length)
        {
            DBConnection dbCon = DBConnection.Instance();
            dbCon.DatabaseName = dbName;
            bool success = false;

            if (dbCon.IsConnect())
            {
                string query = "INSERT INTO console (dir, total_size, file_length)"
                            + " VALUES ('" + dir +  "', '" + total  + "', '" + length + "')";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                dbCon.Open();
                var result = cmd.ExecuteNonQuery();
                if (!result.Equals(0))
                {
                    success = true;
                }
                else
                {
                    success = false;
                }

                dbCon.Close();
            }

            return success;
        }

        public bool UpdateDB(string file, float total, int length)
        {
            DBConnection dbCon = DBConnection.Instance();
            dbCon.DatabaseName = dbName;
            bool success = false;

            if (dbCon.IsConnect())
            {
                string query = "UPDATE console " +
                                "SET dir = '" + file + "'," + 
                                    " total_size = '" + total + "'," +
                                    " file_length = '" + length +"' " + 
                                "WHERE dir = '" + file + "'";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                dbCon.Open();
                var result = cmd.ExecuteNonQuery();
                if (!result.Equals(0))
                {
                    success = true;
                }
                else {
                    success = false;
                }

                dbCon.Close();
            }

            return success;
        }

        public bool DeleteFromDB(string id)
        {
            DBConnection dbCon = DBConnection.Instance();
            dbCon.DatabaseName = dbName;
            bool success = false;

            if (dbCon.IsConnect())
            {
                string query = "DELETE FROM console WHERE id = '" + id + "'";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                dbCon.Open();
                var result = cmd.ExecuteNonQuery();
                if (!result.Equals(0))
                {
                    success = true;
                }
                else
                {
                    success = false;
                }

                dbCon.Close();
            }

            return success;
        }


        public Tuple<string, string, string, string> GetSavedData(string dir)
        {
            DBConnection dbCon = DBConnection.Instance();
            Tuple<string, string, string, string> result = null;

            dbCon.DatabaseName = "umeboluDB";
            if (dbCon.IsConnect())
            {
                string query = "SELECT * FROM console WHERE dir = '" + dir + "'";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                dbCon.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    string id = reader.GetString(0);
                    string fDir = reader.GetString(1);
                    string total = reader.GetString(2);
                    string fileLength = reader.GetString(3);
                    result = Tuple.Create(id, fDir, total, fileLength);
                }

                dbCon.Close();
            }

            return result;
        }

        public void GetSavedData()
        {
            DBConnection dbCon = DBConnection.Instance();

            dbCon.DatabaseName = dbName;
            if (dbCon.IsConnect())
            {
                string query = "SELECT * FROM console";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                dbCon.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string id = reader.GetString(0);
                        string fDir = reader.GetString(1);
                        string total = reader.GetString(2);
                        string fileLength = reader.GetString(3);
                        Console.WriteLine("{0}, {1}, {2}, {3}",
                                        "id: " + id,
                                        "directory: " + fDir,
                                        "total size: " + total,
                                        "Number of files: " + fileLength);
                    }
                }

                dbCon.Close();
            }
        }
    }
}
