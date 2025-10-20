using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using System.IO;
using NBaseData.DAC;
using NBaseData.DS;
using System.Collections.Generic;
using NBaseData.BLC;

namespace NBaseService
{
	public partial interface IService
	{
		[OperationContract]
		byte[] BLC_Excel経常収支管理表_取得(int sy, int sm, int ey, int em,
			string unit, decimal rate, BgYosanHead head, MsUser loginUser);
	}

	public partial class Service
	{
		public byte[] BLC_Excel経常収支管理表_取得(int sy, int sm, int ey, int em,
			string unit, decimal rate, BgYosanHead head, MsUser loginUser)
		{
            string outname = "outPut_[" + loginUser.FullName + "]_経常収支.xlsx";
			
			//パスを取得
			string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
			string filename = path + outname;

			経常収支管理表出力処理 crea = new 経常収支管理表出力処理();
			crea.LoginUser = loginUser;
			bool ret = crea.経常収支管理表作成(filename, sy, sm, ey, em, unit, rate, head);

			if (ret == false)
			{
				return null;
			}


			System.Threading.Thread.Sleep(3000);

			byte[] bytBuffer;
			#region バイナリーへ変換
			using (FileStream objFileStream = new FileStream(filename, FileMode.Open))
			{
				long lngFileSize = objFileStream.Length;

				bytBuffer = new byte[(int)lngFileSize];
				objFileStream.Read(bytBuffer, 0, (int)lngFileSize);
				objFileStream.Close();
			}
			#endregion

			return bytBuffer;
		}
	}
}
