using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
	public class ImageRepository : IImageRepository
	{
		private readonly DbContext _dbContext;

		public ImageRepository(ApplicationDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
		{
			//1. Save the file to wwwroot/images
			var localParh = Path.Combine(Directory.GetCurrentDirectory(), "images", file.FileName);
			using (var stream = new FileStream(localParh, FileMode.Create))
			{
				file.CopyTo(stream);
			}

			//2. upload to database

			var httprequest = $"{Environment.GetEnvironmentVariable("BASE_URL")}/images/{file.FileName}";
			blogImage.Url = httprequest;

			await _dbContext.Set<BlogImage>().AddAsync(blogImage);
			await _dbContext.SaveChangesAsync();

			return blogImage;
		}
	}
}
