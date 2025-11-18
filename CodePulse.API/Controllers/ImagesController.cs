using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Implementation;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository _imageRepository;

		public ImagesController(IImageRepository imageRepository)
		{
			_imageRepository = imageRepository;
		}

		[HttpPost]
		public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string Title)
		{
			ValidateImageUpload(file);
			if (ModelState.IsValid)
			{
				var blogImage = new BlogImage
				{
					FileName = fileName,
					FileExtension = Path.GetExtension(file.FileName),
					Title = Title,
					DateCreated = DateTime.UtcNow
				};

				blogImage = await _imageRepository.Upload(file, blogImage);

				var response = new BlogImageDto
				{
					Id = blogImage.Id,
					FileName = blogImage.FileName,
					FileExtension = blogImage.FileExtension,
					Title = blogImage.Title,
					Url = blogImage.Url,
					DateCreated = blogImage.DateCreated
				};

				return Ok(response);
			}
			return BadRequest(ModelState);
		}

		private void ValidateImageUpload(IFormFile file)
		{
			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
			var maxFileSizeInBytes = 5 * 1024 * 1024; // 5 MB
			var fileExtension = Path.GetExtension(file.FileName).ToLower();
			if (!allowedExtensions.Contains(fileExtension))
			{
				ModelState.AddModelError("File", "Invalid file type. Only JPG, JPEG, PNG, and GIF are allowed.");
			}
			if (file.Length > maxFileSizeInBytes)
			{
				ModelState.AddModelError("File", "File size exceeds the maximum limit of 5 MB.");
			}
		}
	}
}
