using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Objects
{
    public class User : AbstractcAmpObject
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string HashedPassword { get; set; }

        public string Salt { get; set; }

        public override IcAmpObject ToUserInterfaceObject()
        {
            return new UserInterfaceObjects.User()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Username = Username
            };
        }
    }
}
