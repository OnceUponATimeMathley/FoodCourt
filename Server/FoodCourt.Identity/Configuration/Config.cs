using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace FoodCourt.Identity.Configuration
{
    /// <summary>
    /// Config một số liên quan tới IdentityServer4 và môi trường OAuth2.0
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Lấy thông tin của User
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        /// <summary>
        /// Miêu tả một số cụm API, cấp quyền sử dụng theo từng đối tượng Client.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api", "Main API Resource")
            };
        }

        /// <summary>
        /// Đối tượng Client tương ứng ở trên để truy cập API
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials, //Loại User có thể cấp token
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api", "openid", "profile", "email" }
                },
            };
        }
    }
}

