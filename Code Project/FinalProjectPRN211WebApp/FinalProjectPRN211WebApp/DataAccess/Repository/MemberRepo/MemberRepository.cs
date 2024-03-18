using BusinessObject;
using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.MemberRepo
{
    public class MemberRepository : IMemberRepository
    {
        public void AddMember(Account member) => AccountDAO.Instance.AddMember(member);

        public void DeleteMember(int memberId) => AccountDAO.Instance.Delete(memberId);

        public IEnumerable<Account> GetMembersList()
        {
            return AccountDAO.Instance.GetMembersList();
        }

        public IEnumerable<Account> SearchMember(string name)
        {
            return AccountDAO.Instance.SearchMember(name);
        }

        public Account Login(string email, string password)
        {
            return AccountDAO.Instance.Login(email, password);
        }

        public void UpdateMember(Account member) => AccountDAO.Instance.Update(member);

        public IEnumerable<Account> SearchMemberByCountry(string country, IEnumerable<Account> searchList)
        {
            return AccountDAO.Instance.FilterMemberByCountry(country, searchList);
        }

        public IEnumerable<Account> SearchMemberByCity(string country, string city, IEnumerable<Account> searchList)
        {
            return AccountDAO.Instance.FilterMemberByCity(country, city, searchList);
        }

        public Account GetMember(int memberId)
        {
            return AccountDAO.Instance.GetMember(memberId);
        }

        public int GetNextMemberId()
        {
            return AccountDAO.Instance.GetNextMemberId();
        }

        public Account GetMember(string memberEmail) => AccountDAO.Instance.GetMember(memberEmail);
    }
}
