using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using LiteDB;

namespace LiteDBViewer
{
    internal partial class MainForm : Form
    {
        private const int CollectionsResultLimit = 100;
        private readonly bool _encrypted;
        private readonly string _fileName;

        private readonly Dictionary<BsonDocument, LiteFileInfo> _fileStorageBinding =
            new Dictionary<BsonDocument, LiteFileInfo>();

        private LiteDatabase _database;

        private LiteDatabase Database
        {
            get => _database;
            set
            {
                if (_database != value)
                {
                    _database = value;
                    FillDataGridView(null);
                    RefreshCollections();
                }
            }
        }

        private string ConnectionString { get; }

        public MainForm(string fileName, string password = null, bool upgrade = false)
        {
            _encrypted = !string.IsNullOrWhiteSpace(password);
            _fileName = Path.GetFullPath(fileName);

            if (upgrade)
            {
                if (!LiteEngine.Upgrade(fileName, password, false))
                {
                    throw new NotSupportedException("File format do not support upgrade.");
                }
            }
            var args = new List<string>
            {
                $"filename=\"{_fileName}\""
            };
            if (_encrypted)
            {
                args.Add($"password=\"{password}\"");
            }

            ConnectionString = string.Join(";", args);

            InitializeComponent();
            txt_filename.Text = _fileName + (_encrypted ? " [ENCRYPTED]" : string.Empty);

            Database = new LiteDatabase(ConnectionString);
        }


        private void RefreshCollections()
        {
            lb_Collections.Items.Clear();

            if (Database == null)
            {
                return;
            }
            foreach (var collection in Database.GetCollectionNames())
            {
                if (!collection.Equals("_chunks") && !collection.Equals("_files"))
                {
                    lb_Collections.Items.Add(collection);
                }
            }
            lb_Collections.Items.Add("[FILESTORAGE]");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text = Text.Replace("{APPVERSION}", Assembly.GetExecutingAssembly().GetName().Version.ToString())
                .Replace("{DBVERSION}", Assembly.GetAssembly(typeof (LiteDatabase)).GetName().Version.ToString());
            Activate();
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _fileStorageBinding.Clear();
            if (lb_Collections.SelectedItem != null && !lb_Collections.SelectedItem.Equals("[QUERY]") &&
                !lb_Collections.SelectedItem.Equals("[FILESTORAGE]"))
            {
                FillDataGridView(Database.GetCollection(lb_Collections.SelectedItem.ToString())
                    .Find(Query.All(), 0, CollectionsResultLimit));
                txt_query.Text = $@"db.{lb_Collections.SelectedItem}.find limit {CollectionsResultLimit}";
            }
            else if (lb_Collections.SelectedItem?.Equals("[FILESTORAGE]") == true)
            {
                foreach (var fileInfo in Database.FileStorage.FindAll())
                {
                    _fileStorageBinding.Add(fileInfo.AsDocument, fileInfo);
                }
                FillDataGridView(_fileStorageBinding.Keys.ToArray());
                txt_query.Text = @"fs.find";
            }
        }

