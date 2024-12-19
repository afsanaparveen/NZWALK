using NZWalks.API.models.domains;

namespace NZWalks.API.repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
