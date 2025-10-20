using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hachu.Chozo
{
    /// <summary>
    /// 表示情報管理
    /// </summary>
    public class 貯蔵品TreeInfo
    {
        //表示するデータ
        public string 品名 = "";
        public int 繰越 = 0;
        public decimal 繰越金額 = 0;
        public int 受け入れ = 0;
        public decimal 受け入れ金額 = 0;
        public int 計 = 0;
        public int 消費 = 0;
        public decimal 消費金額 = 0;

        public int 残量 = 0;
        public decimal 金額 = 0;    //残量金額

        // 2012.08: 金額計算は貯蔵品リストからするように変更
        //public List<NBaseData.BLC.貯蔵品編集RowData残量> 残量s = new List<NBaseData.BLC.貯蔵品編集RowData残量>();
        public List<NBaseData.BLC.貯蔵品リスト> リストs = new List<NBaseData.BLC.貯蔵品リスト>();

        // 次期改造
        public string カテゴリ = "";

        //--------------------------------------
        //持っておくデータ
        public int Tanka;       //品物の単価
        public string ID = "";
        public string 単位 = "";


        //関連データ
        public NBaseData.DAC.OdChozoShousai OdChozoShousaiData;
        

        public 貯蔵品TreeInfo()
        {
        }

        public 貯蔵品TreeInfo(NBaseData.BLC.貯蔵品編集RowData rd)
        {
            this.ID = rd.ID;
            this.品名 = rd.品名;
            this.繰越 = rd.繰越;
            this.繰越金額 = rd.繰越金額;
            this.受け入れ = rd.受入;
            this.受け入れ金額 = rd.受入金額;
            this.計 = rd.繰越 + rd.受入;
            this.消費 = rd.消費;
            this.消費金額 = rd.消費金額;
            this.残量 = rd.残量;
            this.金額 = rd.残量金額;
            this.OdChozoShousaiData = rd.OdChozoShousaiData;
            // 2012.08: 金額計算は貯蔵品リストからするように変更
            //this.残量s.AddRange(rd.残量s);
            this.リストs.AddRange(rd.リストs);

            // 次期改造
            #region 次期改造
            this.カテゴリ = rd.カテゴリ;
            #endregion
        }
    }

    //検索月管理クラス
    public class 検索月管理
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sm"></param>
        /// <param name="em"></param>
        public 検索月管理(string name, int sm, int em, int offset)
        {
            this.表示名 = name;
            this.StartMonth = sm;
            this.EndMonth = em;
            this.YearOffset = offset;
        }

        /// <summary>
        /// 表示名
        /// </summary>
        public string 表示名 = "";

        /// <summary>
        /// 開始月
        /// </summary>
        public int StartMonth = 0;

        /// <summary>
        /// 終了月
        /// </summary>
        public int EndMonth = 0;

        /// <summary>
        /// 年のオフセット
        /// </summary>
        public int YearOffset;



    }
}
