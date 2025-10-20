using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DS;
using NBaseData.DAC;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using Senin.util;
using NBaseUtil;

namespace Senin
{
    partial class 船員詳細Panel
    {
        private MsSeninCareer seninCareer;
        private MsSeninEtc seninEtc;

        private List<TextBox> textBoxCompanyList = null;
        private List<TextBox> textBoxJoinedList = null;
        private List<TextBox> textBoxLeaveList = null;

        //データを編集したかどうか？
        public bool CareerChangeFlag = false;

        private void Init経歴等()
        {
            if (textBoxCompanyList == null)
            {
                textBoxCompanyList = new List<TextBox>();
                textBoxCompanyList.Add(textBox他社歴1);
                textBoxCompanyList.Add(textBox他社歴2);
                textBoxCompanyList.Add(textBox他社歴3);
                textBoxCompanyList.Add(textBox他社歴4);
                textBoxCompanyList.Add(textBox他社歴5);
            }
            if (textBoxJoinedList == null)
            {
                textBoxJoinedList = new List<TextBox>();
                textBoxJoinedList.Add(textBox他社入社年度1);
                textBoxJoinedList.Add(textBox他社入社年度2);
                textBoxJoinedList.Add(textBox他社入社年度3);
                textBoxJoinedList.Add(textBox他社入社年度4);
                textBoxJoinedList.Add(textBox他社入社年度5);
            }
            if (textBoxLeaveList == null)
            {
                textBoxLeaveList = new List<TextBox>();
                textBoxLeaveList.Add(textBox他社退社年度1);
                textBoxLeaveList.Add(textBox他社退社年度2);
                textBoxLeaveList.Add(textBox他社退社年度3);
                textBoxLeaveList.Add(textBox他社退社年度4);
                textBoxLeaveList.Add(textBox他社退社年度5);
            }

        }

        private void Search経歴等()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                seninCareer = serviceClient.MsSeninCareer_GetRecordsByMsSeninID(NBaseCommon.Common.LoginUser, senin.MsSeninID);

                if (seninCareer == null)
                {
                    seninCareer = new MsSeninCareer();
                }


                seninEtc = serviceClient.MsSeninEtc_GetRecordsByMsSeninID(NBaseCommon.Common.LoginUser, senin.MsSeninID);

                if (seninEtc == null)
                {
                    seninEtc = new MsSeninEtc();
                }
            }

            Init経歴等();

            Clear経歴等();
            Set経歴等();
        }

        private void Set経歴等()
        {
            Init経歴等();

            // 最終学歴
            textBox学校名.Text = seninCareer.AcademicBackground;
            textBox卒業年度.Text = seninCareer.YearOfGraduation;

            // 他社歴
            int index = 0;
            textBoxCompanyList.ForEach(o =>
            {
                if (seninCareer.CompanyList.Count() > index)
                    o.Text = seninCareer.CompanyList[index];
                index++;
            });

            index = 0;
            textBoxJoinedList.ForEach(o =>
            {
                if (seninCareer.JoinedList.Count() > index)
                    o.Text = seninCareer.JoinedList[index];
                index++;
            });

            index = 0;
            textBoxLeaveList.ForEach(o =>
            {
                if (seninCareer.LeaveList.Count() > index)
                    o.Text = seninCareer.LeaveList[index];
                index++;
            });


            // 作業服
            textBox身長.Text = seninEtc.Height;
            textBox体重.Text = seninEtc.Weight;
            textBoxウェスト.Text = seninEtc.Waist;
            textBox股下.Text = seninEtc.Inseam;
            textBox安全靴.Text = seninEtc.ShoeSize;
            textBoxつなぎ.Text = seninEtc.Workwear;

            // 口座
            textBox銀行名1.Text = seninEtc.BankName1;
            textBox支店名1.Text = seninEtc.BranchName1;
            textBox口座番号1.Text = seninEtc.AccountNo1;
            textBox銀行名2.Text = seninEtc.BankName2;
            textBox支店名2.Text = seninEtc.BranchName2;
            textBox口座番号2.Text = seninEtc.AccountNo2;
        }


        private void Fill経歴等()
        {
            if (seninCareer == null)
                seninCareer = new MsSeninCareer();

            // 最終学歴
            seninCareer.AcademicBackground = textBox学校名.Text;
            seninCareer.YearOfGraduation = textBox卒業年度.Text;

            // 他社歴
            int index = 0;
            textBoxCompanyList.ForEach(o =>
            {
                if (seninCareer.CompanyList.Count() > index)
                    seninCareer.CompanyList[index] = o.Text;
                else
                    seninCareer.CompanyList.Add(o.Text);

                index++;
            });

            index = 0;
            textBoxJoinedList.ForEach(o =>
            {
                if (seninCareer.JoinedList.Count() > index)
                    seninCareer.JoinedList[index] = o.Text;
                else
                    seninCareer.JoinedList.Add(o.Text);

                index++;
            });

            index = 0;
            textBoxLeaveList.ForEach(o =>
            {
                if (seninCareer.LeaveList.Count() > index)
                    seninCareer.LeaveList[index] = o.Text;
                else
                    seninCareer.LeaveList.Add(o.Text);

                index++;
            });



            if (seninEtc == null)
                seninEtc = new MsSeninEtc();

            // 作業服
            seninEtc.Height = textBox身長.Text;
            seninEtc.Weight = textBox体重.Text;
            seninEtc.Waist = textBoxウェスト.Text;
            seninEtc.Inseam = textBox股下.Text;
            seninEtc.ShoeSize = textBox安全靴.Text;
            seninEtc.Workwear = textBoxつなぎ.Text;

            // 口座
            seninEtc.BankName1 = textBox銀行名1.Text;
            seninEtc.BranchName1 = textBox支店名1.Text;
            seninEtc.AccountNo1 = textBox口座番号1.Text;
            seninEtc.BankName2 = textBox銀行名2.Text;
            seninEtc.BranchName2 = textBox支店名2.Text;
            seninEtc.AccountNo2 = textBox口座番号2.Text;
        }

        internal void Clear経歴等()
        {
            Init経歴等();

            // 最終学歴
            textBox学校名.Text = null;
            textBox卒業年度.Text = null;

            // 他社歴
            textBoxCompanyList.ForEach(o => o.Text = null);
            textBoxJoinedList.ForEach(o => o.Text = null);
            textBoxLeaveList.ForEach(o => o.Text = null);

            // 作業服
            textBox身長.Text = null;
            textBox体重.Text = null;
            textBoxウェスト.Text = null;
            textBox股下.Text = null;
            textBox安全靴.Text = null;
            textBoxつなぎ.Text = null;

            // 口座
            textBox銀行名1.Text = null;
            textBox支店名1.Text = null;
            textBox口座番号1.Text = null;
            textBox銀行名2.Text = null;
            textBox支店名2.Text = null;
            textBox口座番号2.Text = null;


            this.CareerChangeFlag = false;
        }

        private void Reset経歴等()
        {
            Clear経歴等();
            Set経歴等();
        }


        private void button経歴等更新_Click(object sender, EventArgs e)
        {
            Fill経歴等();

            if (InsertOrUpdate_経歴等(seninCareer, seninEtc))
            {
                MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Search経歴等();
            }
            else
            {
                MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //データを編集した時
        private void CareerDataChange(object sender, EventArgs e)
        {
            this.CareerChangeFlag = true;
        }

    }
}
