using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Linq;
using jQueryMvcVsAngularWebApiTodoList.Models;
using jQueryMvcVsAngularWebApiTodoList.MvcjQuery.ViewModels;

namespace jQueryMvcVsAngularWebApiTodoList.Services
{
    public class TodoListService
    {
        private static IDocumentStore _documentStore;

        //no DI, simple demo
        static TodoListService()
        {
            var documentStore = new EmbeddableDocumentStore
            {
                RunInMemory = true,
                DataDirectory = "App_Data",
            };
            documentStore.Conventions.IdentityPartsSeparator = "-";
            _documentStore = documentStore.Initialize();

            using (var session = _documentStore.OpenSession())
            {
                session.Store(new TodoList
                    {
                        OwnerFirstName = "John",
                        OwnerLastName = "Culviner",
                        Items = new List<TodoListItem>
                            {
                                new TodoListItem
                                {
                                    IsCompleted = false,
                                    Priority = 10,
                                    Task = "Clean house"
                                },
                                new TodoListItem
                                {
                                    IsCompleted = false,
                                    Priority = 1,
                                    Task = "Write code"
                                },
                                new TodoListItem
                                {
                                    IsCompleted = true,
                                    Priority = 5,
                                    Task = "Mow lawn"
                                }
                            }
                    });
                session.SaveChanges();
            }
        }

        public IEnumerable<TodoListSummaryViewModel> GetAll()
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.Query<TodoList>()
                        .Customize(x => x.WaitForNonStaleResultsAsOfLastWrite())
                        .Select(x => new TodoListSummaryViewModel
                           {
                               Id = x.Id,
                               OwnerFirstName = x.OwnerFirstName,
                               OwnerLastName = x.OwnerLastName,
                           });
            }
        }

        public TodoList Get(string id)
        {
            using (var session = _documentStore.OpenSession())
                return session.Load<TodoList>(id);
        }

        public void Post(TodoList todoList)
        {
            using (var session = _documentStore.OpenSession())
            {
                session.Store(todoList);
                session.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (var session = _documentStore.OpenSession())
                session.Advanced.DocumentStore.DatabaseCommands.Delete(id, null);
        }
    }
}