using System.Data.SQLite;


ReadData(CreateConnection());
//InsertCustomer(CreateConnection());
RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version = 3; New = True; Compress = True;");
    try
    {
        connection.Open();
        Console.WriteLine("DB found!");
    }

    catch
    {
        Console.WriteLine("DB not found!");
    }

    return connection;
}


static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(0);
        string readerStringLastName = reader.GetString(1);
        string readerStringDoB = reader.GetString(2);

        Console.WriteLine($"{readerRowId}. Full name: {readerStringFirstName} {readerStringLastName}; Status {readerStringDoB}");
    }

    myConnection.Close();
}


static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fname, lname, dob;

    Console.WriteLine("Enter first name:");
    fname = Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lname = Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyyy):");
    dob = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
        $"VALUES ('{fname}', '{lname}', '{dob}')";

    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Rows inserted: {rowInserted}");


    ReadData(myConnection);
}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    

    string idToDelete;
    Console.WriteLine("Enter and id to delete a customer:");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = "$DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the customer table.");

    ReadData(myConnection);
}