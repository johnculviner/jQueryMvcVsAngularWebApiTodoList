using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jQueryMvcVsAngularWebApiTodoList.Models
{
    public class TodoList
    {
        public TodoList()
        {
            Items = new List<TodoListItem>();
        }

        public string Id { get; set; }

        //owner properties
        [Required]
        [Display(Name="Owner First Name")]
        public string OwnerFirstName { get; set; }
        [Required]
        [Display(Name = "Owner Last Name")]
        public string OwnerLastName { get; set; }

        public List<TodoListItem> Items { get; set; }
    }
}