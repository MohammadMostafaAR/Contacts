using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContacts
{
    public partial class frmAddOrEdit : Form
    {
        IContactsRepository repository;
        public int contactId = 0;
        public frmAddOrEdit()
        {
            InitializeComponent();
            repository = new ContactsRipository();
        }

        private void FrmAddOrEdit_Load(object sender, EventArgs e)
        {
            if(contactId == 0)
            {
                this.Text = "افزون شخص جدید";
            }
            else
            {
                this.Text = "ویرایش شخص";
                DataTable dt = repository.SelectRow(contactId);
                txtName.Text = dt.Rows[0][1].ToString();
                txtFamily.Text = dt.Rows[0][2].ToString();
                txtAge.Text = dt.Rows[0][3].ToString();
                txtMobile.Text = dt.Rows[0][4].ToString();
                txtEmail.Text = dt.Rows[0][5].ToString();
                txtAddress.Text = dt.Rows[0][6].ToString();
                btnSubmit.Text = "ویرایش";
            }
        }

        bool ValidateInput()
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("لطفا نام فرد را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtFamily.Text == "")
            {
                MessageBox.Show("لطفا نام خانوادگی فرد را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtAge.Value == 0)
            {
                MessageBox.Show("لطفا سن فرد را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtMobile.Text == "")
            {
                MessageBox.Show("لطفا شماره تلفن همراه فرد را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("لطفا رایانامه فرد را وارد کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                bool isSuccess;
                if (contactId == 0)
                {
                    isSuccess = repository.Insert(txtName.Text, txtFamily.Text, txtMobile.Text, txtEmail.Text, (int)txtAge.Value, txtAddress.Text);

                }
                else
                {
                    isSuccess = repository.Update(contactId, txtName.Text, txtFamily.Text, txtMobile.Text, txtEmail.Text, (int)txtAge.Value, txtAddress.Text);

                }
                if (isSuccess == true)
                {
                    MessageBox.Show("عملیات با موفقیت انجام شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("عملیات با شکست مواجه شد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
