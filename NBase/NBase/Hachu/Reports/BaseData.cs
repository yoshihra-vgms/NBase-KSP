using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hachu.Reports
{
    public partial class BaseData
    {
        protected string formNumber;
        public string FormNumber
        {
            get { return formNumber; }
        }
        protected string className;
        public string ClassName
        {
            get { return className; }
        }
        public virtual void Output(int FromYear, int FromMonth, int ToYear, int ToMonth)
        {
            // ランニング管理表
            // 入渠管理表
            // 船用品管理表
            MessageBox.Show("派生クラスで、ここのコードを書いてください。");
        }
        public virtual void Output(NBaseData.DAC.MsVessel MsVessel, int FromYear, int FromMonth, int ToYear, int ToMonth)
        {
            // 貯蔵品管理表
            MessageBox.Show("派生クラスで、ここのコードを書いてください。");
        }
        public virtual void Output(NBaseData.DAC.MsVessel MsVessel, int FromYear)
        {
            // 貯蔵品年間管理表
            MessageBox.Show("派生クラスで、ここのコードを書いてください。");
        }
        public virtual void Output(int FromYear, List<bool> Targets)
        {
            // データ出力
            MessageBox.Show("派生クラスで、ここのコードを書いてください。");
        }
    }
}
