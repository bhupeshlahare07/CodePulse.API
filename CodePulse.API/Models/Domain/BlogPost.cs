namespace CodePulse.API.Models.Domain
{
	public class BlogPost
	{
        public Guid Id { get; set; }
		public string Title {  get; set; }
		public string ShortDescription { get; set; }
		public string FeaturedImageUrl { get; set; }
		public string UrlHandel { get; set; }
		public DateTime? PublishedDate { get; set; }
		public string Author {  get; set; }
		public bool Isvisible {  get; set; }
    }
}
