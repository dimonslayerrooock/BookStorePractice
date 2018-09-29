﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.BLL.DTO;
using System.Web.Mvc;
using BookStore.BLL.Interface;
using System.Threading.Tasks;
using BookStore.WEB.Models;
using BookStore.BLL.ShoppinCart;

namespace BookStore.WEB.Controllers
{
    public class ShoppingCartController : Controller
    {
       
        IOrderService orderService;
        public ShoppingCartController(IOrderService service)
        {
            orderService = service;
        }
        public ActionResult Index(string returnUrl)
        {
            ShoppingCartViewModel viewModel = new ShoppingCartViewModel
            {
                returnUrl = returnUrl
            };
            //ViewBag.returnUrl = returnUrl;
            return View(viewModel);//Papapa
        }
        public ActionResult ShowCart(string returnUrl)
        {
            ShoppingCartViewModel viewModel = new ShoppingCartViewModel
            {
                Cart = GetCart(),
                returnUrl = returnUrl
            };
            return PartialView("ShowCart",viewModel);
        }
        public ActionResult AddToCart(int id,string returnUrl,int? page,string category)
        {
            var book = orderService.GetBook(id);
            
            if (book != null)
            {
                GetCart().AddItem(book,1);
            }

            return RedirectToAction("Index", "Home",new { page = page,category = category});
        }
        public ActionResult RemoveFromCart(int id,string returnUrl)
        {
            var book = orderService.GetBook(id);
            if (book != null)
            {
                GetCart().RemoveLine(book);
            }
            return RedirectToAction("Index","ShoppingCart",new { returnUrl = returnUrl });
        }
        
        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                //cart = orderService.GetCart();
                Session["Cart"] = cart;
            }
            return cart;
        }

    }
}