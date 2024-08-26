using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;

namespace CodePulse.API.Repositories.Implementation
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDBContext dBContext;

		public CategoryRepository(ApplicationDBContext dBContext)
		{
			this.dBContext = dBContext;
		}

		public async Task<Category> CreateAsync(Category category)
		{
			await dBContext.Categories.AddAsync(category);
			await dBContext.SaveChangesAsync();
			return category;
		}
	}
}
