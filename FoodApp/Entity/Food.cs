namespace FoodApp.Entity
{
    public class Food
    {
        public int FoodId{get;set;}
        public string? Title{get;set;}
        public string? Content{get;set;}
        public string? Url {get;set;}
        public int UserId {get;set;}
         public User User {get;set;} = null!;
        public int Price{get;set;}

        public List<Comment> Comments {get;set;} = new List<Comment>();
        public List<Category> Categories {get;set;} = new List<Category>();
    }

}