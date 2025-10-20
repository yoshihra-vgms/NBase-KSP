using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBaseData.DAC;
using System.IO;


namespace NBaseData.BLC
{
    public class WTMLinkageProc
    {
        static private string makeFilePath(string fname)
        {
            string basePath = System.Configuration.ConfigurationManager.AppSettings["LinkFilePath"];

            // フォルダーが存在するかどうかを確認
            DirectoryInfo dirinfo = new DirectoryInfo(basePath);
            if (!(dirinfo.Exists))
            {
                // フォルダーが存在しない場合は作成
                dirinfo.Create();
            }

            return basePath + @"\" + fname;
        }

        static private string makeFileName(string fname)
        {
            return fname + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
        }


        static public bool CreateCrewLinkFile(int seninId, string seninName)
        {
            bool ret = true;

            string fileName = makeFileName("Crew");
            string filePath = makeFilePath(fileName);
            //StreamWriter fileSw = new StreamWriter(new FileStream(filePath, FileMode.Create), System.Text.Encoding.GetEncoding("shift_jis"));
            StreamWriter fileSw = new StreamWriter(new FileStream(filePath, FileMode.Create));

            try
            {
                StringBuilder strLine = new StringBuilder();
                strLine.Append(seninId.ToString());

                strLine.Append(",");
                strLine.Append(seninName);


                fileSw.WriteLine(strLine);
  
            }
            catch (Exception e)
            {
                ret = false;
            }
            finally
            {
                fileSw.Close();
            }

            //
            if (ret)
            {
                if (Excecute(fileName) == -1)
                {
                    ret = false;
                }
            }

            return ret;
        }

        static public bool CreateSignOnOffLinkFile(SiCard card)
        {
            bool ret = true;

            if (card.DeleteFlag == 1 && (card.WTMLinkageID == null || card.WTMLinkageID.Length == 0))
            {
                return ret;
            }


            string fileName = "";
            if (card.DeleteFlag == 1)
            {
                fileName = makeFileName("DelSignOnOff");
            }
            else if (card.EndDate == DateTime.MinValue)
            {
                fileName = makeFileName("SignOn");
            }
            else
            {
                fileName = makeFileName("SignOff");
            }
            string filePath = makeFilePath(fileName);

            //StreamWriter fileSw = new StreamWriter(new FileStream(filePath, FileMode.Create), System.Text.Encoding.GetEncoding("shift_jis"));
            StreamWriter fileSw = new StreamWriter(new FileStream(filePath, FileMode.Create));

            try
            {
                StringBuilder strLine = new StringBuilder();

                if (card.DeleteFlag == 1 && (card.WTMLinkageID != null && card.WTMLinkageID.Length > 0))
                {
                    strLine.Append(card.WTMLinkageID);
                }
                else
                {
                    strLine.Append(card.WTMLinkageID);

                    strLine.Append(",");
                    strLine.Append(card.MsSeninID.ToString());

                    strLine.Append(",");
                    strLine.Append(card.CardMsSiShokumeiID);

                    strLine.Append(",");
                    strLine.Append(card.MsVesselID);

                    strLine.Append(",");
                    strLine.Append(card.StartDate.ToString("yyyy/MM/dd"));

                    if (card.EndDate != DateTime.MinValue)
                    {
                        strLine.Append(",");
                        strLine.Append(card.EndDate.ToString("yyyy/MM/dd"));

                    }
                }

                fileSw.WriteLine(strLine);

            }
            catch (Exception e)
            {
                ret = false;
            }
            finally
            {
                fileSw.Close();
            }


            if (ret)
            {
                var exeRet = Excecute(fileName);
                if (exeRet == -1)
                {
                    ret = false;
                }
                else if (card.WTMLinkageID == null || card.WTMLinkageID.Length == 0)
                {
                    var id = ReadRetFile(exeRet);
                    if (id != null)
                    {
                        card.WTMLinkageID = id;
                    }
                }
            }

            return ret;
        }




        static public int Excecute(string filePath)
        {
            int ret = -1;
            try
            {
                string linkageExePath = System.Configuration.ConfigurationManager.AppSettings["linkageExePath"];
                if (linkageExePath != null && linkageExePath.Length > 0)
                {
                    var proc = new System.Diagnostics.Process();

                    proc.StartInfo.FileName = linkageExePath;
                    proc.StartInfo.Arguments = filePath;
                    proc.StartInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
                    proc.StartInfo.UseShellExecute = false; // シェル機能を使用しない
                    proc.Start();

                    proc.WaitForExit();

                    ret = proc.ExitCode;
                }
            }
            catch (Exception ex)
            {
                ret = -1;
            }
            return ret;
        }



        static private string ReadRetFile(int retFileName)
        {
            string retId = null;
            try
            {
                string filePath = makeFilePath(retFileName.ToString());
                if (filePath.Length > 0)
                {
                    StreamReader fileSr = new StreamReader(new FileStream(filePath, FileMode.Open));
                    retId = fileSr.ReadLine();
                    fileSr.Close();
                }
            }
            catch
            {
                // Exception発生時は無視
            }
            return retId;
        }
    }
}
