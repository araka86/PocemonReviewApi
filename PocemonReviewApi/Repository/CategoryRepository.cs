using PocemonReviewApi.Data;
using PocemonReviewApi.Interface;
using PocemonReviewApi.Models;

namespace PocemonReviewApi.Repository
{
    public class CategoryRepository : ICatagoryRepository
    {

        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CategoryExist(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
           return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(x => x.Id == id).FirstOrDefault();
        }

       

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context
                .PokemonCategorys
                .Where(e => e.CategoryId == categoryId)
                .Select(c => c.Pokemon).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
