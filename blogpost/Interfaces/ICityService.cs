using blogpost.Models;

namespace blogpost.Interfaces
{
    public interface ICityService
    {
        ICollection<City> GetCities();
        City GetCity(int cityId);
        City GetCityByAuthor(int cityId);
        ICollection<PostAuthor> GetAuthorsByCity(int cityId);
        bool CityExist(int cityId);
    }
}
