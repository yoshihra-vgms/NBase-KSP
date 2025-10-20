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

		//public ***********************************************************
		/// <summary>
		/// 管理表作成処理
		/// 引数：ファイル名、開始年月、終了年月、単位名、単位、関連YosanHead
		/// 返り値：なし
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="sy"></param>
		/// <param name="sm"></param>
		/// <param name="ey"></param>
		/// <param name="em"></param>
		/// <param name="rate"></param>
		/// <param name="head"></param>
		/// <returns></returns>
		public bool 経常収支管理表作成(
			string filename, int sy, int sm, int ey, int em,
			string unit, decimal rate, BgYosanHead head)
		{
			
			
			//書き込みをする
			//this.Crea = new ExcelCreator.Xlsx.XlsxCreator();			
			using (this.Crea = new ExcelCreator.Xlsx.XlsxCreator())
			{
				//テンプレート名取得
				string tempname = this.TemplateFileName作成();
				
				//int ret = this.Crea.CreateBook(filename, 2, xlVersion.ver2003);
				int ret = this.Crea.OpenBook(filename, tempname);
				if (ret < 0)
				{
					return false;
				}

				
				//テンプレートの作成
				this.TemplateSheet作成(sy, sm, ey, em, unit, rate, head);


				//本体へ書き込み
				this.WriteMainSheet総括(sy, sm, ey, em, unit, rate, head);


				this.Crea.CloseBook(true);
			}

			return true;
		}

		//*********************************************************************
		//private**************************************************************
		//メンバ関数===========================================================

		#region 初期化関数たち

		/// <summary>
		/// 書き込み準備のためいろいろなものを取得する
		/// </summary>
		private void InitPreWritting()
		{
			//縦列の初期化
			this.CreateColInfo();


			//横列の初期化
			this.CreateRowInfo();

		}

		/// <summary>
		/// 縦列データの作成
		/// 引数：なし
		/// 返り値：なし
		/// </summary>
		private void CreateColInfo()
		{
			this.ColInfoList.Clear();

			//MS_VESSEL_TYPEの取得
			List<MsVesselType> vtlist = new List<MsVesselType>();
			vtlist = MsVesselType.GetRecords(this.LoginUser);

			//開始位置の取得
			char nowpos = 経常収支管理表出力処理.Grapth開始位置[0];
			nowpos++;


			foreach (MsVesselType vt in vtlist)
			{
				ColInfo data = new ColInfo();

				data.HeaderName = vt.VesselTypeName;
				data.Col = nowpos.ToString();

				data.VesselTypeData = vt;


				//ADD
				this.ColInfoList.Add(data);

				nowpos++;
			}
			nowpos--;

			//最後の場所を記憶しておく
			this.GrapthLastPosX = nowpos.ToString();
		}

		/// <summary>
		/// 横列データの作成
		/// 引数：なし
		/// 返り値：なし
		/// </summary>
		private void CreateRowInfo()
		{
			this.RowInfoList.Clear();

			//営業Ｇの費目の取得	
			List<MsHimoku> himolist = new List<MsHimoku>();
			//himolist = DbAccessorFactory.FACTORY.MsHimoku_GetRecordsByMsBumonID(NBaseCommon.Common.LoginUser,
			//	経常収支管理表出力処理.営業GBumonID);
			himolist = MsHimoku.GetRecords(this.LoginUser);


			//今の位置
			int pos = Convert.ToInt32(経常収支管理表出力処理.Grapth開始位置[1].ToString());
			pos += 1;

			//取得した費目を一項目としてADDする
			foreach (MsHimoku himo in himolist)
			{
				RowInfo data = new RowInfo();

				data.RowName = himo.HimokuName;
				data.Row = pos;
				data.HimokuData = himo;

				//ADD
				this.RowInfoList.Add(data);

				//次の場所へ(予算＋実績分)
				pos += 経常収支管理表出力処理.費目オフセット;
			}

			//縦列の最後を記憶
			this.GrapthLastPosY = pos - 1;
		}


		#endregion

		/// <summary>
		/// テンプレートシート作成関数
		/// 費目のテンプレートを作成する
		/// </summary>
		/// <param name="sy"></param>
		/// <param name="sm"></param>
		/// <param name="ey"></param>
		/// <param name="em"></param>
		/// <param name="unit"></param>
		/// <param name="rate"></param>
		/// <param name="head"></param>
		private void TemplateSheet作成(int sy, int sm, int ey, int em,
			string unit, decimal rate, BgYosanHead head)
		{
			//シートNo設定
			this.Crea.SheetNo = 経常収支管理表出力処理.費目テンプレートSheetNo;


			this.InitPreWritting();
			this.WriteGrapth総括(sy, sm, ey, em, unit, rate, head);
			

			//元へ戻す
			this.Crea.SheetNo = 0;
		}

		//本体への書き込みをする
		private void WriteMainSheet総括(int sy, int sm, int ey, int em,
			string unit, decimal rate, BgYosanHead head)
		{
			//年月
			this.Write年月(sy, sm, ey, em);

			//単位
			this.Write単位(unit);

			//外航船
			this.Write外航船();

			//船種別
			this.Write船種別();		
	
		}



		/// <summary>
		/// 年月の書き込み
		/// 開始年月、終了年月
		/// </summary>
		private void Write年月(int sy, int sm, int ey, int em)
		{
			//文字列を作成
			string sdate = sy.ToString();
			sdate += "年";
			sdate += sm.ToString();
			sdate += "月　～ ";

			sdate += ey.ToString();
			sdate += "年";
			sdate += em.ToString();
			sdate += "月";

			//年を書き込み
			this.Crea.Cell(経常収支管理表出力処理.Temp年月).Value = sdate;
		}



		/// <summary>
		/// 単位を書き込む
		/// 引数：今回の出力単位
		/// </summary>
		/// <param name="unit"></param>
		private void Write単位(string unit)
		{
			string sdata = "単位：";
			sdata += unit;

			//書き込み
			this.Crea.Cell(経常収支管理表出力処理.Temp単位).Value = sdata;
		}



		/// <summary>
		/// 種別ごとの船を書く
		/// </summary>
		private void Write船種別()
		{
			//船を取得
			List<MsVessel> vessellist = MsVessel.GetRecordsByYojitsuEnabled(this.LoginUser);

			//船区切り文字
			string devchar = "、";

			int i = 0;

			foreach (ColInfo co in this.ColInfoList)
			{
				//無い時
				if (co.VesselTypeData == null)
				{
					continue;
				}

				//区分を書く
				string spos = 経常収支管理表出力処理.Temp船種別名 + i.ToString();
				this.Crea.Cell(spos).Value = co.VesselTypeData.VesselTypeName;

				string data = "";

				//一致する船を描く
				foreach (MsVessel ves in vessellist)
				{
					//一致？
					if (ves.MsVesselTypeID == co.VesselTypeData.MsVesselTypeID)
					{
						data += ves.VesselName + devchar;
					}
				}
				if (data.Length > 0)
				{
					//不要な文字(devchar)を消す
					data = data.Remove(data.Length - 1);
				}


				//一致した船を書く
				spos = 経常収支管理表出力処理.Temp種別別船名 + i.ToString();
				this.Crea.Cell(spos).Value = data;


				//次の場所へ
				i++;

			}
		}

		/// <summary>
		/// 読み込みテンプレート名作成
		/// </summary>
		/// <returns></returns>
		private string TemplateFileName作成()
		{
			string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
			string name = path  + 経常収支管理表出力処理.経常収支Templateファイル名;

			return name;
		}



		/////////////////////////////////////////////////////////////////////
		/// <summary>
		/// 表の書き込み総括
		/// 引数：なし
		/// 返り値：期間、単位、予算頭
		/// </summary>
		private void WriteGrapth総括(int sy, int sm, int ey, int em,
			string unit, decimal rate, BgYosanHead head)
		{

			//ヘッダー
			this.WriteGraphHeader();

			//データ
			this.WriteGrapthData総括(sy, sm, ey, em, unit, rate, head);

			//枠
			this.WriteGrapthLine();
		}

		/// <summary>
		/// 表のヘッダーを描く
		/// </summary>
		private void WriteGraphHeader()
		{
			//タイトルを描く
			//this.Crea.Cell(util.経常収支管理表出力処理.GrapthStartPos).Attr.FontStyle = xlFontStyle.xsBold;
			//this.Crea.Cell(util.経常収支管理表出力処理.GrapthStartPos).Attr.FontName = "ＭＳ Ｐゴシック";
			//this.Crea.Cell(util.経常収支管理表出力処理.Grapth開始位置).Value = "上段：実績\n下段：計画";


			int colpos = Convert.ToInt32(経常収支管理表出力処理.Grapth開始位置[1].ToString());

			bool boldflag = false;

			//まず縦の列を書く
			foreach (ColInfo col in this.ColInfoList)
			{

				//書き込み
				string pos = col.Col + colpos.ToString();
				this.Crea.Cell(pos).Value = col.HeaderName;



				//////////////////////////////////////////////////////
				//線を変更する
				this.Crea.Cell(pos).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
				this.Crea.Cell(pos).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

				//一回だけやめる
				if ((boldflag == false && col.VesselTypeData == null))
				{
					this.Crea.Cell(pos).Attr.LineLeft (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

					boldflag = true;
				}

				//最後は全てをだめにする
				if (col == this.AllSum)
				{
					this.Crea.Cell(pos).Attr.LineRight (ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
				}
			}

			////////////////////////////////////////////////////////////////////////////////////

			string rowpos = 経常収支管理表出力処理.Grapth開始位置[0].ToString();

			//横の列を描く
			foreach (RowInfo row in this.RowInfoList)
			{
				int rc = Convert.ToInt32(row.Row);
				rc++;

				string spos = rowpos + row.Row;
				string spos2 = rowpos + rc.ToString();

				//上限のセルをくっつける		
				this.Crea.Cell(spos + ":" + spos2).Attr.MergeCells = true;

				//書き込み				
				this.Crea.Cell(spos).Value = row.RowName;

			}


		}

		/// <summary>
		/// 表の枠を描く
		/// </summary>
		private void WriteGrapthLine()
		{
			string spos = 経常収支管理表出力処理.Grapth開始位置;
			string epos = this.GrapthLastPosX + this.GrapthLastPosY.ToString();

			//枠線を描く
			this.ChangeLineStyleAll(this.Crea.Cell(spos + ":" + epos), ExcelCreator.BorderStyle.Thin);

		}


		/// <summary>
		/// グラフデータ書き込み総括
		/// 返り値：期間、単位、予算頭
		/// </summary>
		private void WriteGrapthData総括(int sy, int sm, int ey, int em,
			string unit, decimal rate, BgYosanHead head)
		{
			#region 検索年月文字列作成
			//開始
			string start_date = sy.ToString().PadLeft(4, '0');
			start_date += sm.ToString().PadLeft(2, '0');

			//終了
			string end_date = ey.ToString().PadLeft(4, '0');
			end_date += em.ToString().PadLeft(2, '0');
			#endregion

			//タイプごとに回す
			foreach (ColInfo colinfo in this.ColInfoList)
			{
				//船タイプタイプがないとき
				if (colinfo.VesselTypeData == null)
				{
					//this.SelectWriteData(colinfo);
					continue;
				}

				//船タイプに関連する計画値の検索
				List<BgYosanItem> planlist =
						BgYosanItem.GetRecordsByYosanHeadPriodVesselTypeHimokus
						(this.LoginUser, head.YosanHeadID,
						start_date, end_date, colinfo.VesselTypeData.MsVesselTypeID);


				//実績の検索
				List<BgJiseki> exlist =
					BgJiseki.GetRecordsByVesselTypePriodHimokus
					(this.LoginUser, colinfo.VesselTypeData.MsVesselTypeID,
					start_date, end_date);



				//費目ごと
				foreach (RowInfo rowinfo in this.RowInfoList)
				{
					//実績の書き込み
					this.WriteGrapthData実績(colinfo.Col, rowinfo, exlist, rate);


					//計画値費目ごとの書き込み
					this.WriteGrapthData計画値(colinfo.Col, rowinfo, planlist, rate);

				}
			}




		}


		/// <summary>
		/// 計画値書き込み
		/// 引数：横列の位置、関連Rowinfo、計画データ、書き込みRate
		/// </summary>
		/// <param name="col"></param>
		/// <param name="rowinfo"></param>
		/// <param name="planlist"></param>
		private void WriteGrapthData計画値(string col, RowInfo rowinfo, List<BgYosanItem> planlist, decimal rate)
		{
			//書き込み場所の作成
			string cellpos = col + (rowinfo.Row + 経常収支管理表出力処理.計画ColOffset).ToString();

			decimal data = 0;

			//関連費目の取得
			foreach (BgYosanItem item in planlist)
			{
				if (item.MsHimokuID == rowinfo.HimokuData.MsHimokuID)
				{
					data = item.Amount;
					break;
				}
			}

			//セル変更

			//マイナスのとき
			if (data < 0)
			{
				this.Crea.Cell(cellpos).Attr.FontColor = 経常収支管理表出力処理.MinusFontColor;
			}

			//線の変更
			this.Crea.Cell(cellpos).Attr.LineTop(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);

			this.Crea.Cell(cellpos).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
			this.Crea.Cell(cellpos).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);



			//------------------------------------------------------------------------

			//単位に即した値にする
			data /= rate;

			//書き込み
			this.Crea.Cell(cellpos).Value = data;

		}


		/// <summary>
		/// 実績値の書き込み
		/// 引数：横列位置、関係rowinfo、実績データ、書き込みRate
		/// </summary>
		/// <param name="col"></param>
		/// <param name="rowinfo"></param>
		/// <param name="planlist"></param>
		/// <param name="rate"></param>
		private void WriteGrapthData実績(string col, RowInfo rowinfo, List<BgJiseki> jisekilist, decimal rate)
		{
			//書き込み場所の作成
			string cellpos = col + (rowinfo.Row + 経常収支管理表出力処理.実績ColOffset).ToString();

			decimal data = 0;


			//関連データの取得
			foreach (BgJiseki item in jisekilist)
			{
				if (item.MsKamokuID == rowinfo.HimokuData.MsHimokuID)
				{
					data = item.Amount;
					break;
				}
			}

			//Cellの変更
			//マイナスのとき
			if (data < 0)
			{
				this.Crea.Cell(cellpos).Attr.FontColor = 経常収支管理表出力処理.MinusFontColor;
			}

			//線の変更
			this.Crea.Cell(cellpos).Attr.LineBottom(ExcelCreator.BorderStyle.Dashed, ExcelCreator.xlColor.Black);
			this.Crea.Cell(cellpos).Attr.LineLeft(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);
			this.Crea.Cell(cellpos).Attr.LineRight(ExcelCreator.BorderStyle.Thin, ExcelCreator.xlColor.Black);

			//実績は太字
			this.Crea.Cell(cellpos).Attr.FontStyle = ExcelCreator.FontStyle.Bold;

			//------------------------------------------------------------------------

			//単位に即した値にする
			data /= rate;

			//値の書き込み
			this.Crea.Cell(cellpos).Value = data;
		}

		/// <summary>
		/// 指定セルの枠をすべて変更する
		/// 引数：セル、変更スタイル
		/// </summary>
		/// <param name="sy"></param>
		private void ChangeLineStyleAll(ExcelCreator.Xlsx.Cell cell, ExcelCreator.BorderStyle sy)
		{
			cell.Attr.LineTop (sy, ExcelCreator.xlColor.Black);
			cell.Attr.LineBottom(sy, ExcelCreator.xlColor.Black);
			cell.Attr.LineLeft(sy, ExcelCreator.xlColor.Black);
			cell.Attr.LineRight(sy, ExcelCreator.xlColor.Black);
		}


		/// <summary>
		/// 外航船の書き込み
		/// </summary>
		private void Write外航船()
		{
			//(今は特になし)
		}
	}
}


