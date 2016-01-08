namespace AE.Transactions.Abstractions
{
    using Core.DI;

    public interface ITransaction : IDependency
    {
        bool IsStarted { get; }

        void Configure(ITransactionOptions options);

        void Begin();

        void End();

        void Cancel();
    }
}