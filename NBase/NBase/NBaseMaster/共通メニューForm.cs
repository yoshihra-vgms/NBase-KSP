using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseMaster.MsCustomer;
using NBaseMaster.MsVessel;
using NBaseMaster.MsLo;
using NBaseMaster.MsVesselItem;
using NBaseMaster.MsVesselItemVessel;
using NBaseMaster.MsLoVessel;
using NBaseMaster.船員管理;
using NBaseMaster.権限管理;
using NBaseMaster.アラーム管理;
using NBaseData.DS;
using NBaseData.DAC;

namespace NBaseMaster
{
    public partial class 共通メニューForm : Form
    {
        public 共通メニューForm()
        {
            InitializeComponent();

            this.Text = NBaseCommon.Common.WindowTitle("", "共通メニュー", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            EnableComponents();
        }

        private void 共通メニューForm_Load(object sender, EventArgs e)
        {

            // 契約で有効・無効化する

            int w = 0;

            // 共通      groupBox5

            // 発注      groupBox4
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注) == false)
            {
                // 発注管理 非表示
                w += groupBox4.Width;
                groupBox4.Visible = false;
            }

            // 動静      groupBox3
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.動静) == false)
            {
                // 動静管理 非表示
                w += groupBox3.Width;
                groupBox3.Visible = false;
            }

            // 船員      groupBox1
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員) == false)
            {
                // 船員管理 非表示
                w += groupBox1.Width;
                groupBox1.Visible = false;
            }


            // 指摘事項  groupBox7
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.指摘) == false)
            {
                // 指摘事項 非表示
                w += groupBox7.Width;
                groupBox7.Visible = false;
            }


            // ﾄﾞｷｭﾒﾝﾄ   groupBox6
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.文書) == false)
            {
                // ﾄﾞｷｭﾒﾝﾄ管理 非表示
                w += groupBox6.Width;
                groupBox6.Visible = false;
            }
            else
            {
                if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員) == false)
                {
                    button乗船者登録.Visible = true;
                }
                else
                {
                    button乗船者登録.Visible = false;
                }
            }


            // Form＆共通メニューのサイズ変更
            if (w > 249)
            {
                w = 249; // 最大限小さくできるサイズ
            }
            groupBox5.Width -= w;
            this.Width -= w;
        }

        private void EnableComponents()
        {
            #region 共通

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "共通", "ユーザ管理"))
            {
                buttonユーザ管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "共通", "顧客管理"))
            {
                button顧客管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "共通", "船管理"))
            {
                button船管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "共通", "権限管理"))
            {
                button権限管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "共通", "アラーム管理"))
            {
                buttonアラーム管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "共通", "場所管理"))
            {
                button場所管理.Enabled = true;
            }
            #endregion

            #region 発注管理

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "発注管理", "潤滑油管理"))
            {
                button潤滑油管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "発注管理", "潤滑油船管理"))
            {
                button潤滑油船管理.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "発注管理", "船用品管理"))
            {
                button船用品管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "発注管理", "船用品船管理"))
            {
                button船用品船管理.Enabled = true;
            }


            //船用品はマスタ管理しない
            button船用品管理.Visible = false;
            button船用品船管理.Visible = false;

            #endregion

            #region 動静管理

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "動静管理", "貨物管理"))
            {
                button貨物管理.Enabled = true;
            }



            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "動静管理", "場所区分管理"))
            {
                button場所区分管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "動静管理", "基地管理"))
            {
                button基地管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "動静管理", "バース管理"))
            {
                buttonバース管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "動静管理", "場所距離管理"))
            {
                button場所距離管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "動静管理", "輸送品目管理"))
            {
                button輸送品目.Enabled = true;
            }

            #endregion

            #region 船員管理

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "船員管理", "免許／免状管理"))
            {
                button免許免状管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "船員管理", "免許／免状種別管理"))
            {
                button免許免状種別管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "船員管理", "費用科目管理"))
            {
                button費用科目管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "船員管理", "大項目管理"))
            {
                button大項目管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "船員管理", "明細管理"))
            {
                button明細管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "船員管理", "明細科目管理"))
            {
                button明細科目管理.Enabled = true;
            }


            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "船員管理", "講習管理"))
            {
                button講習管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "船員管理", "乗船資格管理"))
            {
                button乗船資格管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "船員管理", "乗船経験管理"))
            {
                button乗船経験管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "船員管理", "基本給管理"))
            {
                button基本給管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "船員管理", "手当管理"))
            {
                button手当管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "船員管理", "作業パターン"))
            {
                button作業パターン.Enabled = true;
            }
            button作業パターン.Visible = false;

            #endregion

            #region 検査・証書管理

            //if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "検査管理", null))
            //{
            //    button検査管理.Enabled = true;
            //}

            //if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "荷役安全設備管理", null))
            //{
            //    button荷役安全設備管理.Enabled = true;
            //}

            //if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "救命設備管理", null))
            //{
            //    button救命設備管理.Enabled = true;
            //}

            //if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "審査管理", null))
            //{
            //    button審査管理.Enabled = true;
            //}

            //if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "証書管理", null))
            //{
            //    button証書管理.Enabled = true;
            //}

            //if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "検船管理", null))
            //{
            //    button検船管理OLD.Enabled = true;
            //}

            #endregion
            #region 指摘事項管理

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "検査種別管理"))
            {
                button検査種別管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "ActionCode管理"))
            {
                buttonActionCode管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "DeficiencyCodeカテゴリ管理"))
            {
                buttonDeficiencyカテゴリ管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "DeficiencyCode管理"))
            {
                buttonDeficiencyCode管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "Accident種類管理"))
            {
                buttonAccident種類管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "KindOfAccident管理"))
            {
                buttonKindOfAccident管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "発生状況管理"))
            {
                button発生状況管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "検船管理"))
            {
                button検船種別管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "VIQ Version管理"))
            {
                buttonVIQVersion管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "VIQ Code名前管理"))
            {
                buttonVIQCode名前管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "VIQ Code管理"))
            {
                buttonVIQCode管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "VIQ No管理"))
            {
                buttonVIQNo管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "スケジュール種別管理"))
            {
                buttonスケジュール種別管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "スケジュール種別詳細管理"))
            {
                buttonスケジュール種別詳細管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "指摘事項管理", "国管理"))
            {
                button国管理.Enabled = true;
            }

            #endregion

            #region ドキュメント管理

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "ドキュメント管理", "ドキュメント分類管理"))
            {
                buttonドキュメント分類管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "ドキュメント管理", "ドキュメント小分類管理"))
            {
                buttonドキュメント小分類管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "共通", "ドキュメント管理", "報告書管理"))
            {
                button報告書管理.Enabled = true;
            }

            #endregion

         }


        #region 共通

        private void button1_Click(object sender, EventArgs e)
        {
            MsUserForm form = new MsUserForm();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            顧客管理一覧Form form = new 顧客管理一覧Form();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            船管理一覧Form form = new 船管理一覧Form();
            form.ShowDialog();
        }
        private void button権限管理_Click(object sender, EventArgs e)
        {
            権限管理Form form = new 権限管理Form();
            form.ShowDialog();
        }

        private void buttonアラーム管理_Click(object sender, EventArgs e)
        {
            アラーム管理Form form = new アラーム管理Form();
            form.ShowDialog();
        }

        #endregion

        #region 発注

        private void button4_Click(object sender, EventArgs e)
        {
            潤滑油管理一覧Form form = new 潤滑油管理一覧Form();
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            船用品管理一覧Form form = new 船用品管理一覧Form();
            form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            船用品船管理一覧Form form = new 船用品船管理一覧Form();
            form.ShowDialog();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            潤滑油船管理一覧Form form = new 潤滑油船管理一覧Form();
            form.ShowDialog();

        }


        #endregion

        #region 動静

        private void button8_Click(object sender, EventArgs e)
        {
            貨物管理.貨物管理Form form = new NBaseMaster.貨物管理.貨物管理Form();

            form.ShowDialog();
        }


        private void button輸送品目_Click(object sender, EventArgs e)
        {
            輸送品目管理.輸送品目管理Form form = new NBaseMaster.輸送品目管理.輸送品目管理Form();
            form.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            場所管理.場所管理Form form = new NBaseMaster.場所管理.場所管理Form();
            form.ShowDialog();
        }
        private void button24_Click(object sender, EventArgs e)
        {
            場所区分管理.場所区分管理Form form = new NBaseMaster.場所区分管理.場所区分管理Form();
            form.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            場所距離管理.場所距離管理Form form = new NBaseMaster.場所距離管理.場所距離管理Form();
            form.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            基地管理.基地管理Form form = new NBaseMaster.基地管理.基地管理Form();
            form.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            バース管理.バース管理Form form = new NBaseMaster.バース管理.バース管理Form();
            form.ShowDialog();
        }

        #endregion

        #region 船員管理

        private void button免許免状管理_Click(object sender, EventArgs e)
        {
            免許免状管理Form form = new 免許免状管理Form();
            form.ShowDialog();
        }

        private void button免許免状種別管理_Click(object sender, EventArgs e)
        {
            免許免状種別管理Form form = new 免許免状種別管理Form();
            form.ShowDialog();
        }

        private void button費用科目管理_Click(object sender, EventArgs e)
        {
            費用科目管理Form form = new 費用科目管理Form();
            form.ShowDialog();
        }

        private void button大項目管理_Click(object sender, EventArgs e)
        {
            大項目管理Form form = new 大項目管理Form();
            form.ShowDialog();
        }

        private void button明細管理_Click(object sender, EventArgs e)
        {
            明細管理Form form = new 明細管理Form();
            form.ShowDialog();
        }

        private void button明細科目管理_Click(object sender, EventArgs e)
        {
            明細科目管理Form form = new 明細科目管理Form();
            form.ShowDialog();
        }

        private void button講習管理_Click(object sender, EventArgs e)
        {
            講習管理Form form = new 講習管理Form();
            form.ShowDialog();
        }


        private void button乗船資格管理_Click(object sender, EventArgs e)
        {
            乗船資格Form form = new 乗船資格Form();
            form.ShowDialog();
        }

        private void button乗船経験管理_Click(object sender, EventArgs e)
        {
            乗船経験Form form = new 乗船経験Form();
            form.ShowDialog();
        }

        private void button基本給管理_Click(object sender, EventArgs e)
        {
            基本給管理一覧Form form = new 基本給管理一覧Form();
            form.ShowDialog();
        }

        #endregion

        #region ドキュメント管理

        private void buttonドキュメント分類管理_Click(object sender, EventArgs e)
        {
            Doc.ドキュメント分類管理.ドキュメント分類管理Form form = new NBaseMaster.Doc.ドキュメント分類管理.ドキュメント分類管理Form();
            form.ShowDialog();
        }

        private void buttonドキュメント小_Click(object sender, EventArgs e)
        {
            Doc.ドキュメント小分類管理.ドキュメント小分類管理Form form = new NBaseMaster.Doc.ドキュメント小分類管理.ドキュメント小分類管理Form();
            form.ShowDialog();
        }

        private void button報告書管理_Click(object sender, EventArgs e)
        {
            Doc.報告書管理.報告書管理Form form = new NBaseMaster.Doc.報告書管理.報告書管理Form();
            form.ShowDialog();
        }

        private void button乗船者登録_Click(object sender, EventArgs e)
        {
            Doc.乗船者登録.乗船者登録Form form = new NBaseMaster.Doc.乗船者登録.乗船者登録Form();
            form.ShowDialog();
        }

        #endregion


        #region 指摘事項管理

        private void button検査種別管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.検査種別管理Form form = new 指摘事項管理.検査種別管理Form();
            form.ShowDialog();
        }

        private void buttonActionCode管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.ActionCode管理Form form = new 指摘事項管理.ActionCode管理Form();
            form.ShowDialog();
        }

        private void buttonDeficiencyカテゴリ管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.DeficiencyCategory管理Form form = new 指摘事項管理.DeficiencyCategory管理Form();
            form.ShowDialog();
        }

        private void buttonDeficiencyCode管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.DeficiencyCode管理Form form = new 指摘事項管理.DeficiencyCode管理Form();
            form.ShowDialog();
        }

        private void buttonAccident種類管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.Accident種類管理Form form = new 指摘事項管理.Accident種類管理Form();
            form.ShowDialog();
        }

        private void buttonKindOfAccident管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.KindOfAccident管理Form form = new 指摘事項管理.KindOfAccident管理Form();
            form.ShowDialog();
        }

        private void button発生状況管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.発生状況管理Form form = new 指摘事項管理.発生状況管理Form();
            form.ShowDialog();
        }

        private void button検船種別管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.検船種別管理Form form = new 指摘事項管理.検船種別管理Form();
            form.ShowDialog();
        }

        private void buttonVIQVersion管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.ViqVersion管理Form form = new 指摘事項管理.ViqVersion管理Form();
            form.ShowDialog();
        }

        private void buttonVIQCode名前管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.ViqCodeName管理Form form = new 指摘事項管理.ViqCodeName管理Form();
            form.ShowDialog();
        }

        private void buttonVIQCode管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.ViqCode管理Form form = new 指摘事項管理.ViqCode管理Form();
            form.ShowDialog();
        }

        private void buttonVIQNo管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.ViqNo管理Form form = new 指摘事項管理.ViqNo管理Form();
            form.ShowDialog();
        }

        private void buttonスケジュール種別管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.スケジュール種別管理Form form = new 指摘事項管理.スケジュール種別管理Form();
            form.ShowDialog();
        }

        private void buttonスケジュール種別詳細管理_Click(object sender, EventArgs e)
        {
            指摘事項管理.スケジュール種別詳細管理Form form = new 指摘事項管理.スケジュール種別詳細管理Form();
            form.ShowDialog();
        }

        private void button国管理_Click(object sender, EventArgs e)
        {
            国管理Form form = new 国管理Form();
            form.ShowDialog();
        }



        #endregion

        #region 検査・証書

        private void button13_Click(object sender, EventArgs e)
        {
            Kensa.検査管理Form form = new NBaseMaster.Kensa.検査管理Form();
            form.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            証書管理.証書管理Form form = new NBaseMaster.証書管理.証書管理Form();
            form.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            審査管理.審査管理Form form = new NBaseMaster.審査管理.審査管理Form();
            form.ShowDialog();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            救命設備管理.救命設備管理Form form = new NBaseMaster.救命設備管理.救命設備管理Form();
            form.ShowDialog();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            荷役安全設備管理.荷役安全設備管理Form form = new NBaseMaster.荷役安全設備管理.荷役安全設備管理Form();
            form.ShowDialog();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            検船管理.検船管理Form form = new NBaseMaster.検船管理.検船管理Form();
            form.ShowDialog();
        }

        #endregion

        private void button作業パターン_Click(object sender, EventArgs e)
        {
            WP.労働パターンForm form = WP.労働パターンForm.GetInstance();
            form.ShowDialog();
        }

        private void button手当管理_Click(object sender, EventArgs e)
        {
            Tek手当管理Form form = new Tek手当管理Form();
            form.ShowDialog();
        }
    }
}
