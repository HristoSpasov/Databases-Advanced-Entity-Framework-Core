namespace _03.Raw_Data
{
    public class Tire
    {
        private double pressure;
        private int age;

        public Tire(double pressure, int age)
        {
            this.Pressure = pressure;
            this.age = age;
        }

        public double Pressure
        {
            get
            {
                return this.pressure;
            }

            private set
            {
                this.pressure = value;
            }
        }

        public int Age
        {
            get
            {
                return this.age;
            }
            private set
            {
                this.age = value;
            }
        }
    }
}
