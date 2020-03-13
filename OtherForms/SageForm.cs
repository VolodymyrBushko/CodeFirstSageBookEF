using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VolodCodeFirstSageBookEF.Database;

namespace VolodCodeFirstSageBookEF.OtherForms
{
    public partial class SageForm : Form
    {
        private enum Operation
        {
            Insert = 1,
            Update,
            Delete
        };

        Operation currentOperation;

        public SageForm()
        {
            InitializeComponent();
            InitializeDataGridViewSages();
            InitializeComboBoxOperation();

            currentOperation = Operation.Insert;
            comboBoxOperation.SelectedItem = "Insert";
        }

        private void InitializeDataGridViewSages()
        {
            using (MyModel context = new MyModel())
            {
                bindingSourceSages.DataSource =
                    context.Sages.Select(s => new
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Age = s.Age
                    }).ToList();
                dataGridViewSages.DataSource = bindingSourceSages;
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
                    !string.IsNullOrWhiteSpace(textBoxAge.Text))
                {
                    using (MyModel context = new MyModel())
                    {
                        Sage sage = new Sage()
                        {
                            Name = textBoxName.Text,
                            Age = Convert.ToInt32(textBoxAge.Text)
                        };

                        context.Sages.Add(sage);
                        context.SaveChanges();
                    }
                    textBoxName.Clear();
                    textBoxAge.Clear();
                    InitializeDataGridViewSages();
                }
            }
            catch { }
        }

        private new void Update()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(textBoxName.Text) &&
                  !string.IsNullOrWhiteSpace(textBoxAge.Text))
                {
                    int id = Convert.ToInt32(dataGridViewSages.SelectedRows[0].Cells["Id"].Value);
                    using (MyModel context = new MyModel())
                    {
                        Sage sage = context.Sages.FirstOrDefault(s => s.Id == id);
                        sage.Name = textBoxName.Text;
                        sage.Age = Convert.ToInt32(textBoxAge.Text);

                        context.SaveChanges();
                    }
                    textBoxName.Clear();
                    textBoxAge.Clear();
                    InitializeDataGridViewSages();
                }
            }
            catch { }
        }

        private void Delete()
        {
            try
            {
                int id = Convert.ToInt32(dataGridViewSages.SelectedRows[0].Cells["Id"].Value);
                using (MyModel context = new MyModel())
                {
                    Sage sage = context.Sages.FirstOrDefault(s => s.Id == id);

                    context.Sages.Remove(sage);
                    context.SaveChanges();
                }
                InitializeDataGridViewSages();
            }
            catch { }
        }
    }
}