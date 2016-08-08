using System;
using System.Linq;
using System.Windows.Forms;

namespace LiteDBViewer
{
    /// <summary>
    ///     Specifies the contextual information about an application thread.
    /// </summary>
    public class MultiFormApplicationContext : ApplicationContext
    {
        private int _closedForms;
        private Form[] _forms;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:LiteDBViewer.MultiFormApplicationContext" /> class with no context.
        /// </summary>
        public MultiFormApplicationContext()
            : this(null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:LiteDBViewer.MultiFormApplicationContext" /> class with the
        ///     specified
        ///     <see cref="T:System.Windows.Forms.Form[]" />.
        /// </summary>
        /// <param name="forms">The main <see cref="T:System.Windows.Forms.Form[]" /> of the application to use for context. </param>
        public MultiFormApplicationContext(Form[] forms)
        {
            Forms = forms;
        }

        /// <summary>
        ///     Gets or sets the <see cref="T:System.Windows.Forms.Form[]" /> to use as context.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.Windows.Forms.Form[]" /> to use as context.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public Form[] Forms
        {
            get { return _forms; }
            set
            {
                EventHandler eventHandler = OnMainFormDestroy;
                if (_forms != null)
                {
                    foreach (var form in _forms)
                    {
                        form.HandleDestroyed -= eventHandler;
                    }
                }
                _closedForms = 0;
                _forms = value;
                if (_forms == null)
                    return;
                foreach (var form in _forms)
                {
                    form.HandleDestroyed += eventHandler;
                    form.Show();
                }
            }
        }

        /// <summary>
        ///     Releases the unmanaged resources used by the <see cref="T:LiteDBViewer.MultiFormApplicationContext" /> and
        ///     optionally
        ///     releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (!disposing || _forms == null)
                return;
            foreach (var form in _forms.Where(f => !f.IsDisposed))
            {
                form.Dispose();
            }
            _forms = null;
        }

        /// <summary>
        ///     Calls <see cref="M:LiteDBViewer.MultiFormApplicationContext.ExitThreadCore" />, which raises the
        ///     <see cref="E:LiteDBViewer.MultiFormApplicationContext.ThreadExit" /> event.
        /// </summary>
        /// <param name="sender">The object that raised the event. </param>
        /// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            _closedForms++;
            if (_forms.Length == _closedForms)
            {
                ExitThreadCore();
            }
        }

        private void OnMainFormDestroy(object sender, EventArgs e)
        {
            var form = (Form) sender;
            if (form.RecreatingHandle)
                return;
            form.HandleDestroyed -= OnMainFormDestroy;
            OnMainFormClosed(sender, e);
        }
    }
}