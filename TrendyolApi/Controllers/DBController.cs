using DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TrendyolApi.Controllers
{
    public class DBController : ApiController
    {
        TRENDYOLEntities trEntity = new TRENDYOLEntities();
        public IEnumerable<book> Get()
        {
            using (trEntity)
            {
                return trEntity.books.ToList();
            }
        }

        public book Get(int ID)
        {
            using (trEntity)
            {
                return trEntity.books.FirstOrDefault(x => x.ID == ID);
            }

        }
        public HttpResponseMessage Put(string author, string title)
        {
            using (trEntity)
            {
                if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "field '<author>,<title>' are required");
                }
                else
                {
                    if (this.Get(author, title))
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "another book with similar title and author already exists");

                    }

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }
        public bool Get(string author, string title)
        {
            using (trEntity)
            {
                DbSet<book> listData = null;
                listData.Add(trEntity.books.FirstOrDefault(x => x.AUTHOR == author && x.TITLE == title));
                if (listData.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

    }
}
