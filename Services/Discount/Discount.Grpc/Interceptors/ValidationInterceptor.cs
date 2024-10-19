using FluentValidation;
using Grpc.Core.Interceptors;
using Grpc.Core;

public class ValidationInterceptor : Interceptor
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationInterceptor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        // Find the validator for the request type
        var validatorType = typeof(IValidator<>).MakeGenericType(request.GetType());
        var validator = _serviceProvider.GetService(validatorType) as IValidator;

        if (validator is not null)
        {
            // Create a ValidationContext for the request
            var validationContext = new ValidationContext<object>(request);
            var validationResult = validator.Validate(validationContext);

            if (!validationResult.IsValid)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument,
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))));
            }
        }

        return await continuation(request, context);
    }
}
