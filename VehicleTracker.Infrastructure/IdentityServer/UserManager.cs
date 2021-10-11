using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace VehicleTracker.Infrastructure.IdentityServer
{
    public class UserManager
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                        new TestUser
                        {
                            SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                            Username = "joshua.dev@yahoo.com",
                            Password = "joshua123",
                            Claims = new List<Claim>
                            {
                                new Claim("given_name", "Joshua"),
                                new Claim("family_name", "dev")
                            }
                        },
            };
        }
    }
}
