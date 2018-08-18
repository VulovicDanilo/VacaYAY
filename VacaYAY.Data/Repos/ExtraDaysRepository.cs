using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.ExtraDays;

namespace VacaYAY.Data.Repos
{
    public class ExtraDaysRepository
    {
        private ApplicationDbContext db { get; set; }

        public ExtraDaysRepository()
        {
            db = new ApplicationDbContext();
        }
        /// <summary>
        /// Creates new <see cref="ExtraDays"/>.
        /// </summary>
        /// <param name="extraDaysAdition"></param>
        /// <returns></returns>
        public bool Add(ExtraDays extraDaysAdition)
        {
            try
            {
                db.ExtraDays.Add(extraDaysAdition);
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
        /// Finds <see cref="ExtraDays"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExtraDays Find(int? id)
        {
            try
            {
                ExtraDays extraDaysAdition = db.ExtraDays.Find(id);
                return extraDaysAdition;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Updates <see cref="ExtraDays"/>.
        /// </summary>
        /// <param name="extraDaysAdition"></param>
        /// <returns></returns>
        public bool Update(ExtraDays extraDaysAdition)
        {
            try
            {
                db.Entry(extraDaysAdition).State = EntityState.Modified;
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
        /// Returns a list of all <see cref="ExtraDays"/>.
        /// </summary>
        /// <returns></returns>
        public List<ExtraDays> All()
        {
            try
            {
                return db.ExtraDays.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Gets <see cref="Employee"/>'s Extra Days.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ExtraDays> GetEmployeesExtraDays(int? id)
        {
            try
            {
                return db.ExtraDays.Where(x => x.EmployeeID == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Deletes <see cref="ExtraDays"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int? id)
        {
            try
            {
                ExtraDays extraDaysAdition = db.ExtraDays.Find(id);
                db.ExtraDays.Remove(extraDaysAdition);
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
