namespace AE.Transactions.Abstractions
{
    using Extensions.DependencyInjection;

    public interface ITransaction : IScopedDependency
    {
        bool IsStarted { get; }

        void Configure(ITransactionOptions options);

        void Begin();

        void End();

        void Cancel();
    }
}