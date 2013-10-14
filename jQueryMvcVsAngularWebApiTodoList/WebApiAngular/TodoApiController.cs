using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using jQueryMvcVsAngularWebApiTodoList.Models;
using jQueryMvcVsAngularWebApiTodoList.MvcjQuery.ViewModels;
using jQueryMvcVsAngularWebApiTodoList.Services;
using jQueryMvcVsAngularWebApiTodoList.WebApiAngular.Filters;

namespace jQueryMvcVsAngularWebApiTodoList.WebApiAngular
{
    [ValidationActionFilter] //handles validation in a uniform way, normally this should be added globally...
    public class TodoApiController : ApiController
    {
        private TodoListService _todoListService;

        public TodoApiController()
        {
            //simple demo, no real DI
            _todoListService = new TodoListService();
        }

        public IEnumerable<TodoListSummaryViewModel> Get()
        {
            return _todoListService.GetAll();
        }

        public TodoList Get(string id)
        {
            return _todoListService.Get(id);
        }

        public HttpResponseMessage Post(TodoList todoList)
        {
            _todoListService.Post(todoList);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(TodoList todoList)
        {
            _todoListService.Delete(todoList.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}