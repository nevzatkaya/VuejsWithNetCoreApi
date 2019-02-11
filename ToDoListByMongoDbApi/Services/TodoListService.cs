using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListByMongoDbApi.Models;


namespace ToDoListByMongoDbApi.Services
{
    public class TodoListService
    {
        private readonly IMongoCollection<ToDoList> _todoList;
        public TodoListService(IConfigurationRoot config)
        {

            var client = new MongoClient(config.GetConnectionString("ToDoListDb"));
            var database = client.GetDatabase("ToDoListDb");
            _todoList = database.GetCollection<ToDoList>("ToDoList");
        }

        public List<ToDoList> Get()
        {
            return _todoList.Find(a => true).ToList();
        }

        public List<ToDoList> GetByCategory(string category)
        {
            return _todoList.Find(a =>a.Category==category).ToList();
        }

        public ToDoList Get(string id)
        {
            return _todoList.Find<ToDoList>(todo => todo.Id == id).FirstOrDefault();
        }

        public ToDoList Create(ToDoList book)
        {
            _todoList.InsertOne(book);
            return book;
        }

        public void Update(string id, ToDoList todoIn)
        {
            _todoList.ReplaceOne(a => a.Id == id, todoIn);
        }
    }
}
