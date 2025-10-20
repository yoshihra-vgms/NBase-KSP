using SqlMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WingData.DAC;
using System.Text.RegularExpressions;

namespace SqlMapperTest
{
    
    
    /// <summary>
    ///SqlMapperTest のテスト クラスです。すべての
    ///SqlMapperTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class SqlMapperTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///現在のテストの実行についての情報および機能を
        ///提供するテスト コンテキストを取得または設定します。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 追加のテスト属性
         //クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            WingDataTest.Common.Initialize();
        }
        //各テストを実行する前にコードを実行するには、TestInitialize を使用
        [TestInitialize()]
        public void MyTestInitialize()
        {
            WingDataTest.Common.DeleteTestData();
            WingDataTest.Common.InsertTestData();
        }
        #endregion


        /// <summary>
        ///GetSql のテスト
        ///</summary>
        [TestMethod()]
        public void GetSqlTest()
        {
            string actual = SqlMapper.SqlMapper.GetSql(typeof(MsUser), "GetRecords");
            Assert.IsNotNull(actual);
            
            actual = SqlMapper.SqlMapper.GetSql(typeof(MsUser), "Exists");
            Assert.IsNotNull(actual);
            string expected = "SELECT      COUNT(*)    FROM      MS_USER    WHERE      MS_USER_ID = :PK";
            Assert.AreEqual(expected, actual.Replace("\n", ""));
        }
    }
}
