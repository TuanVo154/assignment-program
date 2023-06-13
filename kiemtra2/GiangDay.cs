using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiemtra2
{
    internal class GiangDay
    {
        private String _MaGV;
        private String _TenGV;
        private String _MaMH;
        private String _TenMH;
        public String MaGV
        {
            get { return _MaGV; }
            set { _MaGV = value; }
        }

        public String TenGiaoVien
        {
            get { return _TenGV; }
            set { _TenGV = value; }
        }

        public String MaMonHoc
        {
            get { return _MaMH; }
            set { _MaMH = value; }
        }

        public String TenMonHoc
        {
            get { return _TenMH; }
            set { _TenMH = value; }
        }

    }

}
