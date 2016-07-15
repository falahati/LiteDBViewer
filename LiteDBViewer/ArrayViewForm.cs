using System.Collections.Generic;
using System.Windows.Forms;
using LiteDB;

namespace LiteDBViewer
{
    internal partial class ArrayViewForm : Form
    {
        private readonly List<ContextMenu> _contextMenus = new List<ContextMenu>();

        public ArrayViewForm()
        {
            InitializeComponent();
        }

        public ArrayViewForm(BsonValue cell) : this()
        {
            foreach (var item in cell.AsArray)
            {
                string itemString;
                ContextMenu contextMenu = null;
                switch (item.Type)
                {
                    case BsonType.Null:
                        itemString = "[NULL]";
                        break;
                    case BsonType.Document:
                        itemString = "[OBJECT]";
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View Object",
                            (o, args) => new DocumentViewForm(item).ShowDialog(this)));
                        break;
                    case BsonType.Array:
                        itemString = $"[ARRAY({cell.AsArray.Count})]";
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View Array",
                            (o, args) => new ArrayViewForm(item).ShowDialog(this)));
                        break;
                    case BsonType.Binary:
                        itemString = $"[BINARY({cell.AsBinary.Length})]";
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View Binary",
                            (o, args) => new BinaryViewForm(item).ShowDialog(this)));
                        break;
                    case BsonType.String:
                        itemString = cell.AsString;
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View String",
                            (o, args) => new StringViewForm(item).ShowDialog(this)));
                        break;
                    default:
                        itemString = cell.ToString();
                        break;
                }
                listBox.Items.Add(itemString);
                _contextMenus.Add(contextMenu);
            }
        }

        private void listBox_MouseUp(object sender, MouseEventArgs e)
        {
            listBox.SelectedIndex = listBox.IndexFromPoint(e.X, e.Y);
            if (e.Button == MouseButtons.Right && listBox.SelectedIndex >= 0 &&
                listBox.SelectedIndex < _contextMenus.Count && _contextMenus[listBox.SelectedIndex] != null)
            {
                _contextMenus[listBox.SelectedIndex].Show(listBox, e.Location);
            }
        }
    }
}