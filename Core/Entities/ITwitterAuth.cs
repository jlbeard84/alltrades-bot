namespace alltrades_bot.Core.Entities
{
    public interface ITwitterAuth
    {
         string token_type { get; set; }
         
         string access_token { get; set; }
    }
}