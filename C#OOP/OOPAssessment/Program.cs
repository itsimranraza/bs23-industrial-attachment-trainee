using System;

namespace FareEngineAssessment
{
    public enum TripStatus { Pending, Paid, Failed }

    public record Passenger(string Id, string Name);

    public interface IPromotion
    {
        decimal ApplyDiscount(decimal cuurentFare);
    }

    public interface IPaymentService
    {
        bool ProcessPayment(string passengerId, decimal amount);
    }

    public abstract class Vehicle
    {
        
    }

    public class  Trip
    {
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var passenger = new Passenger("P101", "Imran Raza");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error Caught]: {ex.Message}");
            }
        }
    }
}