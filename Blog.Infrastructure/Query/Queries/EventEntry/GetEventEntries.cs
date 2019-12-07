using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Query.Queries.EventEntry
{
    public class GetEventEntries : IQuery<IEnumerable<EventEntryDto>>
    {
    }
}
