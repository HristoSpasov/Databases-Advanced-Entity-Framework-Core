namespace Payments.Entities
{
    using Payments.Entities.Interfaces;

    public class BankAccount : IMoneyOperations
    {
        public BankAccount()
        {
        }

        public BankAccount(string bankName, decimal balance, string swiftCode)
        {
            this.BankName = bankName;
            this.Balance = balance;
            this.SwiftCode = swiftCode;
        }

        public int BankAccountId { get; set; }

        public decimal Balance { get; private set; }

        public string BankName { get; set; }

        public string SwiftCode { get; set; }

        public int PaymentMethodId { get; set; } // Not included in database

        public PaymentMethod PaymentMethod { get; set; } // Not included in database

        public void Deposit(decimal money)
        {
            this.Balance += money;
        }

        public void Withdraw(decimal money)
        {
            this.Balance -= money;
        }
    }
}