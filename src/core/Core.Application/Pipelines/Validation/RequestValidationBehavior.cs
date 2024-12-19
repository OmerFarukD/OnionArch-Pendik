using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using FluentValidation;
using MediatR;
using ValidationException = Core.CrossCuttingConcerns.Exceptions.ExceptionTypes.ValidationException;

namespace Core.Application.Pipelines.Validation;

public class RequestValidationBehavior<TRequest,TResponse>: IPipelineBehavior<TRequest,TResponse>
    where TRequest: IRequest<TResponse>
{
    
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<object> context = new(request);
        IEnumerable<ValidationExceptionModel> errors = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .GroupBy(
                keySelector: p => p.PropertyName,
                resultSelector: (propname, errors) =>
                    new ValidationExceptionModel()
                    {
                        Errors = errors.Select(e => e.ErrorMessage),
                        Property = propname
                    }
            ).ToList();

        if (errors.Any())
            throw new ValidationException(errors);
        TResponse response = await next();
        return response;


    }
}