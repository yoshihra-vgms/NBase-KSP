using Yojitsu;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WingData.DAC;
using Yojitsu.DA;
using WingCommon;
namespace YojitsuTest
{
    
    
    /// <summary>
    ///HimokuTreeReaderTest のテスト クラスです。すべての
    ///HimokuTreeReaderTest 単体テストをここに含めます
    ///</summary>
    [TestClass()]
    public class HimokuTreeReaderTest
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            WingDataTest.Common.Initialize();
            DbAccessorFactory.InitFactory("direct");
        }
        //
        //クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //各テストを実行する前にコードを実行するには、TestInitialize を使用
        [TestInitialize()]
        public void MyTestInitialize()
        {
            WingDataTest.Common.DeleteTestData();
            WingDataTest.Common.InsertTestData();
        }
        
        //各テストを実行した後にコードを実行するには、TestCleanup を使用
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Read のテスト
        ///</summary>
        [DeploymentItem("..\\..\\..\\config\\HimokuTree.xml", "config"), TestMethod()]
        public void ReadTest()
        {
            WingCommon.Common.LoginUser = new MsUser();
            
            HimokuTreeNode rootNode = HimokuTreeReader.GetHimokuTree();
            
            Assert.IsNotNull(rootNode);
        }
    }
}
