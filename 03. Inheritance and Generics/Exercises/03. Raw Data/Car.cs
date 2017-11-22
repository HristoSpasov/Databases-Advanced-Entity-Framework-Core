using System.Collections.Generic;

namespace _03.Raw_Data
{
    public class Car
    {
        private string model;
        private Engine engine;
        private Cargo cargo;
        private Tire[] tires;

        public Car(string model, Engine engine, Cargo cargo, Tire[] tires)
        {
            this.Model = model;
            this.Engine = engine;
            this.Cargo = cargo;
            this.tires = tires;
        }
        public string Model
        {
            get
            {
                return this.model;

            }

            private set
            {
                this.model = value;
            }
        }

        public Engine Engine
        {
            get
            {
                return this.engine;
            }

            private set
            {
                this.engine = value;
            }
        }

        public Cargo Cargo
        {
            get
            {
                return this.cargo;
            }

            private set
            {
                this.cargo = value;
            }
        }

        public IReadOnlyCollection<Tire> Tyres => this.tires;

        public override string ToString()
        {
            return this.Model;
        }
    }
}
