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
            var items = await _itemRepository.GetAll(a => a.Hero);
            var itemDTOs = _mapper.Map<List<GetItemDTO>>(items);
            return Ok(itemDTOs);
        }
    }
}
