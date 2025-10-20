using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBaseData.DS
{
    public interface IYojitsu
    {
        string Nengetsu { get; set; }

        decimal YenAmount { get; set; }
        decimal DollerAmount { get; set; }
        decimal Amount { get; set; }

        decimal PreYenAmount { get; set; }
        decimal PreDollerAmount { get; set; }
        decimal PreAmount { get; set; }

    }
}
