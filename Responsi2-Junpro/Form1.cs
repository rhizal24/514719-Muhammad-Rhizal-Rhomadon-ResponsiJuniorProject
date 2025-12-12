using Responsi2_Junpro.Models;
using Responsi2_Junpro.Repositories;

namespace Responsi2_Junpro
{
    public partial class Form1 : Form
    {
        // ENCAPSULATION: Private fields untuk repository dan dropdown
        private readonly IDatabaseRepository _repository;
        private readonly DropDownMenu _dropDownMenu;
        private int _selectedDeveloperId = 0;

        public Form1()
        {
            InitializeComponent();

            // Inisialisasi repository dan dropdown
            _repository = new DatabaseRepository();
            _dropDownMenu = new DropDownMenu(_repository);

            // Load data saat form dimuat
            this.Load += Form1_Load;

            // Event handlers untuk buttons
            insertButton.Click += InsertButton_Click;
            updateButton.Click += UpdateButton_Click;
            deleteButton.Click += DeleteButton_Click;

            // Event handler untuk DataGridView selection
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            // Load dropdown menus
            _dropDownMenu.LoadProyekDropdown(pilihProyek);
            _dropDownMenu.LoadStatusKontrakDropdown(statusKontrak);

            // Load data ke DataGridView
            LoadDataGrid();
        }

        #region DataGridView Methods

        private void LoadDataGrid()
        {
            dataGridView1.Rows.Clear();

            var developers = _repository.GetAll();
            foreach (var dev in developers)
            {
                dataGridView1.Rows.Add(
                    dev.NamaDeveloper,
                    dev.NamaProyek,
                    dev.StatusKontrak,
                    dev.FiturSelesai,
                    dev.JumlahBug,
                    dev.SkorTotal.ToString("F2"),
                    dev.TotalGaji.ToString("N0")
                );

                // Simpan ID di Tag row terakhir
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].Tag = dev.Id;
            }
        }

        private void DataGridView1_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                var row = dataGridView1.Rows[e.RowIndex];

                // Ambil ID dari Tag
                if (row.Tag != null)
                {
                    _selectedDeveloperId = (int)row.Tag;
                }

                // Isi form dengan data dari row yang dipilih
                namaInput.Text = row.Cells["nama_developer"].Value?.ToString() ?? "";
                _dropDownMenu.SetSelectedValue(pilihProyek, row.Cells["nama_proyek"].Value?.ToString() ?? "");
                _dropDownMenu.SetSelectedValue(statusKontrak, row.Cells["status_kontrak"].Value?.ToString() ?? "");
                completeInput.Text = row.Cells["fitur_selesai"].Value?.ToString() ?? "";
                bugInput.Text = row.Cells["jumlah_bug"].Value?.ToString() ?? "";
            }
        }

        #endregion

        #region CRUD Operations

        private void InsertButton_Click(object? sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            var developer = CreateDeveloperFromForm();

            if (_repository.Insert(developer))
            {
                MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadDataGrid();
            }
        }

        private void UpdateButton_Click(object? sender, EventArgs e)
        {
            if (_selectedDeveloperId <= 0)
            {
                MessageBox.Show("Pilih data yang ingin diupdate dari tabel!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateForm()) return;

            var developer = CreateDeveloperFromForm();
            developer.Id = _selectedDeveloperId;

            if (_repository.Update(developer))
            {
                MessageBox.Show("Data berhasil diupdate!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadDataGrid();
            }
        }

        private void DeleteButton_Click(object? sender, EventArgs e)
        {
            if (_selectedDeveloperId <= 0)
            {
                MessageBox.Show("Pilih data yang ingin dihapus dari tabel!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (_repository.Delete(_selectedDeveloperId))
                {
                    MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDataGrid();
                }
            }
        }

        #endregion

        #region Helper Methods

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(namaInput.Text))
            {
                MessageBox.Show("Nama Developer harus diisi!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                namaInput.Focus();
                return false;
            }

            if (!_dropDownMenu.ValidateDropdown(pilihProyek, "Proyek"))
                return false;

            if (!_dropDownMenu.ValidateDropdown(statusKontrak, "Status Kontrak"))
                return false;

            if (!int.TryParse(completeInput.Text, out int fitur) || fitur < 0)
            {
                MessageBox.Show("Fitur Selesai harus berupa angka positif!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                completeInput.Focus();
                return false;
            }

            if (!int.TryParse(bugInput.Text, out int bug) || bug < 0)
            {
                MessageBox.Show("Jumlah Bug harus berupa angka positif!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bugInput.Focus();
                return false;
            }

            return true;
        }

        private Developer CreateDeveloperFromForm()
        {
            return new Developer(
                namaInput.Text.Trim(),
                _dropDownMenu.GetSelectedValue(pilihProyek),
                _dropDownMenu.GetSelectedValue(statusKontrak),
                int.Parse(completeInput.Text),
                int.Parse(bugInput.Text)
            );
        }

        private void ClearForm()
        {
            namaInput.Clear();
            pilihProyek.SelectedIndex = 0;
            statusKontrak.SelectedIndex = 0;
            completeInput.Clear();
            bugInput.Clear();
            _selectedDeveloperId = 0;
        }

        #endregion

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }
    }
}
