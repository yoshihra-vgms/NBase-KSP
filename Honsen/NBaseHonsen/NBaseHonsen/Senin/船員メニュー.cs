using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBaseHonsen.Senin
{
    public partial class 船員メニュー : Form
    {
        public 船員メニュー()
        {
            InitializeComponent();
        }
        

        private void button乗船_Click(object sender, EventArgs e)
        {
            乗船Form form = new 乗船Form();
            form.ShowDialog();
        }

        
        private void button下船_Click(object sender, EventArgs e)
        {
            下船_乗船休暇Form form = new 下船_乗船休暇Form(下船_乗船休暇Form.Type.下船);
            form.ShowDialog();
        }

        private void button役職変更_Click(object sender, EventArgs e)
        {
            役職変更Form form = new 役職変更Form();
            form.ShowDialog();
        }


        private void button交代_Click(object sender, EventArgs e)
        {
            下船_乗船休暇Form 下船Form = new 下船_乗船休暇Form(下船_乗船休暇Form.Type.下船, true);
            下船Form.ShowDialog();
        }

        
        private void button配乗表_Click(object sender, EventArgs e)
        {
            配乗表Form form = new 配乗表Form();
            form.ShowDialog();
        }


        private void button船員情報_Click(object sender, EventArgs e)
        {
            船員リストForm form = new 船員リストForm();
            form.ShowDialog();
        }


        //==========
        // 下記ボタンは標準版では無効

        private void button乗船休暇_Click(object sender, EventArgs e)
        {
            //下船_乗船休暇Form form = new 下船_乗船休暇Form(下船_乗船休暇Form.Type.乗船休暇);
            //form.ShowDialog();
        }

        private void button転船_Click(object sender, EventArgs e)
        {
            //下船_乗船休暇Form form = new 下船_乗船休暇Form(下船_乗船休暇Form.Type.転船);
            //form.ShowDialog();
        }
    }
}
