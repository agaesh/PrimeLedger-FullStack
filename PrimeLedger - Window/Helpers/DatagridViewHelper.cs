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
        /// <summary>
        /// Adds a new item to a DataGridView bound via BindingSource/BindingList,
        /// and refreshes the grid to show it.
        /// </summary>
        /// <typeparam name="T">Type of items in the BindingList.</typeparam>
        /// <param name="dgv">Target DataGridView.</param>
        /// <param name="newItem">Item to add.</param>
        public static void AddGridItem<T>(DataGridView dgv, T newItem)
        {
            if (dgv.DataSource is BindingSource bs &&
                bs.List is BindingList<T> list)
            {
                list.Add(newItem);

                // Notify grid of changes
                bs.ResetBindings(false);
            }
        }
    }
}
