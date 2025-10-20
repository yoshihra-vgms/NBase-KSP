using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORMapping
{
    public class ParameterConnection : IEnumerable<DBParameter>
    {
        Dictionary<string, DBParameter> Parameters = new Dictionary<string, DBParameter>();

        public void Add(DBParameter Param)
        {
            Parameters.Add(Param.ParameterName, Param);
        }

        public DBParameter this[string ParameterName]
        {
            get
            {
                return Parameters[ParameterName];
            }
        }

        public void AddInnerParams(string prmPrefix, object[] paramArray)
        {
            AddInnerParams(prmPrefix, paramArray, 0);
        }
        private void AddInnerParams(string prmPrefix, object[] paramArray, int index)
        {
            for (int i = 0; i < paramArray.Length; i++)
            {
                Add(new DBParameter(prmPrefix + (index + i).ToString(), paramArray[i]));
            }
        }

        public void AddInnerParams<Type>(string prmPrefix, List<Type> paramArray)
        {
            AddInnerParams(prmPrefix, paramArray, 0);
        }
        private void AddInnerParams<Type>(string prmPrefix, List<Type> paramArray, int index)
        {
            for (int i = 0; i < paramArray.Count; i++)
            {
                Add(new DBParameter(prmPrefix + (index + i).ToString(), paramArray[i]));
            }
        }



        #region IEnumerable<DBParameter> メンバ

        public IEnumerator<DBParameter> GetEnumerator()
        {
            return Parameters.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable メンバ

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        #endregion
    }

    public class DBParameter
    {
        public string ParameterName;
        public object Param;

        public DBParameter(string ParameterName, object Param)
        {
            this.ParameterName = ParameterName;
            this.Param = Param;
        }
    }
}
