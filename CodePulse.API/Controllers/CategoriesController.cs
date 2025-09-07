using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryRepository categoryRepository;

		public CategoriesController(ICategoryRepository categoryRepository)
        {
			this.categoryRepository = categoryRepository;
		}
        [HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
		{
			//Map DTO to Domain Model
			//var category = new Category
			//{
			//	Name = request.Name,
			//	UrlHandel = request.UrlHandel
			//};

			Category obj = new Category();
			obj.Name = request.Name;
			obj.UrlHandel = request.UrlHandel;


			//await dBContext.Categories.AddAsync(category);
			//await dBContext.SaveChangesAsync();

			//await categoryRepository.CreateAsync(category);
			await categoryRepository.CreateAsync(obj);

			//Map Domain Model to Dto
			var response = new CategoryDto
			{
				Id = obj.Id,
				Name = request.Name,
				UrlHandel = request.UrlHandel
			};

			return Ok(response);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
			var categories = await categoryRepository.GetAllAsync();
			var response = categories.Select(c => new CategoryDto
			{
				Id = c.Id,
				Name = c.Name,
				UrlHandel = c.UrlHandel
			}).ToList();

			//Console.WriteLine(response.Count());   // Triggers SQL query #1
			//Console.WriteLine(response.First().Name); // Triggers SQL query #2

			return Ok(response);
		}
		[HttpPost]
		[Route("{id:guid}")]
		public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
		{
			var category = await categoryRepository.GetByIdAsync(id);
			if (category == null)
			{
				return NotFound();
			}
			var response = new CategoryDto
			{
				Id = category.Id,
				Name = category.Name,
				UrlHandel = category.UrlHandel
			};
			return Ok(response);
		}
	}
}
