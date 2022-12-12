Your [question](https://stackoverflow.com/q/74770247/5438626) is how to suppress the default `DataGridView` navigation and substitute your own. The default navigation occurs in the `OnKeyDown` method, and if you override it in your custom control (let's call it `DataGridViewEx`) you should be able to suppress the default navigation by overriding that method, making sure that you _do not call the base class version_ for `Keys.Enter` and `Keys.Back`.

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

***

[![screenshot][1]][1]


  [1]: https://github.com/IVSoftware/current-cell-focus/blob/master/current-cell-focus/Screenshots/screenshot.png