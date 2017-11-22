namespace Payments.Seed
{
    internal static class SeedData
    {
        // User seed data
        public static readonly string[] MaleFirstName = new string[] { "Ivan", "Petur", "Dimitur", "Aleksandur", "Veselin", "Plamen", "Valentin", "Bojidar", "Petko", "Hristo", "Emil", "Vasil", "Martin" };

        public static readonly string[] MaleLastName = new string[] { "Ivanov", "Petrov", "Dimitrov", "Aleksandrov", "Veselinov", "Plamenov", "Valentinov", "Bojidarov", "Petkov", "Hristov", "Emilov", "Vasilev", "Martinov" };

        public static readonly string[] FemaleFirstName = new string[] { "Ivanka", "Peturks", "Dimitrina", "Aleksandra", "Veselina", "Plamena", "Valentina", "Bojidara", "Petka", "Hristina", "Emila", "Vasilka", "Martina" };

        public static readonly string[] FemaleLastName = new string[] { "Ivanova", "Petrova", "Dimitrova", "Aleksandrova", "Veselinova", "Plamenova", "Valentinova", "Bojidarova", "Petkova", "Hristova", "Emilova", "Vasileva", "Martinova" };

        public static readonly string[] EmailDomain = new string[] { "@yahoo.com", "@abv.bg", "@yahoo.co.uk", "@yahoo.ca", "@mail.bg", "@mail.ru", "@softuni.bg", "Bojidar", "@utp.bg", "@gmail.com", "@hotmail.com" };

        public static readonly string PasswordSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_";

        public const int PasswordMinLength = 8;

        public const int PasswordMaxLength = 15;

        // Bank account seed data
        public static readonly decimal MinBalanceValue = 0;

        public static readonly decimal MaxBalanceValue = 1_000_000;

        public static readonly string[] BankNames = new string[] { "Unicredit", "Post bank", "Firt Investment Bank", "Softuni Bank", "Lemon Brothers", "Tokuda bank", "Virtual Bank", "Top Secret Bank" };

        public static readonly string SwiftCodeSymbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public const int SwiftCodeMinLength = 8;

        public const int SwiftCodeMaxLength = 20;

        // Credit card seed data
        public static readonly decimal MinCreditCardLimit = 50;

        public static readonly decimal MaxCreditCardLimit = 100_000;

        public static readonly decimal MinMoneyOwed = 0;

        public static readonly decimal MaxMoneyOwed = 10_000;

        // User payment methods
        public static readonly int MinUserPaymentMethods = 1;

        public static readonly int MaxUserPaymentMethods = 5;
    }
}