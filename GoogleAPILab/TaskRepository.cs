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
using Google.Apis.Tasks.v1;
using Google.Apis.Tasks.v1.Data;

namespace GoogleAPILab
{
    public class TaskRepository
    {
        private UserCredential credential;
        private TasksService service;

        private async System.Threading.Tasks.Task AuthenticateAsync()
        {
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { TasksService.Scope.Tasks },
                    "user", CancellationToken.None, new FileDataStore("TaskrRepository.Store")).Result;
            }


            service = new TasksService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Tasks API Sample",
            });

        }

        public async Task<TaskLists> GetTaskListAsync()
        {
            await AuthenticateAsync();

            TaskLists results = service.Tasklists.List().Execute();
            return results;


        }
    }
}