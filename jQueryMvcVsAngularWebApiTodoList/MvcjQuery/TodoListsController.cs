using System;
using System.Linq;
using System.Web.Mvc;
using jQueryMvcVsAngularWebApiTodoList.Models;
using jQueryMvcVsAngularWebApiTodoList.MvcjQuery.ViewModels;
using jQueryMvcVsAngularWebApiTodoList.Services;

namespace jQueryMvcVsAngularWebApiTodoList.MvcjQuery
{
    public class TodoListsController : Controller
    {
        private TodoListService _todoListService;

        public TodoListsController()
        {
            //simple demo, no real DI
            _todoListService = new TodoListService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_todoListService.GetAll());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("TodoList", new TodoList());
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            return PartialView("TodoList", _todoListService.Get(id));
        }

        [HttpPost]
        public EmptyResult Delete(string id)
        {
            _todoListService.Delete(id);
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult Edit(TodoList todoList)
        {
            if (!ModelState.IsValid){
                return PartialView("TodoList", todoList);
            }

            _todoListService.Post(todoList);
            return new EmptyResult();
        }
    }
}
