using Events.Models;
using System.Collections.Generic;
using System.Linq;

namespace Events.Services
{
    public class SupportService
    {
        private EventsDBContext db = new EventsDBContext();

        private List<Support> getSupportsListByWriterId(int id)
        {
            return db.Support.Where(x => x.WritenBy == id).ToList();
        }

        private List<Support> getSupportsListBySolverId(int id)
        {
            return db.Support.Where(x => x.WritenBy == id).ToList();
        }

        private List<Support> mergeLists(params List<Support>[] supports)
        {
            List<Support> result = new List<Support>();
            for (int i = 0; i < supports.Length; i++)
            {
                result.AddRange(supports[i]);
            }
            return result;
        }

        public List<Support> getSupportList(int id)
        {
            return mergeLists(getSupportsListBySolverId(id), getSupportsListByWriterId(id));
        }

        public int getSupportListLength(int id)
        {
            return mergeLists(getSupportsListBySolverId(id), getSupportsListByWriterId(id)).Count;
        }

        public List<Support> getAllSuports()
        {
            return db.Support.ToList();
        }

        public int getAllSuportsCount()
        {
            return db.Support.ToList().Count;
        }

        public Support getSupportById(int id)
        {
            return db.Support.FirstOrDefault(x => x.Id == id);
        }

        public void AddSupportToDatabase(Support support)
        {
            db.Support.Add(support);
            db.SaveChanges();
        }

        public void deleteSupportFromDatabase(int id)
        {
            db.Support.Remove(getSupportById(id));
            db.SaveChanges();
        }

        public void updateSupport(int id ,int solvedBy, string message)
        {
            Support support = getSupportById(id);
            support.SolvedBy = solvedBy;
            support.Solution = message;
            db.SaveChanges();
        }

        public int GetSupportByTitleAndAuthor(string title, int author)
        {
            return db.Support.Where(x => x.Title == title && x.WritenBy == author).FirstOrDefault().Id;
        }
    }
}
