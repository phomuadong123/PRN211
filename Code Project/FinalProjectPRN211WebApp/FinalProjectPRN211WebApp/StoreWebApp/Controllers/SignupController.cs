using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject;
using DataAccess.Repository.MemberRepo;
using Microsoft.AspNetCore.Authorization;
using StoreWebApp.Models;
using DataAccess.Repository.OrderRepo;
using DataAccess.Repository.OrderDetailRepo;

namespace eStore.Controllers
{
    public class SignupController : Controller
    {
        IMemberRepository memberRepository = new MemberRepository();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BusinessObject.Models.Account member, [FromForm] string confirm)
        {
            try
            {
                if (!member.Password.Equals(confirm))
                {
                    throw new Exception("Confirm and Password are not matched!!!");
                }
                if (ModelState.IsValid)
                {
                    memberRepository.AddMember(member);
                }
                BusinessObject.Models.Account newMember = memberRepository.GetMember(member.Email);
                TempData["Create"] = "Create Member with the ID <strong>" + newMember.MemberId + "</strong> successfully!!";
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Index", member);
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
