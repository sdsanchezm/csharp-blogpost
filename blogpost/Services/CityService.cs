using blogpost.Data;
using blogpost.Interfaces;
using blogpost.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace blogpost.Services
{
    public class CityService : ICityService
    {
        private readonly DataContext _dbContext;

        // private readonly IMapper _mapper;
        //public CityService(DataContext dbContext, IMapper mapper)

        public CityService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CityExist(int cityId)
        {
            return _dbContext.Cities_dbs.Any(c => c.Id == cityId);
        }

        public ICollection<PostAuthor> GetAuthorsByCity(int cityId)
        {
            var authorsList = _dbContext.PostAuthors_dbs.Where(p => p.AuthorPostCity.Id == cityId).ToList();
            return authorsList;
        }

        public ICollection<City> GetCities()
        {
            var c = _dbContext.Cities_dbs.ToList();

            return c;
        }

        public City GetCity(int cityId)
        {
            var c = _dbContext.Cities_dbs.Where(p => p.Id == cityId).FirstOrDefault();
            return c;
        }

        public City GetCityByAuthor(int authorId)
        {
            var city = _dbContext.PostAuthors_dbs.Where(c => c.Id == authorId).Select(p => p.AuthorPostCity).FirstOrDefault();
            return city;
        }

        public bool CreateCity(City city)
        {
            _dbContext.Add(city);
            return Save();
        }

        public bool Save()
        {
            var saved = _dbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCity(City city)
        {
            _dbContext.Update(city);
            return Save();
        }
    }
}
