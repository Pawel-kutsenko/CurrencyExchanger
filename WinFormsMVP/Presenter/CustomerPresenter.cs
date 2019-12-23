﻿using WinFormsMVP.View;
using WinFormsMVP.Model;
using System.Linq;

namespace WinFormsMVP.Presenter
{
    public class CustomerPresenter
    {
        private readonly ICustomerView _view;
        private readonly ICustomerRepository _repository;

        public CustomerPresenter(ICustomerView view, ICustomerRepository repository)
        {
            _view = view;
            view.Presenter = this;
            _repository = repository;

            UpdateCustomerListView();
        }

        public void UpdateCustomerListView()
        {
            var customerNames = from customer in _repository.GetAllCustomers() select customer.Name;
            int selectedCustomer = _view.SelectedCustomer >= 0 ? _view.SelectedCustomer : 0;
            _view.CustomerList = customerNames.ToList();
            _view.SelectedCustomer = selectedCustomer;
        }

        public void UpdateCustomerView(int p)
        {
            // customer list can be cached instead of re-fetching the customer each time
            // this may be infeasible if the list is large
            Customer customer = _repository.GetCustomer(p);
            _view.CustomerName = customer.Name;
            _view.Currency = customer.Currency;
            _view.Cash = customer.Cash;
        }

        public void SaveCustomer()
        {
            Customer customer = new Customer { Name = _view.CustomerName, Currency = _view.Currency, Cash = _view.Cash};
            _repository.SaveCustomer(_view.SelectedCustomer, customer);
            UpdateCustomerListView();
        }

        public void AddCustomer(string name, string currency, string cash)
        {
            Customer customer = new Customer { Name = name, Currency = currency, Cash = cash };
            _repository.AddCustomer(customer);
        }

        public void RemoveCustomer(int id)
        {
            _repository.RemoveCustomer(id);
        }
    }
}