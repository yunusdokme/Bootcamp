namespace FoodApp.Entity
{
    public class Category
    {
        public int CategoryId {get;set;}
        public string? Text{get;set;}
        public string? Url {get;set;}

        public List<Food> Foods {get;set;} =new List<Food>();


    }

}