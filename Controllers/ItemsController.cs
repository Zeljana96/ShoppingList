using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.Data;
using ShoppingList.Dtos;
using ShoppingList.Models;

namespace ShoppingList.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IShoppingListRepo _repository;
        private readonly IMapper _mapper;

        public ItemsController(IShoppingListRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult <IEnumerable<ItemReadDto>> GetAllItems()
        {
            var items = _repository.GetAllItems();
            return  Ok(_mapper.Map<IEnumerable<ItemReadDto>>(items));
        }
        [HttpGet("{name}", Name="GetItemByName")]
        public ActionResult <ItemReadDto> GetItemByName(string name)
        {

            var item = _repository.GetItemByName(name);
            if(item != null)
            {
                return Ok(_mapper.Map<ItemReadDto>(item));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult <ItemReadDto> CreateItem(ItemCreateDto itemCreateDto)
        {
            var itemModel = _mapper.Map<Item>(itemCreateDto);

            var items = _repository.GetAllItems();

            foreach(Item itm in items)
            {
                if(itemModel.ItemName == itm.ItemName)
                {
                    return Ok("This item already exists on the ShoppingList, maybe you just want to update it.");
                }
            }
            
            if(itemModel.Required == 0)
            {
                itemModel.Required = 1;
            }
            if(itemModel.Required != itemModel.Provided)
            {
                itemModel.Status = false;
            }else
            {
                itemModel.Status = true;
            }
            _repository.CreateItem(itemModel);
            _repository.SaveChanges();

            var itemReadDto = _mapper.Map<ItemReadDto>(itemModel); 

            //return CreatedAtRoute(nameof(GetItemByName), new {Id = itemReadDto.Id}, itemReadDto);
            return Ok(itemReadDto); 
        }

        [HttpPut("{name}")]
        public ActionResult UpdateItem(string name, ItemUpdateDto itemUpdateDto)
        {
            var itemModelFromRepo = _repository.GetItemByName(name);
            if(itemModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(itemUpdateDto, itemModelFromRepo);
            
            _repository.UpdateItem(itemModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{name}")]
        public ActionResult PartialItemUpdate(string name, JsonPatchDocument<ItemUpdateDto> patchDoc)
        {
            var itemModelFromRepo = _repository.GetItemByName(name);
            if(itemModelFromRepo == null)
            {
                return NotFound();
            }
            
            var itemToPatch = _mapper.Map<ItemUpdateDto>(itemModelFromRepo);
            patchDoc.ApplyTo(itemToPatch, ModelState);
            

            if(!TryValidateModel(itemToPatch))
            {
                return ValidationProblem(ModelState);
            }
            if(itemToPatch.Provided >= itemToPatch.Required)
            {
                itemToPatch.Status = true;
            }
            else
            {
                itemToPatch.Status = false;
            }
            if(itemToPatch.Required == 0){
                _repository.DeleteItem(itemModelFromRepo);
                _repository.SaveChanges();
            }
            else
            {
                _mapper.Map(itemToPatch, itemModelFromRepo);
                _repository.UpdateItem(itemModelFromRepo);
                _repository.SaveChanges();
            }

            return NoContent();
        }
        [HttpDelete("{name}")]
        public ActionResult  DeleteItem(string name)
        {
            var itemModelFromRepo = _repository.GetItemByName(name);
            if(itemModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteItem(itemModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpGet("{all}/{status}")]
        public ActionResult <IEnumerable<ItemReadDto>> GetItemsByStatus(string status)
        {
            var items = _repository.GetItemsByStatus(status);
           return Ok(_mapper.Map<IEnumerable<ItemReadDto>>(items));
        }
    }
}