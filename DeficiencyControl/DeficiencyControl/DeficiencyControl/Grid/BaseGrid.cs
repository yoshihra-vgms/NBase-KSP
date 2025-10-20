using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace DeficiencyControl.Grid
{
    /// <summary>
    /// グリッド管理基底クラス
    /// </summary>
    public abstract class BaseGrid
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dgv"></param>
        public BaseGrid(DataGridView dgv)
        {
            this.Grid = dgv;
        }


        /// <summary>
        /// 管理グリッド
        /// </summary>
        protected DataGridView Grid = null;


        //データ表示関数
        public abstract bool DispData(object objlist);

        /// <summary>
        /// 現在の選択データindexを取得する 選択なし=-1
        /// </summary>
        /// <returns></returns>
        public virtual int GetSelectDataIndex()
        {
            int ans = 0;

            if (this.Grid.SelectedRows.Count <= 0)
            {
                return -1;
            }
            ans = this.Grid.SelectedRows[0].Index;
            
            return ans;
        }

        /// <summary>
        /// 対象のデータindexの行を選択する
        /// </summary>
        /// <param name="dataindex">選択データindex</param>
        /// <returns></returns>
        public virtual bool SetSelectDataIndex(int dataindex)
        {
            if (dataindex < 0)
            {
                return false;
            }

            if (this.Grid.Rows.Count <= dataindex)
            {
                return false;
            }

            this.Grid.Rows[dataindex].Selected = true;

            return true;
        }

        /// <summary>
        /// 選択データのオブジェクトを取得（複数選択を取得するときはGetSelectDataObjectListのほうを使用すること）
        /// </summary>
        /// <returns></returns>
        public virtual object GetSelectDataObject()
        {
            //選択している？
            if (this.Grid.SelectedRows.Count <= 0)
            {
                return null;
            }

            return this.Grid.SelectedRows[0].Cells[0].Value;
        }

        /// <summary>
        /// 選択データのオブジェクトリストを取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual List<T> GetSelectDataObjectList<T>() where T : class
        {
            List<T> anslist = new List<T>();

            //選択対象をすべて取得
            for (int i = 0; i < this.Grid.SelectedRows.Count; i++)
            {
                T data = this.Grid.SelectedRows[i].Cells[0].Value as T;
                if (data == null)
                {
                    return null;
                }
                
                anslist.Add(data);            
            }

            //最後に選択されたものが最初になってしまうため、選択をした順にする。
            anslist.Reverse();

            return anslist;
        }


        /// <summary>
        /// 現在順番でデータリストを作成する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual List<T> GetDataObjectList<T>()
        {
            List<T> anslist = new List<T>();

            try
            {
                //データを取得
                foreach (DataGridViewRow drow in this.Grid.Rows)
                {
                    T data = (T)drow.Cells[0].Value;
                    anslist.Add(data);
                }
            }
            catch (Exception e)
            {
                return null;
            }

            
            return anslist;
        }


        /// <summary>
        /// 現在のソート選択で再ソートする
        /// <remarks>中で選択位置の復帰も行っています。</remarks>
        /// </summary>
        /// <returns>成功可否</returns>
        protected bool SortSelectColumn()
        {
            //現在のソート状況を取得
            DataGridViewColumn sortcol = this.Grid.SortedColumn;
            SortOrder sor = this.Grid.SortOrder;

            //選択をずらさない配慮をする。
            int selectindex = this.GetSelectDataIndex();

            //---------------------------------------------------------------
            //---------------------------------------------------------------

            //ソートをする。
            if (sortcol != null)
            {
                switch (sor)
                {
                    //昇順
                    case SortOrder.Ascending:
                        this.Grid.Sort(sortcol, System.ComponentModel.ListSortDirection.Ascending);
                        break;

                    //降順
                    case SortOrder.Descending:
                        this.Grid.Sort(sortcol, System.ComponentModel.ListSortDirection.Descending);
                        break;
                }

            }

            //選択場所の復帰
            this.SetSelectDataIndex(selectindex);

            return true;
        }
    }
}
