using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NBaseData.DS
{
    [Serializable]
    public class BlobUnkouhi : IGenericCloneable<BlobUnkouhi>
    {
        public Store 円データ;
        public Store ドルデータ;


        public BlobUnkouhi()
        {
            円データ = new Store();
            ドルデータ = new Store();
        }
        
        
        [Serializable]
        public class Store : IGenericCloneable<Store>
        {
            public bool 運賃1_Checked = true;
            
            // 貨物運賃
            // 運賃1
            public List<Line1> 運賃1 = new List<Line1>();

            // 運賃2
            public decimal 運賃2_固定費;
            public List<Line2> 運賃2_燃料費 = new List<Line2>();
            public List<Line2> 運賃2_港費 = new List<Line2>();
            public List<Line3> 運賃2_追加費 = new List<Line3>(); // 2012.12 Add

            // 燃料費
            public List<Line2> 燃料費 = new List<Line2>();

            // 港費
            public List<Line2> 港費 = new List<Line2>();

            // 貨物費
            public decimal 貨物費;

            // 運賃2
            public decimal その他運航費;
            
            
            public Store()
            {
                for (int i = 0; i < 4; i++)
                {
                    運賃1.Add(new Line1());
                    運賃2_燃料費.Add(new Line2());
                    運賃2_港費.Add(new Line2());
                    燃料費.Add(new Line2());
                    港費.Add(new Line2());
                }

                // 2012.12 : Add 4Lines
                for (int i = 0; i < 2; i++)
                {
                    運賃2_追加費.Add(new Line3());
                }
            }

            // 2012.12 : Add Method
            public void Add追加運賃()
            {
                if (運賃2_追加費 == null)
                {
                    運賃2_追加費 = new List<Line3>();
                    for (int i = 0; i < 2; i++)
                    {
                        運賃2_追加費.Add(new Line3());
                    }
                }
            }


            public decimal 貨物運賃_計()
            {
                if (運賃1_Checked)
                {
                    return Calc_計(運賃1);
                }
                else
                {
                    // 2012.12 : Modify
                    //return (運賃2_固定費 + Calc_計(運賃2_燃料費) + Calc_計(運賃2_港費));
                    return (運賃2_固定費 + Calc_計(運賃2_燃料費) + Calc_計(運賃2_港費) + Calc_計(運賃2_追加費));
                }
            }


            public decimal 燃料費_計()
            {
                return Calc_計(燃料費);
            }


            public decimal 港費_計()
            {
                return Calc_計(港費);
            }


            public decimal 貨物費_計()
            {
                return 貨物費;
            }


            public decimal その他運航費_計()
            {
                return その他運航費;
            }


            //private static decimal Calc_計(List<Line1> lines)
            public static decimal Calc_計(List<Line1> lines)
            {
                decimal total = 0;

                foreach (Line1 l in lines)
                {
                    total += l.単価 * l.数量 * l.航海数;
                }

                return total;
            }
            public static string Calc式(List<Line1> lines)
            {
                string 式 = "";
                decimal total = 0;

                foreach (Line1 l in lines)
                {
                    total = l.単価 * l.数量 * l.航海数;
                    if (total != 0)
                    {
                        if (式.Length > 0)
                            式 += " + ";

                        式 += "(" + l.単価.ToString() + "*" + l.数量.ToString() + "*" + l.航海数.ToString() + ")";
                    }
                }

                return 式;
            }
            public static string Calc式(List<Line1> lines, decimal rate)
            {
                string 式 = "";
                decimal total = 0;

                foreach (Line1 l in lines)
                {
                    total = l.単価 * l.数量 * l.航海数;
                    if (total != 0)
                    {
                        if (式.Length > 0)
                            式 += " + ";

                        式 += "(" + l.単価.ToString() + "*" + l.数量.ToString() + "*" + l.航海数.ToString() + "*" + rate .ToString() +")";
                    }
                }

                return 式;
            }


            //private static decimal Calc_計(List<Line2> lines)
            public static decimal Calc_計(List<Line2> lines)
            {
                decimal total = 0;

                foreach (Line2 l in lines)
                {
                    total += l.単価 * l.数量;
                }

                return total;
            }
            public static string Calc式(List<Line2> lines)
            {
                string 式 = "";
                decimal total = 0;

                foreach (Line2 l in lines)
                {
                    total = l.単価 * l.数量;
                    if (total != 0)
                    {
                        if (式.Length > 0)
                            式 += " + ";

                        式 += "(" + l.単価.ToString() + "*" + l.数量.ToString() + ")";
                    }
                }

                return 式;
            }
            public static string Calc式(List<Line2> lines, decimal rate)
            {
                string 式 = "";
                decimal total = 0;

                foreach (Line2 l in lines)
                {
                    total = l.単価 * l.数量;
                    if (total != 0)
                    {
                        if (式.Length > 0)
                            式 += " + ";

                        式 += "(" + l.単価.ToString() + "*" + l.数量.ToString() + "*" + rate.ToString() + ")";
                    }
                }

                return 式;
            }


            //private static decimal Calc_計(List<Line3> lines)
            public static decimal Calc_計(List<Line3> lines)
            {
                decimal total = 0;
                if (lines != null)
                {
                    foreach (Line3 l in lines)
                    {
                        total += l.単価 * l.数量;
                    }
                }
                return total;
            }
            public static string Calc式(List<Line3> lines)
            {
                string 式 = "";
                decimal total = 0;

                foreach (Line3 l in lines)
                {
                    total = l.単価 * l.数量;
                    if (total != 0)
                    {
                        if (式.Length > 0)
                            式 += " + ";

                        式 += "(" + l.単価.ToString() + "*" + l.数量.ToString() + ")";
                    }
                }

                return 式;
            }
            public static string Calc式(List<Line3> lines, decimal rate)
            {
                string 式 = "";
                decimal total = 0;

                foreach (Line3 l in lines)
                {
                    total = l.単価 * l.数量;
                    if (total != 0)
                    {
                        if (式.Length > 0)
                            式 += " + ";

                        式 += "(" + l.単価.ToString() + "*" + l.数量.ToString() + "*" + rate.ToString() + ")";
                    }
                }

                return 式;
            }

            #region IGenericCloneable<Store> メンバ

            public Store Clone()
            {
                Store clone = new Store();

                clone.運賃1_Checked = 運賃1_Checked;

                for (int i = 0; i < 運賃1.Count; i++)
                {
                    clone.運賃1[i] = 運賃1[i].Clone();
                }
                
                clone.運賃2_固定費 = 運賃2_固定費;

                for (int i = 0; i < 運賃2_燃料費.Count; i++)
                {
                    clone.運賃2_燃料費[i] = 運賃2_燃料費[i].Clone();
                }

                for (int i = 0; i < 運賃2_港費.Count; i++)
                {
                    clone.運賃2_港費[i] = 運賃2_港費[i].Clone();
                }

                // 2012.12 : Add 7Lines
                if (運賃2_追加費 != null)
                {
                    for (int i = 0; i < 運賃2_追加費.Count; i++)
                    {
                        clone.運賃2_追加費[i] = 運賃2_追加費[i].Clone();
                    }
                }

                for (int i = 0; i < 燃料費.Count; i++)
                {
                    clone.燃料費[i] = 燃料費[i].Clone();
                }

                for (int i = 0; i < 港費.Count; i++)
                {
                    clone.港費[i] = 港費[i].Clone();
                }

                clone.貨物費 = 貨物費;
                clone.その他運航費 = その他運航費;
                
                return clone;
            }

            #endregion
        }


        [Serializable]
        public class Line1 : IGenericCloneable<Line1>
        {
            public decimal 単価;
            public decimal 数量;
            public decimal 航海数;

            #region IGenericCloneable<Line1> メンバ

            public Line1 Clone()
            {
                Line1 clone = new Line1();

                clone.単価 = 単価;
                clone.数量 = 数量;
                clone.航海数 = 航海数;
                
                return clone;
            }

            #endregion
        }


        [Serializable]
        public class Line2 : IGenericCloneable<Line2>
        {
            public decimal 単価;
            public decimal 数量;

            #region IGenericCloneable<Line2> メンバ

            public Line2 Clone()
            {
                Line2 clone = new Line2();

                clone.単価 = 単価;
                clone.数量 = 数量;

                return clone;
            }

            #endregion
        }

        [Serializable]
        public class Line3 : IGenericCloneable<Line3>
        {
            public string 項目;
            public decimal 単価;
            public decimal 数量;

            #region IGenericCloneable<Line3> メンバ

            public Line3 Clone()
            {
                Line3 clone = new Line3();

                clone.項目 = 項目;
                clone.単価 = 単価;
                clone.数量 = 数量;

                return clone;
            }

            #endregion
        }

        #region IGenericCloneable<BlobUnkouhi> メンバ

        public BlobUnkouhi Clone()
        {
            BlobUnkouhi clone = new BlobUnkouhi();

            clone.円データ = 円データ.Clone();
            clone.ドルデータ = ドルデータ.Clone();

            return clone;
        }

        #endregion
    }
}
