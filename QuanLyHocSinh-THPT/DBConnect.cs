using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyHocSinh_THPT
{
    internal class DBConnect
    {
        private static string connectionString = "Data Source=DESKTOP-JH4OR7J\\SQLEXPRESS;Initial Catalog=QL_HOCSINH_THPT;Integrated Security=True;";

        private SqlConnection conn;

        //Mo ket noi
        public void OpenConnection()
        {
            if (conn == null)
                conn = new SqlConnection(connectionString);

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            //MessageBox.Show("Kết nối tới DB: " );
        }

        // Dong ket noi
        public void CloseConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
                conn.Close();
        }

        //  Thuc thi lenh select
        public DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn dữ liệu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }

        // Execute sql
        public int ExecuteNonQuery(string query)
        {
            int rowsAffected = 0;
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thực thi lệnh SQL: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return rowsAffected;
        }

        // Thuc thi lenh scalar
        public object ExecuteScalar(string query)
        {
            object result = null;
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                result = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thực thi Scalar: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return result;
        }
    }
}
