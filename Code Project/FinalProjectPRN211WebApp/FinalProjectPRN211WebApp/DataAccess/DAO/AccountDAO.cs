using BusinessObject.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AccountDAO
    {
        // Using Singleton Pattern
        private static AccountDAO instance = null;
        private static object instanceLook = new object();

        public static AccountDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new AccountDAO();
                    }
                    return instance;
                }
            }
        }

        // Get default user from appsettings
        private Account GetDefaultMember()
        {
            Account Default = null;
            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
                string email = config["account:defaultAccount:email"];
                string password = config["account:defaultAccount:password"];
                Default = new Account
                {
                    MemberId = 0,
                    Email = email,
                    Password = password,
                    City = "",
                    Country = "",
                    Fullname = "Admin",
                    CompanyName = ""
                };
            }
            return Default;
        }

        public IEnumerable<Account> GetMembersList()
        {
            IEnumerable<Account> members = null;

            try
            {
                var context = new FstoreContext();
                // Get From Database
                members = context.Accounts;
                // Add Default User
                members = members.Append(GetDefaultMember()).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return members;
        }

        public Account Login(string email, string password)
        {
            Account member = null;
            try
            {
                IEnumerable<Account> members = GetMembersList();
                member = members.SingleOrDefault(mb => mb.Email.Equals(email) && mb.Password.Equals(password));
                if (member == null)
                {
                    throw new Exception("Login failed! Please check your email and password!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return member;
        }

        public Account GetMember(int memberId)
        {
            Account member = null;

            try
            {
                var context = new FstoreContext();
                member = context.Accounts.SingleOrDefault(mem => mem.MemberId == memberId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return member;
        }
        public Account GetMember(string memberEmail)
        {
            Account member = null;

            try
            {
                var context = new FstoreContext();
                member = context.Accounts.SingleOrDefault(mem => mem.Email.Equals(memberEmail));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return member;
        }

        public int GetNextMemberId()
        {
            int nextMemberId = -1;

            try
            {
                var context = new FstoreContext();
                nextMemberId = context.Accounts.Max(mem => mem.MemberId) + 1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return nextMemberId;
        }
        public void AddMember(Account member)
        {
            if (member == null)
            {
                throw new Exception("Member is undefined!!");
            }
            try
            {
                if (GetMember(member.MemberId) == null && GetMember(member.Email) == null)
                {
                    var context = new FstoreContext();
                    context.Accounts.Add(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Member is existed!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(Account member)
        {
            if (member == null)
            {
                throw new Exception("Member is undefined!!");
            }
            try
            {
                Account _mem = GetMember(member.MemberId);
                if (_mem != null)
                {
                    var context = new FstoreContext();
                    context.Accounts.Update(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Member does not exist!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void Delete(int MemberId)
        {
            try
            {
                Account member = GetMember(MemberId);
                if (member != null)
                {
                    var context = new FstoreContext();
                    context.Accounts.Remove(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Member does not exist!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Account> SearchMember(string name)
        {
            IEnumerable<Account> searchResult = null;

            try
            {
                var context = new FstoreContext();
                searchResult = context.Accounts.Where(mem => mem.Fullname.ToLower().Contains(name.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return searchResult;
        }

        public IEnumerable<Account> FilterMemberByCountry(string country, IEnumerable<Account> searchList = null)
        {
            IEnumerable<Account> searchResult = null;
            if (searchList != null)
            {
                var memberSearch = from member in searchList
                                   where member.Country == country
                                   select member;
                if (country.Equals("All"))
                {
                    memberSearch = from member in searchList
                                   select member;
                }
                searchResult = memberSearch.ToList();
            }
            else
            {
                var context = new FstoreContext();
            }
            return searchResult;
        }

        public IEnumerable<Account> FilterMemberByCity(string country, string city, IEnumerable<Account> searchList)
        {
            IEnumerable<Account> searchResult = null;
            var memberSearch = from member in searchList
                               where member.City == city
                               select member;
            if (city.Equals("All"))
            {
                memberSearch = from member in searchList
                               where member.Country == country
                               select member;
                if (country.Equals("All"))
                {
                    memberSearch = from member in searchList
                                   select member;
                }
            }
            searchResult = memberSearch.ToList();
            return searchResult;
        }
    }
}
