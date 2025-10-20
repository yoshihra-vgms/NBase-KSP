using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace DeficiencyControl.Grid
{
    /// <summary>
    /// 一覧チェック付きGrid
    /// </summary>
    public abstract class BaseEditCheckGrid : BaseGrid
    {
        /// <summary>
        /// 一覧Colのチェック場所
        /// </summary>
        public const int CheckColIndex = 1;



        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dgv"></param>
        public BaseEditCheckGrid(DataGridView dgv)
            : base(dgv)
        {
            //Blueモードグリッド整備
            this.Grid.Columns[CheckColIndex].Visible = false;
            if (AppConfig.Config.ConfigData.DeficiencyControlBlueMode == true)
            {
                this.Grid.Columns[CheckColIndex].Visible = true;
                this.Grid.Columns[CheckColIndex].DefaultCellStyle.BackColor = DeficiencyControlColor.DeficiencyControlBlueModeColor;
                this.Grid.ReadOnly = false;
            }
        }


        /// <summary>
        /// チェックが変更されたデータを一括取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract List<T> GetEditCheckList<T>();


        /// <summary>
        /// 全チェックを設定する
        /// </summary>
        /// <param name="f">設定値</param>
        public void SetAllCheck(bool f)
        {
            int count = this.Grid.Rows.Count;

            for (int i = 0; i < count; i++)
            {
                this.Grid[CheckColIndex, i].Value = f;
            }
        }

    }
}
