using BusinessObjects;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface ITagService
    {
        List<Tag> GetTags();
    }
}