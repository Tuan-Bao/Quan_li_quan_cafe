using Quan_li_quan_cafe.Controllers;
using Quan_li_quan_cafe.DataAccess.Repositories;
using Quan_li_quan_cafe.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quan_li_quan_cafe.Models;


namespace Quan_li_quan_cafe.Views
{
    public partial class LoginForm : Form
    {
        private UserController _userController;
        public LoginForm()
        {
            InitializeComponent();
            _userController = new UserController(new UserRepository(new CoffeeShopDbContext()));
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            try
            {
                // Xác thực tài khoản
                var user = await _userController.Authenticate(username, password);

                if (user != null)
                {
                    if (user.Role == "Manager")
                    {
                        MessageBox.Show("Chào mừng quản lí đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Bạn có thể mở form quản lý ở đây nếu muốn
                        Manager f = new Manager();
                        f.Show();
                        this.Hide();
                        f.QuanLiDangXuat += F_QuanLiDangXuat;
                    }
                    else if (user.Role == "Employee")
                    {
                        MessageBox.Show("Chào mừng nhân viên đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Bạn có thể mở form nhân viên ở đây nếu muốn
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đăng nhập thất bại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void F_QuanLiDangXuat(object? sender, EventArgs e)
        {
            if (sender is Manager manager)
            {
                manager.isExit = false;
                manager.Close();
            }
            this.Show();
            textBox1.Text = "";
            textBox2.Text = "";
            checkBox1.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }
    }
}
