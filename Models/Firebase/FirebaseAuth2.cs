using KpiAlumni.Configs;
using Newtonsoft.Json;

namespace KpiAlumni.Models.Firebase;

public class FirebaseAuth2
{
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = "";

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; } = 0;

        [JsonProperty("token_type")]
        public string TokenType { get; set; } = "";

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; } = "";

        [JsonProperty("id_token")]
        public string IdToken { get; set; } = "";

        [JsonProperty("user_id")]
        public string UserId { get; set; } = "";

        [JsonProperty("project_id")]
        public string ProjectId { get; set; } = "";

        public async Task<FirebaseAuth2?> ExchangeRefreshTokenAsync(string refreshToken, string userUid)
        {
            try
            {
                // Vars
                var hClient = new HttpClient();
                var apiKey = FirebaseConfig.ApiKey;
                var url = $"https://securetoken.googleapis.com/v1/token?key={apiKey}";

                // Request Content
                var reqContent = JsonConvert.SerializeObject(new
                {
                    grant_type = "refresh_token",
                    refresh_token = refreshToken
                });
                var requestContent = new StringContent(reqContent, System.Text.Encoding.UTF8, "application/json");

                // Call Firebase endpoint for token exchange
                var response = await hClient.PostAsync(url, requestContent);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response into FirebaseAuth
                var data = JsonConvert.DeserializeObject<FirebaseAuth2>(responseContent);
                if (data == null)
                {
                    return null;
                }

                // If User UID Failed
                if (!data.UserId.Equals(userUid))
                {
                    return null;
                }

                // Update the current object with the new token values
                AccessToken = data.AccessToken;
                IdToken = data.IdToken;
                RefreshToken = data.RefreshToken;
                ExpiresIn = data.ExpiresIn;
                UserId = data.UserId;
                ProjectId = data.ProjectId;

                return this;
            }
            catch (Exception)
            {
                return null;
            }
        }
}