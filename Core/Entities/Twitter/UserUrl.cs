using System.Collections;
using System.Collections.Generic;

namespace alltrades_bot.Core.Entities.Twitter {
    public class UserUrl {
        public string url { get; set; }

        public string expanded_url { get; set; }

        public string display_url { get; set; }

        public IEnumerable<int> indices { get; set; } = new List<int> ();
    }
}