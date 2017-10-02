namespace TowerDefense.Source
{
    public class GuardianType
    {
        public GuardianClass Class { get; }
        public GuardianSpecialization Specialization { get; }

        public GuardianType(GuardianClass @class, GuardianSpecialization specialization)
        {
            Class = @class;
            Specialization = specialization;
        }
    }

    public enum GuardianClass
    {
        Archer,
        Wizard
    }

    public enum GuardianSpecialization
    {
        Dark,
        Light,
        Fire,
        Ice
    }
}