using Newtonsoft.Json;
using WowStats.Common.Models.WargamingCommon;

namespace WowStats.Common.Models.User
{
    public class WargamingUserResponse : WargamingApiResponseBase
    {
        [JsonProperty(PropertyName = "meta")]
		public WargamingApiResponseMetaBase? Meta { get; set; }

		[JsonProperty(PropertyName = "data")]
        public WarshipsUser[]? Users { get; set; }
    }
}
