namespace MyProj.WebApi.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepository Product { get; set; }
        public Task<int> SaveChangesAsync();
        public void BeginTrasaction();
        public Task CommitAsync();
        public Task RollBackAsync();
    }
}
