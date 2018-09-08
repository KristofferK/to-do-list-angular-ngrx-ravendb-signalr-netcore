using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServer.Database
{
    internal class DocumentStoreHolder
    {
        private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        public static IDocumentStore Store => store.Value;

        private static IDocumentStore CreateStore()
        {
            IDocumentStore store = new DocumentStore()
            {
                Urls = new[] { "http://localhost:8080" },
                Database = ""
            }.Initialize();

            try
            {
                store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord("ToDoApplication")));
            }
            catch
            {
            }

            store = new DocumentStore()
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "ToDoApplication"
            }.Initialize();

            return store;
        }
    }
}
