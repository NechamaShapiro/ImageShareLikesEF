using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShareLikesEF.Data
{
    public class ImageRepository
    {
        private string _connectionString;
        public ImageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Image> GetImages()
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.ToList();
        }
        public void Add(Image image)
        {
            using var context = new ImageDbContext(_connectionString);
            context.Images.Add(image);
            context.SaveChanges();
        }
        public Image GetImageById(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.FirstOrDefault(i => i.Id == id);
        }
        public void UpdateLikes(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"UPDATE Images SET Likes = Likes + 1 WHERE Id = {id}");
        }
        public int GetLikes(int id)
        {
            using var context = new ImageDbContext(_connectionString);
            return context.Images.Where(i => i.Id == id).Select(i => i.Likes).FirstOrDefault();
        }
    }
}
