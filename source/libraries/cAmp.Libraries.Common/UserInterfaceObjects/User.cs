using System;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.UserInterfaceObjects
{
    public class User : IcAmpObject
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public int Volume { get; set; }
    }
}
