namespace TowerDefense.GameEngine
{
    public class GuardianType
    {
        public string Class { get; }
        public string Specialization { get; }

        public GuardianType(string @class, string specialization)
        {
            Class = @class;
            Specialization = specialization;
        }
    }
}