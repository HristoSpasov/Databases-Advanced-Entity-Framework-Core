namespace Payments.App
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Payments.Data;
    using Payments.Entities;
    using Payments.Views;

    public class StartUp
    {
        public static void Main()
        {
            PaymentContext context = new PaymentContext();

            // Uncomment to initialize database - change connection string to match your server name
            context.Database.EnsureDeleted();
            context.Database.Migrate();
            Seed.Seed.SeedProcedure();

            // Print user data by id
            PrintUserData(context);

            // Pay bills
            int id = ValidateId();
            Console.Write("Enter money amount > ");
            decimal amount = decimal.Parse(Console.ReadLine());
            string paymentReport = PayBills(id, amount, context);

            Console.WriteLine(paymentReport);
        }

        private static string PayBills(int id, decimal amount, PaymentContext context)
        {
            PaymentMethod[] userData = context.PaymentMethods
                                  .Include(u => u.User)
                                  .Include(ba => ba.BankAccount)
                                  .Include(cc => cc.CreditCard)
                                  .Where(uId => uId.User.UserId == id)
                                  .ToArray();

            string result = $"{amount} $ have been withdrawed from {userData.First().User.FirstName} {userData.First().User.LastName}'s accounts and credit cards.";

            if (userData != null)
            {
                if (UserHasEnoughMoney(userData, amount))
                {
                    // Bank accounts money withdraw
                    foreach (var ba in userData.Where(t => t.Type == PaymentMethodType.BankAccount).OrderBy(bId => bId.BankAccountId))
                    {
                        if (ba.BankAccount.Balance < amount)
                        {
                            // There are not enough money in this bank account
                            amount -= ba.BankAccount.Balance;
                            ba.BankAccount.Withdraw(ba.BankAccount.Balance);
                        }
                        else
                        {
                            // Reduce current bank account with remaining balance
                            ba.BankAccount.Withdraw(amount);
                            amount = 0;

                            break;
                        }
                    }

                    // Check credit cards
                    if (amount > 0)
                    {
                        foreach (var cc in userData.Where(t => t.Type == PaymentMethodType.CreditCard).OrderBy(cId => cId.CreditCardId))
                        {
                            if (cc.CreditCard.LimitLeft < amount)
                            {
                                amount -= cc.CreditCard.Limit;
                                cc.CreditCard.Withdraw(cc.CreditCard.LimitLeft);
                            }
                            else
                            {
                                cc.CreditCard.Withdraw(amount);
                                amount = 0;

                                break;
                            }
                        }
                    }
                }
                else
                {
                    result = $"Insufficient funds!";
                }
            }
            else
            {
                result = $"User with id {id} not found!";
            }

            context.SaveChanges();

            return result;
        }

        private static bool UserHasEnoughMoney(PaymentMethod[] userData, decimal amount)
        {
            decimal totalAmount = userData.Where(t => t.Type == PaymentMethodType.CreditCard).Select(cc => cc.CreditCard.LimitLeft).Sum() + userData.Where(t => t.Type == PaymentMethodType.BankAccount).Select(ba => ba.BankAccount.Balance).Sum();

            return totalAmount >= amount;
        }

        private static void PrintUserData(PaymentContext context)
        {
            int id = ValidateId();

            UserViewModel userData = GetUserFromContext(id, context);

            if (userData != null)
            {
                Console.WriteLine(userData);
            }
            else
            {
                Console.WriteLine($"User with id {id} not found!");
            }
        }

        private static UserViewModel GetUserFromContext(int id, PaymentContext context)
        {
            return context.Users.Where(uid => uid.UserId == id)
                .Select(e => new UserViewModel
                {
                    UserId = e.UserId,
                    FullName = e.FirstName + " " + e.LastName,
                    BankAccounts = e.PaymentMethods.Where(ba => ba.BankAccount != null).Select(ba => ba.BankAccount).ToArray(),
                    CreditCards = e.PaymentMethods.Where(cc => cc.CreditCard != null).Select(cc => cc.CreditCard).ToArray()
                })
                .SingleOrDefault();
        }

        private static int ValidateId()
        {
            int id = default(int);

            while (true)
            {
                Console.Write("Enter user id > ");

                string input = Console.ReadLine();

                if (input.All(c => char.IsDigit(c)))
                {
                    id = int.Parse(input);

                    break;
                }

                Console.WriteLine("Invalid input! ");
            }

            return id;
        }
    }
}