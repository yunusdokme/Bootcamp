using Microsoft.AspNetCore.Authentication.Cookies;

namespace FoodApp.Entity
{

public class Comment
{ 
    public int CommentId{get;set;}
    public string? Text{get;set;}
    public DateTime PublishedOn{get;set;}

    public int FoodId{get;set;}
    public Food Food{get;set;} = null!;

    public int UserId {get;set;}
    public User User{get;set;}= null!;

}
}