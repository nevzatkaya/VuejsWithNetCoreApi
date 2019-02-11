const app = new Vue({
    el: '#app',
    name: 'ToDoList',
    data: {
        Description:"",
        categoryName:"",
        dataItem: {Title: '', Status: false},
        dataList: [
            {Id: 1, Description: 'ders notlarını hazırla uzun şeyler yazmak istiyorum buraya çok ama çok uzun şeyler', Status: true, Category:"Genel"},
            {Id: 2, Description: 'intro videosunu hazırla', Status: false, Category: "Genel"},
            {Id: 3, Description: 'kahve al', Status: false, Category: "Genel"},
        ],
        categories:[
            
        ],
        activeCategoryToDoList:[],
        activeCategoryCompletedList:[],
        activeCategory:[],
        allResult:[]

    },
    mounted: function () {
        fetch('http://localhost:5001/GetCategories')
        .then(response => response.json())
        .then(data => this.categories=data);

      },
    methods:{
        changedCategory(categoryName)
        {
            this.activeCategory= this.categories.find(item => item.title === categoryName);
        
            fetch('http://localhost:5001/GetToDoListByCategory/'+categoryName)
            .then(response => response.json())
            .then((data)=>{
                console.log(data);
                this.allResult=[];
                this.allResult=data;
           
            this.activeCategoryToDoList=this.allResult.filter(item => item.status==true);

            this.activeCategoryCompletedList= this.allResult.filter(item => item.status==false);
            } );

           // console.log(result);
         

            //list.filter(function(element){ return element.age >= 10; })
        },
        addCategory()
        {   
           this.$http.post('http://localhost:5001/CreateCategory',{"Title":this.categoryName}).then(response => response.json())
           .then(data =>  this.categories.push(data));
        },
        SaveToDo()
        {
            this.$http.post('http://localhost:5001/CreateToDo',{"Description":this.Description,"Category":this.activeCategory.title,"Status":true}).then(response => response.json())
           .then(data =>  this.activeCategoryToDoList.push(data));
        },
        UpdateToDo(Id)
        {
            this.$http.post('http://localhost:5001/UpdateToDo',{"Id":Id}).then(response => response.json())
           .then(data =>  this.activeCategoryCompletedList.push(data));
           
           var ind= this.activeCategoryToDoList.findIndex(x => x.id==Id);
          this.activeCategoryToDoList.splice(ind,1);
        }

    }

})