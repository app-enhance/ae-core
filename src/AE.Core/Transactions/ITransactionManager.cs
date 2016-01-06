namespace AE.Core.Transactions
{
    using DI;

    public interface ITransactionManager : ISingletonDependency
    {
        void RegisterTransaction(ITransaction transaction);

        void UnregisterTransaction(ITransaction transaction);

        void InterruptAllTransactions();

        ITransaction GetCurrenTransaction();
    }
}