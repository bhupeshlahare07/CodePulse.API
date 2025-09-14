using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

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

		public async Task<IEnumerable<Category>> GetAllAsync()
		{
			return await dBContext.Categories.ToListAsync();
		}

		public async Task<Category?> GetByIdAsync(Guid Id)
		{
			return await dBContext.Categories.FirstOrDefaultAsync(x => x.Id == Id);
		}

		public async Task<Category?> UpdateAsync(Category category)
		{
			var existingCategory = dBContext.Categories.FirstOrDefault(x => x.Id == category.Id);
			if (existingCategory != null)
			{
				dBContext.Entry(existingCategory).CurrentValues.SetValues(category);
				await dBContext.SaveChangesAsync();
				return category;
			}
			return null;
		}

        public async Task<Category?> DeleteAsync(Guid id)
        {
			var existingCategory = await dBContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
			if(existingCategory is null)
			{ 
				return null; 
			}
            //await dBContext.Categories.Where(x => x.Id == id).ExecuteDeleteAsync();
			dBContext.Categories.Remove(existingCategory);
			await dBContext.SaveChangesAsync();
			return existingCategory;
        }
    }
}
