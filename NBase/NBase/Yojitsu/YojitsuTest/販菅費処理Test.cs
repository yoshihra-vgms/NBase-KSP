using Yojitsu.util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WingData.DAC;
using System;

namespace YojitsuTest
{
    
    
    /// <summary>
    ///販菅費処理Test のテスト クラスです。すべての
    ///販菅費処理Test 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class 販菅費処理Test
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
        // 
        //テストを作成するときに、次の追加属性を使用することができます:
        //
        //クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //各テストを実行する前にコードを実行するには、TestInitialize を使用
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //各テストを実行した後にコードを実行するには、TestCleanup を使用
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///稼働率計算 のテスト
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Yojitsu.dll")]
        public void 稼働率計算Test()
        {
            // 例１）4月15日～9月15日
            BgKadouVessel kv = new BgKadouVessel();
            kv.KadouStartDate = new DateTime(2010, 4, 15);
            kv.KadouEndDate = new DateTime(2010, 9, 15);

            Decimal actual = 販菅費処理_Accessor.稼働率計算(kv);
            Assert.AreEqual(0.4166m, Math.Floor(actual * 10000) / 10000);

            // 例２）7月1日～9月30日
            kv = new BgKadouVessel();
            kv.KadouStartDate = new DateTime(2010, 7, 1);
            kv.KadouEndDate = new DateTime(2010, 9, 30);

            actual = 販菅費処理_Accessor.稼働率計算(kv);
            Assert.AreEqual(0.25m, actual);

            // 4月1日～3月31日
            kv = new BgKadouVessel();
            kv.KadouStartDate = new DateTime(2010, 4, 1);
            kv.KadouEndDate = new DateTime(2011, 3, 31);

            actual = 販菅費処理_Accessor.稼働率計算(kv);
            Assert.AreEqual(1.0m, actual);

            // 4月15日～5月15日
            kv = new BgKadouVessel();
            kv.KadouStartDate = new DateTime(2010, 4, 15);
            kv.KadouEndDate = new DateTime(2010, 5, 15);

            actual = 販菅費処理_Accessor.稼働率計算(kv);
            Assert.AreEqual(0.0833m, Math.Floor(actual * 10000) / 10000);

            // 4月1日～4月30日
            kv = new BgKadouVessel();
            kv.KadouStartDate = new DateTime(2010, 4, 1);
            kv.KadouEndDate = new DateTime(2010, 4, 30);

            actual = 販菅費処理_Accessor.稼働率計算(kv);
            Assert.AreEqual(0.0833m, Math.Floor(actual * 10000) / 10000);

            // 4月30日～5月1日
            kv = new BgKadouVessel();
            kv.KadouStartDate = new DateTime(2010, 4, 30);
            kv.KadouEndDate = new DateTime(2010, 5, 1);

            actual = 販菅費処理_Accessor.稼働率計算(kv);
            Assert.AreEqual(0.0m, actual);
        }
    }
}
