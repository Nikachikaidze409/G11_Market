namespace Market.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connection = "(localdb)\\MSSQLLocalDB";
            string sqll = "select * from Products";
            DBSql sql = new DBSql(connection);

            sql.ExecuteReader(sqll);
        }
    }
}
