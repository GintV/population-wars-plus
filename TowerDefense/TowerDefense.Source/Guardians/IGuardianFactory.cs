namespace TowerDefense.Source.Guardians
{
    public interface IGuardianFactory
    {
        Maybe<IGuardian> CreateGuardian(GuardianType guardianType);
    }
}