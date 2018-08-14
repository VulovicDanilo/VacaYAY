using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Contracts;
using VacaYAY.Entities.Employees;

namespace VacaYAY.Data.Repos
{
    public class ContractRepository
    {
        private ApplicationDbContext db { get; set; }

        public ContractRepository()
        {
            db = new ApplicationDbContext();
        }
        /// <summary>
        /// Creates new <see cref="Contract"/>.
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public bool Add(Contract contract)
        {
            try
            {
                db.Contracts.Add(contract);
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
        /// Finds <see cref="Contract"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contract Find(int? id)
        {
            try
            {
                Contract contract = db.Contracts.Find(id);
                return contract;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Updates <see cref="Contract"/>.
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public bool Update(Contract contract)
        {
            try
            {
                db.Entry(contract).State = EntityState.Modified;
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
        /// Returns a list of all <see cref="Contract"/>.
        /// </summary>
        /// <returns></returns>
        public List<Contract> All()
        {
            try
            {
                return db.Contracts.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public List<Contract> GetContractsWithEmployee(int? id)
        {
            try
            {
                return db.Contracts.Where(x => x.Employee.EmployeeID == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Deletes <see cref="Contract"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int? id)
        {
            try
            {
                Contract contract = db.Contracts.Find(id);
                db.Contracts.Remove(contract);
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
