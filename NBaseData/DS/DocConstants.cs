using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBaseData.DS
{
    public class DocConstants
    {
        public enum ClassEnum
        {
            USER = 0,
            BUMON,
            VESSEL
        }
        public enum FlagEnum
        {
            OFF = 0,
            ON
        }
        //public enum RoleEnum
        //{
        //    役員 = 1,
        //    管理責任者,
        //    船,
        //    部門,
        //    経営責任者,
        //    海務監督_旅客,
        //    船員担当者_旅客,
        //    工務監督_旅客,
        //    海務監督_貨物,
        //    船員担当者_貨物,
        //    工務監督_貨物,
        //    船舶部部長
        //}
        public enum RoleEnum
        {
            役員 = 1,
            管理責任者,
            船,
            部門,
            経営責任者,
            海務監督,
            船員担当者,
            工務監督
        }
        public enum LinkSakiEnum
        {
            報告書マスタ = 1,
            管理記録,
            公文書_規則
        }
        public enum StatusEnum
        {
            未完了 = 0,
            完了
        }
        //public enum UserFlagEnum
        //{
        //    役員 = 1,
        //    管理責任者,
        //    ＧＬ,
        //    ＴＬ,
        //    経営責任者,
        //    海務監督_旅客,
        //    船員担当者_旅客,
        //    工務監督_旅客,
        //    海務監督_貨物,
        //    船員担当者_貨物,
        //    工務監督_貨物,
        //    船舶部部長
        //}
        //public enum UserFlagEnum
        //{
        //    役員 = 1,
        //    管理責任者,
        //    ＧＬ,
        //    ＴＬ,
        //    経営責任者,
        //    海務監督,
        //    船員担当者,
        //    工務監督
        //}
        //public const int UserFlag一般 = 99;


        public class ClassItem
        {
            public ClassEnum enumClass;
            public RoleEnum enumRole;
            public string bumonId;
            public string menuName;
            public string viewName1;
            public string viewName2;

            public ClassItem(ClassEnum cEnum, RoleEnum kEnum, string bumonId, string menuName, string name1, string name2 = null)
            {
                this.enumClass = cEnum;
                this.enumRole = kEnum;
                this.bumonId = bumonId;
                this.menuName = menuName;
                this.viewName1 = name1;
                this.viewName2 = name2;
            }
        }
        public static List<ClassItem> ClassItemList()
        {
            List<ClassItem> list = new List<ClassItem>();

            //list.Add(new ClassItem(ClassEnum.USER, RoleEnum.経営責任者, null, "経営責任者", "経営責任者"));
            //list.Add(new ClassItem(ClassEnum.USER, RoleEnum.管理責任者, null, "管理責任者", "管理責任者"));
            //list.Add(new ClassItem(ClassEnum.USER, RoleEnum.海務監督_旅客, null, "海務監督(旅客)", "海務監督","(旅客)"));
            //list.Add(new ClassItem(ClassEnum.USER, RoleEnum.船員担当者_旅客, null, "船員担当者(旅客)", "船員担当者","(旅客)"));
            //list.Add(new ClassItem(ClassEnum.USER, RoleEnum.工務監督_旅客, null, "工務監督(旅客)", "工務監督", "(旅客)"));
            //list.Add(new ClassItem(ClassEnum.USER, RoleEnum.海務監督_貨物, null, "海務監督(貨物)", "海務監督", "(貨物)"));
            //list.Add(new ClassItem(ClassEnum.USER, RoleEnum.船員担当者_貨物, null, "船員担当者(貨物)", "船員担当者", "(貨物)"));
            //list.Add(new ClassItem(ClassEnum.USER, RoleEnum.工務監督_貨物, null, "工務監督(貨物)", "工務監督", "(貨物)"));
            //list.Add(new ClassItem(ClassEnum.USER, RoleEnum.役員, null, "役員", "役員"));
            //list.Add(new ClassItem(ClassEnum.USER, RoleEnum.船舶部部長, null, "船舶部部長", "船舶部", "部長"));
            //list.Add(new ClassItem(ClassEnum.BUMON, RoleEnum.部門, "0", "安全運航・海技Ｔ", "安全運航","海技Ｔ"));
            //list.Add(new ClassItem(ClassEnum.BUMON, RoleEnum.部門, "1", "船舶管理・技術Ｔ", "船舶管理", "技術Ｔ"));
            //list.Add(new ClassItem(ClassEnum.BUMON, RoleEnum.部門, "2", "船員Ｔ", "船員Ｔ"));
            //list.Add(new ClassItem(ClassEnum.BUMON, RoleEnum.部門, "4", "八戸支社", "八戸支社"));

            list.Add(new ClassItem(ClassEnum.USER, RoleEnum.経営責任者, null, "経営責任者", "経営責任者"));
            list.Add(new ClassItem(ClassEnum.USER, RoleEnum.管理責任者, null, "管理責任者", "管理責任者"));
            list.Add(new ClassItem(ClassEnum.USER, RoleEnum.海務監督, null, "海務監督", "海務監督"));
            list.Add(new ClassItem(ClassEnum.USER, RoleEnum.船員担当者, null, "船員担当者", "船員担当者"));
            list.Add(new ClassItem(ClassEnum.USER, RoleEnum.工務監督, null, "工務監督", "工務監督"));
            list.Add(new ClassItem(ClassEnum.USER, RoleEnum.役員, null, "役員", "役員"));
            list.Add(new ClassItem(ClassEnum.BUMON, RoleEnum.部門, "0", "海務部", "海務部"));
            list.Add(new ClassItem(ClassEnum.BUMON, RoleEnum.部門, "1", "工務部", "工務部"));
            list.Add(new ClassItem(ClassEnum.BUMON, RoleEnum.部門, "2", "船員部", "船員部"));

            return list;
        }

        public static string ClassName(int enumRole)
        {
            string retStr = "";

            if (ClassItemList().Any(o => (int)o.enumRole == enumRole))
            {
                retStr = ClassItemList().Where(o => (int)o.enumRole == enumRole).First().menuName;
            }

            return retStr;
        }
    }
}
