using Raven.Client.Documents;
using Raven.Client.Documents.Commands;
using Raven.Client.Documents.Session;
using SignalRServer.Models;
using Sparrow.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalRServer.Database
{
    public class DatabaseAccessLayer
    {
        private static IDocumentStore store;
        
        public DatabaseAccessLayer()
        {
            if (store == null)
            {
                store = DocumentStoreHolder.Store;
                store.OpenSession();
            }
        }

        public void Put(Task task)
        {
            var session = store.OpenSession();
            var docInfo = new DocumentInfo
            {
                Collection = "Tasks"
            };
            var blittableDoc = session.Advanced.EntityToBlittable.ConvertEntityToBlittable(task, docInfo);

            var command = new PutDocumentCommand(task.Id, null, blittableDoc);
            session.Advanced.RequestExecutor.Execute(command, session.Advanced.Context);
        }

        public List<Task> GetTasks()
        {
            var session = store.OpenSession();
            var command = new GetDocumentsCommand(start: 0, pageSize: 1024);
            session.Advanced.RequestExecutor.Execute(command, session.Advanced.Context);
            
            return command.Result.Results
                .OfType<BlittableJsonReaderObject>()
                .Select(obj => new Task()
                {
                    Id = ((BlittableJsonReaderObject)obj["@metadata"])["@id"].ToString(),
                    Title = obj["Title"].ToString(),
                    Completed = (bool)obj["Completed"]
                })
                .ToList();
        }
    }
}
