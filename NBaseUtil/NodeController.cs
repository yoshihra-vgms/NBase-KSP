using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;

namespace NBaseUtil
{
    public class NodeController
    {
        /// <summary>
        /// 
        /// </summary>
        Hashtable TopKeyHash = null;
        Hashtable TopNodeHash = null;
        Hashtable TopInfoHash = null;
        Hashtable TopSubNodeHash = null;

        /// <summary>
        /// 
        /// </summary>
        /// 
        Hashtable SecondKeyHash = null;
        Hashtable SecondNodeHash = null;
        Hashtable SecondInfoHash = null;
        Hashtable SecondParentNodeHash = null;

        public int Count
        {
            get
            {
                return TopKeyHash.Count + SecondKeyHash.Count;
            }
        }

        public NodeController()
        {
            // 第１階層
            TopKeyHash = new Hashtable();
            TopNodeHash = new Hashtable();
            TopInfoHash = new Hashtable();
            TopSubNodeHash = new Hashtable();

            // 第２階層
            SecondKeyHash = new Hashtable();
            SecondNodeHash = new Hashtable();
            SecondInfoHash = new Hashtable();
            SecondParentNodeHash = new Hashtable();
        }
        public void Clear()
        {
            TopKeyHash.Clear();
            TopNodeHash.Clear();
            TopInfoHash.Clear();
            TopSubNodeHash.Clear();

            SecondKeyHash.Clear();
            SecondNodeHash.Clear();
            SecondInfoHash.Clear();
            SecondParentNodeHash.Clear();
        }
        public void SetNode(string key, object info, TreeListViewNode node)
        {
            // 第１階層に登録
            TopKeyHash.Add(node, key);
            TopNodeHash.Add(key, node);
            TopInfoHash.Add(key, info);
        }
        public void SetSubNodes(string topKey, string subKey, object info, TreeListViewNode node)
        {
            // 第１階層に登録
            List<TreeListViewNode> nodes = null;
            if (TopSubNodeHash.Contains(topKey) == false)
            {
                nodes = new List<TreeListViewNode>();
                nodes.Add(node);
                TopSubNodeHash.Add(topKey, nodes);
            }
            else
            {
                nodes = TopSubNodeHash[topKey] as List<TreeListViewNode>;
                nodes.Add(node);
                TopSubNodeHash[topKey] = nodes;
            }
            TreeListViewNode topNode = TopNodeHash[topKey] as TreeListViewNode;

            // 第２階層に登録
            SecondKeyHash.Add(node, subKey);
            SecondNodeHash.Add(subKey, node);
            SecondInfoHash.Add(subKey, info);
            SecondParentNodeHash.Add(subKey, topNode);
        }
        public string GetTopKey(TreeListViewNode node)
        {
            string key = null;
            if (TopKeyHash.Contains(node) == true)
            {
                key = TopKeyHash[node] as string;
            }
            return key;
        }
        public string GetSecondKey(TreeListViewNode node)
        {
            string key = null;
            if (SecondKeyHash.Contains(node) == true)
            {
                key = SecondKeyHash[node] as string;
            }
            return key;
        }
        public TreeListViewNode GetTopNode(string key)
        {
            TreeListViewNode node = null;
            if (TopNodeHash.Contains(key) == true)
            {
                node = TopNodeHash[key] as TreeListViewNode;
            }
            return node;
        }
        public TreeListViewNode GetSecondNode(string key)
        {
            TreeListViewNode node = null;
            if (SecondNodeHash.Contains(key) == true)
            {
                node = SecondNodeHash[key] as TreeListViewNode;
            }
            return node;
        }
        public TreeListViewNode GetParentNode(string key)
        {
            TreeListViewNode parentNode = null;
            if (SecondParentNodeHash.Contains(key) == true)
            {
                parentNode = SecondParentNodeHash[key] as TreeListViewNode;
            }
            return parentNode;
        }
        public object GetTopInfo(string key)
        {
            object info = null;
            if (TopInfoHash.Contains(key) == true)
            {
                info = TopInfoHash[key];
            }
            return info;
        }
        public object GetSecondInfo(string key)
        {
            object info = null;
            if (SecondInfoHash.Contains(key) == true)
            {
                info = SecondInfoHash[key];
            }
            return info;
        }
        public object GetParentInfo(string key)
        {
            object parentInfo = null;
            if (SecondParentNodeHash.Contains(key) == true)
            {
                TreeListViewNode parentNode = null;
                parentNode = SecondParentNodeHash[key] as TreeListViewNode;

                string parentKey = null;
                if (TopKeyHash.Contains(parentNode) == true)
                {
                    parentKey = TopKeyHash[parentNode] as string;

                    if (TopInfoHash.Contains(parentKey) == true)
                    {
                        parentInfo = TopInfoHash[parentKey];
                    }
                }

            }
            return parentInfo;
        }
        public List<TreeListViewNode> GetNodes(string key)
        {
            List<TreeListViewNode> nodes = null;
            if (TopSubNodeHash.Contains(key) == true)
            {
                nodes = TopSubNodeHash[key] as List<TreeListViewNode>;
            }
            return nodes;
        }

        public void RemoveTopKey(string key)
        {
            if (TopSubNodeHash.Contains(key) == true)
            {
                List<TreeListViewNode> nodes = TopSubNodeHash[key] as List<TreeListViewNode>;
                foreach (TreeListViewNode subNode in nodes)
                {
                    if (SecondKeyHash.Contains(subNode) == true)
                    {
                        string secondKey = SecondKeyHash[subNode] as string;

                        SecondKeyHash.Remove(subNode);
                        SecondNodeHash.Remove(secondKey);
                        SecondInfoHash.Remove(secondKey);
                        SecondParentNodeHash.Remove(secondKey);
                    }
                }
            }
            TreeListViewNode node = TopNodeHash[key] as TreeListViewNode;

            TopKeyHash.Remove(node);
            TopNodeHash.Remove(key);
            TopInfoHash.Remove(key);
            TopSubNodeHash.Remove(key);
        }

        public void RemoveSecondKey(string key)
        {
            if (SecondNodeHash.Contains(key) == true)
            {
                TreeListViewNode secondNode = SecondNodeHash[key] as TreeListViewNode;
                TreeListViewNode parentNode = SecondParentNodeHash[key] as TreeListViewNode;

                string topKey = TopKeyHash[parentNode] as string;
                List<TreeListViewNode> nodes = TopSubNodeHash[topKey] as List<TreeListViewNode>;
                nodes.Remove(secondNode);
                TopSubNodeHash[topKey] = nodes;

                SecondKeyHash.Remove(secondNode);
                SecondNodeHash.Remove(key);
                SecondInfoHash.Remove(key);
                SecondParentNodeHash.Remove(key);
            }
         }

        public List<object> GetAllSecondInfo()
        {
            List<object> ret = new List<object>();
            foreach (string key in SecondInfoHash.Keys)
            {
                ret.Add(SecondInfoHash[key]);
            }
            return ret;
        }
    }
}
