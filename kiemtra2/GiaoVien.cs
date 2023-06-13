using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kiemtra2
{
    internal class GiaoVien
    {
        private String _MaGV;
        private String _HoTen;
        public String MaGiaoVien
        {
            get { return _MaGV; }
            set { _MaGV = value; }
        }

        public String HoTen
        {
            get { return _HoTen; }
            set { _HoTen = value; }
        }
    }
}
