using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public static class GatewayCodes 
    {
        public static string TranslateCode(int? code )
        {
            switch (code)
            {
                case 0:
                    return "پرداخت انجام نشد";

                case 1:
                    return "پرداخت با موفقیت انجام شد";
                    
                case 2:
                    return "تراکنش قبلا وریفای شده است";
                    
                case -1:
                    return "amount نمی تواند خالی باشد";
                    
                case -2:
                    return "کد پین درگاه نمی تواند خالی باشد";
                    
                case -3:
                    return "callback نمی تواند خالی باشد";
                    
                case -4:
                    return "amount باید عددی باشد";
                    
                case -5:
                    return "amount باید بین 100 تا 50,000,000 تومان باشد";
                    
                case -6:
                    return "کد پین درگاه اشتباه هست";
                    
                case -7:
                    return "transid نمی تواند خالی باشد";
                    
                case -8:
                    return "تراکنش مورد نظر وجود ندارد";
                    
                case -9:
                    return "کد پین درگاه با درگاه تراکنش مطابقت ندارد";
                    
                case -10:
                    return "مبلغ با مبلغ تراکنش مطابقت ندارد";
                    
                case -11:
                    return "درگاه درانتظار تایید و یا غیر فعال است";
                    
                case -12:
                    return "امکان ارسال درخواست برای این پذیرنده وجود ندارد";
                    
                case -13:
                    return "شماره کارت باید 16 رقم چسبیده بهم باشد";
                    
                default:
                    return "خطای نامشخص";
                    
            }
        }

    }
}
