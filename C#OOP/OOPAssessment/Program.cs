using System;
using System.Runtime.InteropServices;

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

    public class CreditCardPaymentService : IPaymentService
    {
        public bool ProcessPayment(string passengerId, decimal amount)
        {
            Console.WriteLine($"[Payment Gateway] Processing ${amount:F2} for Passenger: {passengerId}...");
            return true;
        }
    }
    public class  Trip
    {
        public Vehicle AssignedVehicle { get; }
        public Passenger TripPassenger { get; }
        public decimal DistanceKms { get; }
        public decimal DurationMinutes { get; }
        public IPromotion? Promotion { get; }
        public TripStatus Status { get; private set; }

        public Trip(Vehicle vehicle, Passenger passenger, decimal distanceKms, decimal durationMinutes, IPromotion? promotion = null)
        {
            AssignedVehicle = vehicle ?? throw new ArgumentNullException(nameof(vehicle), "Vehicle must be assigned to a trip.");
            TripPassenger = passenger ?? throw new ArgumentNullException(nameof(passenger), "Passenger must be assigned.");

            if (distanceKms < 0)
            {
                throw new ArgumentException("Distance cannot be negative", nameof(distanceKms));
            }
            if (durationMinutes <= 0)
            {
                throw new ArgumentException("Duration must be greater than zero.", nameof(durationMinutes));
            }

            DistanceKms = distanceKms;
            DurationMinutes = durationMinutes;
            Promotion = promotion;
            Status = TripStatus.Pending;
        }
        
        public decimal CalculateFinalFare()
        {
            decimal fare = AssignedVehicle.CalculateBaseTripFare(DistanceKms, DurationMinutes);

            if(Promotion != null)
            {
                fare = Promotion.ApplyDiscount(fare);
            }

            return Math.Max(fare, AssignedVehicle.BaseFare);
        }

        public void CompleteTrip(IPaymentService paymentService)
        {
            if (paymentService == null)
            {
                throw new ArgumentNullException(nameof(paymentService));
            }
            if(Status == TripStatus.Paid)
            {
                throw new InvalidOperationException("Trip is already paid.");
            }

            decimal finalFare = CalculateFinalFare();
            bool isSuccess = paymentService.ProcessPayment(TripPassenger.Id, finalFare);

            Status = isSuccess ? TripStatus.Paid : TripStatus.Failed;

            Console.WriteLine($"[System] Trip status updated to: {Status}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var passenger = new Passenger("P101", "Imran Raza");
                var paymentService = new CreditCardPaymentService();

                var standardCar = new StandardCar("DHA-12-3456");
                var trip1 = new Trip(standardCar, passenger, distanceKms: 10, durationMinutes: 20);
                trip1.CompleteTrip(paymentService);

                var luxurySedan = new LuxurySedan("CTG-99-8888");
                var tenPercentOff = new PercentageDiscount(10);
                var trip2 = new Trip(luxurySedan, passenger, distanceKms: 15, durationMinutes: 30, promotion: tenPercentOff);
                trip2.CompleteTrip(paymentService);

                var hugeDiscount = new FlatDiscount(50.0m);
                var trip3 = new Trip(standardCar, passenger, distanceKms: 2, durationMinutes: 5, promotion: hugeDiscount);

                var invalidTrip = new Trip(standardCar, passenger, distanceKms: -5, durationMinutes: 10);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error Caught]: {ex.Message}");
            }
        }
    }
}