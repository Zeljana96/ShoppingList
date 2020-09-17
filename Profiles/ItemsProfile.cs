using AutoMapper;
using ShoppingList.Dtos;
using ShoppingList.Models;

namespace ShoppingList.Profiles
{
    public class ItemsProfile : Profile
    {
        public ItemsProfile()
        {
            CreateMap<Item, ItemReadDto>();
            CreateMap<ItemCreateDto, Item>();
            CreateMap<ItemUpdateDto, Item>();
            CreateMap<Item, ItemUpdateDto>();
        }
    }
}