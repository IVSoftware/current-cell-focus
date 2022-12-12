using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace current_cell_focus
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();
    }

    // Custom control
    class DataGridViewEx : DataGridView
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AllowUserToAddRows = false;
            DataSource = Records;

            // Format columns
            Records.Add(new Record());
            foreach (DataGridViewColumn col in Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            Records.Clear();

            // Load test data
            Records.Add(new Record
            {
                Column1 = "1",
                Column2 = "2",
                Column3 = "3"
            });
            Records.Add(new Record
            {
                Column1 = "4",
                Column2 = "5",
                Column3 = "6"
            });
            Records.Add(new Record
            {
                Column1 = "7",
                Column2 = "8",
                Column3 = "9"
            });
        }
        BindingList<Record> Records = new BindingList<Record>();
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (CurrentCell != null)
            {
                var row = CurrentCell.RowIndex;
                var col = CurrentCell.ColumnIndex;
                switch (e.KeyData)
                {
                    case Keys.Return:
                        col++;
                        // [ToDo] Allow for non-visible rows (will fail).
                        if(col.Equals(Columns.Count))
                        {
                            col = 0;
                            row++;
                            // Either insert after the current row as
                            // shown here, or add to the end of the list.
                            Records.Insert(row, new Record());
                        }
                        CurrentCell = this[col, row];
                        break;
                    case Keys.Back:
                        col--;
                        if(col >= 0)
                        {
                            CurrentCell = this[col, row];
                        }
                        break;
                    default:
                        base.OnKeyDown(e);
                        break;
                }
            }
        }
    }

    class Record
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
    }
}
