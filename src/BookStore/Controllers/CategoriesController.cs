using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.API.DTOs.Category;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : MainController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(IMapper mapper,
                                    ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();

            if (categories == null) return NotFound();

            return Ok(_mapper.Map<IEnumerable<CategoryResultDTO>>(categories));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null) return NotFound();

            return Ok(_mapper.Map<CategoryResultDTO>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDTO categoryDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            var category = _mapper.Map<Category>(categoryDTO);

            var result = await _categoryService.Add(category);

            if (result == null) return BadRequest();

            return Ok(_mapper.Map<CategoryResultDTO>(result));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, CategoryEditDTO categoryDTO)
        {
            if(id != categoryDTO.Id) return BadRequest();

            if (!ModelState.IsValid) return BadRequest();

            var category = _mapper.Map<Category>(categoryDTO);

            await _categoryService.Update(category);

            return Ok(categoryDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null) return NotFound();

            var result = await _categoryService.Remove(category);

            if (!result) return BadRequest();

            return Ok();
        }

        [Route("search/{category}")]
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Search(string category)
        {
            var categories = _mapper.Map<List<Category>>(await _categoryService.Search(category));

            if (categories == null || categories.Count == 0) return NotFound("No category was found!!"); ;

            return Ok(categories);
        }
    }
}
