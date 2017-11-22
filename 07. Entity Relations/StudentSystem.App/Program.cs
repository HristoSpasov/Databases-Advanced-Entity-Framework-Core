namespace P01_StudentSystem
{
    using P01_StudentSystem.Data;

    public class Program
    {
        public static void Main()
        {
            StudentSystemContext context = new StudentSystemContext();

            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();
        }
    }
}
