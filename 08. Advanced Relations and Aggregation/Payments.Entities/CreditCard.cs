namespace Payments.Entities
{
    using System;
    using Payments.Entities.Interfaces;

    public class CreditCard : IMoneyOperations
    {
        public CreditCard()
        {
        }

        public CreditCard(decimal limit, decimal moneyOwed, DateTime expirationDate)
        {
            this.Limit = limit;
            this.MoneyOwed = moneyOwed;
            this.ExpirationDate = expirationDate;
        }

        public int CreditCardId { get; set; }

        public decimal Limit { get; private set; }

        public decimal MoneyOwed { get; private set; }

        public decimal LimitLeft => this.Limit - this.MoneyOwed; // Not included in database

        public DateTime ExpirationDate { get; set; }

        public int PaymentMethodId { get; set; } // Not included in database

        public PaymentMethod PaymentMethod { get; set; } // Not included in database

        public void Deposit(decimal money)
        {
            this.Limit += money;
        }

        public void Withdraw(decimal money)
        {
            this.Limit -= money;
        }
    }
}