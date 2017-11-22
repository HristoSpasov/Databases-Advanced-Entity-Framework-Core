namespace Payments.Seed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Payments.Data;
    using Payments.Entities;

    public static class Seed
    {
        private static int defaulEntitiesToGenerate = 1000;
        private static Random rnd = new Random();

        public static void SeedProcedure()
        {
            PaymentContext context = new PaymentContext();

            int entitiesToGenerate = SetEntitiesToGenerate();

            Console.WriteLine("Generating values...");

            User[] userEntities = GenerateUsers(entitiesToGenerate);
            context.Users.AddRange(userEntities);

            CreditCard[] creditCards = GenerateUsersCreditCards(entitiesToGenerate);
            context.CreditCards.AddRange(creditCards);

            BankAccount[] bankAccounts = GenerateBankAccounts(entitiesToGenerate);
            context.BankAccounts.AddRange(bankAccounts);

            context.SaveChanges();

            // Add payments methods
            PaymentMethod[] paymentMethods = GeneratePaymentMethods(context);
            context.PaymentMethods.AddRange(paymentMethods);

            context.SaveChanges();

            Console.WriteLine("Seed data successful!");
        }

        private static PaymentMethod[] GeneratePaymentMethods(PaymentContext context)
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            int currentCreditCardId = 1;
            int currentBankAccountId = 1;

            // For each user generate payment methods
            foreach (User user in context.Users)
            {
                int paymentMethodsToGenerateForCurrentUser = rnd.Next(SeedData.MinUserPaymentMethods, SeedData.MaxUserPaymentMethods);

                // Generate credit card paymennts
                for (int i = 0; i < paymentMethodsToGenerateForCurrentUser; i++)
                {
                    PaymentMethod currentPayment = new PaymentMethod()
                    {
                        CreditCardId = currentCreditCardId++,
                        Type = PaymentMethodType.CreditCard,
                        UserId = user.UserId
                    };

                    paymentMethods.Add(currentPayment);
                }

                // Generate bank accounts payments
                for (int i = 0; i < paymentMethodsToGenerateForCurrentUser; i++)
                {
                    PaymentMethod currentPayment = new PaymentMethod()
                    {
                        BankAccountId = currentBankAccountId++,
                        Type = PaymentMethodType.BankAccount,
                        UserId = user.UserId
                    };

                    paymentMethods.Add(currentPayment);
                }
            }

            return paymentMethods.ToArray();
        }

        private static BankAccount[] GenerateBankAccounts(int entitiesToGenerate)
        {
            BankAccount[] bankAccounts = new BankAccount[entitiesToGenerate * SeedData.MaxUserPaymentMethods];

            for (int i = 0; i < bankAccounts.Length; i++)
            {
                string bankName = SeedData.BankNames[rnd.Next(0, SeedData.BankNames.Length)];
                decimal balance = rnd.Next((int)SeedData.MinBalanceValue, (int)SeedData.MaxBalanceValue);
                string swiftCode = SwiftCodeGenerator(rnd.Next(SeedData.SwiftCodeMinLength, SeedData.SwiftCodeMaxLength));

                BankAccount currentAccount = new BankAccount(bankName, balance, swiftCode);

                bankAccounts[i] = currentAccount;
            }

            return bankAccounts;
        }

        private static string SwiftCodeGenerator(int swiftCodeLength)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < swiftCodeLength; i++)
            {
                sb.Append(SeedData.SwiftCodeSymbols[rnd.Next(0, SeedData.SwiftCodeSymbols.Length)]);
            }

            return sb.ToString();
        }

        private static CreditCard[] GenerateUsersCreditCards(int entitiesToGenerate)
        {
            CreditCard[] creditCards = new CreditCard[entitiesToGenerate * SeedData.MaxUserPaymentMethods];

            for (int i = 0; i < creditCards.Length; i++)
            {
                decimal limit = rnd.Next((int)SeedData.MinCreditCardLimit, (int)SeedData.MaxCreditCardLimit);
                decimal moneyOwed = rnd.Next((int)SeedData.MinMoneyOwed, (int)SeedData.MaxMoneyOwed);
                DateTime expirationDate = DateTime.UtcNow.AddYears(rnd.Next(0, 5)).AddMonths(rnd.Next(1, 12));

                CreditCard currentCard = new CreditCard(limit, moneyOwed, expirationDate);

                creditCards[i] = currentCard;
            }

            return creditCards;
        }

        private static User[] GenerateUsers(int entitiesToGenerate)
        {
            User[] maleUsers = UserGenerator(SeedData.MaleFirstName, SeedData.MaleLastName, SeedData.EmailDomain, entitiesToGenerate / 2);
            User[] femaleUsers = UserGenerator(SeedData.FemaleFirstName, SeedData.FemaleLastName, SeedData.EmailDomain, entitiesToGenerate - maleUsers.Length);

            return maleUsers.Concat(femaleUsers).ToArray();
        }

        private static User[] UserGenerator(string[] firstNames, string[] lastNames, string[] emailDomain, int entitiesToGenerate)
        {
            User[] users = new User[entitiesToGenerate];

            for (int i = 0; i < users.Length; i++)
            {
                User currentUser = new User();
                currentUser.FirstName = firstNames[rnd.Next(0, SeedData.MaleFirstName.Length)];
                currentUser.LastName = lastNames[rnd.Next(0, SeedData.MaleLastName.Length)];
                currentUser.Email = $"{currentUser.FirstName.ToLower()}.{currentUser.LastName.ToLower()}{SeedData.EmailDomain[rnd.Next(0, SeedData.EmailDomain.Length)]}";
                currentUser.Password = PasswordGenerator(rnd.Next(SeedData.PasswordMinLength, SeedData.PasswordMaxLength));

                users[i] = currentUser;
            }

            return users;
        }

        private static string PasswordGenerator(int passwordLength)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < passwordLength; i++)
            {
                sb.Append(SeedData.PasswordSymbols[rnd.Next(0, SeedData.PasswordSymbols.Length)]);
            }

            return sb.ToString();
        }

        private static int SetEntitiesToGenerate()
        {
            Console.Write($"Enter entities to generate. If left empty or invalid input entered will generate default ({defaulEntitiesToGenerate}) entities > ");

            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || input.Any(c => !char.IsDigit(c)))
            {
                return defaulEntitiesToGenerate;
            }

            return int.Parse(input);
        }
    }
}