﻿using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
	public class BlogPostRepository : IBlogPostRepository
	{
		private readonly ApplicationDBContext dbContext;

		public BlogPostRepository(ApplicationDBContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<BlogPost> CreateAsync(BlogPost blogPost)
		{
			await dbContext.BlogPosts.AddAsync(blogPost);
			await dbContext.SaveChangesAsync();
			return blogPost;
        }

		public async Task<IEnumerable<BlogPost>> GetAllAsync()
		{
			return await dbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
		}

		public async Task<BlogPost?> GetByIdAsync(Guid id)
		{
			return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

		public async Task<BlogPost?> UpdateByIdAsync(BlogPost blogPost)
		{
			var existingBlogPost = await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
			if (existingBlogPost == null)
			{
				return null;
			}

			//update BlogPost
			dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

			//update Category
			existingBlogPost.Categories = blogPost.Categories;

			await dbContext.SaveChangesAsync();
			return blogPost;
        }
		public async Task<BlogPost?> DeleteByIdAsync(Guid id)
		{
			var existingBlogPost = await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
			if (existingBlogPost == null)
			{
				return null;
			}
			dbContext.BlogPosts.Remove(existingBlogPost);
			await dbContext.SaveChangesAsync();
			return existingBlogPost;
        }

    }
}
