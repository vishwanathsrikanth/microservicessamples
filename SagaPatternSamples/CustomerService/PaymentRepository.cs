using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService
{
    public static class PaymentRepository
    {
        public static IList<CustomerAccountModel> customerAccountModels = 
            new List<CustomerAccountModel>();

        static PaymentRepository()
        {
            var customerAccountModel = new CustomerAccountModel { CustomerId = 1, MaximumOrderLimit = 1000 };
            customerAccountModels.Add(customerAccountModel);
        }
    }
}
