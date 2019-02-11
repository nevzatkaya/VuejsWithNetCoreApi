using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListByMongoDbApi.Models;

namespace ToDoListByMongoDbApi.Services
{
    public class CategoryService
    {
        private readonly IMongoCollection<Category> _categoryList;
        public CategoryService(IConfigurationRoot config)
        {

            var client = new MongoClient(config.GetConnectionString("ToDoListDb"));
            var database = client.GetDatabase("ToDoListDb");
            _categoryList = database.GetCollection<Category>("Category");
        }

        public Category Create(Category category)
        {
            _categoryList.InsertOne(category);
            return category;
        }

        public List<Category> Get()
        {
            return _categoryList.Find(a => true).ToList();
        }
    }
}
