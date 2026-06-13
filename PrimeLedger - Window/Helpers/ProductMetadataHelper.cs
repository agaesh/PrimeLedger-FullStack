using PrimeLedger.Helpers;
using PrimeLedger.Shared.DTO.Products;
using PrimeLedger.Shared.Enums;
using PrimeLedger___Window.Services;
using System.ComponentModel;
using System.Threading.Tasks.Sources;

public static class ProductMetadataHelper
{

    /// <summary>
    /// Maps a ProductMetadataDTO to the provided UI controls.
    /// This avoids duplicating cell-click logic across Group, Brand, Category, and UOM.
    /// </summary>
    public static void MapToControls(ProductMetadataDTO dto, TextBox txtCode, TextBox txtDescription,
                                     RadioButton rbActive, RadioButton rbInactive, Button BtnCreate)
    {
        if (dto == null) return;

        txtCode.Text = dto.Code;
        txtDescription.Text = dto.Description;

        rbActive.Checked = dto.Status == StatusEnum.ACTIVE;
        rbInactive.Checked = dto.Status == StatusEnum.INACTIVE;

        BtnCreate.Text = "Update";
    }

    /// <summary>
    /// Deletes a ProductMetadata record from a DataGridView.
    /// Shows a confirmation dialog, calls the API to remove the record,
    /// and updates the BindingList to keep the grid in sync.
    /// Use this helper whenever you need consistent delete logic
    /// across Group, SubGroup, Brand, Category, or UOM.
    /// </summary>
    /// <param name="client">The HttpClient used to call the API.</param>
    /// <param name="endpoint">The API endpoint (e.g. "/brand", "/subgroup").</param>
    /// <param name="id">The record ID to delete.</param>
    /// <param name="dgv">The DataGridView bound to the list.</param>
    /// <param name="idColumnName">The column name containing the ID.</param>
    /// <param name="entityName">Friendly name for messages (e.g. "Brand").</param>
    public static async Task DeleteRecordAsync(
        ApiClient client,
        string endpoint,
        int id,
        DataGridView dgv,
        string idColumnName,
        string entityName)
    {
        try
        {
            // Confirm deletion
            var confirm = MessageBox.Show(
                $"Are you sure you want to delete this {entityName}?",
                "Delete Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No) return;

            // Call API
            await client.DeleteAsync($"{endpoint}/{id}");

            MessageBox.Show($"{entityName} with ID {id} has been deleted.", "Deleted",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Remove from BindingList
            var bs = dgv.DataSource as BindingSource;

            if (bs != null)
            {
                var list = bs.List as BindingList<ProductMetadataDTO>;

                if (list != null)
                {
                    var itemToRemove = list.FirstOrDefault(x => x.Id == id);

                    if (itemToRemove != null)
                    {
                        list.Remove(itemToRemove);
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}