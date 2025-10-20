using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;

namespace NBaseMaster.権限管理
{
    public partial class 権限管理Form : Form
    {
        private List<MsBumon> bumons;
        private List<MsRole> roles;

        private static readonly string 利用可 = "利用可";
        private static readonly string 利用不可 = "-";


        public 権限管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.Name = "機能名1";
            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = "機能名2";
            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.Name = "機能名3";

            dataGridView1.Columns.Add(col1);
            dataGridView1.Columns.Add(col2);
            dataGridView1.Columns.Add(col3);

            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;

            bumons = Load_部門();

            foreach (MsBumon b in bumons)
            {
                for (int i = 0; i < 2; i++)
                {
                    string name = b.BumonName;

                    if (i == 0)
                    {
                        name += "\n担当者";
                    }
                    else
                    {
                        name += "\n管理者";
                    }

                    DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
                    col.Name = name;
                    col.Items.Add(利用不可);
                    col.Items.Add(利用可);
                    dataGridView1.Columns.Add(col);

                    int k = dataGridView1.Columns.Count - 1;

                    dataGridView1.Columns[k].Width = 100;
                }
            }
        }


        private List<MsBumon> Load_部門()
        {
            List<MsBumon> bumons = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                bumons = serviceClient.MsBumon_GetRecords(NBaseCommon.Common.LoginUser);
            }

