using BusinessObjects;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly FUNewsManagementDbContext _context;

        public TagRepository(FUNewsManagementDbContext context)
        {
            _context = context;
        }

        public List<Tag> GetTags()
        {
            return _context.Tags.ToList();
        }
    }
}