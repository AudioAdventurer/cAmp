using System;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.UserInterfaceObjects
{
    public class WebSession : IWebSession
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
    }
}
