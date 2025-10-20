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
        private MsSeninAddress seninAddress;

        private void Init住所()
        {
            if(comboBox都道府県_現住所.Items.Count == 0)
            {
                MsSiOptions dmy = new MsSiOptions();
                dmy.MsSiOptionsID = null;
                dmy.Name = "";
                comboBox都道府県_現住所.Items.Add(dmy);
                comboBox都道府県_住民票.Items.Add(dmy);
                comboBox都道府県_本籍.Items.Add(dmy);

                foreach (MsSiOptions o in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.都道府県))
                {
                    comboBox都道府県_現住所.Items.Add(o);
                    comboBox都道府県_住民票.Items.Add(o);
                    comboBox都道府県_本籍.Items.Add(o);
                }
            }
        }

        private void Search住所()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                seninAddress = serviceClient.MsSeninAddress_GetRecordsByMsSeninID(NBaseCommon.Common.LoginUser, senin.MsSeninID);

                if (seninAddress == null)
                {
                    seninAddress = new MsSeninAddress();
                }
            }

            Set住所();
        }

        private void Set住所()
        {
            maskedTextBox郵便番号_現住所.Text = seninAddress.ZipCode ;
            foreach (MsSiOptions o in comboBox都道府県_現住所.Items)
            {
                if (o.MsSiOptionsID == seninAddress.Prefectures)
                {
                    comboBox都道府県_現住所.SelectedItem = o;
                    break;
                }
            }
            textBox市区町村_現住所.Text = seninAddress.CityTown;
            textBox番地_現住所.Text = seninAddress.Street;


            maskedTextBox郵便番号_住民票.Text = seninAddress.A_ZipCode;
            foreach (MsSiOptions o in comboBox都道府県_住民票.Items)
            {
                if (o.MsSiOptionsID == seninAddress.A_Prefectures)
                {
                    comboBox都道府県_住民票.SelectedItem = o;
                    break;
                }
            }
            textBox市区町村_住民票.Text = seninAddress.A_CityTown;
            textBox番地_住民票.Text = seninAddress.A_Street;


            maskedTextBox郵便番号_本籍.Text = seninAddress.P_ZipCode;
            foreach (MsSiOptions o in comboBox都道府県_本籍.Items)
            {
                if (o.MsSiOptionsID == seninAddress.P_Prefectures)
                {
                    comboBox都道府県_本籍.SelectedItem = o;
                    break;
                }
            }
            textBox市区町村_本籍.Text = seninAddress.P_CityTown;
            textBox番地_本籍.Text = seninAddress.P_Street;


            seninAddress.EditFlag = false;

        }



        private bool Validate住所()
        {
            return true;
        }



        private void Fill住所()
        {
            if (seninAddress == null)
            {
                seninAddress = new MsSeninAddress();
            }

            seninAddress.ZipCode = maskedTextBox郵便番号_現住所.Text;
            seninAddress.Prefectures = (comboBox都道府県_現住所.SelectedItem is MsSiOptions) ? (comboBox都道府県_現住所.SelectedItem as MsSiOptions).MsSiOptionsID : null;
            seninAddress.CityTown = textBox市区町村_現住所.Text;
            seninAddress.Street = textBox番地_現住所.Text;

            seninAddress.A_ZipCode = maskedTextBox郵便番号_住民票.Text;
            seninAddress.A_Prefectures = (comboBox都道府県_住民票.SelectedItem is MsSiOptions) ? (comboBox都道府県_住民票.SelectedItem as MsSiOptions).MsSiOptionsID : null;
            seninAddress.A_CityTown = textBox市区町村_住民票.Text;
            seninAddress.A_Street = textBox番地_住民票.Text;


            seninAddress.P_ZipCode = maskedTextBox郵便番号_本籍.Text;
            seninAddress.P_Prefectures = (comboBox都道府県_本籍.SelectedItem is MsSiOptions) ? (comboBox都道府県_本籍.SelectedItem as MsSiOptions).MsSiOptionsID : null;
            seninAddress.P_CityTown = textBox市区町村_本籍.Text;
            seninAddress.P_Street = textBox番地_本籍.Text;

        }

        private void 住所_ValueChanged(object sender, EventArgs e)
        {
            if (seninAddress == null)
            {
                seninAddress = new MsSeninAddress();
            }
            seninAddress.EditFlag = true;
        }


        internal void Clear住所()
        {
            seninAddress = new MsSeninAddress();

            maskedTextBox郵便番号_現住所.Text = null;
            comboBox都道府県_現住所.SelectedIndex = -1;
            textBox市区町村_現住所.Text = null;
            textBox番地_現住所.Text = null;

            maskedTextBox郵便番号_住民票.Text = null;
            comboBox都道府県_住民票.SelectedIndex = -1;
            textBox市区町村_住民票.Text = null;
            textBox番地_住民票.Text = null;

            maskedTextBox郵便番号_本籍.Text = null;
            comboBox都道府県_本籍.SelectedIndex = -1;
            textBox市区町村_本籍.Text = null;
            textBox番地_本籍.Text = null;

            seninAddress.EditFlag = false;
        }

        private void Reset連絡先()
        {
            maskedTextBoxTEL.Text = null;
            maskedTextBoxFAX.Text = null;
            maskedTextBox携帯.Text = null;
            textBoxメール.Text = null;

            if (senin != null && senin.Tel != null)
                maskedTextBoxTEL.Text = senin.Tel.Trim();
            if (senin != null && senin.Fax != null)
                maskedTextBoxFAX.Text = senin.Fax.Trim();
            if (senin != null && senin.Keitai != null)
                maskedTextBox携帯.Text = senin.Keitai.Trim();
            if (senin != null && senin.Mail != null)
                textBoxメール.Text = senin.Mail.Trim();

            Set住所();
        }



        private void button連絡先更新_Click(object sender, EventArgs e)
        {
            Fill住所();

            Save(seninAddress);
        }


        private void button現住所と同じ_Click(object sender, EventArgs e)
        {
            maskedTextBox郵便番号_住民票.Text = maskedTextBox郵便番号_現住所.Text;
            comboBox都道府県_住民票.SelectedIndex = comboBox都道府県_現住所.SelectedIndex;
            textBox市区町村_住民票.Text = textBox市区町村_現住所.Text;
            textBox番地_住民票.Text = textBox番地_現住所.Text;
        }


    }
}
