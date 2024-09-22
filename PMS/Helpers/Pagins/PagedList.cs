using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Common.Pagins
{
    public class PagedList<TEntity> : List<TEntity>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }

        public bool HasPrevious
        {
            get
            {
                return CurrentPage > 1;
            }
        }
        public bool HasNext
        {
            get
            {
                return CurrentPage < TotalPages;
            }
        }

        public PagedList(List<TEntity> items, int currentPage, int itemsPerPage, int totalItems)
        {
            TotalItems = totalItems;
            ItemsPerPage = itemsPerPage;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalItems / (double)itemsPerPage);
            this.AddRange(items);
        }

        //public static async Task<PagedList<TEntity>> CreateAsync(IQueryable<TEntity> source, int currentPage, int itemsPerPage)
        //{
        //    var totalItems = await source.CountAsync();
        //    var items = await source.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage).ToListAsync();
        //    return new PagedList<TEntity>(items, currentPage, itemsPerPage, totalItems);
        //}
    }
}
