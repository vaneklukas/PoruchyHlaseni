using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace PoruchyUdrzba.Model
{
    class Database
    {
        string connectionString = ConfigurationManager.AppSettings.Get("db_path");

        public DataTable dataTable ()
        {
            DataTable dataTable = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + connectionString + ';'))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("Select * from Poruchy where open=1", conn);
                SQLiteDataReader reader = command.ExecuteReader();
                dataTable.Load(reader);
                reader.Close();
            }
            return dataTable;
        }
        public DataTable dataTableOld()
        {
            DataTable dataTable = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + connectionString + ';'))
            {
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("Select * from Poruchy where open=0", conn);
                SQLiteDataReader reader = command.ExecuteReader();
                dataTable.Load(reader);
                reader.Close();
            }
            return dataTable;
        }
        public void updateData(int id, string comment, string name)
        {
            string datetime = DateTime.Now.ToString();
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + connectionString + ';'))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand("UPDATE Poruchy SET finishTime =:finishtime"+
                        ", maintenance =:maintenance, open =0, commentM =:commentm where poruchy_ID=:id",conn);
                    cmd.Parameters.Add("finishtime", DbType.String).Value =datetime;
                    cmd.Parameters.Add("maintenance", DbType.String).Value = name;
                    cmd.Parameters.Add("commentm", DbType.String).Value = comment;
                    cmd.Parameters.Add("id", DbType.Int32).Value = id;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (SQLiteException x)
                {

                    LogClass log = new LogClass();
                    log.writeLog(x.ToString());
                }
            }
        }
        public DataTable getRecord(int errorId)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + connectionString + ';'))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Poruchy WHERE poruchy_id=:P1", conn);
                    cmd.Parameters.Add("P1", DbType.Int32).Value = errorId;
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                }
                catch (SQLiteException x)
                {

                    LogClass log = new LogClass();
                    log.writeLog(x.ToString());
                }
            }
            return dt;
        }

        public void updateTakeOver(int id, string nameMt)
        {
            string datetime = DateTime.Now.ToString();
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + connectionString + ';'))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand("UPDATE Poruchy SET openMan =:openMan" +
                        ", MaintOp =:MaintOp where poruchy_ID=:id", conn);
                    cmd.Parameters.Add("openMan", DbType.String).Value = datetime;
                    cmd.Parameters.Add("MaintOp", DbType.String).Value = nameMt;
                    cmd.Parameters.Add("id", DbType.Int32).Value = id;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (SQLiteException x)
                {

                    LogClass log = new LogClass();
                    log.writeLog(x.ToString());
                }
            }
        }
        public DataTable getEmails(int errorId)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + connectionString + ';'))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand("select Email from Emails WHERE stredisko_id=9 or stredisko=(SELECT stredisko FROM Poruchy WHERE poruchy_ID=:P1)", conn);
                    cmd.Parameters.Add("P1", DbType.Int32).Value = errorId;
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                }
                catch (SQLiteException x)
                {
                    LogClass log = new LogClass();
                    log.writeLog(x.ToString());
                }
            }
            return dt;
        }
    }
}
