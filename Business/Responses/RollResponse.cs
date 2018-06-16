using System;
using System.Linq;
using System.Text.RegularExpressions;
using alltrades_bot.Core;
using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Entities.Twitter;

namespace alltrades_bot.Business.Responses
{
    public class RollResponse : BaseTwitterResponse
    {
        private const string RegExPattern = "([0-9]{1,3})[Dd]([0-9]{1,3})";

        private const string ResponseMessage = "Hey @{0}, you rolled {1} for a result of {2}";

        private const string DieIdentifier = "d";
        private const string DefaultDieTypeName = "1d6";
        private const int DefaultDieNum = 1;
        private const int DefaultDieType = 6;

        public RollResponse(Tweet tweet)
            : base(tweet)
        { }

        protected override ITwitterResponseMessage ImplementRespond()
        {
            var message = Tweet.truncated.HasValue && Tweet.truncated.Value
                ? Tweet.extended_tweet.full_text
                : Tweet.text;

            var regex = new Regex(RegExPattern);

            var matches = regex.Matches(message);

            var dieType = DefaultDieTypeName;
            var result = 0;

            if (matches.Any())
            {
                var matchValue = matches
                    .First()
                    .Value
                    .ToLower();

                var halves = matchValue.Split(DieIdentifier);

                if (halves.Count() == 2 && 
                    int.TryParse(halves[0], out var dieNum) && 
                    int.TryParse(halves[1], out var dieSize))
                {
                    dieType = matchValue;

                    result = Roll(
                        dieNum,
                        dieSize);
                }
                else
                {
                    result = RollDefault();
                }
            }
            else
            {
                result = RollDefault();
            }

            var response = new TwitterResponseMessage(
                string.Format(ResponseMessage, Tweet.user.screen_name, dieType, result));

            return response;
        }

        private int RollDefault()
        {
            return Roll(
                DefaultDieNum,
                DefaultDieType);
        }

        private int Roll(
            int num,
            int size)
        {
            var total = 0;
            var random = new Random();

            if (size < 1)
            {
                size = 2;
            }

            for (var i = 0; i < num; i++)
            {
                total += random.Next(1, size);
            }

            return total;
        }
    }
}