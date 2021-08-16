using System;
using System.Collections.Generic;
using System.Text;

namespace IorwoodDemo.Utility
{
    public class StaticValue
    {
        public const string ShoppingCart = "ShoppingCart";

        public const string Admin = "Admin";
        public const string Manager = "Manager";

        
        /// <summary>
        /// Siparis hereketleri
        /// </summary>
        public const string OrderSubmitted = "OrderSubmitted";  //Siparis verilende commit olanda
        public const string OrderOnTheWay = "OrderOnTheWay";    //Hazirlanib yola cixanda
        public const string OrderDelivered = "OrderDelivered";     //Siparis catdirildi
        public const string OrderCancelled = "OrderCancelled";
        public const string LocalSaleProcess = "LocalSaleProcess";


        /// <summary>
        /// Odeme hereketleri
        /// </summary>
        public const string PaymentIsWaiting = "PaymentIsWaiting";
        public const string PaymentDone = "PaymentDone";


        /// <summary>
        /// Urun Stok Degeri
        /// </summary>
        public const string StockLessThanMinimum = "StockLessThanMinimum";
    }
}
