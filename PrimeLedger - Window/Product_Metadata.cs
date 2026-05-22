
using Newtonsoft.Json;
using PrimeLedger___Window.Services;
using PrimeLedger_Window.DTO;
using System.ComponentModel;

namespace PrimeLedger___Window
{
    public partial class Product_Metadata : Form
    {
        private List<ProductMetadataDTO> _groupData;
        private ApiClient _client;
        public Product_Metadata()
        {
            InitializeComponent();
            _groupData = new List<ProductMetadataDTO>();
            _client = new ApiClient();
        }

        private async void Product_Metadata_Load(object sender, EventArgs e)
        {
            try
            {
                _groupData = await LoadCodeType("GROUP");
                dgvGroup.AutoGenerateColumns = false;
                dgvGroup.DataSource = _groupData;
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
                "SUBGROUP" when id.HasValue => $"/group/{id}/subgroups",
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

                rbActive.Checked = record.Status.ToLower() == "active";
                rbInactive.Checked = record.Status.ToLower() == "inactive";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void dgvGroup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var parentId = Convert.ToInt32(dgvGroup.Rows[e.RowIndex].Cells[colID.Name].Value);

                var data = await LoadCodeType("SUBGROUP", parentId);
                dgvSubGroup.AutoGenerateColumns = false;
                dgvSubGroup.DataSource = data;

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

                txtSubGroupCode.Text = row.Cells[colSubGroupCode.Name]?.Value?.ToString();
                txtSubGroupDesc.Text = row.Cells[colSubGroupDescription.Name]?.Value?.ToString();

                rbActiveSub.Checked = row.Cells[colSubStatus.Name].Value?.ToString()?.ToLower() == "active";
                rbInactiveSub.Checked = row.Cells[colSubStatus.Name].Value?.ToString()?.ToLower() == "inactive";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
