using System;
using System.Linq;
using System.Windows.Forms;
using VolodCodeFirstSageBookEF.Database;

namespace VolodCodeFirstSageBookEF.OtherForms
{
    public partial class SageBookForm : Form
    {
        private enum Operation
        {
            Insert = 1,
            Delete
        };

        Operation currentOperation;

        public SageBookForm()
        {
            InitializeComponent();

            dataGridViewSageBook.Columns.Add("Sage", "Sage");
            dataGridViewSageBook.Columns.Add("Book", "Book");

            InitializeDataGridViewSageBook();
            InitializeComboBoxSages();
            InitializeComboBoxBooks();
            InitializeComboBoxOperation();

            currentOperation = Operation.Insert;
            comboBoxOperation.SelectedItem = "Insert";
        }

        private void InitializeDataGridViewSageBook()
        {
            using (MyModel context = new MyModel())
            {
                dataGridViewSageBook.Rows.Clear();

                foreach (Sage sage in context.Sages)
                {
                    foreach (Book book in sage.Books)
                    {
                        int index = dataGridViewSageBook.Rows.Add();

                        dataGridViewSageBook.Rows[index].Cells["Sage"].Value = sage.Name;
                        dataGridViewSageBook.Rows[index].Cells["Sage"].Tag = sage.Id;

                        dataGridViewSageBook.Rows[index].Cells["Book"].Value = book.Name;
                        dataGridViewSageBook.Rows[index].Cells["Book"].Tag = book.Id;
                    }
                }
            }
        }

        private void InitializeComboBoxSages()
        {
            using (MyModel context = new MyModel())
            {
                comboBoxSages.Items.AddRange(context.Sages.ToArray());
            }
        }

        private void InitializeComboBoxBooks()
        {
            using (MyModel context = new MyModel())
            {
                comboBoxBooks.Items.AddRange(context.Books.ToArray());
            }
        }

        private void InitializeComboBoxOperation()
        {
            string[] operations = { "Insert", "Delete" };
            foreach (string operation in operations)
                comboBoxOperation.Items.Add(operation);
        }

        private void comboBoxOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((sender as ComboBox).Text)
            {
                case "Insert": currentOperation = Operation.Insert; break;
                case "Delete": currentOperation = Operation.Delete; break;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            switch (currentOperation)
            {
                case Operation.Insert: Insert(); break;
                case Operation.Delete: Delete(); break;
            }
        }

        private void Insert()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(comboBoxSages.Text) &&
                    !string.IsNullOrWhiteSpace(comboBoxBooks.Text))
                {
                    using (MyModel context = new MyModel())
                    {

                        int idSage = (comboBoxSages.SelectedItem as Sage).Id;
                        int idBook = (comboBoxBooks.SelectedItem as Book).Id;

                        Sage sage = context.Sages.FirstOrDefault(s => s.Id == idSage);
                        Book book = context.Books.FirstOrDefault(b => b.Id == idBook);

                        sage.Books.Add(book);
                        context.SaveChanges();
                    }
                    InitializeDataGridViewSageBook();
                }
            }
            catch { }
        }

        public void Delete()
        {
            try
            {
                int idSage = Convert.ToInt32(dataGridViewSageBook.SelectedRows[0].Cells["Sage"].Tag);
                int idBook = Convert.ToInt32(dataGridViewSageBook.SelectedRows[0].Cells["Book"].Tag);

                using (MyModel context = new MyModel())
                {
                    Sage sage = context.Sages.First(s => s.Id == idSage);
                    Book book = context.Books.First(b => b.Id == idBook);

                    sage.Books.Remove(book);
                    context.SaveChanges();
                }
                InitializeDataGridViewSageBook();
            }
            catch { }
        }
    }
}