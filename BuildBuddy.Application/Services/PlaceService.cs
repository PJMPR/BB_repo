using BuildBuddy.Application.Abstractions;
using BuildBuddy.Contract;
using BuildBuddy.Data.Abstractions;
using BuildBuddy.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace BuildBuddy.Application.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IRepositoryCatalog _repositories;

        public PlaceService(IRepositoryCatalog repositories)
        {
            _repositories = repositories;
        }

        public async Task<IEnumerable<PlaceDto>> GetAllPlacesAsync()
        {
            return await _repositories.Places
                .Entities
                .Select(place => new PlaceDto
                {
                    Id = place.Id,
                    Address = place.Address,
                })
                .ToListAsync();
        }

        public async Task<PlaceDto> GetPlaceByIdAsync(int id)
        {
            var place = await _repositories.Places
                .Entities
                .FirstOrDefaultAsync(p => p.Id == id);

            if (place == null)
            {
                return null;
            }

            return new PlaceDto
            {
                Id = place.Id,
                Address = place.Address,
            };
        }

        public async Task<PlaceDto> CreatePlaceAsync(PlaceDto placeDto)
        {
            var place = new Place
            {
                Address = placeDto.Address,
            };

            _repositories.Places.Add(place);
            await _repositories.SaveChangesAsync();

            placeDto.Id = place.Id;
            return placeDto;
        }

        public async Task UpdatePlaceAsync(int id, PlaceDto placeDto)
        {
            var place = await _repositories.Places
                .Entities
                .FirstOrDefaultAsync(p => p.Id == id);

            if (place != null)
            {
                place.Address = placeDto.Address;

                await _repositories.SaveChangesAsync();
            }
        }

        public async Task DeletePlaceAsync(int id)
        {
            var place = await _repositories.Places
                .Entities
                .FirstOrDefaultAsync(p => p.Id == id);
            if (place != null)
            {
                _repositories.Places.Remove(place);
                await _repositories.SaveChangesAsync();
            }
        }
    }
}
