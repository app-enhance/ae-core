namespace AE.Transactions.Abstractions
{
    using Core.DI;

    public interface ITransaction : IDependency
    {
        void Configure(ITransactionOptions options);

        void Begin();

        void End();

        void Cancel();

        bool IsStarted();
    }
}