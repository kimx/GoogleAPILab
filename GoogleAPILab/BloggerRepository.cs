using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Blogger.v3;
using Google.Apis.Blogger.v3.Data;
using Google.Apis.Services;
using System.Threading;
using System.IO;
using Google.Apis.Util.Store;

namespace GoogleAPILab
{
    /// <summary>
    /// 要先啟用 https://console.developers.google.com/apis/api/blogger.googleapis.com/quotas?project=kimxinfo
    /// </summary>
    public class BloggerRepository
    {
        private UserCredential credential;
        private BloggerService service;

        private async Task AuthenticateAsync()
        {

            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { BloggerService.Scope.BloggerReadonly },
                                   "user", CancellationToken.None
                                   //, new FileDataStore("Blogger.Auth.Store")
                                   ).Result;
                ;
            }


            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "BloggerApp",
            };

            service = new BloggerService(initializer);
        }

        #region IBloggerRepository members

        public async Task<IEnumerable<Blog>> GetBlogsAsync()
        {
            await AuthenticateAsync();

            var list = await service.Blogs.ListByUser("self").ExecuteAsync();
            if (list.Items == null) return Enumerable.Empty<Blog>();

            return from blog in list.Items
                   select new Blog
                   {
                       Id = blog.Id,
                       Name = blog.Name,
                       Posts = blog.Posts
                   };
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(string blogId)
        {
            await AuthenticateAsync();
            var list = await service.Posts.List(blogId).ExecuteAsync();
            return from post in list.Items
                   select new Post
                   {
                       Title = post.Title,
                       Content = post.Content
                   };
        }

        #endregion
    }
}
