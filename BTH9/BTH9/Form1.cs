using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace BTH9
{
    public partial class frmCustomer : Form
    {
        string cnStr = "";
        SqlConnection cn;
        DataSet ds;
        public frmCustomer()
        {
            InitializeComponent();
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            cnStr = ConfigurationManager.ConnectionStrings["cnStr"].ConnectionString;
            cn = new SqlConnection(cnStr);
            dgvKhachHang.DataSource = GetCustomerDatase().Tables[0];
        }

        public DataSet GetCustomerDatase()
        {
            try
            {
                string sql = "SELECT * FROM KhachHang";
                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                /*DataSet*/ ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            finally
            {
                cn.Close(); 
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            //////////INSERT
            string ins = "INSERT INTO KhachHang(MaKH, TenKH, DiaChi, DienThoai, Fax) VALUES(@id, @name, @address, @phone, @fax)";
            //string ins = "INSERT INTO KhachHang(MaKH) VALUES(@id)";

            SqlDataAdapter da = new SqlDataAdapter(); //=>UPDATE
            SqlCommand cmd = new SqlCommand(ins, cn);
            cmd.Parameters.Add("@id",SqlDbType.NVarChar,4,"MaKH");
            cmd.Parameters.Add("@name",SqlDbType.NVarChar,30,"TenKH");
            cmd.Parameters.Add("@address",SqlDbType.NVarChar,30,"DiaChi");
            cmd.Parameters.Add("@phone",SqlDbType.NVarChar,7,"DienThoai");
            cmd.Parameters.Add("@fax",SqlDbType.NVarChar,12,"Fax");

            da.InsertCommand = cmd;

            //////////DELETE
            string del = "DELETE FROM KhachHang WHERE MaKH = @id";
            cmd = new SqlCommand(del, cn);
            cmd.Parameters.Add("@id", SqlDbType.NVarChar, 4, "MaKH");
            da.DeleteCommand = cmd;

            //////////

            da.Update(ds);

            //Refresh
            //dgvKhachHang.DataSource = GetCustomerDatase().Tables[0];
        }
    }
}
