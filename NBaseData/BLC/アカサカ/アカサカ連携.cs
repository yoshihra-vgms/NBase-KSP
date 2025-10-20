using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WingData.DAC;
using WingData.DAC.DJ;

namespace WingData.BLC.アカサカ
{
    public class アカサカ連携
    {
        private MsUser loginUser;
        public string URL = "";
        public readonly string 本船一覧Command = "CMD=IGT_GETLIST&CMDVER=100";
        public readonly string 動静Command = "CMD=IGT_GETNAVIREPORT&CMDVER=100&HONSENID=";
        public List<Info> Infos = new List<Info>();
        
        public アカサカ連携()
        {
            loginUser = new MsUser();
            loginUser.MsUserID = "1";
            loginUser.LoginID = "AKASAKABATCH";

        }

        public bool Kick()
        {
            string RecordDateTime = Get最新RecordDateTime();

            if (URL == "")
            {
                アカサカ連携.Info info = new Info();

                info.InfoKind = 1;
                info.Msg = "HonsenNetURLが設定されていません";
                Infos.Add(info);
                return false;
            }

            #region アカサカ連携開始ログ
            {
                Info StartInfo = new Info();
                StartInfo.InfoKind = 0;
                StartInfo.Msg = "URI:" + URL;
                Infos.Add(StartInfo);
            }
            {
                Info StartInfo = new Info();
                StartInfo.InfoKind = 0;
                StartInfo.Msg = "RECORD_DATETIME:" + RecordDateTime;
                Infos.Add(StartInfo);
            }
            #endregion
            List<string> Honsens = 本船一覧();

            foreach (string HonsenID in Honsens)
            {
                通信 http = new 通信();
                string Command = URL + "?" + 動静Command + HonsenID + "&RECORD_DATETIME=" + RecordDateTime;
                http.Kick(Command);
                foreach (string Cmd in http.CmdData)
                {
                    LineProcess(Cmd, HonsenID);
                }
            }
            //LineProcess("CMD=IGT_GETNAVIREPORT,HONSENID=50052,NAVICODE=5005220100217020000,PORTCODE=6,BERTHCODE=,PLAN_ARRIVAL=20100217020000,PLAN_ATTACH=20100216084000,PLAN_OPE_START=20100216090000,PLAN_OPE_END=20100216121500,PLAN_DETACH=20100216122500,PLAN_LEAVE=20100216123000,RESULT_ARRIVAL=20100215020000,RESULT_ATTACH=20100215020001,RESULT_OPE_START=20100215020002,RESULT_OPE_END=20100215020003,RESULT_DETACH=20100215020004,RESULT_LEAVE=20100215020005,OIL_REFUEL_A=,OIL_REFUEL_C=,OIL_NAVIGATING_A=,OIL_NAVIGATING_C=,OIL_MOORING_A=,OIL_MOORING_C=,VOYAGENO=1234,ITEM_NAME_0=000:LPG,ITEM_QUANTITY_0=1,ITEM_STATUS_0=0,ITEM_NAME_1=000:LPG,ITEM_QUANTITY_1=2,ITEM_STATUS_1=0,ITEM_NAME_2=ITEM2,ITEM_QUANTITY_2=2,ITEM_STATUS_2=0,ITEM_NAME_3=ITEM3,ITEM_QUANTITY_3=3,ITEM_STATUS_3=1,RECORD_DATETIME=20100215020616");

            #region アカサカ連携終了ログ
            {
                Info StartInfo = new Info();
                StartInfo.InfoKind = 0;
                StartInfo.Msg = "アカサカ連携終了";
                Infos.Add(StartInfo);
            }
            #endregion

            return true;
        }

        public bool Kick(string Data)
        {
            LineProcess(Data, "");
            return true;
        }

        private string Get最新RecordDateTime()
        {
            DjDousei d = DjDousei.GetRecordByMaxRecordDateTime(loginUser);

            if (d.RecordDateTime == "")
            {
                //過去データの参照は最大14日まで
                DateTime dd = DateTime.Today.AddDays(-13);
                return dd.ToString("yyyyMMddhhmmss");
            }
            else
            {
                return d.RecordDateTime;
            }
        }

        private List<string> 本船一覧()
        {
            string Command = URL + "?" + 本船一覧Command;

            通信 http = new 通信();
            http.Kick(Command);

            string[]SplitedDatas = http.Body.Split(',');
            List<string> ret = new List<string>();
            int VesselCount = 0;
            //船の数を取得
            VesselCount = int.Parse(http.FindData("LISTCOUNT="));

            #region 船番号を取得
            for (int i = 0; i < VesselCount; i++)
            {
                string VesselNo = http.FindData("HONSENID_" + i.ToString() + "=");
                ret.Add(VesselNo);
            }
            #endregion
            return ret;
        }

        private bool LineProcess(string LineData, string HonsenID)
        {
            アカサカParser parser = new アカサカParser(LineData);
            List<WingData.DAC.DjDousei> douseis = parser.Parse(loginUser, HonsenID);

            foreach (Info info in parser.Infos)
            {
                Infos.Add(info);
            }

            #region CMDデータをデータベースに保存
            if (parser.NAVI_CODE != "")
            {
                DjAkasaka.InsertRecord(loginUser, parser.NAVI_CODE, LineData);
            }
            #endregion

            if (douseis != null)
            {
                WingData.DAC.DjDousei InsertDousei = new WingData.DAC.DjDousei();
                foreach (WingData.DAC.DjDousei d in douseis)
                {
                    if (d != null && d.DjDouseiID != "")
                    {
                        InsertDousei.DjDouseis.Add(d);
                    }
                }
                InsertDousei.UpdateRecord(loginUser);

                //アカサカの動静情報を簡易動静にも連携する
                WingData.BLC.動静処理 logic = new WingData.BLC.動静処理();
                logic.簡易動静連携(loginUser, InsertDousei);
            }
            return true;
        }

        public class Info
        {
            /// <summary>
            /// info : 0
            /// err  : 1
            /// </summary>
            public int InfoKind = 0;

            public string Msg = "";

            public Info()
            {

            }
        }

    }
}
