using Responsi2_Junpro.Models;
using Responsi2_Junpro.Repositories;

namespace Responsi2_Junpro
{
    /// <summary>
    /// Class untuk mengelola dropdown menu (ComboBox)
    /// </summary>
    public class DropDownMenu
    {
        private readonly IDatabaseRepository _repository;

        public DropDownMenu(IDatabaseRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Mengisi ComboBox Proyek dengan data dari database
        /// </summary>
        public void LoadProyekDropdown(ComboBox comboBox)
        {
            comboBox.Items.Clear();

            var proyekList = _repository.GetAllProyek();
            foreach (var proyek in proyekList)
            {
                comboBox.Items.Add(proyek.NamaProyek);
            }

            comboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Mengisi ComboBox Status Kontrak dengan opsi statis
        /// </summary>
        public void LoadStatusKontrakDropdown(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            comboBox.Items.Add("Full Time");
            comboBox.Items.Add("Freelance");

            comboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Mendapatkan nilai yang dipilih dari ComboBox
        /// </summary>
        public string GetSelectedValue(ComboBox comboBox)
        {
            if (comboBox.SelectedIndex < 0 || comboBox.SelectedItem == null)
                return string.Empty;

            return comboBox.SelectedItem.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Mengatur pilihan ComboBox berdasarkan nilai
        /// </summary>
        public void SetSelectedValue(ComboBox comboBox, string value)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i]?.ToString() == value)
                {
                    comboBox.SelectedIndex = i;
                    return;
                }
            }
            comboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Validasi apakah dropdown sudah dipilih dengan benar
        /// </summary>
        public bool ValidateDropdown(ComboBox comboBox, string fieldName)
        {
            if (comboBox.SelectedIndex < 0)
            {
                MessageBox.Show($"Silakan pilih {fieldName}!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox.Focus();
                return false;
            }
            return true;
        }
    }
}
