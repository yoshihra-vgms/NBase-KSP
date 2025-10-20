using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DS;
using NBaseData.DAC;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using Senin.util;

namespace Senin
{
    partial class 船員詳細Panel
    {
        private SiKenshinPmhKa kenshinPmhKa = null;


        private void Search既往歴()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                kenshinPmhKa = serviceClient.SiKenshinPmhKa_GetRecordByMsSeninID(NBaseCommon.Common.LoginUser, senin.MsSeninID);
            }

            if (kenshinPmhKa != null)
            {
                checkBox_AlcID.Checked  = kenshinPmhKa.AlcId == 1 ? true : false;
                checkBox_喫煙.Checked = kenshinPmhKa.Smoking == 1 ? true : false;
                textBox_診断名1.Text = kenshinPmhKa.Pmh1;
                textBox_診断名2.Text = kenshinPmhKa.Pmh2;
                textBox_診断名3.Text = kenshinPmhKa.Pmh3;
                textBox_診断名4.Text = kenshinPmhKa.Pmh4;
                textBox_診断名5.Text = kenshinPmhKa.Pmh5;
                checkBox_卵.Checked = kenshinPmhKa.EggAllergy == 1 ? true : false;
                checkBox_乳.Checked = kenshinPmhKa.MilkAllergy == 1 ? true : false;
                checkBox_小麦.Checked = kenshinPmhKa.WheatAllergy == 1 ? true : false;
                checkBox_えび.Checked = kenshinPmhKa.ShrimpAllergy == 1 ? true : false;
                checkBox_かに.Checked = kenshinPmhKa.CrabAllergy ==1 ? true : false;
                checkBox_落花生.Checked = kenshinPmhKa.PeanutAllergy == 1 ? true : false;
                checkBox_そば.Checked = kenshinPmhKa.BuckwheatAllergy == 1 ? true : false;

                // 2021/11/19打ち合わせで非表示の指示有り
                //checkBox_他1.Checked = kenshinPmhKa.EtcAllergy1 == 1 ? true : false;
                //checkBox_他2.Checked = kenshinPmhKa.EtcAllergy2 == 1 ? true : false;
                //checkBox_他3.Checked = kenshinPmhKa.EtcAllergy3 == 1 ? true : false;
            }
            else
            {

            }

        }
        internal void Clear既往歴()
        {
            checkBox_AlcID.Checked = false;
            checkBox_喫煙.Checked = false;
            textBox_診断名1.Text = null;
            textBox_診断名2.Text = null;
            textBox_診断名3.Text = null;
            textBox_診断名4.Text = null;
            textBox_診断名5.Text = null;
            checkBox_卵.Checked = false;
            checkBox_乳.Checked = false;
            checkBox_小麦.Checked = false;
            checkBox_えび.Checked = false;
            checkBox_かに.Checked = false;
            checkBox_落花生.Checked = false;
            checkBox_そば.Checked = false;

            // 2021/11/19打ち合わせで非表示の指示有り
            //checkBox_他1.Checked = false;
            //checkBox_他2.Checked = false;
            //checkBox_他3.Checked = false;
        }


        private void button既往歴登録_Click(object sender, EventArgs e)
        {
            Fill既往歴();
            if (InsertOrUpdate_既往歴(kenshinPmhKa))
            {
                MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Search既往歴();
        }

        private void Fill既往歴()
        {
            if (kenshinPmhKa == null)
            {
                kenshinPmhKa = new SiKenshinPmhKa();
            }

            kenshinPmhKa.AlcId = checkBox_AlcID.Checked ? 1 : 0;
            kenshinPmhKa.Smoking = checkBox_喫煙.Checked ? 1 : 0;
            kenshinPmhKa.Pmh1 = textBox_診断名1.Text;
            kenshinPmhKa.Pmh2 = textBox_診断名2.Text;
            kenshinPmhKa.Pmh3 = textBox_診断名3.Text;
            kenshinPmhKa.Pmh4 = textBox_診断名4.Text;
            kenshinPmhKa.Pmh5 = textBox_診断名5.Text;
            kenshinPmhKa.EggAllergy = checkBox_卵.Checked ? 1 : 0;
            kenshinPmhKa.MilkAllergy = checkBox_乳.Checked ? 1 : 0;
            kenshinPmhKa.WheatAllergy = checkBox_小麦.Checked ? 1 : 0;
            kenshinPmhKa.ShrimpAllergy = checkBox_えび.Checked ? 1 : 0;
            kenshinPmhKa.CrabAllergy = checkBox_かに.Checked ? 1 : 0;
            kenshinPmhKa.PeanutAllergy = checkBox_落花生.Checked ? 1 : 0;
            kenshinPmhKa.BuckwheatAllergy = checkBox_そば.Checked ? 1 : 0;

            // 2021/11/19打ち合わせで非表示の指示有り
            //kenshinPmhKa.EtcAllergy1 = checkBox_他1.Checked ? 1 : 0;
            //kenshinPmhKa.EtcAllergy2 = checkBox_他2.Checked ? 1 : 0;
            //kenshinPmhKa.EtcAllergy3 = checkBox_他3.Checked ? 1 : 0;
        }
    }
}
