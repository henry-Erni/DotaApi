using AutoMapper;
using DotaApi.Domain.Contracts;
using DotaApi.Domain.Entities;
using DotaApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DotaApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ItemController : ControllerBase
    {
        public readonly IBaseRepository<Item> _itemRepository;
        public readonly IMapper _mapper;

        public ItemController(IBaseRepository<Item> itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetItems()
        {
            var items = await _itemRepository.GetAll();
            var itemDTOs = _mapper.Map<List<GetItemDTO>>(items);
            return Ok(itemDTOs);
        }

        [HttpGet("items/{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var item = await _itemRepository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            var itemDto = _mapper.Map<GetItemDTO>(item);
            return Ok(itemDto);
        }

        [HttpPost("items")]
        public async Task<IActionResult> CreateItem([FromBody] CreateItem createItem)
        {
            var item = _mapper.Map<Item>(createItem);
            var createdItem = await _itemRepository.Add(item);
            var itemDto = _mapper.Map<GetItemDTO>(createdItem);
            return CreatedAtAction(nameof(GetItem), new { id = itemDto.Id }, itemDto);
        }

        [HttpPut("items/{id}")]

        public async Task<IActionResult> UpdateItem(Guid id, [FromBody] UpdateItem updateItem)
        {
            var item = await _itemRepository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            _mapper.Map(updateItem, item);
            var updatedItem = await _itemRepository.Update(item);
            var itemDto = _mapper.Map<GetItemDTO>(updatedItem);
            return Ok(itemDto);
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _itemRepository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            var deletedItem = await _itemRepository.Delete(id);
            var itemDto = _mapper.Map<GetItemDTO>(deletedItem);
            return Ok(itemDto);
        }
    }
}
