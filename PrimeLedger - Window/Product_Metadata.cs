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
        private ApiClient _client;
        BindingList<ProductMetadataDTO>? BindingList;
        private LongOperation _loading;

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
                "CATEGORY" => $"brand/{id}/category/",
                "CATEGORY_TRANSACTION" => $"category/{id}",
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

        private void dgvGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.RowIndex < 0) return;
                var record = _groupData.Find(g => g.Id == Convert.ToInt32(dgvGroup.Rows[e.RowIndex].Cells[colID.Name].Value));

                txtGroupCode.Text = record.Code.ToString();
                txtDescription.Text = record.Description.ToString();

                rbActive.Checked = record.Status == StatusEnum.ACTIVE;
                rbInactive.Checked = record.Status == StatusEnum.INACTIVE;

                BtnCreateGroup.Text = "Update";
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
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex != colDeleteGroup.Index) return;

                int id = Convert.ToInt32(dgvGroup.Rows[e.RowIndex].Cells[colID.Name].Value);

                await _client.DeleteAsync($"/group/{id}");

                MessageBox.Show($"Group with ID {id} has been deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);


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
                var data = await LoadCodeType("SUBGROUP", parentId);
                var BindingList = new BindingList<ProductMetadataDTO>(data);

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

        private void dgvSubGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var row = dgvSubGroup.Rows[e.RowIndex];
                //Setting the values to the textboxes
                txtSubGroupCode.Text = row.Cells[colSubGroupCode.Name]?.Value?.ToString();
                txtSubGroupDesc.Text = row.Cells[colSubGroupDescription.Name]?.Value?.ToString();
                rbActiveSub.Checked = row.Cells[colSubStatus.Name].Value?.ToString()?.ToLower() == "active";
                rbInactiveSub.Checked = row.Cells[colSubStatus.Name].Value?.ToString()?.ToLower() == "inactive";
                //Changing the button text to Update, Later useful for update functionality
                BtnCreateSubGroup.Text = "Update";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public async Task<bool> CreateAsync(CreateProductMetadataDTO dto, string type)
        {
            try
            {
                var result = await _client.PostAsync<CreateProductMetadataDTO, ApiResponse<ProductMetadataDTO>>(
                    BuildEndpoint(type, null),
                    dto
                );

                return result.Success;
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

                    var isSuccess = await CreateAsync(createDto, "GROUP_TRANSACTION");

                    var id = Convert.ToInt32(dgvGroup.CurrentRow.Cells[colID.Name].Value);
                    DataGridViewHelper.UpdateGridItemProperties<ProductMetadataDTO>(
                        dgvGroup,
                        id,
                        new Dictionary<string, object>
                        {
                            { "Id",createDto.Id }, // Temporary ID, will be updated after API response
                            { "Code", createDto.Code },
                            { "Description", createDto.Description },
                            { "Type", createDto.Type },
                            { "Status", createDto.Status },
                            { "ParentId", createDto.ParentId },
                            { "CreatedAt", createDto.CreatedAt
                        }
                       }
                    );

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
                    await CreateAsync(createDto,"SUBGROUP_TRANSACTION");

                    var id = Convert.ToInt32(dgvSubGroup.CurrentRow.Cells[colSubGroupID.Name].Value);
                    DataGridViewHelper.UpdateGridItemProperties<ProductMetadataDTO>(
                        dgvSubGroup,
                        id,
                        new Dictionary<string, object>
                        {
                            { "Id",createDto.Id }, // Temporary ID, will be updated after API response
                            { "Code", createDto.Code },
                            { "Description", createDto.Description },
                            { "Type", createDto.Type },
                            { "Status", createDto.Status },
                            { "ParentId", parentId },
                            { "CreatedAt", createDto.CreatedAt }
                        }
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

    }
}
