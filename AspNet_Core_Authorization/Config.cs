using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet_Core_Authorization
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
                {
                    UserClaims=new List<string>{"userID"}//添加userID
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
               {
                   // 可在此处添加其他模式

                   // 使用密码模式
                   new Client
                   {
                       ClientId = "pwd.client",
                       AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                       ClientSecrets =
                       {
                           new Secret("secret".Sha256())
                       },
                       AllowedScopes ={ "api1" }
                   }
            };
        }
    }
}
