using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : Controller
	{
		private readonly IBlogPostRepository blogPostRepository;

		public BlogPostController(IBlogPostRepository blogPostRepository)
		{
			this.blogPostRepository = blogPostRepository;
		}

		[HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody]CreateBlogPostRequestDto request)
		{
			//Convert Dto to Domain Model
			var blogPost = new BlogPost
			{
				Title = request.Title,
				ShortDescription = request.ShortDescription,
				UrlHandel = request.UrlHandel,
				PublishedDate = request.PublishedDate,
				Author = request.Author,
				FeaturedImageUrl = request.FeaturedImageUrl,
                Isvisible = request.Isvisible
			};
			blogPost = await blogPostRepository.CreateAsync(blogPost);

			//Convert Domain Model to Dto
			var response = new BlogPostDto
			{
				Id = blogPost.Id,
				Title = blogPost.Title,
				ShortDescription = blogPost.ShortDescription,
				UrlHandel = blogPost.UrlHandel,
				PublishedDate = blogPost.PublishedDate,
				Author = blogPost.Author,
				FeaturedImageUrl = blogPost.FeaturedImageUrl,
				Isvisible = blogPost.Isvisible
            };
			return Ok(response);
        }
    }
}
