using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WowStats.Common.Models.User;
using WowStats.Common.Services.Abstractions;
using WowStats.Common.Utilities;

namespace WowStats.Common.Services
{
    public class WarshipsUserService : IWarshipsUserService
    {
		#region Fields
		private ILogger _logger;
        private HttpClient _httpClient;
        private WargamingApiHelper _ApiHelper;
        #endregion

        #region Ctor(s)
        public WarshipsUserService(ILogger<WarshipsUserService> logger, HttpClient client, WargamingApiHelper apiHelper)
        {
            _logger = logger;
            _httpClient = client;
            _ApiHelper = apiHelper;
        }
		#endregion

		#region Public Methods
		public async Task<WarshipsUser?> GetWarshipsUserAsync(string userId)
        {
            WarshipsUser? wargamingUser = null;
			string requestUri;

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(userId);
            }

            requestUri = _ApiHelper.GetWargamingUserApiUrl(userId);

            using (HttpResponseMessage response = await _httpClient.GetAsync(requestUri))
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();                   
					WargamingUserResponse? apiUserResponse = JsonConvert.DeserializeObject<WargamingUserResponse?>(json);

                    if (apiUserResponse != null && apiUserResponse.Status == "ok" && apiUserResponse?.Meta?.Count == 1)
                    {
                        wargamingUser = apiUserResponse?.Users?[0];
                    }
                }
            }

            return wargamingUser;
        }
		#endregion
	}
}
