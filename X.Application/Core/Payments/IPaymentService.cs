namespace X.Application.Core.Payments;

public interface IPaymentService
{
    Task<string> ProcessPaymentAsync(decimal amount, string currency, string paymentMethod, string description);
    Task<string> RefundPaymentAsync(string transactionId, decimal amount);
    Task<string> GetTransactionStatusAsync(string transactionId);
}