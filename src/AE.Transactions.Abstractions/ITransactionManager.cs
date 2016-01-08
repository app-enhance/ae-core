namespace AE.Transactions.Abstractions
{
    using Extensions.DependencyInjection;

    public interface ITransactionManager : ISingletonDependency
    {
        void RegisterTransaction(ITransaction transaction);

        void UnregisterTransaction(ITransaction transaction);

        void InterruptAllTransactions();

        ITransaction GetCurrenTransaction();
    }
}