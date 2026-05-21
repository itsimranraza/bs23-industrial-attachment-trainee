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

    public class StandardCar : Vehicle
    {
        public override decimal PerKmRate => 2.50m;
        public override decimal PerMinuteRate => 0.50m;

        public StandardCar(string licensePlate, decimal baseFare = 10.0m) : base(licensePlate, baseFare) { }
    }

    public class LuxurySedan : Vehicle
    {
        public override decimal PerKmRate => 5.00m;
        public override decimal PerMinuteRate => 1.50m;

        private readonly decimal _luxuryTax;

        public LuxurySedan(string licensePlate, decimal baseFare = 25.0m, decimal luxuryTax = 15.0m) : base(licensePlate, baseFare)
        {
            if (luxuryTax < 0)
            {
                throw new ArgumentException("Luxury tax cannot be negative", nameof(luxuryTax));
            }

            _luxuryTax = luxuryTax;
        }

        public override decimal CalculateBaseTripFare(decimal distanceKms, decimal durationMinutes)
        {
            return base.CalculateBaseTripFare(distanceKms, durationMinutes) + _luxuryTax;
        }
    }

    public class PercentageDiscount : IPromotion
    {
        private readonly decimal _percentage;

        public PercentageDiscount(decimal percentage)
        {
            if (percentage is < 0 or > 100)
            {
                throw new ArgumentException("Percentage must be between 0 and 100.");
            }

            _percentage = percentage;
        }

        public decimal ApplyDiscount(decimal currentFare)
        {
            return currentFare - (currentFare * (_percentage / 100));
        }
    }

    public class FlatDiscount : IPromotion
    {
        private readonly decimal _discountAmount;

        public FlatDiscount(decimal discountAmount)
        {
            if (discountAmount < 0)
            {
                throw new ArgumentException("Discount amount cannot be negative");
            }

            _discountAmount = discountAmount;
        }

        public decimal ApplyDiscount(decimal currentFare)
        {
            return currentFare - _discountAmount;
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

                var standardCar = new StandardCar("DHA-12-3456");

                var luxurySedan = new LuxurySedan("CTG-99-8888");
                var tenPercentOff = new PercentageDiscount(10);

                var hugeDiscount = new FlatDiscount(50.0m);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error Caught]: {ex.Message}");
            }
        }
    }
}