using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NBaseData.DS
{
    [Serializable]
    public class BlobUnkouhiList
    {
        public List<BlobUnkouhi> List { get; private set; }

        public BlobUnkouhiList()
        {
            List = new List<BlobUnkouhi>();
            
            for (int i = 0; i < 12; i++)
            {
                List.Add(new BlobUnkouhi());
            }
        }


        public static byte[] ToBytes(BlobUnkouhiList obj)
        {
            using (MemoryStream mem = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(mem, obj);

                return mem.ToArray();
            }
        }


        public static BlobUnkouhiList ToObject(byte[] b)
        {
            using (MemoryStream mem = new MemoryStream(b, false))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (BlobUnkouhiList)bf.Deserialize(mem);
            }
        }


        public void Copy(int copySrcIndex)
        {
            BlobUnkouhi srcUnkouhi = List[copySrcIndex];
            
            for (int i = copySrcIndex + 1; i < List.Count; i++)
            {
                List[i] = srcUnkouhi.Clone();
            }
        }


        public decimal[] 貨物運賃_計()
        {
            decimal[] total = new decimal[2];
            
            foreach (BlobUnkouhi u in List)
            {
                total[0] += u.円データ.貨物運賃_計();
                total[1] += u.ドルデータ.貨物運賃_計();
            }

            return total;
        }


        public decimal[] 燃料費_計()
        {
            decimal[] total = new decimal[2];

            foreach (BlobUnkouhi u in List)
            {
                total[0] += u.円データ.燃料費_計();
                total[1] += u.ドルデータ.燃料費_計();
            }

            return total;
        }


        public decimal[] 港費_計()
        {
            decimal[] total = new decimal[2];

            foreach (BlobUnkouhi u in List)
            {
                total[0] += u.円データ.港費_計();
                total[1] += u.ドルデータ.港費_計();
            }

            return total;
        }


        public decimal[] 貨物費_計()
        {
            decimal[] total = new decimal[2];

            foreach (BlobUnkouhi u in List)
            {
                total[0] += u.円データ.貨物費_計();
                total[1] += u.ドルデータ.貨物費_計();
            }

            return total;
        }


        public decimal[] その他運航費_計()
        {
            decimal[] total = new decimal[2];

            foreach (BlobUnkouhi u in List)
            {
                total[0] += u.円データ.その他運航費_計();
                total[1] += u.ドルデータ.その他運航費_計();
            }

            return total;
        }
    }
}
