using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Configuration;
using System.Windows.Forms;

namespace Library
{
    /*public static string Sha1(string plaintext)
    {
        System.Security.Cryptography.SHA1Managed  sha1 = new System.Security.Cryptography.SHA1Managed();
        var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
        var hashBytes = sha1.ComputeHash(plaintextBytes);

        StringBuilder sb = new StringBuilder();
        foreach (var hashByte in hashBytes)
        {
            sb.AppendFormat("{0:x2}", hashByte);
        }
        return sb.ToString();
    }*/
    class DB
    {
        private string provider;//stored in appconfig (now it is required)
        private string connectionstring;//stored in appconfig
        private DbProviderFactory factory;//special object, that creates database command objects
        public DB() {//empty constructor
            provider = ConfigurationManager.AppSettings["provider"];//getting value from appconfig
            connectionstring = ConfigurationManager.AppSettings["connectionString"];//getting value from appconfig
            factory = DbProviderFactories.GetFactory(provider);//setting a special factory method, 
        }
        public void commr(string commandstr, string col, ref List<string> result)//list<string>
        {
            using (DbConnection connection = factory.CreateConnection())//creating a temporaly connection, that is used in the brackets
            {
                if (connection == null)//check if connection is established
                {
                    MessageBox.Show("Connection Error");//a message box will pop up
                    return;//terminate the method
                }

                connection.ConnectionString = connectionstring;//use connection string to connect to the database
                connection.Open();//open a connection

                DbCommand command = factory.CreateCommand();// create a command object to send sql commands to database
                if (command == null)
                {
                    MessageBox.Show("Command Error");//a message box will pop up
                    return;//terminate the method
                }

                command.Connection = connection;// connect two objects

                command.CommandText = commandstr;// SQL query passed here
                using (DbDataReader dataReader = command.ExecuteReader())//Create a reader inside the brackets
                {
                    while (dataReader.Read())
                    {
                        result.Add(dataReader[col].ToString().Trim());//trim -> removes empty spaces, and add content to result list
                    }
                }
            }
        }
        public void comma(string commandstr)
        {
            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                {

                    MessageBox.Show("Connection Error");
                    return;
                }

                connection.ConnectionString = connectionstring;
                connection.Open();

                DbCommand command = factory.CreateCommand();
                if (command == null)
                {
                    MessageBox.Show("Command Error");
                    return;
                }

                command.Connection = connection;
                command.CommandText = commandstr;
                command.ExecuteNonQuery();
            }
        }
    }
}
