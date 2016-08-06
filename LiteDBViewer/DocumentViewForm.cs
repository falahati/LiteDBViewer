using System;
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

        public DocumentViewForm(BsonDocument document) : this()
        {
            if (document == null)
            {
                return;
            }
            var maxLength = Math.Max(document.RawValue.Keys.Any() ? document.RawValue.Keys.Max(key => key.Length) : 0,
                10);
            foreach (var item in document.RawValue.OrderBy(pair => pair.Key))
            {
                string itemString;
                ContextMenu contextMenu = null;
                switch (item.Value.Type)
                {
                    case BsonType.Null:
                        itemString = "[NULL]";
                        break;
                    case BsonType.Document:
                        itemString = item.Value.AsDocument.RawValue.ContainsKey("_type")
                            ? $"[OBJECT: {item.Value.AsDocument.RawValue["_type"]}]"
                            : "[OBJECT]";
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View Object",
                            (o, args) => new DocumentViewForm(item.Value.AsDocument).ShowDialog(this)));
                        break;
                    case BsonType.Array:
                        itemString = $"[ARRAY({item.Value.AsArray.Count})]";
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View Array",
                            (o, args) => new ArrayViewForm(item.Value.AsArray).ShowDialog(this)));
                        break;
                    case BsonType.Binary:
                        itemString = $"[BINARY({item.Value.AsBinary.Length})]";
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View Binary",
                            (o, args) => new BinaryViewForm(item.Value.AsBinary).ShowDialog(this)));
                        break;
                    case BsonType.String:
                        itemString = item.Value.AsString;
                        contextMenu = new ContextMenu();
                        contextMenu.MenuItems.Add(new MenuItem("View String",
                            (o, args) => new StringViewForm(item.Value.AsString).ShowDialog(this)));
                        break;
                    default:
                        itemString = item.Value.ToString();
                        break;
                }
                var keyName = item.Key;
                switch (item.Key)
                {
                    case "_type":
                        // ReSharper disable once RedundantBaseQualifier
                        base.Text = $"[OBJECT: {itemString}]";
                        keyName = "[TYPE]";
                        break;
                    case "_id":
                        keyName = "[ID]";
                        break;
                }
                listBox.Items.Add(keyName + ": " + new string(' ', maxLength - keyName.Length) + itemString);
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

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}