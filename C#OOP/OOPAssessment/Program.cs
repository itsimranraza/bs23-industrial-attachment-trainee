using System;

namespace FareEngineAssessment
{
    public enum TripStatus { Pending, Paid, Failed }

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
            
        }
    }
}