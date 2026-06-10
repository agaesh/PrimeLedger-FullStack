using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace PrimeLedger.Helpers
{
    public static class DataGridViewHelper
    {
        /// <summary>
        /// Updates one or more properties of a bound item in a DataGridView.
        /// </summary>
        public static void UpdateGridItemProperties<T>(
            DataGridView dgv,
            int id,
            Dictionary<string, object> updates)
        {
            if (dgv.DataSource is BindingSource bs &&
                bs.List is BindingList<T> list)
            {
                var item = list.FirstOrDefault(g =>
                {
                    var idProp = typeof(T).GetProperty("Id");
                    return idProp != null && (int)idProp.GetValue(g) == id;
                });

                if (item != null)
                {
                    foreach (var kvp in updates)
                    {
                        var prop = typeof(T).GetProperty(kvp.Key);
                        if (prop != null && prop.CanWrite)
                        {
                            prop.SetValue(item, kvp.Value);
                        }
                    }

                    // Notify grid of changes
                    bs.ResetBindings(false);
                }
            }
        }
    }
}
