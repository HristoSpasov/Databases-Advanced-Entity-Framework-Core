namespace Payments.Views
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Payments.Entities;

    public class UserViewModel
    {
        public UserViewModel()
        {
        }

        public int UserId { get; set; }

        public string FullName { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; }

        public ICollection<CreditCard> CreditCards { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"User: {this.FullName}");
            sb.AppendLine("Bank Accounts:");

            foreach (BankAccount ba in this.BankAccounts.OrderBy(id => id.BankAccountId))
            {
                sb.AppendLine($"-- ID: {ba.BankAccountId}");
                sb.AppendLine($"--- Balance: {ba.Balance:F2}");
                sb.AppendLine($"--- Bank: {ba.BankName}");
                sb.AppendLine($"--- SWIFT: {ba.SwiftCode}");
            }

            sb.AppendLine("Credit Cards");

            foreach (CreditCard cc in this.CreditCards)
            {
                sb.AppendLine($"-- ID: {cc.CreditCardId}");
                sb.AppendLine($"--- Limit: {cc.Limit:F2}");
                sb.AppendLine($"--- Money Owed: {cc.MoneyOwed}");
                sb.AppendLine($"--- Limit Left: {cc.LimitLeft}");
                sb.AppendLine($"--- Expiration Date: {cc.ExpirationDate.ToString("yyyy/MM")}");
            }

            return sb.ToString().Trim();
        }
    }
}