namespace alltrades_bot.Core.Entities {
    public interface ITwitterResponseMessage {
        string Message { get; set; }

        string ResponseID { get; set; }
    }
}