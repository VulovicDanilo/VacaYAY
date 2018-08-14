using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacaYAY.Entities.Comments;

namespace VacaYAY.Data.Repos
{
    public class CommentRepository
    {
        private ApplicationDbContext db { get; set; }

        public CommentRepository()
        {
            db = new ApplicationDbContext();
        }
        /// <summary>
        /// Creates new <see cref="Comments"/>.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool Add(Comment comment)
        {
            try
            {
                db.Comments.Add(comment);
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
        /// Finds <see cref="Comment"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Comment Find(int? id)
        {
            try
            {
                Comment comment = db.Comments.Find(id);
                return comment;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Updates <see cref="Comment"/>.
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool Update(Comment comment)
        {
            try
            {
                db.Entry(comment).State = EntityState.Modified;
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
        /// Returns a list of all <see cref="Comment"/>.
        /// </summary>
        /// <returns></returns>
        public List<Comment> All()
        {
            try
            {
                return db.Comments.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Deletes <see cref="Comment"/> with specific id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int? id)
        {
            try
            {
                Comment comment = db.Comments.Find(id);
                db.Comments.Remove(comment);
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
