using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using NBaseData.DAC;

using ExcelCreator = AdvanceSoftware.ExcelCreator;


namespace NBaseService
{
	public partial class 経常収支管理表出力処理
	{

		//メンバ変数========================================================
		//縦データ情報
		private List<ColInfo> ColInfoList = new List<ColInfo>();

		//横データ情報
		private List<RowInfo> RowInfoList = new List<RowInfo>();

		//内航船計
		ColInfo VesselSum = null;

		//外航船
		ColInfo OutVessel = null;

		//全体合計
		ColInfo AllSum = null;


		//グラフ最後の場所
		string GrapthLastPosX = "";
		int GrapthLastPosY = 0;

		//ExcelCreator
		private ExcelCreator.Xlsx.XlsxCreator Crea = null;

		public MsUser LoginUser = null;

		//定義たち--------------------------------------------------
		/// <summary>
		/// 営業Gの部門ID番号
		/// </summary>
		private static readonly int 営業GBumonID = 1;

		/// <summary>
		/// タイトル名
		/// </summary>
		private static readonly string TitleName = "経常収支実績、投入先別収入";

		/// <summary>
		/// 1列目フォントサイズ
		/// </summary>
		private static readonly int TitleRow文字サイズ = 22;

		/// <summary>
		/// タイトルを書く場所
		/// </summary>
		private static readonly string Pos題名 = "D1";

		/// <summary>
		/// 年月をかく場所
		/// </summary>
		private static readonly string Pos年月 = "B1";

		/// <summary>
		/// 表のstart地点
		/// </summary>
		private static readonly string Grapth開始位置 = "B3";

		/// <summary>
		/// 1 データのオフセット
		/// </summary>
		private static readonly int 費目オフセット = 2;


		/// <summary>
		/// 内航船計タイトル
		/// </summary>
		private static readonly string VesselSumName = "内航船計";

		/// <summary>
		/// 外航船タイトル
		/// </summary>
		private static readonly string OutVesselName = "外航船";

		/// <summary>
		/// 全体合計タイトル
		/// </summary>
		private static readonly string AllSumName = "全体合計";

		/// <summary>
		///　横タイトルの幅
		/// </summary>
		private static readonly int RowTitleWidth = 22;

		/// <summary>
		/// 船タイプごとの船を書く場所のオフセット
		/// </summary>
		private static readonly int 船種別OffsetY = 4;

		/// <summary>
		/// 船タイプと船をかく場所
		/// </summary>
		private static readonly string VesselTypeNameColLine = "B";
		private static readonly string VesselTypeNameVesselColLine = "C";

		/// <summary>
		/// 計画と実績の書き込み場所オフセット
		/// </summary>
		private static readonly int 実績ColOffset = 0;
		private static readonly int 計画ColOffset = 1;


		/// <summary>
		/// 小数を出すときのフォーマット
		/// </summary>
		private static readonly string DevRateString = "F3";

		/// <summary>
		/// マイナスになったときの文字の色
		/// </summary>
		private static readonly Color MinusFontColor = Color.Red;

		/// <summary>
		/// 単位を書き込む場所Y
		/// </summary>
		private static readonly int Write単位PosY = 2;


		//////////////////////////////////////////////////////////////////////////
		//龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗//
		//龗//////////////////////////////////////////////////////////////////龗//
		//龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗//
		//龗//////////////////////////////////////////////////////////////////龗//
		//龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗//
		//龗//////////////////////////////////////////////////////////////////龗//
		//龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗龗//
		//////////////////////////////////////////////////////////////////////////


		/// <summary>
		/// 費目テンプレート書き込みシート番号
		/// </summary>
		private static readonly int 費目テンプレートSheetNo = 1;

		/// <summary>
		/// 年月をかく場所
		/// </summary>
		private static readonly string Temp年月 = "**NENGETSU";

		/// <summary>
		/// 単位を描く場所
		/// </summary>
		private static readonly string Temp単位 = "**UNIT";

		/// <summary>
		/// 船種別名を書く場所
		/// </summary>
		private static readonly string Temp船種別名 = "**VESSEL_TYPE_TYPE_NAME";

		/// <summary>
		/// 種別別船を書く
		/// </summary>
		private static readonly string Temp種別別船名 = "**VESSEL_TYPE_NAME";

		/// <summary>
		/// 経常収支templateファイル名
		/// </summary>
		private static readonly string 経常収支Templateファイル名 = "Template_経常収支実績.xlsx";

		//class------------------------------------------------------
		//縦のヘッダーデータ
		private class ColInfo
		{
			public enum TYPE
			{
				VESSEL,	//船種別データ
				VES_SUM,	//内航船計
				OUT_VES,	//外航船
				ALL_SUM,	//全体合計
			}

			/// <summary>
			/// ヘッダー名
			/// </summary>
			public string HeaderName = "";

			/// <summary>
			/// 自分の存在する縦列番号
			/// </summary>
			public string Col = "";

			/// <summary>
			/// 関連するかもしれないMsVesselType
			/// </summary>
			public MsVesselType VesselTypeData = null;

			/// <summary>
			/// 自分は何かのデータ
			/// </summary>
			public TYPE Type = TYPE.VESSEL;

		}

		//横のデータ
		private class RowInfo
		{
			/// <summary>
			/// 横列の名前
			/// </summary>
			public string RowName = "";

			/// <summary>
			/// 自分の存在する場所(先頭)
			/// </summary>
			public int Row = 0;

			/// <summary>
			/// 関連する費目データ
			/// </summary>
			public MsHimoku HimokuData = null;
		}



	}
}
