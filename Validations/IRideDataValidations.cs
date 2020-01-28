using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Validations
{
    public interface IRideRootDataValidations
    {
        bool IsValidTimeFormat(string time);

        bool IsValidPlace(string place);

        bool IsValidDateFormat(string date);

    }
}
