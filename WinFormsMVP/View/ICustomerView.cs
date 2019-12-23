using System.Collections.Generic;

namespace WinFormsMVP.View
{
    public interface ICustomerView
    {
        IList<string> CustomerList { get; set; }

        int SelectedCustomer { get; set; }

        string CustomerName { get; set; }

        string Currency { get; set; }

        string Cash { get; set; }

        Presenter.CustomerPresenter Presenter { set; }
    }
}