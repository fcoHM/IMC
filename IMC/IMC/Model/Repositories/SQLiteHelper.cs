public class SQLiteBase
{
    protected string _rutaBD;
    protected SQLiteConnection _connection;

    public SQLiteBase()
    {
        _rutaBD = FileAccesHelper.GetPathFile("IMC.db");
        _connection = new SQLiteConnection(_rutaBD);
    }
}

public class SQLiteHelper<T> : SQLiteBase where T : BaseModel, new()
{
    public SQLiteHelper() : base()
    {
        _connection.CreateTable<T>(); // Aquí ya sí puedes usar T
    }

    public List<T> GetAllData()
        => _connection.Table<T>().ToList();

    public int Add(T row)
    {
        _connection.Insert(row);
        return row.ID;
    }

    public int Update(T row)
        => _connection.Update(row);

    public int Delete(T row)
        => _connection.Delete(row);

    public T Get(int ID)
        => _connection.Table<T>().FirstOrDefault(w => w.ID == ID);
}
