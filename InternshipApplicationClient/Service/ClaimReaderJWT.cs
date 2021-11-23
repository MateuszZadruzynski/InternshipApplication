using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace InternshipApplicationClient.Service
{
    public static class ClaimReaderJWT
    {
        public static IEnumerable<Claim> GetClaims(string token)
        {
            var claimList = new List<Claim>();

            var split = token.Split(".")[1];
            var json = Parse(split);

            var key = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            claimList.AddRange(key.Select(k => new Claim(k.Key, k.Value.ToString())));
            return claimList;
        }

        private static byte[] Parse(string split)
        {
            switch (split.Length % 4)
            {
                case 2: split += "=="; break;
                case 3: split += "="; break;
            }
            return Convert.FromBase64String(split);
        }
    }
}
