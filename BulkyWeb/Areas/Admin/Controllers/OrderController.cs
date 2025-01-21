﻿using Bookly.DataAccess.Repository.Interface;
using Bookly.Models.Models;
using Bookly.Models.ViewModels;
using Bookly.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BooklyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _work;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _work = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int orderId)
        {
            OrderVM orderVM = new()
            {
                OrderHeader = _work.OrderHeader.Get(u => u.Id == orderId, includeProperties:"ApplicationUser"),
                OrderDetail = _work.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties:"Product")
            };
            
            
            return View(orderVM);
        }

        #region API CALLS


        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> objOrderHeadersList = _work.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();


            switch (status)
            {
                case "pending":
                    objOrderHeadersList = objOrderHeadersList.Where(u => u.PaymentStatus == SD.PaymentStatusDelayedPayment);
                    break;
                case "inprocess":
                    objOrderHeadersList = objOrderHeadersList.Where(u => u.OrderStatus == SD.StatusInProcess);
                    break;
                case "completed":
                    objOrderHeadersList = objOrderHeadersList.Where(u => u.OrderStatus == SD.StatusShipped);
                    break;
                case "approved":
                    objOrderHeadersList = objOrderHeadersList.Where(u => u.OrderStatus == SD.StatusApproved);
                    break;
                default:
                    break;

            }



            return Json(new { data = objOrderHeadersList });
        }


        #endregion
    }
}

