using Newtonsoft.Json;

namespace WowStats.Common.Models.User
{
    public class WarshipsUser
    {
		[JsonProperty(PropertyName = "account_id")]
		public int AccountId { get; set; }

		[JsonProperty(PropertyName = "nickname")]
        public string? Nickname { get; set; }
    }
}
