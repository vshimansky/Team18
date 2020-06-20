using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VirusAlertAPI.Models;

namespace VirusAlertAPI.Controllers
{
    public class NewsController : ApiController
    {
        // GET api/news/
        /// <summary>
        /// Получение всех последних новостей
        /// </summary>
        /// <returns></returns>
        public List<DB.News> Get()
        {
            var db = new DB.DataContext();
            return db.news.ToList();
        }

        // GET api/news/
        /// <summary>
        /// Получение всех последних новостей по номеру региона
        /// </summary>
        /// <returns></returns>
        public List<DB.News> Get(int region)
        {
            var db = new DB.DataContext();
            return db.news.Where(x => x.newRegId == region).ToList();
        }
    }
}
