using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VolodCodeFirstSageBookEF.Database;

namespace VolodCodeFirstSageBookEF.OtherForms
{
    public partial class BookForm : Form
    {
        private enum Operation
        {
            Insert = 1,
            Update,
            Delete
        };

        Operation currentOperation;

        public BookForm()
        {
            InitializeComponent();
            InitializeDataGridViewBooks();
            InitializeComboBoxOperation();

            currentOperation = Operation.Insert;
            comboBoxOperation.SelectedItem = "Insert";
        }

        private void InitializeDataGridViewBooks()
        {
            using (MyModel context = new MyModel())
            {
                bindingSourceBooks.DataSource =
                    context.Books.Select(b => new
                    {
                        Id = b.Id,
                        Name = b.Name,
                        Price = b.Price
                    }).ToList();
                dataGridViewBooks.DataSource = bindingSourceBooks;
            }
        }

        private void InitializeComboBoxOperation()
        {
            string[] operations = { "Insert", "Update", "Delete" };
            foreach (string operation in operations)
                comboBoxOperation.Items.Add(operation);
        }

        private void comboBoxOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((sender as ComboBox).Text)
            {
                case "Insert": currentOperation = Operation.Insert; break;
                case "Update": currentOperation = Operation.Update; break;
                case "Delete": currentOperation = Operation.Delete; break;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            switch (currentOperation)
            {
                case Operation.Insert: Insert(); break;
                case Operation.Update: Update(); break;
                case Operation.Delete: Delete(); break;
            }
        }

        private void Insert()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(textBoxName.Text) &&
                    !string.IsNullOrWhiteSpace(textBoxPrice.Text))
                {
                    using (MyModel context = new MyModel())
                    {
                        Book book = new Book()
                        {
                            Name = textBoxName.Text,
                            Price = Convert.ToInt32(textBoxPrice.Text)
                        };

                        context.Books.Add(book);
                        context.SaveChanges();
                    }
                    textBoxName.Clear();
                    textBoxPrice.Clear();
                    InitializeDataGridViewBooks();
                }
            }
            catch { }
        }

        private new void Update()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(textBoxName.Text) &&
                  !string.IsNullOrWhiteSpace(textBoxPrice.Text))
                {
                    int id = Convert.ToInt32(dataGridViewBooks.SelectedRows[0].Cells["Id"].Value);
                    using (MyModel context = new MyModel())
                    {
                        Book book = context.Books.FirstOrDefault(b => b.Id == id);
                        book.Name = textBoxName.Text;
                        book.Price = Convert.ToInt32(textBoxPrice.Text);

                        context.SaveChanges();
                    }
                    textBoxName.Clear();
                    textBoxPrice.Clear();
                    InitializeDataGridViewBooks();
                }
            }
            catch { }
        }

        private void Delete()
        {
            try
            {
                int id = Convert.ToInt32(dataGridViewBooks.SelectedRows[0].Cells["Id"].Value);
                using (MyModel context = new MyModel())
                {
                    Book book = context.Books.FirstOrDefault(b => b.Id == id);

                    context.Books.Remove(book);
                    context.SaveChanges();
                }
                InitializeDataGridViewBooks();
            }
            catch { }
        }
    }
}