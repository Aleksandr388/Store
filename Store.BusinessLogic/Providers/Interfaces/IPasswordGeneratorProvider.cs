namespace Store.BusinessLogic.Providers.Interfaces
{
    public interface IPasswordGeneratorProvider
    {
        public string RandomPasswordGenerator(int length);
    }
}
