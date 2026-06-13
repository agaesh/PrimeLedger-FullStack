using LoadingIndicator.WinForms;
using PrimeLedger.Helpers;
using PrimeLedger.Shared.DTO;
using PrimeLedger.Shared.DTO.Products;
using PrimeLedger.Shared.Enums;
using PrimeLedger___Window.Services;
using System.ComponentModel;

namespace PrimeLedger___Window
{
    public partial class Product_Metadata : Form
    {
        private List<ProductMetadataDTO> _groupData;
        private List<ProductMetadataDTO> _subgroupData;
        private List<ProductMetadataDTO> _brandData;
        private List<ProductMetadataDTO> _categoryData;
        private ApiClient _client;
        BindingList<ProductMetadataDTO>? BindingList;
        private LongOperation _loading;

        private bool IsGroupLoaded = false;
        private bool IsSubGroupLoaded = false;
        private bool IsBrandLoaded = false;
        private bool IsCategoryLoaded = false;
        private bool IsUomLoaded = false;

        public Product_Metadata()
        {
            InitializeComponent();
            _groupData = new List<ProductMetadataDTO>();
            _client = new ApiClient();
            _loading = new LongOperation(this);
        }

        private async void Product_Metadata_Load(object sender, EventArgs e)
        {
            try
            {
                using (_loading.Start())
                {
                    await Task.Delay(3000);
                    _groupData = await LoadCodeType("GROUP");
                }

                dgvGroup.AutoGenerateColumns = false;

                var bindingList = _groupData != null
                ? new BindingList<ProductMetadataDTO>(_groupData)
                : new BindingList<ProductMetadataDTO>();

                var bindingSource = new BindingSource(bindingList, null);
                dgvGroup.DataSource = bindingSource;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private string BuildEndpoint(string codeType, int? id)
        {
            return codeType switch
            {
                "GROUP" => "/group",
                "GROUP_TRANSACTION" => $"/group/{id}",
                "SUBGROUP" when id.HasValue => $"/group/{id}/subgroups",
                "SUBGROUP_TRANSACTION" => $"/subgroup/{id}",
                "BRAND"=> $"/brand/{id}",
                "BRAND_TRANSACTION" => $"/brand/{id}",
                "CATEGORY" => $"/brand/{id}/categories",
                "CATEGORY_TRANSACTION" => $"/category/{id}",
                _ => throw new ArgumentException("Invalid codeType or missing id")
            };
        }
        public async Task<List<ProductMetadataDTO>> LoadCodeType(string codeType, int? id = null)
        {
            try
            {
                var endpoint = BuildEndpoint(codeType, id);
                return await _client.GetAsync<List<ProductMetadataDTO>>(endpoint);

            }
            catch (HttpRequestException)
            {
                MessageBox.Show(
                    "Unable to connect to the server.\n\n" +
                    "Please Contact your Administrator.",
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return new List<ProductMetadataDTO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading code type: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<ProductMetadataDTO>();
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var dgv = sender as DataGridView;
                if (dgv == null) return;


                switch (dgv.Name)
                {
                    case "dgvGroup":
                        var groupId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[colID.Name].Value);
                        var groupRecord = _groupData.Find(g => g.Id == groupId);
                        ProductMetadataHelper.MapToControls(
                            groupRecord,
                            txtGroupCode,
                            txtDescription,
                            rbActive,
                            rbInactive,
                            BtnCreateGroup
                        );
                        break;

                    case "dgvSubGroup":
                        var subGroupId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[colSubGroupID.Name].Value);
                        var subGroupRecord = _subgroupData.Find(sg => sg.Id == subGroupId);
                        ProductMetadataHelper.MapToControls(
                            subGroupRecord,
                            txtSubGroupCode,
                            txtSubGroupDesc,
                            rbActiveSub,
                            rbInactiveSub,
                            BtnCreateSubGroup
                        );
                        break;

                    case "dgvBrand":
                        var BrandId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[colBrandID.Name].Value);
                        var brandRecord = _brandData.Find(b => b.Id == BrandId);
                        ProductMetadataHelper.MapToControls(
                            brandRecord,
                            txtBrandCode,
                            txtBrandDesc,
                            rbBrandActive,
                            rbBrandInactive,
                            BtnCreateBrand
                        );
                        break;
                    case "dgvCategory":
                        var CategoryId = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[colCategoryID.Name].Value);
                        var categoryRecord = _categoryData.Find(c => c.Id == CategoryId);
                        ProductMetadataHelper.MapToControls(
                            categoryRecord,
                            txtCategoryCode,
                            txtCategoryDesc,
                            rbCategoryActive,
                            rbCategoryInactive,
                            BtnCreateCategory
                        );
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void dgvGroup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //prevent event triggering when user clicking the header row
                if (e.RowIndex < 0) return;
                // Check if the clicked cell is in the Delete Button column
                if (e.ColumnIndex != colDeleteGroup.Index) return;
                //Delete Confirmation
                var messagebox = MessageBox.Show(
                    "Are you sure you want to delete this record?",
                    "Delete Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                //Delete Prevention when user click no
                if (messagebox == DialogResult.No) return;

                int id = Convert.ToInt32(dgvGroup.Rows[e.RowIndex].Cells[colID.Name].Value);
                //calling the delete api method
                await _client.DeleteAsync($"/group/{id}");
                MessageBox.Show($"Group with ID {id} has been deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //remove the deleted code from the datagridview
                if(dgvGroup.DataSource is BindingSource bs && bs.List is BindingList<ProductMetadataDTO> list)
                {
                    var itemToRemove = list.FirstOrDefault(g => g.Id == id);
                    if (itemToRemove != null)
                    {
                        list.Remove(itemToRemove);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void dgvGroup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var parentId = Convert.ToInt32(dgvGroup.Rows[e.RowIndex].Cells[colID.Name].Value);
                _subgroupData = await LoadCodeType("SUBGROUP", parentId);
                var BindingList = new BindingList<ProductMetadataDTO>(_subgroupData);

                var BindingSource = new BindingSource(BindingList, null);
                dgvSubGroup.AutoGenerateColumns = false;
                dgvSubGroup.DataSource = BindingSource;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public async Task<ProductMetadataDTO> CreateAsync(CreateProductMetadataDTO dto, string type)
        {
            try
            {
                var result = await _client.PostAsync<CreateProductMetadataDTO, ApiResponse<ProductMetadataDTO>>(
                    BuildEndpoint(type, null),
                    dto
                );

                return result.Data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateAsync(int id, UpdateProductMetadataDTO dto, string type)
        {
            try
            {
                await _client.PutAsync(BuildEndpoint(type, id), dto);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private async void BtnCreateGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (BtnCreateGroup.Text == "Update" && dgvGroup.CurrentRow != null)
                {
                    var id = Convert.ToInt32(dgvGroup.CurrentRow.Cells[colID.Name].Value);
                    var updateDto = new UpdateProductMetadataDTO
                    {
                        Description = txtDescription.Text,
                        Status = rbActive.Checked ? StatusEnum.ACTIVE : StatusEnum.INACTIVE,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await UpdateAsync(id, updateDto, "GROUP_TRANSACTION");

                    //Updating the local datagridview after update
                    DataGridViewHelper.UpdateGridItemProperties<ProductMetadataDTO>(
                      dgvGroup,
                      id,
                      new Dictionary<string, object>
                        {
                            { "Description", updateDto.Description },
                            { "Status", updateDto.Status},
                            { "UpdatedAt", updateDto.UpdatedAt }
                        }
                    );

                    MessageBox.Show("Group updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var createDto = new CreateProductMetadataDTO
                    {
                        Code = txtGroupCode.Text,
                        Description = txtDescription.Text,
                        Type = Codetype.GROUP,
                        Status = rbActive.Checked ? StatusEnum.ACTIVE : StatusEnum.INACTIVE,
                        ParentId = 0,
                        CreatedAt = DateTime.UtcNow
                    };

                    var record = await CreateAsync(createDto, "GROUP_TRANSACTION");
                    //Adding Item after create
                    DataGridViewHelper.AddGridItem<ProductMetadataDTO>(dgvGroup, record);

                    MessageBox.Show("Group has been Created Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnCreateSubGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (BtnCreateSubGroup.Text == "Update")
                {

                    var id = Convert.ToInt32(dgvSubGroup.CurrentRow.Cells[colSubGroupID.Name].Value);
                    var updateDto = new UpdateProductMetadataDTO
                    {
                        Description = txtSubGroupDesc.Text,
                        Status = rbActiveSub.Checked ? StatusEnum.ACTIVE : StatusEnum.INACTIVE,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await UpdateAsync(id, updateDto, "SUBGROUP_TRANSACTION");

                    //Updating the local datagridview after update
                    DataGridViewHelper.UpdateGridItemProperties<ProductMetadataDTO>(
                      dgvSubGroup,
                      id,
                      new Dictionary<string, object>
                        {
                            { "Description", updateDto.Description },
                            { "Status", updateDto.Status},
                            { "UpdatedAt", updateDto.UpdatedAt }
                        }
                    );

                    MessageBox.Show("SubGroup updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (BtnCreateSubGroup.Text == "Create")
                {
                    if (dgvGroup.CurrentRow == null)
                    {
                        MessageBox.Show("Please select a parent group first.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var parentId = Convert.ToInt32(dgvGroup.CurrentRow.Cells[colID.Name].Value);
                    var createDto = new CreateProductMetadataDTO
                    {
                        Code = txtSubGroupCode.Text,
                        Description = txtDescription.Text,
                        Type = Codetype.SUBGROUP,
                        Status = rbActive.Checked ? StatusEnum.ACTIVE : StatusEnum.INACTIVE,
                        ParentId = parentId,
                        CreatedAt = DateTime.UtcNow
                    };

                    var record =await CreateAsync(createDto, "SUBGROUP_TRANSACTION");

                    DataGridViewHelper.AddGridItem<ProductMetadataDTO>(
                        dgvSubGroup,
                        record
                    );
                    // Success message handled inside SaveOrUpdateGroupAsync
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BtnClearGroup_Click(object sender, EventArgs e)
        {
            txtGroupCode.Text = "";
            txtDescription.Text = "";
            rbActive.Checked = true;
            rbInactive.Checked = false;
            BtnCreateGroup.Text = "Create";
        }

        private void BtnClearSubGroup_Click(object sender, EventArgs e)
        {
            txtSubGroupCode.Text = "";
            txtSubGroupDesc.Text = "";
            rbActiveSub.Checked = true;
            rbInactive.Checked = false;
            BtnCreateSubGroup.Text = "Create";
        }

        private async void dgvSubGroup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //prevent event triggering when user clicking the header row
                if (e.RowIndex < 0) return;

                // Check if the clicked cell is in the Delete Button column
                if (e.ColumnIndex != colDeleteSubGroup.Index) return;

                //Delete Confirmation
                var messagebox = MessageBox.Show(
                 "Are you sure you want to delete this record?",
                 "Delete Confirmation",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question);

                //Delete Prevention when user click no
                if (messagebox == DialogResult.No) return;

                int id = Convert.ToInt32(dgvSubGroup.Rows[e.RowIndex].Cells[colSubGroupID.Name].Value);

                //calling the delete api method
                await _client.DeleteAsync($"/subgroup/{id}");

                MessageBox.Show($"SubGroup with ID {id} has been deleted.", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //removing the deleted code from the datagridview
                if (dgvSubGroup.DataSource is BindingSource bs && bs.List is BindingList<ProductMetadataDTO> list)
                {
                    var itemToRemove = list.FirstOrDefault(g => g.Id == id);
                    if (itemToRemove != null)
                    {
                        list.Remove(itemToRemove);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabGroupSubGroup && !IsGroupLoaded)
            {
                using (_loading.Start())
                {

                    _groupData = await LoadCodeType("GROUP");
                }
                DataGridViewHelper.BindData(dgvGroup, _groupData);
            }
            else if (tabControl1.SelectedTab == tabBrandCategory && !IsBrandLoaded)
            {

                using (_loading.Start())
                {

                    _brandData = await LoadCodeType("BRAND");
                }
                DataGridViewHelper.BindData(dgvBrand, _brandData);
                IsBrandLoaded = true;
            }
        }

        #region "Brand Events"
        private async void dgvBrand_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int BrandID = Convert.ToInt32(dgvBrand.SelectedRows[0].Cells[colBrandID.Name].Value);
            _categoryData = await LoadCodeType("CATEGORY", BrandID);

            DataGridViewHelper.BindData(dgvCategory, _categoryData);

        }

        private async void dgvBrand_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                int id = Convert.ToInt32(dgvBrand.SelectedRows[0].Cells[colBrandID.Name].Value);

                await ProductMetadataHelper.DeleteRecordAsync(_client, BuildEndpoint("BRAND", null), id, dgvBrand, colBrandID.Name, "SubGroup");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClearBrand_Click(object sender, EventArgs e)
        {
            txtBrandCode.Text = "";
            txtBrandDesc.Text = "";
            rbBrandActive.Checked = true;
            rbBrandInactive.Checked = false;
            BtnCreateBrand.Text = "Create";
        }

        private async void BtnCreateBrand_Click(object sender, EventArgs e)
        {
            try
            {
                if (BtnCreateBrand.Text == "Update" && dgvBrand.CurrentRow != null)
                {
                    var id = Convert.ToInt32(dgvBrand.CurrentRow.Cells[colBrandID.Name].Value);
                    var updateDto = new UpdateProductMetadataDTO
                    {
                        Description = txtBrandDesc.Text,
                        Status = rbBrandActive.Checked ? StatusEnum.ACTIVE : StatusEnum.INACTIVE,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await UpdateAsync(id, updateDto, "BRAND_TRANSACTION");

                    // Update DataGridView after update
                    DataGridViewHelper.UpdateGridItemProperties<ProductMetadataDTO>(
                        dgvBrand,
                        id,
                        new Dictionary<string, object>
                        {
                        { "Description", updateDto.Description },
                        { "Status", updateDto.Status },
                        { "UpdatedAt", updateDto.UpdatedAt }
                        }
                    );

                    MessageBox.Show("Brand updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var createDto = new CreateProductMetadataDTO
                    {
                        Code = txtBrandCode.Text,
                        Description = txtBrandDesc.Text,
                        Type = Codetype.BRAND,
                        Status = rbBrandActive.Checked ? StatusEnum.ACTIVE : StatusEnum.INACTIVE,
                        ParentId = null,
                        CreatedAt = DateTime.UtcNow
                    };

                    var result = await CreateAsync(createDto, "BRAND_TRANSACTION");

                    var id = Convert.ToInt32(dgvBrand.CurrentRow.Cells[colBrandID.Name].Value);

                    DataGridViewHelper.AddGridItem<ProductMetadataDTO>(
                      dgvBrand, result);

                    MessageBox.Show("Brand has been Created Successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Category Events"

        private async void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex != colDeleteCategory.Index) return;
                int id = Convert.ToInt32(dgvCategory.SelectedRows[0].Cells[colCategoryID.Name].Value);

                await ProductMetadataHelper.DeleteRecordAsync(_client,"/category", id, dgvCategory, colCategoryID.Name, "Category");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnClearCategory_Click(object sender, EventArgs e)
        {
            txtCategoryCode.Text = "";
            txtCategoryDesc.Text = "";
            rbCategoryActive.Checked = true;
            rbCategoryInactive.Checked = false;
            BtnCreateCategory.Text = "Create";
        }
        private async void BtnCreateCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (BtnCreateCategory.Text == "Update" && dgvCategory.CurrentRow != null)
                {
                    var id = Convert.ToInt32(dgvCategory.CurrentRow.Cells[colCategoryID.Name].Value);
                    var updateDto = new UpdateProductMetadataDTO
                    {
                        Description = txtCategoryDesc.Text,
                        Status = rbCategoryActive.Checked ? StatusEnum.ACTIVE : StatusEnum.INACTIVE,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await UpdateAsync(id, updateDto, "CATEGORY_TRANSACTION");

                    // Update DataGridView after update
                    DataGridViewHelper.UpdateGridItemProperties<ProductMetadataDTO>(
                        dgvCategory,
                        id,
                        new Dictionary<string, object>
                        {
                        { "Description", updateDto.Description },
                        { "Status", updateDto.Status },
                        { "UpdatedAt", updateDto.UpdatedAt }
                        }
                    );

                    MessageBox.Show("Category updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var createDto = new CreateProductMetadataDTO
                    {
                        Code = txtCategoryCode.Text,
                        Description = txtCategoryDesc.Text,
                        Type = Codetype.CATEGORY,
                        Status = rbCategoryActive.Checked ? StatusEnum.ACTIVE : StatusEnum.INACTIVE,
                        ParentId = Convert.ToInt32(dgvBrand.SelectedRows[0].Cells[colBrandID.Name].Value),
                        CreatedAt = DateTime.UtcNow
                    };

                    var record = await CreateAsync(createDto, "CATEGORY_TRANSACTION");
                    //Updateing the dgv after creating new category.
                    DataGridViewHelper.AddGridItem<ProductMetadataDTO>(
                      dgvCategory, record);
                    //Success MessageBox after creating new category.
                    MessageBox.Show("Category has been Created Successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
