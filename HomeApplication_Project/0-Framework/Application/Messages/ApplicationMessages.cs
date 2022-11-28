using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public static class ApplicationMessages
    {
        public const string RecordAlreadyExists = "Another Record With Name-{0}-Already Exists!,";
        public const string RecordNotFound = "There Is Not Any Records Matching With The Given Information!";
        public const string RecordAlreadyExistsNonArgument = "Another Record With Same Information Already Exists!";
        public const string PasswordNotMatch = "Passwords Don't Match";
        public const string WrongUserOrPass = "Username or Password Is Not Correct!!";
        public const string SuccessfulPayment = "پرداخت با موفقیت انجام شد.";
        public const string UnSuccessfulPayment = "پرداخت با موفقیت انجام نشد. درصورت کسر وجه از حساب، مبلغ تا 24 ساعت دیگر به حساب شما بازگردانده خواهد شد.";
        public const string GatewayDescription = "خرید از درگاه لوازم خانگی و دکوری";
        public const string CashPayDescription = "سفارش شما با موفقیت ثبت شد. پس از تماس کارشناسان ما و پرداخت وجه، سفارش ارسال خواهد شد.\n شناسه پرداخت جهت پیگیری : {0}";
        public const string CustomerPurchase = "خرید مشتری";
    }
}
