public interface IPaymentStrategy
{
    public void Pay(int amount);
}
public class CreditCard : IPaymentStrategy
{
    public void Pay(int amount)
    {
        Console.WriteLine($"Amount {amount} paid by credit card.");
    }
}
public class PayPal : IPaymentStrategy
{
    public void Pay(int amount)
    {
        Console.WriteLine($"Amount {amount} paid by Paypal.");
    }

    public ICollection<string> GetTransactionHistory()
    {
        return new List<string> { "Transaction 1", "Transaction 2", "Transaction 3" };
    }
}
public class ShopingCart
{
    private IPaymentStrategy _strategy;
    public void SetPaymentStrategy(IPaymentStrategy strategy)
    {
        _strategy = strategy;
    }
    public void Checkout(int amount)
    {
        _strategy.Pay(amount);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var cart1 = new ShopingCart();
        var pay1 = new CreditCard();
        cart1.SetPaymentStrategy(pay1);
        cart1.Checkout(2332);

        var cart2 = new ShopingCart();
        var pay2 = new PayPal();
        cart2.SetPaymentStrategy(pay2);
        cart2.Checkout(3454);

    }
}