using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseData.DAC;

namespace Yojitsu.DA
{
    internal class ExcelSource_共通割掛船員 : IExcelSource
    {
        private BgYosanHead yosanHead;


        public ExcelSource_共通割掛船員(BgYosanHead yosanHead)
        {
            this.yosanHead = yosanHead;
        }


        #region IExcelSource メンバ
        public bool ReadFile(string fileName, out Excelファイル読込Form.YosanObjectCollection yosanObject, int vesselId)
        {
            yosanObject = new Yojitsu.Excelファイル読込Form.YosanObjectCollection();

            ExcelObject_人員配置表 jininObj;
            ExcelObject_タリフ表 tariffObj;

            ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator();

            try
            {
                if (xls.ReadBook(fileName) == 0)
                {
                    jininObj = ExcelObject_人員配置表.Create(fileName, xls);
                    tariffObj = ExcelObject_タリフ表.Create(fileName, xls);
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

            Dictionary<string, MsVessel> msVesselDic = Excelファイル読込Form.CreateMsVesselDic();
            Dictionary<string, MsHimoku> msHimokuDic = Excelファイル読込Form.CreateMsHimokuDic();

            foreach (KeyValuePair<string, ExcelObject_人員配置表.人員配置_職種別_Col> pair in jininObj.職種別_Dic)
            {
                string vesselName = pair.Key;

                if (vesselName == ExcelObject_人員配置表.予備員)
                {
                    continue;
                }

                if (!msVesselDic.ContainsKey(vesselName))
                {
                    MessageBox.Show("予実機能で利用可能な船ではありません。\n人員配置表シートの '" + vesselName + "' ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                int msVesselId = msVesselDic[vesselName].MsVesselID;

                ExcelObject_人員配置表.人員配置_職種別_Col vesselCol = pair.Value;

                // 月額に設定する費目の計算.
                foreach (KeyValuePair<string, ExcelObject_タリフ表.HimokuCol> pair2 in tariffObj.HimokuDic)
                {
                    string himokuName = pair2.Key;

                    if (himokuName != ExcelObject_タリフ表.保険料 && !msHimokuDic.ContainsKey(himokuName))
                    {
                        MessageBox.Show("存在しない費目です。\nタリフ表シートの '" + himokuName + "' ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    int msHimokuId;

                    if (himokuName == ExcelObject_タリフ表.保険料)
                    {
                        msHimokuId = Constants.MS_HIMOKU_ID_船員保険料A;
                    }
                    else
                    {
                        msHimokuId = msHimokuDic[himokuName].MsHimokuID;
                    }


                    ExcelObject_タリフ表.HimokuCol himokuCol = pair2.Value;

                    foreach (KeyValuePair<string, decimal> pair3 in himokuCol.AmountDic)
                    {
                        string shokumei = pair3.Key;
                        decimal amount = pair3.Value;

                        //int count = vesselCol.GetCount(shokumei);
                        decimal count = vesselCol.GetCount(shokumei);

                        //if (count == int.MinValue)
                        if (count == decimal.MinValue)
                        {
                            MessageBox.Show("存在しない職名です。\n人員配置表シートの '" + shokumei + "' ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        for (int i = 0; i < NBaseData.DS.Constants.PADDING_MONTHS.Length; i++)
                        {
                            int year = yosanHead.Year;

                            if (i >= 9)
                            {
                                year++;
                            }

                            string nengetsu = year.ToString() + NBaseData.DS.Constants.PADDING_MONTHS[i];

                            if (yosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.見直し && Constants.IsKamiki(nengetsu))
                            {
                                continue;
                            }

                            yosanObject.AddAmount(msVesselId, msHimokuId, nengetsu, amount * count);
                        }
                    }
                }

                foreach (KeyValuePair<string, decimal> pair2 in jininObj.費目別_Dic[vesselName].AmountDic)
                {
                    string himokuName = pair2.Key;

                    if (!msHimokuDic.ContainsKey(himokuName))
                    {
                        MessageBox.Show("存在しない費目です。\n人員配置表シートの '" + himokuName + "' ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    int msHimokuId = msHimokuDic[himokuName].MsHimokuID;

                    if (himokuName == ExcelObject_タリフ表.船員給料_手当)
                    {
                        decimal inc = pair2.Value / 12;

                        for (int i = 0; i < NBaseData.DS.Constants.PADDING_MONTHS.Length; i++)
                        {
                            int year = yosanHead.Year;

                            if (i >= 9)
                            {
                                year++;
                            }

                            string nengetsu = year.ToString() + NBaseData.DS.Constants.PADDING_MONTHS[i];

                            yosanObject.AddAmount(msVesselId, msHimokuId, nengetsu, inc);
                        }
                    }
                    // 年額に設定する費目の計算 (マンニング費).
                    else
                    {
                        yosanObject.AddAmount(msVesselId, msHimokuId, yosanHead.Year.ToString(), pair2.Value);
                    }
                }
            }

            //int 総人員数 = 0;
            //Dictionary<string, int> 各船_人員数 = new Dictionary<string, int>();
            decimal 総人員数 = 0;
            Dictionary<string, decimal> 各船_人員数 = new Dictionary<string, decimal>();

            foreach (KeyValuePair<string, ExcelObject_人員配置表.人員配置_職種別_Col> pair in jininObj.職種別_Dic)
            {
                string vesselName = pair.Key;

                if (vesselName == ExcelObject_人員配置表.予備員 || !msVesselDic.ContainsKey(vesselName))
                {
                    continue;
                }

                各船_人員数[vesselName] = pair.Value.GetCount();
                総人員数 += pair.Value.GetCount();
            }

            // 船員保険料(B) の計算.
            if (tariffObj.HimokuDic.ContainsKey(ExcelObject_タリフ表.保険料) && jininObj.職種別_Dic.ContainsKey(ExcelObject_人員配置表.予備員))
            {
                ExcelObject_タリフ表.HimokuCol hCol = tariffObj.HimokuDic[ExcelObject_タリフ表.保険料];
                ExcelObject_人員配置表.人員配置_職種別_Col vCol = jininObj.職種別_Dic[ExcelObject_人員配置表.予備員];

                decimal 船員保険料B_総額 = 0;

                foreach (KeyValuePair<string, decimal> pair3 in hCol.AmountDic)
                {
                    string shokumei = pair3.Key;
                    decimal amount = pair3.Value;

                    //int count = vCol.GetCount(shokumei);
                    decimal count = vCol.GetCount(shokumei);

                    //if (count == int.MinValue)
                    if (count == decimal.MinValue)
                    {
                        MessageBox.Show("存在しない職名です。\n人員配置表シートの '" + shokumei + "' ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    船員保険料B_総額 += amount * count;
                }

                //foreach (KeyValuePair<string, int> pair in 各船_人員数)
                foreach (KeyValuePair<string, decimal> pair in 各船_人員数)
                {
                    string vesselName = pair.Key;

                    if (vesselName == ExcelObject_人員配置表.予備員 || !msVesselDic.ContainsKey(vesselName))
                    {
                        continue;
                    }

                    decimal percentage = (decimal)pair.Value / (decimal)総人員数;
                    decimal amount = 船員保険料B_総額 * percentage * 12;

                    int msVesselId = msVesselDic[vesselName].MsVesselID;
                    int msHimokuId = Constants.MS_HIMOKU_ID_船員保険料B;

                    yosanObject.AddAmount(msVesselId, msHimokuId, yosanHead.Year.ToString(), amount);
                }
            }

            // 賞与引当金 の計算.
            if (tariffObj.HimokuDic2.ContainsKey(ExcelObject_タリフ表.賞与引当金))
            {
                decimal 賞与引当金総額 = tariffObj.HimokuDic2[ExcelObject_タリフ表.賞与引当金];

                //foreach (KeyValuePair<string, int> pair in 各船_人員数)
                foreach (KeyValuePair<string, decimal> pair in 各船_人員数)
                {
                    string vesselName = pair.Key;

                    if (vesselName == ExcelObject_人員配置表.予備員 || !msVesselDic.ContainsKey(vesselName))
                    {
                        continue;
                    }

                    decimal percentage = (decimal)pair.Value / (decimal)総人員数;
                    decimal amount = 賞与引当金総額 * percentage;

                    int msVesselId = msVesselDic[vesselName].MsVesselID;
                    int msHimokuId = msHimokuDic[ExcelObject_タリフ表.賞与引当金].MsHimokuID;

                    yosanObject.AddAmount(msVesselId, msHimokuId, yosanHead.Year.ToString(), amount);
                }
            }

            // 予備船員費(A) の計算.
            if (tariffObj.HimokuDic2.ContainsKey(ExcelObject_タリフ表.予備船員費A))
            {
                decimal 予備船員費A総額 = tariffObj.HimokuDic2[ExcelObject_タリフ表.予備船員費A];

                //foreach (KeyValuePair<string, int> pair in 各船_人員数)
                foreach (KeyValuePair<string, decimal> pair in 各船_人員数)
                {
                    string vesselName = pair.Key;

                    if (vesselName == ExcelObject_人員配置表.予備員 || !msVesselDic.ContainsKey(vesselName))
                    {
                        continue;
                    }

                    decimal percentage = (decimal)pair.Value / (decimal)総人員数;
                    decimal amount = 予備船員費A総額 * percentage;

                    int msVesselId = msVesselDic[vesselName].MsVesselID;
                    int msHimokuId = msHimokuDic[ExcelObject_タリフ表.予備船員費A].MsHimokuID;

                    yosanObject.AddAmount(msVesselId, msHimokuId, yosanHead.Year.ToString(), amount);
                }
            }

            // 予備船員費(B) の計算.
            if (tariffObj.HimokuDic2.ContainsKey(ExcelObject_タリフ表.予備船員費B) &&
                  tariffObj.HimokuDic.ContainsKey(ExcelObject_タリフ表.船員給料_手当) &&
                  jininObj.職種別_Dic.ContainsKey(ExcelObject_人員配置表.予備員))
            {
                ExcelObject_タリフ表.HimokuCol hCol = tariffObj.HimokuDic[ExcelObject_タリフ表.船員給料_手当];
                ExcelObject_人員配置表.人員配置_職種別_Col vCol = jininObj.職種別_Dic[ExcelObject_人員配置表.予備員];

                decimal 予備船員費B_総額 = 0;

                foreach (KeyValuePair<string, decimal> pair3 in hCol.AmountDic)
                {
                    string shokumei = pair3.Key;
                    decimal amount = pair3.Value;

                    //int count = vCol.GetCount(shokumei);
                    decimal count = vCol.GetCount(shokumei);

                    //if (count == int.MinValue)
                    if (count == decimal.MinValue)
                    {
                        MessageBox.Show("存在しない職名です。\n人員配置表シートの '" + shokumei + "' ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    予備船員費B_総額 += amount * count;
                }

                // 2013.06 : 2月に追加しましたが、予備船員費Bには、航海手当を含まないとのことで、コメントへ
                // 2013.02 : Add-->
                ExcelObject_タリフ表.HimokuCol hCol2 = tariffObj.HimokuDic[ExcelObject_タリフ表.航海手当];
                foreach (KeyValuePair<string, decimal> pair3 in hCol2.AmountDic)
                {
                    string shokumei = pair3.Key;
                    decimal amount = pair3.Value;

                    //int count = vCol.GetCount(shokumei);
                    decimal count = vCol.GetCount(shokumei);

                    if (count == decimal.MinValue)
                    {
                        MessageBox.Show("存在しない職名です。\n人員配置表シートの '" + shokumei + "' ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    予備船員費B_総額 += amount * count;
                }
                // <-- Add End

                //foreach (KeyValuePair<string, int> pair in 各船_人員数)
                foreach (KeyValuePair<string, decimal> pair in 各船_人員数)
                {
                    string vesselName = pair.Key;

                    if (vesselName == ExcelObject_人員配置表.予備員 || !msVesselDic.ContainsKey(vesselName))
                    {
                        continue;
                    }

                    decimal percentage = (decimal)pair.Value / (decimal)総人員数;
                    decimal amount = (予備船員費B_総額 * 12 + tariffObj.HimokuDic2[ExcelObject_タリフ表.予備船員費B]) * percentage;

                    int msVesselId = msVesselDic[vesselName].MsVesselID;
                    int msHimokuId = Constants.MS_HIMOKU_ID_予備船員費B;

                    yosanObject.AddAmount(msVesselId, msHimokuId, yosanHead.Year.ToString(), amount);
                }
            }

            return true;
        }

        #endregion
    }
}
