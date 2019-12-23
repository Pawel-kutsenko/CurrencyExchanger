using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace WinFormsMVP.Model
{
    internal class CustomerXmlRepository : ICustomerRepository
    {
        private readonly string _xmlFilePath;
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(List<Customer>));
        private readonly Lazy<List<Customer>> _customers;

        public CustomerXmlRepository(string fullPath)
        {
            _xmlFilePath = fullPath + @"\customers.xml";

            if (!File.Exists(_xmlFilePath))
                CreateCustomerXmlStub();

            _customers = new Lazy<List<Customer>>(() =>
            {
                using (var reader = new StreamReader(_xmlFilePath))
                {
                    return (List<Customer>)_serializer.Deserialize(reader);
                }
            });
        }

        private void CreateCustomerXmlStub()
        {
            var stubCustomerList = new List<Customer> {
                new Customer {Name = "Joe", Currency = "Euro", Cash = "1657"},
                new Customer {Name = "Jane", Currency = "Dollar", Cash = "999"},
                new Customer {Name = "Steve", Currency = "Yena", Cash = "125456"}
            };
            SaveCustomerList(stubCustomerList);
        }

        private void SaveCustomerList(List<Customer> customers)
        {
            using (var writer = new StreamWriter(_xmlFilePath, false))
            {
                _serializer.Serialize(writer, customers);
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customers.Value;
        }

        public Customer GetCustomer(int id)
        {
            return _customers.Value[id];
        }

        public void SaveCustomer(int id, Customer customer)
        {
            _customers.Value[id] = customer;
            SaveCustomerList(_customers.Value);
        }

        public void AddCustomer(Customer obj)
        {
            var newCustomers = _customers.Value;
            newCustomers.Add(obj);
            SaveCustomerList(newCustomers);
        }

        public void RemoveCustomer(int id)
        {
            _customers.Value.Remove(GetCustomer(id));
            SaveCustomerList(_customers.Value);
        }
    }
}