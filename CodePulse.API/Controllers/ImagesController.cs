using CodePulse.API.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
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
            }
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
