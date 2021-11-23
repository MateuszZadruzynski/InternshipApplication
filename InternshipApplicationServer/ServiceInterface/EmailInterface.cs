using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace InternshipApplicationServer.ServiceInterface
{
    public interface EmailInterface
    {
        public void Send(Email emailData);
    }
}
