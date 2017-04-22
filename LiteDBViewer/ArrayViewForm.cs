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

        public ArrayViewForm(BsonArray array) : this()
        {
            var intLength = array.Count.ToString().Length;
            for (var i = 0; i < array.Count; i++)
            {
                var item = array[i];
                string itemString;
                ContextMenu contextMenu = null;
                switch (item.Type)
                {
                    case BsonType.Null:
                        itemString = "[NULL]";
                        break;
                    case BsonType.Document:
                        itemString = item.AsDocument.RawValue.ContainsKey("_type")
                            ? $"[OBJECT: {item.AsDocument.RawValue["_type"]}]"
                            : "[OBJECT]";
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View Object",
                            (o, args) => new DocumentViewForm(item.AsDocument).ShowDialog(this)));
                        break;
                    case BsonType.Array:
                        itemString = $"[ARRAY({item.AsArray.Count})]";
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View Array",
                            (o, args) => new ArrayViewForm(item.AsArray).ShowDialog(this)));
                        break;
                    case BsonType.Binary:
                        itemString = $"[BINARY({item.AsBinary.Length})]";
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View Binary",
                            (o, args) => new BinaryViewForm(item.AsBinary).ShowDialog(this)));
                        break;
                    case BsonType.String:
                        itemString = item.AsString;
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View String",
                            (o, args) => new StringViewForm(item.AsString).ShowDialog(this)));
                        break;
                    case BsonType.DateTime:
                        itemString = item.AsDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View String",
                            (o, args) => new StringViewForm(item.AsDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff")).ShowDialog(this)));
                        break;
                    default:
                        itemString = item.ToString();
                        break;
                }
                listBox.Items.Add($"[{i.ToString($"D{intLength}")}] {itemString}");
                _contextMenus.Add(contextMenu);
            }
        }

        private void listBox_Mouse(object sender, MouseEventArgs e)
        {
            listBox.SelectedIndex = listBox.IndexFromPoint(e.X, e.Y);
            if ((e.Button == MouseButtons.Right || (e.Button == MouseButtons.Left && e.Clicks > 1)) &&
                listBox.SelectedIndex >= 0 &&
                listBox.SelectedIndex < _contextMenus.Count && _contextMenus[listBox.SelectedIndex] != null)
            {
                _contextMenus[listBox.SelectedIndex].Show(listBox, e.Location);
            }
        }

        private void Close_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}