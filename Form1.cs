using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolodCodeFirstSageBookEF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void sageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OtherForms.SageForm sageForm = new OtherForms.SageForm();
            sageForm.ShowDialog();
        }

        private void bookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OtherForms.BookForm bookForm = new OtherForms.BookForm();
            bookForm.ShowDialog();
        }

        private void sageBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OtherForms.SageBookForm sageBookForm = new OtherForms.SageBookForm();
            sageBookForm.ShowDialog();
        }
    }
}