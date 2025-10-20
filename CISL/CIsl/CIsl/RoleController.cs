using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CIsl.DB.WingDAC;
using System.Windows.Forms;

namespace CIsl
{
    public class RoleData
    {
        public RoleData()
        {
            this.ControlRole = this.DefaultControlRoleDelegateProc;
        }


        public RoleData(ERoleName1 name1, ERoleName2 name2, ERoleName3 name3, Control con, ControlRoleDelegate dele = null)        
        {
            this.Name1 = name1;
            this.Name2 = name2;
            this.Name3 = name3;

            this.Control = con;

            this.ControlRole = dele;
            if (this.ControlRole == null)
            {
                this.ControlRole = this.DefaultControlRoleDelegateProc;
            }
        }

        /// <summary>
        /// コントロール制御
        /// </summary>
        /// <param name="con">対象</param>
        /// <param name="ena">有効可否 true=有効</param>        
        public delegate void ControlRoleDelegate(RoleData data, bool ena);


        public ERoleName1 Name1 = ERoleName1.MAX;
        public ERoleName2 Name2 = ERoleName2.MAX;
        public ERoleName3 Name3 = ERoleName3.MAX;

        /// <summary>
        /// 制御
        /// </summary>
        public Control Control = null;


        /// <summary>
        /// コールバック
        /// </summary>
        public ControlRoleDelegate ControlRole = null;


        /// <summary>
        /// デフォルト制御 enableを制御する
        /// </summary>
        /// <param name="ena"></param>
        /// <param name="con"></param>
        protected virtual void DefaultControlRoleDelegateProc(RoleData data, bool ena)
        {
            if (data.Control == null)
            {
                return;
            }

            data.Control.Enabled = ena;

            
        }
        
    }



    /// <summary>
    /// ロール制御
    /// </summary>
    public class RoleController
    {
        /// <summary>
        /// チェック
        /// </summary>
        /// <param name="rolelist"></param>
        /// <param name="datalist"></param>
        public static void ControlRole(List<MsRole> rolelist, List<RoleData> datalist)
        {
            foreach (RoleData data in datalist)
            {
                bool ret = CheckRole(rolelist, data.Name1, data.Name2, data.Name3);
                data.ControlRole(data, ret);
                
            }
        }



        /// <summary>
        /// ロールチェック true=有効、権限あり false=無効、権限なし
        /// </summary>
        /// <param name="rolelist">チェックする権限、自分の部署の権限だけをリストアップしてあること</param>
        /// <param name="ename1"></param>
        /// <param name="ename2"></param>
        /// <param name="ename3"></param>
        /// <returns></returns>
        public static bool CheckRole(List<MsRole> rolelist, ERoleName1 ename1, ERoleName2 ename2, ERoleName3 ename3)
        {
            string name1 = ename1.ToString();
            if (ename1 == ERoleName1.MAX)
            {
                name1 = string.Empty;
            }

            string name2 = ename2.ToString();
            if (ename2 == ERoleName2.MAX)
            {
                name2 = string.Empty;
            }
            string name3 = ename3.ToString();
            if (ename3 == ERoleName3.MAX)
            {
                name3 = string.Empty;
            }
            //--------------------------------------------------------------------------------------------------------------------------------------

            //合致するロール情報を取得
            var n = from f in rolelist where f.name1 == name1.ToString() && f.name2 == name2.ToString() && f.name3 == name3.ToString() select f;
            if (n.Count() != 1)
            {
                //ない時、もしくは一つに絞れないときは失敗として無効
                return false;
            }
            MsRole role = n.First();
            if (role.enable_flag == 1)
            {
                return true;
            }

            return false;

        }
    }
}
