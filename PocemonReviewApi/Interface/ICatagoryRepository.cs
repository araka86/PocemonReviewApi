using PocemonReviewApi.Models;

namespace PocemonReviewApi.Interface
{
    public interface ICatagoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Pokemon> GetPokemonByCategory(int categoryId);
        bool CategoryExist(int id);
        bool CreateCategory(Category category);
        bool Save();
    }
}
