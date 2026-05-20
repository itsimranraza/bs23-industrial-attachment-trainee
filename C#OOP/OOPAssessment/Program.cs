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
        public string LicensePlate { get; init; }
        public decimal BaseFare { get; init; }

        public abstract decimal PerKmRate { get; }
        public abstract decimal PerMinuteRate { get; }

        protected Vehicle(string licensePlate, decimal baseFare)
        {
            if(string.IsNullOrWhiteSpace(licensePlate))
            {
                throw new ArgumentException("License plate cannot be empty", nameof(licensePlate));
            }
            if (baseFare < 0)
            {
                throw new ArgumentException("Base fare cannot be negative", nameof(baseFare));
            }

            LicensePlate = licensePlate;
            BaseFare = baseFare;
        }

        public virtual decimal CalculateBaseTripFare(decimal distanceKms, decimal durationMinutes)
        {
            return (distanceKms * PerKmRate) + (durationMinutes * PerMinuteRate);
        }
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