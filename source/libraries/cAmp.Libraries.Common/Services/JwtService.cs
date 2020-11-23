using System;
using System.Collections.Generic;
using System.Text;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Security;

namespace cAmp.Libraries.Common.Services
{
    public class JwtService
    {
        private readonly string _jwtSecret;

        public JwtService(string jwtSecret)
        {
            _jwtSecret = jwtSecret;
        }

        public string CreateJwt(IWebSession session)
        {
            return JwtHelper.CreateJwt(session, _jwtSecret, 1);
        }
    }
}
