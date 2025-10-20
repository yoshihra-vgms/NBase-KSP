using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using NBaseData.DAC;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace Yojitsu.DA
{
    class HimokuTreeReader_入渠
    {
        private static HimokuTreeNode ROOT_NODE;
        private static Dictionary<int, MsHimoku> himokuDic = null;

        public static HimokuTreeNode GetHimokuTree()
        {
            if (ROOT_NODE == null)
            {
                XmlDocument doc = new XmlDocument();
                Assembly asm = Assembly.GetEntryAssembly();
                string fullPath = asm.Location;

                // 2. フル・パスからディレクトリ・パス部分を抽出する
                string dirPath = Path.GetDirectoryName(fullPath);
                doc.Load(dirPath + "\\Template/HimokuTree_入渠.xml");

                ROOT_NODE = new HimokuTreeNode();
                CreateHimokuTreeNodes(ROOT_NODE, doc.DocumentElement.ChildNodes);
            }

            return ROOT_NODE;
        }

        private static void CreateHimokuTreeNodes(HimokuTreeNode parent, XmlNodeList xmlNodeList)
        {
            // 2010.05.31: staticに変更
            //Dictionary<int, MsHimoku> himokuDic = CreateMsHimokuDic();
            if (himokuDic == null)
            {
                himokuDic = CreateMsHimokuDic();
            }

            foreach (XmlNode node in xmlNodeList)
            {
                if (node.Attributes == null)
                {
                    continue;
                }
                
                HimokuTreeNode n = new HimokuTreeNode();
                XmlAttribute himokuId = node.Attributes["MsHimokuID"];

                if (himokuId != null)
                {
                    int id = Int32.Parse(himokuId.Value);

                    if (himokuDic.ContainsKey(id))
                    {
                        n.MsHimoku = himokuDic[id];
                        n.Name = n.MsHimoku.HimokuName;

                        XmlAttribute bgColor = node.Attributes["BgColor"];

                        if (bgColor != null)
                        {
                            n.BgColor = ColorTranslator.FromHtml(bgColor.Value);
                        }

                        XmlAttribute dollar = node.Attributes["Dollar"];

                        if (dollar != null)
                        {
                            n.Dollar = Boolean.Parse(dollar.Value);
                        }

                        parent.Add(n);

                        CreateHimokuTreeNodes(n, node.ChildNodes);
                    }
                }
                else
                {
                    XmlAttribute name = node.Attributes["Name"];

                    if (name != null)
                    {
                        n.Name = name.Value;

                        XmlAttribute bgColor = node.Attributes["BgColor"];

                        if (bgColor != null)
                        {
                            n.BgColor = ColorTranslator.FromHtml(bgColor.Value);
                        }

                        parent.Add(n);

                        CreateHimokuTreeNodes(n, node.ChildNodes);
                    }
                }
            }
        }

        
        private static Dictionary<int, MsHimoku> CreateMsHimokuDic()
        {
            Dictionary<int, MsHimoku> result = new Dictionary<int, MsHimoku>();

            foreach (MsHimoku h in DbAccessorFactory.FACTORY.MsHimoku_GetRecordsWithMsKamoku(NBaseCommon.Common.LoginUser))
            {
                result.Add(h.MsHimokuID, h);
            }

            return result;
        }


        public static HimokuTreeNode GetHimokuTreeNode(int msHimokuId)
        {
            foreach (HimokuTreeNode n in GetHimokuTree())
            {
                if (n.MsHimoku.MsHimokuID == msHimokuId)
                {
                    return n;
                }
            }

            return null;
        }
    }
}
