using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yojitsu.DA
{
    public interface IExcelSource
    {
        bool ReadFile(string fileName, out Yojitsu.Excelファイル読込Form.YosanObjectCollection yosanObject, int msVesselId);
    }
}
