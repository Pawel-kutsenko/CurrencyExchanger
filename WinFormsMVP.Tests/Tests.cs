using System.Collections.Generic;
using System.Linq;
using Moq;
using WinFormsMVP.Model;
using WinFormsMVP.Presenter;
using WinFormsMVP.View;
using Xunit;

namespace WinFormsMVP.Tests
{
    public class PresenterTests
    {
        private readonly List<Customer> stubCustomerList = new List<Customer> {
                new Customer {Name = "Jack", Currency = "Rub", Cash = "123456"},
                new Customer {Name = "Jill", Currency = "Rub", Cash = "124456"},
                new Customer {Name = "Sam", Currency = "Blr Rub", Cash = "125456"}
        };

        private readonly ICustomerView mockCustomerView;
        private readonly ICustomerRepository mockCustomerRepository;
        private readonly CustomerPresenter presenter;

        public PresenterTests()
        {
            mockCustomerView = Mock.Of<ICustomerView>(view =>
                view.CustomerList == new List<string>());
            mockCustomerRepository = Mock.Of<ICustomerRepository>(repository =>
                repository.GetAllCustomers() == stubCustomerList);

            presenter = new CustomerPresenter(mockCustomerView, mockCustomerRepository);
        }

        [Fact]
        public void Presenter_constructor_ShouldFillViewCustomerList()
        {
            var customerNames = from customer in stubCustomerList select customer.Name;

            Assert.Equal<IList<string>>(mockCustomerView.CustomerList, customerNames.ToList());
        }

        [Fact]
        public void Presenter_UpdateCustomerView_ShouldPopulateViewWithRightCustomer()
        {
            var mockRepo = Mock.Get(mockCustomerRepository);
            mockRepo.Setup(repository => repository.GetCustomer(1)).Returns(stubCustomerList[1]);

            presenter.UpdateCustomerView(1);

            var mockView = Mock.Get(mockCustomerView);
            mockView.VerifySet(view => view.CustomerName = stubCustomerList[1].Name);
            mockView.VerifySet(view => view.Currency = stubCustomerList[1].Currency);
            mockView.VerifySet(view => view.Cash = stubCustomerList[1].Cash);
        }

        [Fact]
        public void Presenter_SaveCustomer_ShouldSaveSelectedCustomerToRepository()
        {
            var mockView = Mock.Get(mockCustomerView);
            mockView.Setup(view => view.SelectedCustomer).Returns(2);
            mockView.Setup(view => view.CustomerName).Returns(stubCustomerList[2].Name);
            mockView.Setup(view => view.Currency).Returns(stubCustomerList[2].Currency);
            mockView.Setup(view => view.Cash).Returns(stubCustomerList[2].Cash);

            presenter.SaveCustomer();

            var mockRepo = Mock.Get(mockCustomerRepository);
            mockRepo.Verify(repository => repository.SaveCustomer(2, stubCustomerList[2]));
        }
    }
}
