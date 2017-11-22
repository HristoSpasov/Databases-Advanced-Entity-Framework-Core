namespace _11._Latest10Projs
{
    using System.Linq;
    using System.Text;
    using P02_DatabaseFirst;
    using P02_DatabaseFirst.Data;

    public class Program
    {
        public static void Main()
        {
            using (SoftUniContext db = new SoftUniContext())
            {
                StringBuilder sb = new StringBuilder();

                db.Projects
                    .Select(p => new
                    {
                        Name = p.Name,
                        Description = p.Description,
                        StartDate = p.StartDate
                    })
                    .OrderByDescending(d => d.StartDate)
                    .Take(10)
                    .OrderBy(n => n.Name)
                    .ToList()
                    .ForEach(r => sb.AppendLine(r.Name).AppendLine(r.Description).AppendLine(r.StartDate.ToString("M/d/yyyy h:mm:ss tt")));

                FileCreator.CreateFile(sb.ToString());
            }
        }
    }
}
