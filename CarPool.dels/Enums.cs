using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public enum BookingStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public enum Places
    {
        Secunderabad=0,
        Miyapur,
        Madhapur,
        Durgamcheruvu,
        Lingampally,
        Kothaguda,
        Kondapur,
        Hitechcity
    }

    //public enum CarType
    //{    
    //    PrimeSuv,
    //    PrimeExec,
    //    PrimePlay,
    //    PrimeSeldon,
    //    mini,
    //    micro,
    //}

}
