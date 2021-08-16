using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using IorwoodDemo.DataAccess.UnitOfWork.Concrete;
using IorwoodDemo.Extensions;
using IorwoodDemo.Model.ViewModel;
using IorwoodDemo.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IorwoodDemo.Extensions
{
    /// <summary>
    /// test surumu
    /// </summary>
    public  class CartHelper 
    {
        private readonly IHttpContextAccessor _accessor;
        
        public  CartHelper(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public  bool IsInCart(int id)
        {
            List<object> SessionCartList = _accessor.HttpContext.Session.GetObject<List<object>>(StaticValue.ShoppingCart);
            
            
            if (SessionCartList != null)
            {
                List<object> SessionProductList;
                foreach (var item in SessionCartList)
                {
                    SessionProductList = ((IEnumerable<object>)item).ToList();
                   // SessionProductList.Contains(id);
                    if (Convert.ToInt32(SessionProductList[0]) == id)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void AddToCart(int id, int count)
        {
            List<object> SessionCartList = _accessor.HttpContext.Session.GetObject<List<object>>(StaticValue.ShoppingCart);


            if (SessionCartList == null || SessionCartList.Count == 0)
            {
                SessionCartList = new List<object>();
                SessionCartList.Add(new List<object>() { id, count });
                _accessor.HttpContext.Session.SetObject(StaticValue.ShoppingCart, SessionCartList);
            }
            else
            {
                if (!IsInCart(id))
                {
                    List<object> SessionProductList = SessionCartList.ToList();
                    SessionCartList.Add(new List<object>() { id, count });
                    _accessor.HttpContext.Session.SetObject(StaticValue.ShoppingCart, SessionCartList);
                }
            }


        }
    }
}
