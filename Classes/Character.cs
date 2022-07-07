using Newtonsoft.Json;
using System;
using System.Net;

namespace LineLauncher.Classes
{
    public class Character
    {
        public string Identifier { get; set; }
        public CharacterInfo CharacterInfo { get; set; }

        public Character(string identifier)
        {
            this.Identifier = identifier;
            try
            {
                using (var webClient = new WebClient())
                {
                    string response = webClient.DownloadString(Utils.LineVInfo.APIUrl + "?hex=" + this.Identifier + "&action=" + Utils.LineVInfo.characterInfoAPIAction);
                    this.CharacterInfo = JsonConvert.DeserializeObject<CharacterInfo>(response);
                }
            }
            catch (Exception e)
            {
                Logger.Error(String.Format("Cannot get char info - {0}", e.Message));
            }
        }
    }

    public partial class CharacterInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("steamAvatar")]
        public string SteamAvatar { get; set; }
    }
}
