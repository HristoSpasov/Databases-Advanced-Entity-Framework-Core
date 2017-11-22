namespace _4.Speed_Racing
{
    public class Car
    {
        public Car(string model, double fuelAmount, double fuelConsumptionPerKm)
        {
            this.Model = model;
            this.FuelAmount = fuelAmount;
            this.FuelConsumptionPerKm = fuelConsumptionPerKm;
            this.KilometersTravelled = 0;
        }
        public string Model { get; set; }
        public double FuelAmount { get; set; }
        public double FuelConsumptionPerKm { get; set; }
        public int KilometersTravelled { get; set; }

        public override string ToString()
        {
            return $"{this.Model} {this.FuelAmount:F2} {this.KilometersTravelled}";
        }
    }
}
