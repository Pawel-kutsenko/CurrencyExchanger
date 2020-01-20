using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsMVP.View
{
    internal partial class CustomerForm : Form, ICustomerView
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        public IList<string> CustomerList
        {
            get { return (IList<string>)this.customerListBox.DataSource; }
            set { this.customerListBox.DataSource = value; }
        }

        public int SelectedCustomer
        {
            get { return this.customerListBox.SelectedIndex; }
            set { this.customerListBox.SelectedIndex = value; }
        }

        public string Currency
        {
            get { return this.currency_cb.Text; }
            set { this.currency_cb.Text = value; }
        }

        public string CustomerName
        {
            get { return this.nameTextBox.Text; }
            set { this.nameTextBox.Text = value; }
        }

        public string Cash
        {
            get { return this.cashTextBox.Text; }
            set { this.cashTextBox.Text = value; }
        }

        public Presenter.CustomerPresenter Presenter
        { private get; set; }

        private void customerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Presenter.UpdateCustomerView(customerListBox.SelectedIndex);
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            Presenter.SaveCustomer();
            Presenter.UpdateCustomerListView();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            Presenter.AddCustomer(this.nameTextBox.Text, this.currency_cb.Text, this.cashTextBox.Text);
            Presenter.UpdateCustomerListView();
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            Presenter.RemoveCustomer(customerListBox.SelectedIndex);
            Presenter.UpdateCustomerListView();
        }

        private void CashInputChanged(object sender, EventArgs e)
        {
            Presenter.CurrencyExchange(this.currency_cb.Text, this.currency_to_cb.Text, this.out_textbox.Text);
        }
    }
}