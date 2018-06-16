namespace alltrades_bot.Core.Entities {
    public class TwitterResponseMessage : ITwitterResponseMessage {
        public string Message { get; set; }

        public string ResponseID { get; set; }

        public TwitterResponseMessage () { }

        public TwitterResponseMessage (
            string message) {
            Message = message;
        }
    }
}