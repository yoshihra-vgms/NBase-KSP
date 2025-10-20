using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseData.DAC;

namespace Yojitsu.DA
{
    internal class ExcelSource_貸船借船料 : IExcelSource
    {
         private BgYosanHead yosanHead;


        public ExcelSource_貸船借船料(BgYosanHead yosanHead)
        {
            this.yosanHead = yosanHead;
        }


       #region IExcelSource メンバ

        public bool ReadFile(string fileName, out Excelファイル読込Form.YosanObjectCollection yosanObject, int msVesselId)
        {
            yosanObject = new Yojitsu.Excelファイル読込Form.YosanObjectCollection();

            ExcelObject_貸船借船料 kashiObj;
            ExcelObject_貸船借船料 kariObj;

            ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator();

            try
            {
                if (xls.ReadBook(fileName) == 0)
                {
                    kashiObj = ExcelObject_貸船借船料.Create(fileName, xls, ExcelObject_貸船借船料.ConfigType.貸船料, yosanObject, yosanHead, msVesselId);
                    kariObj = ExcelObject_貸船借船料.Create(fileName, xls, ExcelObject_貸船借船料.ConfigType.借船料, yosanObject, yosanHead, msVesselId);
                }
                else
                {
                    MessageBox.Show("ファイルが読み込めませんでした。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                xls.CloseBook(true);
            }
            
            return true;
        }

        #endregion
    }
}
