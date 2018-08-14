using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.ExtraDaysAditions;

namespace VacaYAY.Data.Repos
{
    public class ExtraDaysAditionRepository
    {
        private ApplicationDbContext db { get; set; }

        public ExtraDaysAditionRepository()
        {
            db = new ApplicationDbContext();
        }
        /// <summary>
        /// Creates new <see cref="ExtraDaysAdition"/>.
        /// </summary>
        /// <param name="extraDaysAdition"></param>
        /// <returns></returns>
        public bool Add(ExtraDaysAdition extraDaysAdition)
        {
            try
            {
                db.ExtraDaysAditions.Add(extraDaysAdition);
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
        /// Finds <see cref="ExtraDaysAdition"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExtraDaysAdition Find(int? id)
        {
            try
            {
                ExtraDaysAdition extraDaysAdition = db.ExtraDaysAditions.Find(id);
                return extraDaysAdition;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Updates <see cref="ExtraDaysAdition"/>.
        /// </summary>
        /// <param name="extraDaysAdition"></param>
        /// <returns></returns>
        public bool Update(ExtraDaysAdition extraDaysAdition)
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
        /// Returns a list of all <see cref="ExtraDaysAdition"/>.
        /// </summary>
        /// <returns></returns>
        public List<ExtraDaysAdition> All()
        {
            try
            {
                return db.ExtraDaysAditions.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Deletes <see cref="ExtraDaysAdition"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int? id)
        {
            try
            {
                ExtraDaysAdition extraDaysAdition = db.ExtraDaysAditions.Find(id);
                db.ExtraDaysAditions.Remove(extraDaysAdition);
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