            return bumons;
        }


        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void Search()
        {
            roles = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                roles = serviceClient.MsRole_SearchRecords(NBaseCommon.Common.LoginUser, textBox機能名.Text.Trim());
            }


            var tmp = roles as IEnumerable<MsRole>;


            // 検索結果を契約情報でフィルタする
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注) == false)
            {
                // 発注管理 非表示
                tmp = tmp.Where(o => o.Name1 != MsRole.KEY_発注管理);
                tmp = tmp.Where(o => o.Name2 != MsRole.KEY_発注管理);
            }

            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.動静) == false)
            {
                // 動静管理 非表示
                tmp = tmp.Where(o => o.Name1 != MsRole.KEY_動静管理);
                tmp = tmp.Where(o => o.Name2 != MsRole.KEY_動静管理);
            }

            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員) == false)
            {
                // 船員管理 非表示
                tmp = tmp.Where(o => o.Name1 != MsRole.KEY_船員管理);
                tmp = tmp.Where(o => o.Name2 != MsRole.KEY_船員管理);
            }
            else
            {
                if (NBaseCommon.Common.配乗計画TYPE == MsPlanType.PlanTypeOneMonth || NBaseCommon.Common.配乗計画TYPE == MsPlanType.PlanTypeHarfPeriod)
                {
                    List<MsPlanType> planTypeList = MsPlanType.GetRecords();

                    string planTypeMenuStr = "";
                    if (NBaseCommon.Common.配乗計画TYPE == MsPlanType.PlanTypeOneMonth)
                    {
                        planTypeMenuStr = "配乗計画（" + planTypeList.Where(o => o.Type == MsPlanType.PlanTypeHarfPeriod).First().Name + "）";
                    }
                    else
                    {
                        planTypeMenuStr = "配乗計画（" + planTypeList.Where(o => o.Type == MsPlanType.PlanTypeOneMonth).First().Name + "）";
                    }
                    tmp = tmp.Where(o => o.Name3 != planTypeMenuStr);
                }
            }

            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.指摘) == false)
            {
                // 指摘事項管理 非表示
                tmp = tmp.Where(o => o.Name1 != MsRole.KEY_指摘事項管理);
                tmp = tmp.Where(o => o.Name2 != MsRole.KEY_指摘事項管理);
            }

            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.文書) == false)
            {
                // ﾄﾞｷｭﾒﾝﾄ管理 非表示
                tmp = tmp.Where(o => o.Name1 != MsRole.KEY_文書管理);
                tmp = tmp.Where(o => o.Name2 != MsRole.KEY_文書管理);
                tmp = tmp.Where(o => o.Name2 != MsRole.KEY_文書管理2);
            }

            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.予実) == false)
            {
                // 予算実績管理 非表示
                tmp = tmp.Where(o => o.Name1 != MsRole.KEY_予実管理);
                tmp = tmp.Where(o => o.Name2 != MsRole.KEY_予実管理);
            }

            roles = tmp.ToList();

            SetRows(CreateRowDic(roles));
        }


        private Dictionary<RoleNameKey, Dictionary<RoleBumonKey, MsRole>> CreateRowDic(List<MsRole> roles)
        {
            var dic = new Dictionary<RoleNameKey, Dictionary<RoleBumonKey, MsRole>>();

            foreach (MsRole r in roles)
            {
                RoleNameKey key = new RoleNameKey(r.Name1, r.Name2, r.Name3);

                if (!dic.ContainsKey(key))
                {
                    dic[key] = new Dictionary<RoleBumonKey, MsRole>();
                }

                dic[key][new RoleBumonKey(r.MsBumonID, r.AdminFlag)] = r;
            }

            return dic;
        }


        private void SetRows(Dictionary<RoleNameKey, Dictionary<RoleBumonKey, MsRole>> dic)
        {
            dataGridView1.Rows.Clear();

            int n = 0;
            foreach (KeyValuePair<RoleNameKey, Dictionary<RoleBumonKey, MsRole>> pair in dic)
            {
                dataGridView1.Rows.Add();

                dataGridView1.Rows[n].Cells[0].Value = pair.Key.name1;
                dataGridView1.Rows[n].Cells[1].Value = pair.Key.name2;
                dataGridView1.Rows[n].Cells[2].Value = pair.Key.name3;

                int k = 3;
                foreach (MsBumon b in bumons)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        MsRole r = pair.Value[new RoleBumonKey(b.MsBumonID, i)];

                        if (r.EnableFlag == 0)
                        {
                            dataGridView1.Rows[n].Cells[k].Value = 利用不可;
                        }
                        else
                        {
                            dataGridView1.Rows[n].Cells[k].Value = 利用可;
                        }

                        dataGridView1.Rows[n].Cells[k].Tag = r;
                        k++;
                    }
                }

                n++;
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox機能名.Text = string.Empty;
            dataGridView1.Rows.Clear();
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private class RoleNameKey
        {
            public string name1, name2, name3;


            public RoleNameKey(string name1, string name2, string name3)
            {
                this.name1 = name1;
                this.name2 = name2;
                this.name3 = name3;
            }


            public override bool Equals(object obj)
            {
                RoleNameKey other = obj as RoleNameKey;

                return name1 == other.name1 && name2 == other.name2 && name3 == other.name3;
            }


            public override int GetHashCode()
            {
                return name1.GetHashCode() ^ name2.GetHashCode() ^ name3.GetHashCode();
            }
        }


        private class RoleBumonKey
        {
            public string bumonId;
            public int adminFlag;


            public RoleBumonKey(string bumonId, int adminFlag)
            {
                this.bumonId = bumonId;
                this.adminFlag = adminFlag;
            }


            public override bool Equals(object obj)
            {
                RoleBumonKey other = obj as RoleBumonKey;

                return bumonId == other.bumonId && adminFlag == other.adminFlag;
            }


            public override int GetHashCode()
            {
                return bumonId.GetHashCode() ^ adminFlag;
            }
        }


        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
        }


        private void Save()
        {
            FillInstance();

            if (InsertOrUpdate())
            {
                MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Dispose();
            }
            else
            {
                MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void FillInstance()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int k = 3; k < dataGridView1.Columns.Count; k++)
                {
                    MsRole r = dataGridView1.Rows[i].Cells[k].Tag as MsRole;

                    if (dataGridView1.Rows[i].Cells[k].Value.ToString() == 利用不可)
                    {
                        r.EnableFlag = 0;
                    }
                    else
                    {
                        r.EnableFlag = 1;
                    }
                }
            }
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsRole_InsertOrUpdate(NBaseCommon.Common.LoginUser, roles);
            }

            return result;
        }
    }
}
