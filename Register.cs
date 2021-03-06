using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
namespace DoAn
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           



            if (Tuser.Text == "user")
            {
                label7.Visible = true;
                label6.Visible = false;
            }
            else
            {
                if (pass.Text != rpass.Text)
                {
                    label6.Visible=true;
                    label7.Visible=false;
                } else
                {
                    string username = Tuser.Text;
                    string password = pass.Text;
                    string password2 = pass2.Text;

                    // khởi tạo connect
                    SQLiteConnection _con = new SQLiteConnection();
                    string _strConnect = "Data Source=MyDatabase.db;Version=3;";
                    _con.ConnectionString = _strConnect;
                    _con.Open(); // mở kết nối
                    // Tìm xem đã có user trùng tên đăng nhập chưa?
                    string FinduserSql = "SELECT * FROM users WHERE username= ?";
                    // Thực thi câu lệnh
                    SQLiteCommand cmd1 = new SQLiteCommand(FinduserSql, _con);
                    cmd1.Parameters.Add("$username", DbType.String).Value = username;
                    SQLiteDataReader rdr = cmd1.ExecuteReader();

                    if (rdr.HasRows) // có user tồn tại
                    {
                        label7.Visible = true;
                        label6.Visible = false;
                    }
                    else
                    {
                        // câu lệnh insert user
                        string InsertSql = @"INSERT INTO users (id,username, password, pass2, fullname, displayname, email, address, phone, avatar) values ($id,$username, $password, $password2, $fullname, $displayname, $email, $address, $phone, $avatar)";
                        // Thực thi câu lệnh
                        SQLiteCommand cmd = new SQLiteCommand(InsertSql, _con);
                        cmd.Parameters.Add("$id", DbType.String).Value = Guid.NewGuid().ToString();
                        cmd.Parameters.Add("$username", DbType.String).Value = username;
                        cmd.Parameters.Add("$password", DbType.String).Value = password;
                        cmd.Parameters.Add("$password2", DbType.String).Value = password2;
                        cmd.Parameters.Add("$fullname", DbType.String).Value = "null";
                        cmd.Parameters.Add("$displayname", DbType.String).Value = "null";
                        cmd.Parameters.Add("$email", DbType.String).Value = "null";
                        cmd.Parameters.Add("$address", DbType.String).Value = "null";
                        cmd.Parameters.Add("$phone", DbType.String).Value = "null";
                        cmd.Parameters.Add("$avatar", DbType.String).Value = "avatar.jpg";

                        cmd.ExecuteNonQuery();
                        _con.Close();
                        MessageBox.Show("Sign Up Successed");
                        Application.Restart();
                        Environment.Exit(0);
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }
    }
}
