using System.Collections.Generic;

namespace alltrades_bot.Core.Entities.Twitter {
    public class SearchResponse {
        public List<Tweet> statuses = new List<Tweet> ();

        public SearchMetadata search_metadata;
    }
}