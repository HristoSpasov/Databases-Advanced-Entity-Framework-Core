namespace Payments.Entities.Interfaces
{
    internal interface IMoneyOperations
    {
        void Withdraw(decimal money);

        void Deposit(decimal money);
    }
}