        public void FillDataGridView(IEnumerable<BsonDocument> documents)
        {
            if (lb_Collections.Items.Contains("[QUERY]"))
            {
                lb_Collections.Items.Remove("[QUERY]");
            }
            dataGridView.DataSource = null;
            if (documents != null)
            {
                var dt = new LiteDataTable(documents.ToString());
                foreach (var doc in documents)
                {
                    var dr = dt.NewRow() as LiteDataRow;
                    if (dr != null)
                    {
                        dr.UnderlyingValue = doc;
                        foreach (var property in doc.RawValue)
                        {
                            if (!property.Value.IsMaxValue && !property.Value.IsMinValue)
                            {
                                if (!dt.Columns.Contains(property.Key))
                                {
                                    dt.Columns.Add(new DataColumn(property.Key, typeof (string)));
                                }
                                switch (property.Value.Type)
                                {
                                    case BsonType.Null:
                                        dr[property.Key] = "[NULL]";
                                        break;
                                    case BsonType.Document:
                                        dr[property.Key] = property.Value.AsDocument.RawValue.ContainsKey("_type")
                                            ? $"[OBJECT: {property.Value.AsDocument.RawValue["_type"]}]"
                                            : "[OBJECT]";
                                        break;
                                    case BsonType.Array:
                                        dr[property.Key] = $"[ARRAY({property.Value.AsArray.Count})]";
                                        break;
                                    case BsonType.Binary:
                                        dr[property.Key] = $"[BINARY({property.Value.AsBinary.Length})]";
                                        break;
                                    case BsonType.DateTime:
                                        dr[property.Key] = property.Value.AsDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                        break;
                                    case BsonType.String:
                                        dr[property.Key] = property.Value.AsString;
                                        break;
                                    case BsonType.Int32:
                                    case BsonType.Int64:
                                        dr[property.Key] = property.Value.AsInt64.ToString();
                                        break;
                                    case BsonType.Decimal:
                                    case BsonType.Double:
                                        dr[property.Key] = property.Value.AsDecimal.ToString(CultureInfo.InvariantCulture);
                                        break;
                                    default:
                                        dr[property.Key] = property.Value.ToString();
                                        break;
                                }
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }
                dataGridView.DataSource = dt;
            }
        }

        private void dataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || (e.Button == MouseButtons.Left && e.Clicks > 1))
            {
                var currentMouseOver = dataGridView.HitTest(e.X, e.Y);
                if (currentMouseOver.RowIndex >= 0)
                {
                    var dataRowValue =
                        ((dataGridView.Rows[currentMouseOver.RowIndex].DataBoundItem as DataRowView)?.Row as
                            LiteDataRow)?.UnderlyingValue;
                    if (dataRowValue != null)
                    {
                        var m = new ContextMenu();
                        m.MenuItems.Add(new MenuItem("View Row as Object",
                            (o, args) => new DocumentViewForm(dataRowValue.AsDocument).ShowDialog(this)));
                        if (_fileStorageBinding.ContainsKey(dataRowValue))
                        {
                            m.MenuItems.Add("-");
                            m.MenuItems.Add(new MenuItem("View Stored Binary Data",
                                (o, args) =>
                                {
                                    var bytes = new byte[_fileStorageBinding[dataRowValue].Length];
                                    _fileStorageBinding[dataRowValue].OpenRead().Read(bytes, 0, bytes.Length);
                                    new BinaryViewForm(bytes).ShowDialog(this);
                                }));
                            m.MenuItems.Add(new MenuItem("Save Stored Data to File",
                                (o, args) =>
                                {
                                    try
                                    {
                                        var extention =
                                            Path.GetExtension(_fileStorageBinding[dataRowValue].Filename)?.ToLower() ??
                                            ".*";
                                        var sfd = new SaveFileDialog
                                        {
                                            RestoreDirectory = true,
                                            Title = @"Save data to file",
                                            Filter = $@"'{extention}' File|*{extention}|All Files|*.*",
                                            FileName = _fileStorageBinding[dataRowValue].Filename
                                        };
                                        if (sfd.ShowDialog() != DialogResult.OK)
                                            return;
                                        _fileStorageBinding[dataRowValue].SaveAs(sfd.FileName);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, @"Saving Data", MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                                    }
                                }));
                        }
                        if (currentMouseOver.ColumnIndex >= 0)
                        {
                            var cell = dataRowValue[dataGridView.Columns[currentMouseOver.ColumnIndex].Name];
                            switch (cell.Type)
                            {
                                case BsonType.String:
                                    m.MenuItems.Add("-");
                                    m.MenuItems.Add(new MenuItem("View String",
                                        (o, args) => new StringViewForm(cell.AsString).ShowDialog(this)));
                                    break;
                                case BsonType.Document:
                                    m.MenuItems.Add("-");
                                    m.MenuItems.Add(new MenuItem("View Object",
                                        (o, args) => new DocumentViewForm(cell.AsDocument).ShowDialog(this)));
                                    break;
                                case BsonType.Array:
                                    m.MenuItems.Add("-");
                                    m.MenuItems.Add(new MenuItem("View Array",
                                        (o, args) => new ArrayViewForm(cell.AsArray).ShowDialog(this)));
                                    break;
                                case BsonType.Binary:
                                    m.MenuItems.Add("-");
                                    m.MenuItems.Add(new MenuItem("View Binary",
                                        (o, args) => new BinaryViewForm(cell.AsBinary).ShowDialog(this)));
                                    break;
                            }
                        }
                        m.Show(dataGridView, new Point(e.X, e.Y));
                    }
                }
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                RunQuery(txt_query.Text);
                if (!lb_Collections.Items.Contains("[QUERY]"))
                {
                    lb_Collections.Items.Add("[QUERY]");
                }
                lb_Collections.SelectedItem = "[QUERY]";
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                if (Database != null)
                {
                    Database.Dispose();
                    Database = null;
                }
            }
            base.Dispose(disposing);
        }

        private void RunQuery(string query)
        {
            try
            {
                txt_query.Text = query;
                FillDataGridView(null);
                var results = Database.Engine.Run(query);
                RefreshCollections();
                FillDataGridView(
                    results.Select(
                        item =>
                            item.IsDocument
                                ? item.AsDocument
                                : new BsonDocument(new Dictionary<string, BsonValue> {{"[Result]", item}})));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Bad Query", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Info_Click(object sender, EventArgs e)
        {
            var infos = new Dictionary<string, BsonValue>
            {
                // ReSharper disable RedundantCast
                {"Engine.DatabaseVersion", new BsonValue((int) Database.Engine.UserVersion)},
                {"Engine.CacheSize", new BsonValue((int) Database.Engine.CacheSize)},
                {"Engine.CacheUsed", new BsonValue((int) Database.Engine.CacheUsed)},
                {"Engine.Timeout", new BsonValue((int) Database.Engine.Timeout.TotalSeconds)},
                {"FileName", new BsonValue(_fileName)},
                {"Encrypted", new BsonValue(_encrypted)}
                // ReSharper restore RedundantCast
            };
            new DocumentViewForm(new BsonDocument(infos)).ShowDialog();
        }

        private void Export_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                RestoreDirectory = true,
                Title = @"Dump Database data to file",
                Filter = @"Dump file|*.dmp"
            };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                using (var writer = File.CreateText(sfd.FileName))
                {
                    foreach (var name in Database.GetCollectionNames())
                    {
                        writer.WriteLine("-- Collection '{0}'", name);
                        var col = Database.GetCollection(name);
                        foreach (var index in col.GetIndexes().Where(x => x.Field != "_id"))
                        {
                            writer.WriteLine("db.{0}.ensureIndex {1}{2}", name, index.Field,
                                index.Unique ? " [unique]" : "");
                        }
                        foreach (var doc in col.Find(Query.All()))
                        {
                            writer.WriteLine("db.{0}.insert {1}", name, JsonSerializer.Serialize(doc));
                        }
                        writer.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Dumping Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            Database = new LiteDatabase(ConnectionString);
        }
    }
}