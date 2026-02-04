using AspUrlShortnerer.Domain.Services;
using AspUrlShortnerer.Domain.entities;

namespace AspUrlShortnerer.Application
{
    public class RegistrationApplcation
    {
        public RegistrationApplcation() { }
        public UserLogin CreateUser()
        {
            return new UserLogin();
        }
    }
}
