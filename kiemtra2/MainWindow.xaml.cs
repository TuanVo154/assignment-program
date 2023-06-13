using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace kiemtra2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBConnect db = new DBConnect();
        SqlDataAdapter sdaGiaoVien = new SqlDataAdapter();
        SqlDataAdapter sdaGiangDay = new SqlDataAdapter();
        SqlDataAdapter sdaMonHoc = new SqlDataAdapter();

        DataSet dsGiaoVien = new DataSet();
        DataSet dsGiangDay = new DataSet();
        DataSet dsMonHoc = new DataSet();

        DataTable dtGiaoVien = new DataTable();
        DataTable dtGiangDay = new DataTable();
        DataTable dtMonHoc = new DataTable();

        List<GiaoVien> listGiaoVien = new List<GiaoVien>();
        List<GiangDay> listGiangDay = new List<GiangDay>();
        List<MonHoc> listMonHoc = new List<MonHoc>();
        public MainWindow()
        {
            InitializeComponent();
            db.connect();

            //Giáo viên

            String selsqlGiaoVien = "SELECT * FROM GiaoVien;";
            SqlCommand selcmdGiaoVien = new SqlCommand(selsqlGiaoVien, db.getConnect);
            sdaGiaoVien.SelectCommand = selcmdGiaoVien;

            String inssqlGiaoVien = "INSERT INTO GiaoVien(MaGV, HoTen) VALUES (@magv,@hoten);";

            SqlParameter magv = new SqlParameter();
            magv.ParameterName = "@magv";
            magv.SqlDbType = SqlDbType.VarChar;
            magv.SourceColumn = "MaGV";

            SqlParameter hoten = new SqlParameter();
            hoten.ParameterName = "@hoten";
            hoten.SqlDbType = SqlDbType.NVarChar;
            hoten.SourceColumn = "HoTen";

            SqlCommand inscmdGiaoVien = new SqlCommand(inssqlGiaoVien, db.getConnect);
            inscmdGiaoVien.Parameters.Add(magv);
            inscmdGiaoVien.Parameters.Add(hoten);
            sdaGiaoVien.InsertCommand = inscmdGiaoVien;

            String delSqlGiaoVien = "DELETE FROM GiaoVien WHERE MaGV=@mgv;";
            SqlParameter mgv = new SqlParameter();
            mgv.ParameterName = "@mgv";
            mgv.SqlDbType = SqlDbType.NVarChar;
            mgv.SourceColumn = "MaGV";

            SqlCommand delcmdGiaoVien = new SqlCommand(delSqlGiaoVien, db.getConnect);
            sdaGiaoVien.DeleteCommand = delcmdGiaoVien;
            delcmdGiaoVien.Parameters.Add(mgv);

            sdaGiaoVien.Fill(dsGiaoVien);
            dtGiaoVien = dsGiaoVien.Tables[0];
            foreach (DataRow dr in dtGiaoVien.Rows)
            {
                GiaoVien gv = new GiaoVien();
                gv.MaGiaoVien = dr["MaGV"].ToString();
                gv.HoTen = dr["HoTen"].ToString();
                listGiaoVien.Add(gv);
            }
            lstGV.ItemsSource = listGiaoVien;

            // Môn học 

            String selsqlMonHoc = "SELECT * FROM MonHoc;";
            SqlCommand selcmdMonHoc = new SqlCommand(selsqlMonHoc, db.getConnect);
            sdaMonHoc.SelectCommand = selcmdMonHoc;

            String inssqlMonHoc = "INSERT INTO MonHoc(MaMH, TenMH) VALUES (@mamh, @tenmh);";

            SqlParameter mamh = new SqlParameter();
            mamh.ParameterName = "@mamh";
            mamh.SqlDbType = SqlDbType.VarChar;
            mamh.SourceColumn = "MaMH";

            SqlParameter tenmh = new SqlParameter();
            tenmh.ParameterName = "@tenmh";
            tenmh.SqlDbType = SqlDbType.NVarChar;
            tenmh.SourceColumn = "TenMH";

            SqlCommand inscmdMonHoc = new SqlCommand(inssqlMonHoc, db.getConnect);
            inscmdMonHoc.Parameters.Add(mamh);
            inscmdMonHoc.Parameters.Add(tenmh);
            sdaMonHoc.InsertCommand = inscmdMonHoc;

            String delSqlMonHoc = "DELETE FROM MonHoc WHERE MaMH = @mmh;";
            SqlParameter mmh = new SqlParameter();
            mmh.ParameterName = "@mmh";
            mmh.SqlDbType = SqlDbType.NVarChar;
            mmh.SourceColumn = "MaMH";

            SqlCommand delcmdMonHoc = new SqlCommand(delSqlMonHoc, db.getConnect);
            delcmdMonHoc.Parameters.Add(mmh);
            sdaMonHoc.DeleteCommand = delcmdMonHoc;


            sdaMonHoc.Fill(dsMonHoc);
            dtMonHoc = dsMonHoc.Tables[0];
            foreach (DataRow dr in dtMonHoc.Rows)
            {
                MonHoc mh = new MonHoc();
                mh.MaMonHoc = dr["MaMH"].ToString();
                mh.TenMonHoc = dr["TenMH"].ToString();
                listMonHoc.Add(mh);
            }
            lstMonHoc.ItemsSource = listMonHoc;

            // Giảng dạy

            String selsqlGiangDay = "SELECT GiangDay.MaMH, GiangDay.MaGV, MonHoc.TenMH, GiaoVien.HoTen FROM GiangDay, GiaoVien, MonHoc Where GiangDay.MaMH = MonHoc.MaMH and GiangDay.MaGV = GiaoVien.MaGV;";
            SqlCommand selcmdGiangDay = new SqlCommand(selsqlGiangDay, db.getConnect);
            sdaGiangDay.SelectCommand = selcmdGiangDay;

            String inssqlGiangDay = "INSERT INTO GiangDay(MaMH, MaGV) VALUES (@mamhgd, @magvgd);";

            SqlParameter magvgd = new SqlParameter();
            magvgd.ParameterName = "@magvgd";
            magvgd.SqlDbType = SqlDbType.VarChar;
            magvgd.SourceColumn = "MaGV";

            SqlParameter mamhgd = new SqlParameter();
            mamhgd.ParameterName = "@mamhgd";
            mamhgd.SqlDbType = SqlDbType.NVarChar;
            mamhgd.SourceColumn = "MaMH";
            
            SqlCommand inscmdGiangDay = new SqlCommand(inssqlGiangDay, db.getConnect);
            inscmdGiangDay.Parameters.Add(mamhgd);
            inscmdGiangDay.Parameters.Add(magvgd);
            sdaGiangDay.InsertCommand = inscmdGiangDay;

            String delSqlGiangDay = "DELETE FROM GiangDay WHERE MaGV=@mgvgd and MaMH = @mmhgd;";
            SqlParameter mgvgd = new SqlParameter();
            mgvgd.ParameterName = "@mgvgd";
            mgvgd.SqlDbType = SqlDbType.NVarChar;
            mgvgd.SourceColumn = "MaGV";

            SqlParameter mmhgd = new SqlParameter();
            mmhgd.ParameterName = "@mmhgd";
            mmhgd.SqlDbType = SqlDbType.NVarChar;
            mmhgd.SourceColumn = "MaMH";

            SqlCommand delcmdGiangDay = new SqlCommand(delSqlGiangDay, db.getConnect);
            delcmdGiangDay.Parameters.Add(mgvgd);
            delcmdGiangDay.Parameters.Add(mmhgd);
            sdaGiangDay.DeleteCommand = delcmdGiangDay;

            sdaGiangDay.Fill(dsGiangDay);
            dtGiangDay = dsGiangDay.Tables[0];
            foreach (DataRow dr in dtGiangDay.Rows)
            {
                GiangDay gd = new GiangDay();
                gd.MaMonHoc = dr["MaMH"].ToString();
                gd.MaGV = dr["MaGV"].ToString();
                gd.TenMonHoc = dr["TenMH"].ToString();
                gd.TenGiaoVien = dr["HoTen"].ToString();
                listGiangDay.Add(gd);
            }
            lstPhanCong.ItemsSource = listGiangDay;
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            db.close();
            Application.Current.Shutdown(); 
        }

        private void btnThemMonHoc_Click(object sender, RoutedEventArgs e)
        {
            if (!KiemTraTTMon()) return;
            if (KiemTraMaSoMon() == true)
            {
                TBao("Mã số môn này đã có, xin vui lòng nhập mã số khác");
                txtMaMonHoc.SelectAll();
                txtMaMonHoc.Focus();
                return;
            }
            MonHoc mh = new MonHoc();
            mh.MaMonHoc = txtMaMonHoc.Text;
            mh.TenMonHoc = txtTenMonHoc.Text;
            listMonHoc.Add(mh);
            DataRow dr = dtMonHoc.NewRow();
            dr["MaMH"] = mh.MaMonHoc;
            dr["TenMH"] = mh.TenMonHoc;
            dtMonHoc.Rows.Add(dr);
            lstMonHoc.ItemsSource = null;
            lstMonHoc.ItemsSource = listMonHoc;
            sdaMonHoc.Update(dsMonHoc);
        }
        private void btnXoaMH_Click(object sender, RoutedEventArgs e)
        {
            if (lstMonHoc.SelectedIndex != -1)
            {
                MessageBoxResult Tl = MessageBox.Show("Bạn có thật sự muốn xóa bỏ môn học này không?", "Cảnh báo xóa dữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (Tl == MessageBoxResult.Yes)
                {
                    int sel = lstMonHoc.SelectedIndex;
                    dtMonHoc.Rows[sel].Delete();
                    sdaMonHoc.Update(dsMonHoc);
                    listMonHoc.RemoveAt(sel);
                    lstMonHoc.ItemsSource = null;
                    lstMonHoc.ItemsSource = listMonHoc;
                    sdaGiangDay.Update(dsGiangDay);
                }
            }
            else MessageBox.Show("Vui lòng chọn môn học muốn xóa", "Cảnh báo");
         
        }

        private void btnThemGV_Click(object sender, RoutedEventArgs e)
        {
            if (!KiemTraTTGV()) return;
            if (KiemTraMaSoGV() == true)
            {
                TBao("Mã số giáo viên này đã có, xin vui lòng nhập mã số khác");
                txtMaGV.SelectAll();
                txtMaGV.Focus();
                return;
            }
            GiaoVien gv = new GiaoVien();
            gv.MaGiaoVien = txtMaGV.Text;
            gv.HoTen = txtTenGV.Text;
            listGiaoVien.Add(gv);
            DataRow dr = dtGiaoVien.NewRow();
            dr["MaGV"] = gv.MaGiaoVien;
            dr["HoTen"] = gv.HoTen;

            dtGiaoVien.Rows.Add(dr);
            lstGV.ItemsSource = null;
            lstGV.ItemsSource = listGiaoVien;
            sdaGiaoVien.Update(dsGiaoVien);
        }

        private void btnXoaGV_Click(object sender, RoutedEventArgs e)
        {
            if (lstGV.SelectedIndex != -1)
            {
                MessageBoxResult Tl = MessageBox.Show("Bạn có thật sự muốn xóa bỏ giáo viên này không?", "Cảnh báo xóa dữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (Tl == MessageBoxResult.Yes)
                {
                    int sel = lstGV.SelectedIndex;
                    dtGiaoVien.Rows[sel].Delete();
                    sdaGiaoVien.Update(dsGiaoVien);
                    listGiaoVien.RemoveAt(sel);
                    lstGV.ItemsSource = null;
                    lstGV.ItemsSource = listGiaoVien;
                }
            }
            else MessageBox.Show("Vui lòng chọn giáo viên muốn xóa", "Cảnh báo");
        }

        private void btnThemPCGD_Click(object sender, RoutedEventArgs e)
        {
            GiaoVien gv = new   GiaoVien();
            gv = (GiaoVien)lstGV.SelectedItem;

            MonHoc mh = new MonHoc();
            mh = (MonHoc)lstMonHoc.SelectedItem;

            GiangDay gd = new GiangDay();
            gd.MaGV = gv.MaGiaoVien;
            gd.TenGiaoVien = gv.HoTen;
            gd.MaMonHoc = mh.MaMonHoc;
            gd.TenMonHoc = mh.TenMonHoc;
            listGiangDay.Add(gd);
            DataRow dr = dtGiangDay.NewRow();
            dr["MaMH"] = gd.MaMonHoc;
            dr["MaGV"] = gd.MaGV;
            dtGiangDay.Rows.Add(dr);
            lstPhanCong.ItemsSource = null;
            lstPhanCong.ItemsSource = listGiangDay; 
            sdaGiangDay.Update(dsGiangDay);

        }

        private void btnXoaPC_Click(object sender, RoutedEventArgs e)
        {
            if (lstPhanCong.SelectedIndex != -1)
            {
                MessageBoxResult Tl = MessageBox.Show("Bạn có thật sự muốn xóa bỏ phân công giảng dạy này không?", "Cảnh báo xóa dữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (Tl == MessageBoxResult.Yes)
                {
                    int sel = lstPhanCong.SelectedIndex;
                    dtGiangDay.Rows[sel].Delete();
                    sdaGiangDay.Update(dsGiangDay);
                    listGiangDay.RemoveAt(sel);
                    lstPhanCong.ItemsSource = null;
                    lstPhanCong.ItemsSource = listGiangDay;
                }
            }
            else MessageBox.Show("Vui lòng chọn phân công giảng dạy muốn xóa", "Cảnh báo");
         
        }

        private void TBao(string TBao, string TieuDe = "Kiểm tra thông tin")
        {
            MessageBox.Show(TBao);
        }

        private bool KiemTraTTMon()
        {
            if (txtMaMonHoc.Text.Trim() == "")
            {
                TBao("Xin cho biết mã số môn học");
                txtMaMonHoc.SelectAll();
                txtMaMonHoc.Focus();
                return false;
            }

            if (txtMaMonHoc.Text.Length > 5)
            {
                TBao("Mã số môn học không được vượt quá 5 ký tự");
                txtMaMonHoc.SelectAll();
                txtMaMonHoc.Focus();
                return false;
            }

            if (txtTenMonHoc.Text.Trim() == "")
            {
                TBao("Xin cho biết tên môn học");
                txtTenMonHoc.SelectAll();
                txtTenMonHoc.Focus();
                return false;
            }

            return true;
        }

        private bool KiemTraTTGV()
        {
            if (txtMaGV.Text.Trim() == "")
            {
                TBao("Xin cho biết mã số giáo viên");
                txtMaGV.SelectAll();
                txtMaGV.Focus();
                return false;
            }

            if (txtMaGV.Text.Length > 5)
            {
                TBao("Mã số giáo viên không được vượt quá 5 ký tự");
                txtMaGV.SelectAll();
                txtMaGV.Focus();
                return false;
            }

            if (txtTenGV.Text.Trim() == "")
            {
                TBao("Xin cho biết tên giáo viên");
                txtTenMonHoc.SelectAll();
                txtTenMonHoc.Focus();
                return false;
            }

            return true;
        }
        private bool KiemTraMaSoMon()
        {
            for (int i = 0; i < lstMonHoc.Items.Count; i++)
            {
                MonHoc mh = (MonHoc)(lstMonHoc.Items[i]);
                if (string.Compare(mh.MaMonHoc, txtMaMonHoc.Text, true) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool KiemTraMaSoGV()
        {
            for (int i = 0; i < lstGV.Items.Count; i++)
            {
                GiaoVien gv = (GiaoVien)(lstGV.Items[i]);
                if (string.Compare(gv.MaGiaoVien, txtMaGV.Text, true) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void lstGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
