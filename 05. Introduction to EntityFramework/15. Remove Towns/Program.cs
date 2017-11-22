namespace _15._Remove_Towns
{
    using System;
    using System.Linq;
    using System.Text;
    using P02_DatabaseFirst.Data;
    using P02_DatabaseFirst.Data.Models;

    public class Program
    {
        public static void Main()
        {
            using (SoftUniContext context = new SoftUniContext())
            {
                string townToDeleteInput = Console.ReadLine();

                Town townToDelete = context.Towns.SingleOrDefault(t => t.Name == townToDeleteInput);

                if (townToDelete != null)
                {
                    // Get town id
                    int townToDeleteId = townToDelete.TownId;

                    // Address to be deleted
                    Address[] addressesToDelete = context.Addresses.Where(id => id.TownId == townToDeleteId).ToArray();
                    int addressesToBeRemovedCount = addressesToDelete.Length;

                    // Set AddressId in Employees table to null
                    foreach (var a in addressesToDelete)
                    {
                        context.Employees.Where(addr => addr.AddressId == a.AddressId).ToList().ForEach(set => set.AddressId = null);
                    }

                    // Delete addresses
                    context.Addresses.RemoveRange(addressesToDelete);

                    // Remove town from db
                    context.Towns.Remove(townToDelete);

                    context.SaveChanges(); // Uncomment to save changes to db

                    if (addressesToBeRemovedCount > 1)
                    {
                        Console.WriteLine($"{addressesToBeRemovedCount} addresses in {townToDeleteInput} were deleted");
                    }
                    else 
                    {
                        Console.WriteLine($"{addressesToBeRemovedCount} address in {townToDeleteInput} was deleted");
                    }
                }
                else
                {
                    Console.WriteLine("Town does not exist.");
                }
            }
        }
    }
}
