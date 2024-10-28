namespace Domain.Interfaces;

public interface IDataSaverFactory
{
    ICollection<IDataSaver> GetDataSavers();
}