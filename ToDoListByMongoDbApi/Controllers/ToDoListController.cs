using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using ToDoListByMongoDbApi.Services;
using ToDoListByMongoDbApi.Models;
using Microsoft.AspNetCore.Cors;

namespace ToDoListByMongoDbApi.Controllers
{
    [Route("api/[controller]")]
  
    public class ToDoListController : Controller
    {
        // GET api/values
        private readonly TodoListService _todoListService;

        private readonly CategoryService _categoryService;

        public ToDoListController(TodoListService todoListService,CategoryService categoryService)
        {
            _todoListService = todoListService;
            _categoryService = categoryService;
        }

        [HttpGet("/GetCategories")]
        [EnableCors("AllowAllOrigins")]
        public ActionResult GetCategories()
        {
            //_categoryService.Create(new Category() { Title = "Havale" });
            //_categoryService.Create(new Category() { Title = "Talimat" });
            //_categoryService.Create(new Category() { Title = "Tanımlı Hesaplar" });
            // _todoListService.Create(new ToDoList() {Category = "Havale", Description = "Sayfa açıldıktan sonra hesapların listelenmesi" });
            return Json(_categoryService.Get());
        }

        [HttpPost("/CreateCategory")]
        [EnableCors("AllowAllOrigins")]
        public ActionResult CreateCategory([FromBody]Category category)
        {
            var todo = _categoryService.Create(new Category() { Title=category.Title });

            return Json(todo);
        }

        [HttpGet]
        public ActionResult Get()
        {
           // _todoListService.Create(new ToDoList() {Category = "Havale", Description = "Sayfa açıldıktan sonra hesapların listelenmesi" });
            return Json(_todoListService.Get());
        }

        [HttpGet("/GetToDoListByCategory/{categoryName}")]
        
        public ActionResult GetToDoListByCategory(string categoryName)
        {
            var todo = _todoListService.GetByCategory(categoryName);
            
            return Json(todo);
        }

        [HttpPost("/CreateToDo")]
        public ActionResult CreateToDo([FromBody]ToDoList todoList)
        {
            var todo=_todoListService.Create(todoList);

            return Json(todo);
        }

        [HttpPost("/UpdateToDo")]
        public IActionResult Update([FromBody]ToDoList todoList)
        {
            var todo = _todoListService.Get(todoList.Id);
            todo.Status = !todo.Status;
            _todoListService.Update(todoList.Id,todo);                     

            return Json(todo);
        }
    }
}
