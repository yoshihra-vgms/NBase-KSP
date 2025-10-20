using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseData.DS
{
    public class MsRoleTableCache
    {
        private static readonly MsRoleTableCache INSTANCE = new MsRoleTableCache();

        public IMsRoleDacProxy DacProxy { get; set; }

        private List<MsRole> msRoleList;
        private MsUserBumon userBumon;

        private Dictionary<RoleKey, MsRole> msRoleDic;


        private MsRoleTableCache()
        {
        }


        public static MsRoleTableCache instance()
        {
            return INSTANCE;
        }


        private void LoadRecords(MsUser loginUser)
        {
            if (msRoleList == null)
            {
                msRoleList = DacProxy.MsRole_GetRecords(loginUser);
            }

            if (userBumon == null)
            {
                userBumon = DacProxy.MsUserBumon_GetRecord(loginUser);
            }

            msRoleDic = new Dictionary<RoleKey, MsRole>();

            foreach (MsRole role in msRoleList)
            {
                RoleKey key = new RoleKey(role.MsBumonID, role.AdminFlag, role.Name1, role.Name2, role.Name3);
                msRoleDic[key] = role;
            }
        }


        public bool Enabled(MsUser loginUser, string name1, string name2, string name3)
        {
            LoadRecords(loginUser);

            if (name1 == null)
            {
                name1 = string.Empty;
            }

            if (name2 == null)
            {
                name2 = string.Empty;
            }

            if (name3 == null)
            {
                name3 = string.Empty;
            }

            RoleKey key = new RoleKey(userBumon.MsBumonID, loginUser.AdminFlag, name1, name2, name3);

            if (!msRoleDic.ContainsKey(key))
            {
                return false;
            }

            return msRoleDic[key].EnableFlag == 1;
        }

        public bool Is海務部(MsUser loginUser)
        {
            LoadRecords(loginUser);

            return (userBumon.MsBumonID == MsBumon.ToId(MsBumon.MsBumonIdEnum.海務部));
        }
        public bool Is工務部(MsUser loginUser)
        {
            LoadRecords(loginUser);

            return (userBumon.MsBumonID == MsBumon.ToId(MsBumon.MsBumonIdEnum.工務部));
        }
        public bool Is船員部(MsUser loginUser)
        {
            LoadRecords(loginUser);

            return (userBumon.MsBumonID == MsBumon.ToId(MsBumon.MsBumonIdEnum.船員部));
        }




        private class RoleKey
        {
            public string msBumonId;
            public int adminFlag;
            public string name1, name2, name3;


            public RoleKey(string msBumonId, int adminFlag, string name1, string name2, string name3)
            {
                this.msBumonId = msBumonId;
                this.adminFlag = adminFlag;
                this.name1 = name1;
                this.name2 = name2;
                this.name3 = name3;
            }


            public override bool Equals(object obj)
            {
                RoleKey other = obj as RoleKey;

                return msBumonId == other.msBumonId && adminFlag == other.adminFlag && name1 == other.name1 && name2 == other.name2 && name3 == other.name3;
            }


            public override int GetHashCode()
            {
                return msBumonId.GetHashCode() ^ adminFlag ^ name1.GetHashCode() ^ name2.GetHashCode() ^ name3.GetHashCode();
            }
        }
    }
}
