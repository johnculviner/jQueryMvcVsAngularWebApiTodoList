using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace jQueryMvcVsAngularWebApiTodoList.Models
{
    public class TodoListItem
    {
        public TodoListItem()
        {
            Priority = 5;
        }

        public bool IsCompleted { get; set; }
        [Required]
        public string Task { get; set; }
        [Range(1,10)]
        public int Priority { get; set; }
    }
}
