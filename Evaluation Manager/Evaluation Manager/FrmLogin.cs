﻿using Evaluation_Manager.Models;
using Evaluation_Manager.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evaluation_Manager
{
    public partial class FrmLogin : Form
    {
        public static Teacher LoggedTeacher { get; private set; }
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            LoggedTeacher = TeacherRepository.GetTeacher(username);

            if(username == " "|| password == " ")
            {
                MessageBox.Show("Popunite sva polja","Pogreška",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else if(LoggedTeacher != null && LoggedTeacher.CheckPassword(password))
            {
                FrmStudents frmStudents = new FrmStudents();
                Hide();
                frmStudents.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("Korisničko ime ili lozinka nisu ispravni", "Neuspjela prijava", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
