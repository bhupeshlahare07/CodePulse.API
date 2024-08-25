using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ApplicationDBContext dBContext;

		public CategoriesController(ApplicationDBContext dBContext)
        {
			this.dBContext = dBContext;
		}
        [HttpPost]
		public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
		{
			//Map DTO to Domain Model
			var category = new Category
			{
				Name = request.Name,
				UrlHandel = request.UrlHandel
			};

			await dBContext.Categories.AddAsync(category);
			await dBContext.SaveChangesAsync();

			//Map Domain Model to Dto
			var response = new CategoryDto
			{
				Id = category.Id,
				Name = request.Name,
				UrlHandel = request.UrlHandel
			};

			return Ok(response);
		}
	}
}
