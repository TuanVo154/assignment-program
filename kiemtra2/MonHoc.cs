using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiemtra2
{
    internal class MonHoc
    {
        private String _MaMH;
        private String _TenMH;
        public String TenMonHoc
        {
            get { return _TenMH; }
            set { _TenMH = value; }
        }

        public String MaMonHoc
        {
            get { return _MaMH; }
            set { _MaMH = value; }
        }
    }
}
