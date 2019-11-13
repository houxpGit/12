using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPlatformLib
{
    public interface IControlDriver
    {
        bool GetDriverStatus();
        void FreshDriverStatus();
    }
}
