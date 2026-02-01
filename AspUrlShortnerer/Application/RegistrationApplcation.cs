using AspUrlShortnerer.Domain.Services;

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
