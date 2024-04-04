using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerRemote.Platforms.Android
{
    public interface IVolumeControl
    {
        void IncreaseVolume();
        void DecreaseVolume();
    }
}
