using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComputersShopContracts.BindingModels;
using ComputersShopContracts.BusinessLogicsContracts;
using ComputersShopContracts.ViewModels;


namespace ComputersShopView
{
    public partial class FormCreateOrder : Form
    {
        private readonly IComputerLogic _logicC;

        private readonly IOrderLogic _logicO;

        private readonly IClientLogic _logicCl;

        public FormCreateOrder(IComputerLogic logicC, IOrderLogic logicO, IClientLogic logicCl)
        {
            InitializeComponent();
            _logicC = logicC;
            _logicO = logicO;
            _logicCl = logicCl;
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<ComputerViewModel> list = _logicC.Read(null);
                if (list != null)
                {
                    comboBoxComputer.DisplayMember = "ComputerName";
                    comboBoxComputer.ValueMember = "Id";
                    comboBoxComputer.DataSource = list;
                    comboBoxComputer.SelectedItem = null;
                }
                List<ClientViewModel> listClient = _logicCl.Read(null);
                if (listClient != null)
                {
                    comboBoxClient.DisplayMember = "FullName";
                    comboBoxClient.ValueMember = "Id";
                    comboBoxClient.DataSource = listClient;
                    comboBoxClient.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxComputer.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxComputer.SelectedValue);
                    ComputerViewModel car = _logicC.Read(new ComputerBindingModel
                    {
                        Id = id
                    })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * car?.Price ?? 0).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void ComboBoxComputer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComputer.SelectedValue == null)
            {
                MessageBox.Show("Выберите автомобиль", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if(comboBoxClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                int cars = Convert.ToInt32(comboBoxComputer.SelectedValue);
                int Counst = Convert.ToInt32(textBoxCount.Text);
                int Sums = Convert.ToInt32(textBoxSum.Text);
                _logicO.CreateOrder(new CreateOrderBindingModel
                {
                    ComputerId = Convert.ToInt32(comboBoxComputer.SelectedValue),
                    ClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
