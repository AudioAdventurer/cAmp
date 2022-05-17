using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Records
{
    public record User : AbstractcAmpRecord
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Username { get; init; }

        public string HashedPassword { get; init; }

        public string Salt { get; init; }

        public int Volume { get; set; }

        public override IcAmpObject ToUserInterfaceObject()
        {
            return new UserInterfaceObjects.User()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Volume = Volume
            };
        }
    }
}
