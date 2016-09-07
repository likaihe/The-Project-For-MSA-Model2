﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;


namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult Index()
        {


            var customers = _context.Customers.Include(c => c.MembershipeType).ToList();
            return View(customers);
        }

        public ActionResult New()
        {
            var membershiptype = _context.MembershipType.ToList();

            var ViewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershiptype,

            };
            return View("CustomerForm", ViewModel);
        }

       [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Customer customer)
        {

            //if (!ModelState.IsValid)
            //{
            //    var viewModel = new CustomerFormViewModel
            //    {
            //        Customer = customer,
            //        MembershipTypes = _context.MembershipType
            //    };

            //    return View("CustomerForm", viewModel);
            //}

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerDb.Name = customer.Name;
                customerDb.Birthday = customer.Birthday;
                customerDb.MembershipeTypeId = customer.MembershipeTypeId;
                customerDb.IsSubscribedToLettler = customer.IsSubscribedToLettler;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }


        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipeType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipType.ToList()

            };

            return View("CustomerForm", viewModel);
        }
    }
}