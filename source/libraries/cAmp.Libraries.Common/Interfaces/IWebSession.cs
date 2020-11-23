using System;

namespace cAmp.Libraries.Common.Interfaces
{
    public interface IWebSession
    {
        Guid Id { get; set; }
        Guid UserId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Username { get; set; }
    }
}
