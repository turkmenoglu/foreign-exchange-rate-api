namespace ForeignExchangeRate.Contract
{
    public interface IRequest<TResponse> : MediatR.IRequest<TResponse> where TResponse: IResponse
    {
    }
}
