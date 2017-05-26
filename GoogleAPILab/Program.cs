using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Tasks.v1;
using Google.Apis.Tasks.v1.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleAPILab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=========Start==========");
            BloggerLab();
            TaskLab();
            Console.WriteLine("=========End==========");
        }

        static void BloggerLab()
        {
            {
                Console.WriteLine("Blogger OAuth2 Sample");
                Console.WriteLine("===================");
                BloggerRepository blogger = new BloggerRepository();
                var blogs = blogger.GetBlogsAsync().Result;
                foreach (var blog in blogs)
                {
                    Console.WriteLine($"{blog.Name}:${blog.Posts.TotalItems}");
                    foreach (var post in blog.Posts.Items)
                    {
                        Console.WriteLine(post.Content);
                    }
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static void TaskLab()
        {
            {
                Console.WriteLine("Tasks OAuth2 Sample");
                Console.WriteLine("===================");
                TaskRepository tasks = new TaskRepository();
                foreach (TaskList list in tasks.GetTaskListAsync().Result.Items)
                {
                    Console.WriteLine("\t\t" + list.Title);
                }
                //UserCredential credential;
                //using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
                //{
                //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                //        GoogleClientSecrets.Load(stream).Secrets,
                //        new[] { TasksService.Scope.Tasks },
                //        "user", CancellationToken.None, new FileDataStore("Tasks.Auth.Store")).Result;
                //}

                //// Create the service.
                //var service = new TasksService(new BaseClientService.Initializer()
                //{
                //    HttpClientInitializer = credential,
                //    ApplicationName = "Tasks API Sample",
                //});

                //TaskLists results = service.Tasklists.List().Execute();
                //Console.WriteLine("\tLists:");

                //foreach (TaskList list in results.Items)
                //{
                //    Console.WriteLine("\t\t" + list.Title);
                //}

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
