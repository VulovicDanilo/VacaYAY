using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VacaYAY.Entities.Requests;
using VacaYAY.Entities.Resolutions;
using static VacaYAY.Common.Enums;

namespace VacaYAY.Data.Repos
{
    [Authorize]
    public class RequestRepository
    {
        private ApplicationDbContext db { get; set; }

        public RequestRepository()
        {
            db = new ApplicationDbContext();
        }
        /// <summary>
        /// Creates new <see cref="Request"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool Add(Request request)
        {
            try
            {
                db.Requests.Add(request);
                db.SaveChanges();
                db.Entry(request).Reference(x => x.Employee).Load();
                //db.Entry(request).Reference(x => x.Comments).Load();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        /// <summary>
        /// Finds <see cref="Request"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Request Find(int? id)
        {
            try
            {
                Request request = db.Requests.Find(id);
                return request;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Updates <see cref="Request"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool Update(Request request)
        {
            try
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        /// <summary>
        /// Returns a list of all <see cref="Request"/>.
        /// </summary>
        /// <returns></returns>
        public List<Request> All()
        {
            try
            {
                List<Request> requests = db.Requests.Where(x => x.Employee.Active).ToList();
                requests.Reverse();
                return requests;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Returns a list of all pending <see cref="Request"/>'s.
        /// </summary>
        /// <returns></returns>
        public List<Request> AllPending()
        {
            try
            {
                return db.Requests
                    .Where(x => x.Status == Status.Pending)
                    .Where(x => x.Employee.Active).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Returns a list of all approved <see cref="Request"/>'s.
        /// </summary>
        /// <returns></returns>
        public List<Request> AllApproved()
        {
            try
            {
                return db.Requests
                    .Where(x => x.Status == Status.Approved)
                    .Where(x => x.Employee.Active)
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Returns a list of all rejected <see cref="Request"/>'s.
        /// </summary>
        /// <returns></returns>
        public List<Request> AllRejected()
        {
            try
            {
                return db.Requests.Where(x => x.Status == Status.Rejected).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public List<Request> AllUsersRequests(string userID)
        {
            try
            {
                List<Request> list = db.Requests.Where(x => x.Employee.UserID == userID).ToList();
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Deletes <see cref="Request"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int? id)
        {
            try
            {
                Request request = db.Requests.Find(id);
                db.Requests.Remove(request);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
