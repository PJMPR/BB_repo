﻿using BuildBuddy.Application.Abstractions;
using BuildBuddy.Contract;
using BuildBuddy.Data.Abstractions;
using BuildBuddy.Data.Model;

namespace BuildBuddy.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IRepositoryCatalog _repositories;

        public ItemService(IRepositoryCatalog repositories)
        {
            _repositories = repositories;
        }

        public async Task<IEnumerable<ItemDto>> GetAllItemsAsync()
        {
            return await _repositories.Items.GetAsync(item => new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    QuantityMax = item.QuantityMax,
                    Metrics = item.Metrics,
                    QuantityLeft = item.QuantityLeft,
                    PlaceId = item.PlaceId
                });
        }

        public async Task<ItemDto> GetItemByIdAsync(int id)
        {
            var item = await _repositories.Items.GetByID(id);

            if (item == null)
            {
                return null;
            }

            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                QuantityMax = item.QuantityMax,
                Metrics = item.Metrics,
                QuantityLeft = item.QuantityLeft,
                PlaceId = item.PlaceId
            };
        }

        public async Task<ItemDto> CreateItemAsync(ItemDto itemDto)
        {
            var item = new Item
            {
                Name = itemDto.Name,
                QuantityMax = itemDto.QuantityMax,
                Metrics = itemDto.Metrics,
                QuantityLeft = itemDto.QuantityLeft,
                PlaceId = itemDto.PlaceId
            };

            _repositories.Items.Insert(item);
            await _repositories.SaveChangesAsync();

            itemDto.Id = item.Id;
            return itemDto;
        }

        public async Task UpdateItemAsync(int id, ItemDto itemDto)
        {
            var item = await _repositories.Items.GetByID(id);

            if (item != null)
            {
                item.Name = itemDto.Name;
                item.QuantityMax = itemDto.QuantityMax;
                item.Metrics = itemDto.Metrics;
                item.QuantityLeft = itemDto.QuantityLeft;
                item.PlaceId = itemDto.PlaceId;

                await _repositories.SaveChangesAsync();
            }
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _repositories.Items.GetByID(id);
            if (item != null)
            {
                _repositories.Items.Delete(item);
                await _repositories.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<ItemDto>> GetAllItemsByPlaceAsync(int placeId)
        {
            return await _repositories.Items.GetAsync(item => new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                QuantityMax = item.QuantityMax,
                Metrics = item.Metrics,
                QuantityLeft = item.QuantityLeft,
                PlaceId = item.PlaceId
            },
            item => item.PlaceId == placeId);
        }
    }
}
