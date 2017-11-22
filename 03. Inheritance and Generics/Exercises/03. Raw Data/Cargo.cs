namespace _03.Raw_Data
{
    public class Cargo
    {
        private int cargoWeight;
        private string cargoType;

        public Cargo(int cargoWeight, string cargoType)
        {
            this.CargoWeight = cargoWeight;
            this.CargoType = cargoType;
        }

        public int CargoWeight
        {
            get
            {
                return this.cargoWeight;
            }
            private set
            {
                this.cargoWeight = value;
            }
        }
        
        public string CargoType
        {
            get
            {
                return this.cargoType;
            }
            private set
            {
                this.cargoType = value;
            }
        }
    }
}
