using Raven.Client.Documents;
using Raven.Client.Documents.Commands;
using Raven.Client.Documents.Session;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using SignalRServer.Models;
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

        public void Add(Task task)
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
    }
}
