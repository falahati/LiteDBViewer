using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LiteDB;

namespace LiteDBViewer
{
    internal partial class DocumentViewForm : Form
    {
        private readonly List<ContextMenu> _contextMenus = new List<ContextMenu>();

        public DocumentViewForm()
        {
            InitializeComponent();
        }

        public DocumentViewForm(BsonValue cell) : this()
        {
            var maxLength = cell.AsDocument.RawValue.Keys.Max(key => key.Length);
            foreach (var item in cell.AsDocument.RawValue.OrderBy(pair => pair.Key))
            {
                string itemString;
                ContextMenu contextMenu = null;
                switch (item.Value.Type)
                {
                    case BsonType.Null:
                        itemString = "[NULL]";
                        break;
                    case BsonType.Document:
                        itemString = "[OBJECT]";
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View Object",
                            (o, args) => new DocumentViewForm(item.Value).ShowDialog(this)));
                        break;
                    case BsonType.Array:
                        itemString = $"[ARRAY({item.Value.AsArray.Count})]";
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View Array",
                            (o, args) => new ArrayViewForm(item.Value).ShowDialog(this)));
                        break;
                    case BsonType.Binary:
                        itemString = $"[BINARY({item.Value.AsBinary.Length})]";
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View Binary",
                            (o, args) => new BinaryViewForm(item.Value).ShowDialog(this)));
                        break;
                    case BsonType.String:
                        itemString = item.Value.AsString;
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View String",
                            (o, args) => new StringViewForm(item.Value).ShowDialog(this)));
                        break;
                    default:
                        itemString = item.Value.ToString();
                        break;
                }
                listBox.Items.Add(item.Key + ": " + new string(' ', maxLength - item.Key.Length) + itemString);
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