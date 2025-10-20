using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ORMapping;

namespace SyncClient
{
    public interface IClientDb
    {
        /// <summary>
        /// データベース作成
        /// </summary>
        void CreateDb();

        /// <summary>
        /// データベース変更
        /// </summary>
        void AlterTables();

        DataSet BuildUnsendDataSet();

        decimal[] GetMaxDataNo();

        void SetMaxDataNo(decimal maxDataNoOfVesselIdZero, decimal maxDataNo, decimal postionOfZero, decimal postion, DBConnect dbConnect);

        DataSet BuildUnsendDocumentFileDataSet();
        int[] GetDocumentParameter();

        DataSet BuildUnsendAttachFileDataSet();
    }
}
