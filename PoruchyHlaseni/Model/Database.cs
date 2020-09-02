using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace PoruchyHlaseni.Model
{
    class Database
    {
        string connectionString = ConfigurationManager.AppSettings.Get("db_path");
        public DataTable getStredisko()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + connectionString + ';'))
                {
                    conn.Open();

                    SQLiteCommand command = new SQLiteCommand("Select * from Stredisko", conn);
                    SQLiteDataReader reader = command.ExecuteReader();
                    dataTable.Load(reader);

                    reader.Close();
                }
            }
            catch (SQLiteException x)
            {
                LogClass log = new LogClass();
                log.writeLog(x.ToString());
            }
            
            return dataTable;
        }

        public DataTable getMachines()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + connectionString + ';'))
                {
                    conn.Open();

                    SQLiteCommand command = new SQLiteCommand("Select * from Machine", conn);
                    SQLiteDataReader reader = command.ExecuteReader();
                    dataTable.Load(reader);

                    reader.Close();
                }
            }
            catch (SQLiteException x)
            {

                LogClass log = new LogClass();
                log.writeLog(x.ToString());
            }
           
            return dataTable;
        }
        public DataTable getTypesOfError()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + connectionString + ';'))
                {
                    conn.Open();

                    SQLiteCommand command = new SQLiteCommand("Select * from Types", conn);
                    SQLiteDataReader reader = command.ExecuteReader();
                    dataTable.Load(reader);

                    reader.Close();
                }
            }
            catch (SQLiteException x)
            {

                LogClass log = new LogClass();
                log.writeLog(x.ToString());
            }           
            return dataTable;
        }

        public bool insertData(string[]data, string dateTime)
        {
            bool success= true;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + connectionString + ';'))
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand("INSERT into Poruchy (stredisko,machine,startTime,operator,open,commentOp,type) VALUES(:P1,:P2, :P3, :P4, :P5, :P6, :P7)", conn);
                    command.Parameters.Add(new SQLiteParameter("P1", data[0]));
                    command.Parameters.Add(new SQLiteParameter("P2", data[2]));
                    command.Parameters.Add(new SQLiteParameter("P3", dateTime));
                    command.Parameters.Add(new SQLiteParameter("P4", data[4]));
                    command.Parameters.Add(new SQLiteParameter("P5", 1));
                    command.Parameters.Add(new SQLiteParameter("P6", data[3]));
                    command.Parameters.Add(new SQLiteParameter("P7", data[1]));
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (SQLiteException x)
            {
                success = false;
                LogClass log = new LogClass();
                log.writeLog(x.ToString());
            }
            if (success)
            {

                SendEmail sendEmail = new SendEmail();
                sendEmail.sendNewFailure(data, dateTime,getEmails(data[0]));
            }
            return success;
        }
        public DataTable getEmails(string stredisko)
        {
            DataTable dt = new DataTable();
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + connectionString + ';'))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand("select Email from Emails WHERE stredisko_id=9 or stredisko=:P1", conn);
                    cmd.Parameters.Add("P1", DbType.String).Value = stredisko;
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
