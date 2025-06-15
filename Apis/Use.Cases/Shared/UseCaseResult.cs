namespace ManagementProducts.Use.Cases.Shared
{
    public class UseCaseResult<TPayload, TFailure> where TFailure : Failure
    {
        private readonly TPayload payload;
        private readonly TFailure failure;
        private readonly bool isFailure;

        public UseCaseResult(TPayload payload)
        {
            this.payload = payload;
            isFailure = false;
        }

        public UseCaseResult(TFailure failure)
        {
            this.failure = failure;
            isFailure = true;
        }

        public T Match<T>(Func<TPayload, T> payloadFunc, Func<TFailure, T> failureFunc)
            => isFailure ? failureFunc(failure) : payloadFunc(payload);

        public TPayload Payload() => Match(l => l, r => default);

        public TFailure Failure() => Match(l => default, r => r);

        public static implicit operator UseCaseResult<TPayload, TFailure>(TPayload payload) => new UseCaseResult<TPayload, TFailure>(payload);

        public static implicit operator UseCaseResult<TPayload, TFailure>(TFailure failure) => new UseCaseResult<TPayload, TFailure>(failure);
    }
}
