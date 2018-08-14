using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Requests;
using VacaYAY.Entities.Resolutions;

namespace VacaYAY.Data.Repos
{
    public class ResolutionRepository
    {
        private ApplicationDbContext db { get; set; }

        public ResolutionRepository()
        {
            db = new ApplicationDbContext();
        }
        /// <summary>
        /// Creates new <see cref="Resolution"/>.
        /// </summary>
        /// <param name="resolution"></param>
        /// <returns></returns>
        public bool Add(Resolution resolution)
        {
            try
            {
                db.Resolutions.Add(resolution);
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
        /// Finds <see cref="Resolution"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Resolution Find(int? id)
        {
            try
            {
                Resolution resolution = db.Resolutions.Find(id);
                return resolution;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Updates <see cref="Resolution"/>.
        /// </summary>
        /// <param name="resolution"></param>
        /// <returns></returns>
        public bool Update(Resolution resolution)
        {
            try
            {
                db.Entry(resolution).State = EntityState.Modified;
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
        /// Returns a list of all <see cref="Resolution"/>.
        /// </summary>
        /// <returns></returns>
        public List<Resolution> All()
        {
            try
            {
                return db.Resolutions.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Returns a list of all user's <see cref="Resolution"/>'s.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Resolution> GetEmployeesResolutions(int? id)
        {
            try
            {
                return db.Resolutions.Where(x => x.Request.Employee.EmployeeID == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Deletes <see cref="Resolution"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int? id)
        {
            try
            {
                Resolution resolution = db.Resolutions.Find(id);
                db.Resolutions.Remove(resolution);
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
