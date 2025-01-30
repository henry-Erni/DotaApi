using AutoMapper;
using DotaApi.Domain.Contracts;
using DotaApi.Domain.Entities;
using DotaApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DotaApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class HeroController : ControllerBase
    {
        private readonly IBaseRepository<Hero> _heroRepository;
        private readonly IMapper _mapper;

        public HeroController(IBaseRepository<Hero> heroRepository, IMapper mapper)
        {
            _heroRepository = heroRepository;
            _mapper = mapper;
        }

        [HttpGet("heroes")]
        public async Task<IActionResult> GetHeroes()
        {
            var heroes = await _heroRepository.GetAll();
            var heroesDtos = _mapper.Map<List<GetHeroDTO>>(heroes);
            return Ok(heroesDtos);
        }

        [HttpGet("heroes/{id}")]
        public async Task<IActionResult> GetHero(Guid id)
        {
            var hero = await _heroRepository.Get(id);
            if (hero == null)
            {
                return NotFound();
            }
            var heroDto = _mapper.Map<GetHeroDTO>(hero);
            return Ok(heroDto);
        }

       
        [HttpPost("heroes")]
        public async Task<IActionResult> CreateHero(CreateHero createHero)
        {
            var hero = _mapper.Map<Hero>(createHero);
            await _heroRepository.Add(hero);
            return CreatedAtAction(nameof(GetHero), new { id = hero.Id }, hero);
        }

        [HttpPut("heroes/{id}")]
        public async Task<IActionResult> UpdateHero(Guid id, [FromBody] UpdateHero updateHero)
        {
            var hero = await _heroRepository.Get(id);
            if (hero == null)
            {
                return NotFound();
            }
            _mapper.Map(updateHero, hero);
            var updatedHero = await _heroRepository.Update(hero);
            var heroDto = _mapper.Map<GetHeroDTO>(updatedHero);
            return Ok(heroDto);
        }


        [HttpDelete("heroes/{id}")]
        public async Task<IActionResult> DeleteHero(Guid id)
        {
            var hero = await _heroRepository.Delete(id);
            if (hero == null)
            {
                return NotFound();
            }
            var heroDto = _mapper.Map<GetHeroDTO>(hero);
            return Ok(hero);
        }

    }
}
