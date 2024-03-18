using BusinessObject;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.MemberRepo
{
    public interface IMemberRepository
    {
        public IEnumerable<Account> GetMembersList();
        public int GetNextMemberId();
        public Account Login(string email, string password);
        public void AddMember(Account member);
        public void UpdateMember(Account member);
        public void DeleteMember(int MemberID);
        public Account GetMember(int memberId);
        public Account GetMember(string memberEmail);
        public IEnumerable<Account> SearchMember(string name);
        public IEnumerable<Account> SearchMemberByCountry(string country, IEnumerable<Account> searchList);
        public IEnumerable<Account> SearchMemberByCity(string country, string city, IEnumerable<Account> searchList);
    }
}
