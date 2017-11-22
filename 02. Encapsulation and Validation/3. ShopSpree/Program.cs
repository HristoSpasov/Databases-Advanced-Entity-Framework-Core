namespace _3.ShopSpree
{
    using Entities;
    public class Program
    {
        public static void Main()
        {
            Database db = new Database();
            Engine engine = new Engine(db);
            engine.Run();
        }
    }
}
