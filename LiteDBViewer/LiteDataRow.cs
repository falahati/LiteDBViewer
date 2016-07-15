using System;
using System.Data;
using LiteDB;

namespace LiteDBViewer
{
    /// <summary>
    ///     Represents a row of data in a
    ///     <see cref="T:LiteDBViewer.LiteDataTable" />.
    /// </summary>
    [Serializable]
    public class LiteDataRow : DataRow
    {
        /// <summary>
        ///     Initializes a new instance of the LiteDataRow. Constructs a row from the builder. Only for internal usage.
        /// </summary>
        /// <param name="builder">builder </param>
        public LiteDataRow(DataRowBuilder builder)
            : base(builder)
        {
        }

        internal BsonDocument UnderlyingValue { get; set; }
    }
}