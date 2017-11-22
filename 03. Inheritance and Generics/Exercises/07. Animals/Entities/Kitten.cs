namespace _07.Animals.Entities
{
    public class Kitten : Cat
    {
        public Kitten(string name, int age, string gender) : base(name, age, "Female")
        {
        }

        public override string ProduceSound()
        {
            return "Meow";
        }
    }
}
