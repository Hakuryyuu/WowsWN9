using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using WowStats.Common.Config;
using WowStats.Common.Enums;

namespace WowStats.Common.Utilities
{
    public class WargamingApiHelper
    {
        private WargamingSettings _wargamingSettings;

        public WargamingApiHelper(IOptions<WargamingSettings> wargamingSettingsOptions)
        {
            _wargamingSettings = wargamingSettingsOptions.Value;
        }

        public Uri GetWargamingApiBaseUri()
        {
            return new Uri(_wargamingSettings.ApiUrl);
        }

        public string GetWargamingUserApiUrl(string userId, WargamingSearchType searchType = WargamingSearchType.Exact)
        {
            string returnUrl = $"{_wargamingSettings.ApiUrl}/wot/account/list/?application_id={_wargamingSettings.ApplicationId}&search={userId}";

			if (searchType == WargamingSearchType.Exact)
            {
                returnUrl += "&type=exact";
			}

            return returnUrl;
		}

        public string GetWarshipInfoApiUrl(int shipId)
        {
            return $"https://api.worldofwarships.com/wows/encyclopedia/ships/?language=en&application_id={_wargamingSettings.ApplicationId}&ship_id={shipId}";
		}
    }
}
