using System.Collections.Generic;

namespace alltrades_bot.Core.Entities.Twitter {
    public class UserUrls {
        public IList<UserUrl> urls { get; set; } = new List<UserUrl> ();
    }
}