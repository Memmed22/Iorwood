using IorwoodDemo.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.Utility.StaticValues
{
    public static class CashDeskStaticValues
    {

         static List<StaticScheme> staticScheme = new List<StaticScheme>();
        public static List<StaticScheme> StaticValues()
        {
            staticScheme = new List<StaticScheme>();
            staticScheme.Add(new StaticScheme { Key = "Salary", Text = "Maaş", Meta = "FrontExpense" });
            staticScheme.Add(new StaticScheme { Key = "StaticExpense",  Text = "Sabit Xərcəmələr", Meta = "FrontExpense" });
            staticScheme.Add(new StaticScheme { Key = "DailyExpense",Text = "Günlük Xərcəmələr", Meta = "FrontExpense" });
            staticScheme.Add(new StaticScheme { Key = "Supplier",Text = "Firma Xərcəmələri", Meta = "FrontExpense" });
            staticScheme.Add(new StaticScheme { Key = "Refund",  Text = "Geri Mal Qayatarma", Meta = "BackExpense" });
            staticScheme.Add(new StaticScheme { Key = "CashDeskClearInFlow", Text = "Clearing Cash Desk Inflow", Meta = "BackInflow" });

            return staticScheme;
        }

        
    }
}
