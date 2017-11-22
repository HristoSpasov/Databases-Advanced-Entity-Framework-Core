namespace _01.Initial_Setup
{
    using System;
    using System.Data.SqlClient;

    public class Program
    {
        private static string DatabaseName = "MinionsDB";
        private static string ServerName = @"M6800\SQLEXPRESS";

        public static void Main()
        {
            SqlConnectionStringBuilder builder = CreateInitialConnectionData();
            SqlConnection connection = new SqlConnection(builder.ToString());

            CreateDatabase(connection);  // Create database
            connection =  AddConnectionDatabaseInfo(builder, connection); // Add newly created database to connection

            // Create tables
            CreateTables(connection); // Create database tables

        }

        private static void CreateTables(SqlConnection connection)
        {
            connection.Open();

            using (connection)
            {
                try
                {
                    CreateTableCountries(connection); // Create countries table
                    CreateTableTowns(connection); // Create table minions
                    CreateTableMinions(connection); // Create table Minions
                    CreateTableEvilnessFactors(connection); // Create table evilness factors
                    CreateTableVillains(connection); // Create table villains
                    CreateTableMinionsVillains(connection); // Create table minions villains
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
               
        }

        private static void CreateTableMinionsVillains(SqlConnection connection)
        {
            SqlCommand create = new SqlCommand("CREATE TABLE MinionsVillains " +
                                                      "(" +
                                                           "[MinionId] INT FOREIGN KEY REFERENCES Minions(Id), " +
                                                           "[VillainId] INT FOREIGN KEY REFERENCES Villains(Id)," +
                                                           "CONSTRAINT PK_mv PRIMARY KEY([MinionId], [VillainId])  " +
                                                      ");", connection);

            create.ExecuteNonQuery();
        }

        private static void CreateTableVillains(SqlConnection connection)
        {
            SqlCommand create = new SqlCommand("CREATE TABLE Villains " +
                                                        "(" +
                                                             "[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL, " +
                                                             "[Name] NVARCHAR(50), " +
                                                             "[EvilnessFactorId] INT FOREIGN KEY REFERENCES EvilnessFactors(Id) " +
                                                        ");", connection);
            create.ExecuteNonQuery();
        }

        private static void CreateTableEvilnessFactors(SqlConnection connection)
        {
            SqlCommand create = new SqlCommand("CREATE TABLE EvilnessFactors " +
                                                        "(" +
                                                             "[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL, " +
                                                             "[Name] NVARCHAR(50) " +
                                                        ");", connection);

            create.ExecuteNonQuery();
        }

        private static void CreateTableCountries(SqlConnection connection)
        {
            SqlCommand create = new SqlCommand("CREATE TABLE Countries " +
                                                      "(" +
                                                           "[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL, " +
                                                           "[Name] NVARCHAR(50) " +
                                                      ");", connection);

            create.ExecuteNonQuery();
        }

        private static void CreateTableTowns(SqlConnection connection)
        {

            SqlCommand create = new SqlCommand("CREATE TABLE Towns " +
                                               "(" +
                                                    "[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL, " +
                                                    "[Name] NVARCHAR(50), " +
                                                    "[CountryId] INT FOREIGN KEY REFERENCES Countries(Id) " +
                                               ");", connection);

            create.ExecuteNonQuery();
        }

        private static void CreateTableMinions(SqlConnection connection)
        {
            SqlCommand create = new SqlCommand("CREATE TABLE Minions" +
                                                        "(" +
                                                             "[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL, " +
                                                             "[Name] NVARCHAR(50), " +
                                                             "[Age] INT," +
                                                             "[TownId] INT FOREIGN KEY REFERENCES Towns(Id) " +
                                                        ");", connection);

            create.ExecuteNonQuery();
        }

        private static SqlConnection AddConnectionDatabaseInfo(SqlConnectionStringBuilder builder, SqlConnection connection)
        {
            builder.Add("Database", DatabaseName);
            return new SqlConnection(builder.ToString());
        }

        private static void CreateDatabase(SqlConnection connection)
        {
            connection.Open();

            using (connection)
            {
                try
                {
                    SqlCommand createDbCommand = new SqlCommand("CREATE DATABASE MinionsDB;", connection);
                    createDbCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static SqlConnectionStringBuilder CreateInitialConnectionData()
        {
            return new SqlConnectionStringBuilder()
                       {
                           { "Server", ServerName },
                           //{ "Database", DatabaseName },
                           { "Integrated Security", "True"}
                       };
        }
    }
}
