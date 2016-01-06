namespace AE.Core.Transactions
{
    using DI;

    public interface ITransaction : IDependency
    {
        void Configure(ITransactionOptions options);

        void Begin();

        void End();

        void Cancel();

        bool IsStarted();
    }
}