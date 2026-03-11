using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
	public class ImageRepository : IImageRepository
	{
		//private readonly DbContext _dbContext;
		private readonly IWebHostEnvironment _webhostEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ApplicationDBContext _dbContext;

		public ImageRepository(ApplicationDBContext dbContext, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
		{
			_dbContext = dbContext;
			_webhostEnvironment = webHostEnvironment;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
		{
			//1. Save the file to wwwroot/images
			var localPath = Path.Combine(_webhostEnvironment.ContentRootPath,"Images",$"{blogImage.FileName}{blogImage.FileExtension.ToLower()}");
			using var stream = new FileStream(localPath, FileMode.Create);
			await file.CopyToAsync(stream);



			//var localParh = Path.Combine(Directory.GetCurrentDirectory(), "images", file.FileName);
			//using (var stream = new FileStream(localParh, FileMode.Create))
			//{
			//	file.CopyTo(stream);
			//}

			//2. upload to database

			var httprequest = _httpContextAccessor.HttpContext.Request;
			var urlPath = $"{httprequest.Scheme}://{httprequest.Host}{httprequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";
			blogImage.Url = urlPath;
			await _dbContext.BlogImages.AddAsync(blogImage);
			await _dbContext.SaveChangesAsync();
			return blogImage;

			//var httprequest = $"{Environment.GetEnvironmentVariable("BASE_URL")}/images/{file.FileName}";
			//blogImage.Url = httprequest;

			//await _dbContext.Set<BlogImage>().AddAsync(blogImage);
			//await _dbContext.SaveChangesAsync();

			//return blogImage;
		}
	}
}
